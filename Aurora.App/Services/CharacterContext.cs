using Builder.Presentation;
using Builder.Presentation.Models;
using Builder.Presentation.Services;
using Builder.Presentation.Services.Data;

namespace Aurora.App.Services;

/// <summary>
/// Guards the process-wide <see cref="CharacterManager.Current"/> singleton so that the active
/// MAUI tab (or web session) can deterministically mutate exactly its own character.
///
/// Every mutation entry point (Build rule apply, equipment toggle, text save, PDF generation,
/// snapshot rebuild after an edit) must wrap its work in <c>using (await CharacterContext.EnterAsync(tab))</c>.
/// Read-only rendering from a tab's cached <see cref="CharacterTab.Snapshot"/> does not need a context.
///
/// On a swap-away, the outgoing tab's state is serialized via
/// <see cref="CharacterFile.SerializeCharacter"/> and stored on the tab. On swap-in, the incoming
/// tab's <see cref="CharacterTab.StateXml"/> (or its original file, on first activation) is hydrated
/// through the existing <see cref="CharacterFile.Load(string)"/> pipeline via a temp file.
///
/// Entering the same tab twice in a row costs a single lock acquire — no reload — so batches of
/// operations on one tab are cheap.
/// </summary>
public static class CharacterContext
{
    private static readonly SemaphoreSlim _lock = new(1, 1);
    private static CharacterTab? _active;

    /// <summary>The tab currently loaded into the singleton, or null if nothing is active.</summary>
    public static CharacterTab? ActiveTab => _active;

    /// <summary>
    /// Take the context lock and ensure <paramref name="tab"/>'s character is the one loaded into
    /// <see cref="CharacterManager.Current"/>. Dispose the returned scope to release the lock.
    /// </summary>
    public static async Task<Scope> EnterAsync(CharacterTab tab)
    {
        if (tab == null) throw new ArgumentNullException(nameof(tab));

        await _lock.WaitAsync();
        try
        {
            if (!ReferenceEquals(_active, tab))
            {
                await SwapAsync(tab);
                _active = tab;
            }
            return new Scope();
        }
        catch
        {
            _lock.Release();
            throw;
        }
    }

    /// <summary>
    /// Drops the active tab reference so the next <see cref="EnterAsync"/> on any tab will hydrate
    /// fresh. Call this when closing a tab or reloading element data.
    /// </summary>
    public static async Task InvalidateAsync()
    {
        await _lock.WaitAsync();
        try { _active = null; }
        finally { _lock.Release(); }
    }

    /// <summary>
    /// Captures the active tab's current character state into its <see cref="CharacterTab.StateXml"/>
    /// buffer (so unsaved edits survive), then drops the active tab reference. Call this immediately
    /// before an external flow mutates <see cref="CharacterManager.Current"/> outside of EnterAsync —
    /// for example <see cref="CharacterService.CreateNewCharacterAsync"/> and
    /// <see cref="CharacterService.LoadCharacterAsync"/>.
    /// </summary>
    public static async Task CaptureAndInvalidateAsync()
    {
        await _lock.WaitAsync();
        try { CaptureAndInvalidateLocked(); }
        finally { _lock.Release(); }
    }

    /// <summary>
    /// Acquires the context lock, captures the active tab's state, drops the active tab reference,
    /// and returns a scope that releases the lock on dispose. Used by <see cref="CharacterService"/>
    /// to hold the lock for the duration of a full-file load (so a concurrent EnterAsync from a
    /// mutation site cannot stomp the singleton mid-load). Equivalent to
    /// <see cref="CaptureAndInvalidateAsync"/> + manually holding the lock until the load finishes,
    /// without exposing the semaphore.
    /// </summary>
    public static async Task<Scope> EnterForLoadAsync()
    {
        await _lock.WaitAsync();
        try
        {
            CaptureAndInvalidateLocked();
            return new Scope();
        }
        catch
        {
            _lock.Release();
            throw;
        }
    }

    private static void CaptureAndInvalidateLocked()
    {
        if (_active != null && CharacterManager.Current.Character != null)
        {
            try
            {
                _active.StateXml = _active.File.SerializeCharacter(CharacterManager.Current.Character);
            }
            catch (Exception ex)
            {
                DebugLogService.Instance.LogException(ex, "CharacterContext.CaptureAndInvalidateLocked");
            }
        }
        _active = null;
    }

    private static async Task SwapAsync(CharacterTab incoming)
    {
        // 1. Capture the outgoing tab's live state so unsaved edits are not lost on swap.
        if (_active != null && CharacterManager.Current.Character != null)
        {
            try
            {
                _active.StateXml = _active.File.SerializeCharacter(CharacterManager.Current.Character);
            }
            catch (Exception ex)
            {
                DebugLogService.Instance.LogException(ex, "CharacterContext.SwapAsync:capture");
            }
        }

        // 2. Reset ancillary singletons (prepared spells, expander registry) before hydrating.
        CharacterLoadCompatibilityService.PrepareForCharacterLoad();

        // 3. Hydrate the incoming tab. Prefer the in-memory snapshot (unsaved edits); fall back
        //    to the on-disk file path for the first activation of a tab.
        if (incoming.StateXml is { Length: > 0 } bytes)
        {
            string temp = Path.Combine(
                Path.GetTempPath(),
                $"aurora_ctx_{Guid.NewGuid():N}.dnd5e");
            try
            {
                await File.WriteAllBytesAsync(temp, bytes);
                await incoming.File.Load(temp);
            }
            finally
            {
                try { if (File.Exists(temp)) File.Delete(temp); } catch { }
            }
        }
        else
        {
            await incoming.File.Load();
        }

        // 4. Re-point the tab's cached Character at the freshly-loaded singleton character.
        incoming.Character = CharacterManager.Current.Character;
    }

    /// <summary>
    /// Disposable handle returned by <see cref="EnterAsync"/>. Disposing releases the context lock;
    /// the active tab stays loaded so the next EnterAsync for the same tab is free.
    /// </summary>
    public readonly struct Scope : IDisposable
    {
        public void Dispose() => _lock.Release();
    }
}
