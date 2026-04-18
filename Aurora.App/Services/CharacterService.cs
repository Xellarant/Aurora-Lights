using Builder.Core.Events;
using Builder.Presentation;
using Builder.Presentation.Events.Shell;
using Builder.Presentation.Models;
using Builder.Presentation.Services;
using Builder.Presentation.Services.Data;
using Builder.Presentation.Views.Sliders;

namespace Aurora.App.Services;

public sealed record NewCharacterInfo(string Name, string PlayerName);

/// <summary>
/// Wraps DataManager to provide character file listing and full character loading.
/// InitializeDirectories() is called eagerly; InitializeElementDataAsync() is called
/// lazily on first character load (it can take a few seconds).
/// Subscribes to the EventAggregator to surface loading progress for the UI.
/// </summary>
public sealed class CharacterService :
    ISubscriber<CharacterLoadingSliderProgressEvent>,
    ISubscriber<CharacterLoadingSliderStatusUpdateEvent>
{
    private bool _directoriesInitialized;
    private bool _elementsInitialized;
    private readonly SemaphoreSlim _elementLock  = new(1, 1);
    // Prevents concurrent character loads from corrupting the CharacterManager singleton.
    private readonly SemaphoreSlim _characterLock = new(1, 1);

    public Character? CurrentCharacter { get; private set; }
    public CharacterFile? CurrentCharacterFile { get; private set; }

    // ── Loading progress ────────────────────────────────────────────────────
    public int    LoadingPercent { get; private set; }
    public string LoadingStatus  { get; private set; } = "";

    /// <summary>Fires on the background thread whenever progress or status changes.</summary>
    public event Action? LoadingProgressChanged;

    public CharacterService()
    {
        ApplicationContext.Current.EventAggregator.Subscribe(this);
    }

    void ISubscriber<CharacterLoadingSliderProgressEvent>.OnHandleEvent(CharacterLoadingSliderProgressEvent e)
    {
        LoadingPercent = e.ProgressPercentage;
        LoadingProgressChanged?.Invoke();
    }

    void ISubscriber<CharacterLoadingSliderStatusUpdateEvent>.OnHandleEvent(CharacterLoadingSliderStatusUpdateEvent e)
    {
        LoadingStatus = e.StatusMessage ?? "";
        LoadingProgressChanged?.Invoke();
    }
    // ────────────────────────────────────────────────────────────────────────

    public void EnsureDirectoriesInitialized()
    {
        if (_directoriesInitialized) return;
        DataManager.Current.InitializeDirectories();
        _directoriesInitialized = true;
    }

    // Keep original name so callers (LoadCharacterFiles) still compile.
    public void EnsureInitialized() => EnsureDirectoriesInitialized();

    private string? _initDiagnostic;

    private async Task EnsureElementsLoadedAsync()
    {
        if (_elementsInitialized) return;
        await _elementLock.WaitAsync();
        try
        {
            if (_elementsInitialized) return;
            EnsureDirectoriesInitialized();
            DataManager.Current.InitializeFileLogger();
            await DataManager.Current.InitializeElementDataAsync();
            _elementsInitialized = true;

            var testId = "ID_WOTC_MOTM_RACE_GOBLIN";
            var testElement = DataManager.Current.ElementsCollection.GetElement(testId);
            _initDiagnostic = testElement != null
                ? $"Custom elements OK (e.g. {testId} found)"
                : $"⚠ Custom elements MISSING — {testId} not in collection. " +
                  $"Custom dir: {DataManager.Current.UserDocumentsCustomElementsDirectory}";
        }
        finally
        {
            _elementLock.Release();
        }
    }

    /// <summary>
    /// Starts loading element data eagerly (e.g. on app launch) so it is ready before
    /// the user selects a character. Safe to call multiple times; the SemaphoreSlim
    /// ensures only one load runs and subsequent callers return immediately.
    /// </summary>
    public Task PreloadAsync() => EnsureElementsLoadedAsync();

    /// <summary>
    /// Forces element data to be reloaded from disk on the next call to
    /// <see cref="PreloadAsync"/> or any character load. Call this after
    /// adding or updating custom content directories / index files.
    /// Callers must close all character tabs before invoking this.
    /// </summary>
    public async Task ReloadElementsAsync()
    {
        await _elementLock.WaitAsync();
        try
        {
            _elementsInitialized = false;
            _initDiagnostic = null;
        }
        finally
        {
            _elementLock.Release();
        }
        await PreloadAsync();
    }

    public int ElementCount => _elementsInitialized ? DataManager.Current.ElementsCollection.Count : -1;

    public string CustomElementsDirectory => DataManager.Current.UserDocumentsCustomElementsDirectory ?? "(not initialized)";

    public IReadOnlyList<CharacterFile> LoadCharacterFiles()
    {
        EnsureInitialized();
        return DataManager.Current.LoadCharacterFiles()
            .OrderBy(x => !x.IsFavorite)
            .ThenBy(x => x.DisplayName)
            .ToList();
    }

    /// <summary>True while a character is being loaded (lock is held).</summary>
    public bool IsCharacterLoading => _characterLock.CurrentCount == 0;

    /// <summary>
    /// Returns true if <paramref name="file"/> is already loaded into
    /// <see cref="CurrentCharacter"/> and no other load has since overwritten it.
    /// </summary>
    public bool IsPreloaded(CharacterFile file) =>
        CurrentCharacterFile?.FilePath == file.FilePath && CurrentCharacter != null;

    /// <summary>
    /// Starts loading <paramref name="file"/> on a background thread without blocking
    /// the caller. Safe to call speculatively — errors are swallowed. The caller can
    /// check <see cref="IsPreloaded"/> later to see whether it finished.
    /// </summary>
    public void BeginPreload(CharacterFile file) =>
        // LoadCharacterAsync catches all exceptions internally, so fire-and-forget is safe.
        _ = LoadCharacterAsync(file);

    public async Task<(bool Success, string Message)> LoadCharacterAsync(CharacterFile file)
    {
        // Serialize all character loads — CharacterManager.Current is a singleton and
        // cannot handle concurrent LoadCharacterAsync calls safely.
        await _characterLock.WaitAsync();
        try
        {
            await EnsureElementsLoadedAsync();
            LoadingPercent = 0;
            LoadingStatus  = "";
            try
            {
                // Clear prepared spell state from any previous character load.
                CharacterLoadCompatibilityService.PrepareForCharacterLoad();

                var result    = await Task.Run(async () => await file.Load());
                var character = CharacterManager.Current?.Character;
                if (character != null)
                {
                    // CharacterManager sets IsEquipped/EquippedLocation on item objects but does NOT
                    // call the inventory slot methods (EquipArmor/EquipPrimary/EquipSecondary) during
                    // load — that was handled by the WPF InventoryViewModel. Do it here so that the
                    // EquippedArmor/EquippedPrimary/EquippedSecondary references are non-null and
                    // equipped state round-trips correctly between the two apps.
                    CharacterLoadCompatibilityService.RestoreEquippedSlots(character);

                    CurrentCharacter     = character;
                    CurrentCharacterFile = file;

                    // Remember this as the MRU character so the next app launch can preload it.
                    if (!string.IsNullOrEmpty(file.FilePath))
                        Preferences.Default.Set("app.mru_character", file.FilePath);
                }
                return (result.Success || CurrentCharacter != null,
                        result.Success ? string.Empty
                            : $"⚠ Partial load: {result.Message}\n\nElements loaded: {ElementCount}\n{_initDiagnostic}\nCustom dir: {CustomElementsDirectory}");
            }
            catch (Exception ex)
            {
                DebugLogService.Instance.LogException(ex, "CharacterService.LoadCharacterAsync");
                return (false, $"{ex.GetType().Name}: {ex.Message}\n\n{ex.StackTrace}");
            }
        }
        finally
        {
            _characterLock.Release();
        }
    }

    public string CharactersDirectory => DataManager.Current.UserDocumentsRootDirectory;

    /// <summary>
    /// Creates a new Level-1 character, saves it to disk, and returns the CharacterFile.
    /// Applies the DefaultHpMethod preference — registering the average HP option element
    /// if Average is selected. Callers should immediately open a tab and navigate to /build.
    /// </summary>
    public async Task<(CharacterFile? File, string? Error)> CreateNewCharacterAsync(
        string name, string playerName, HpMethod hpMethod)
    {
        await EnsureElementsLoadedAsync();
        try
        {
            var character = await CharacterManager.Current.New(initializeFirstLevel: true);
            character.Name       = string.IsNullOrWhiteSpace(name) ? "New Character" : name.Trim();
            character.PlayerName = playerName.Trim();

            // Apply HP method preference
            if (hpMethod == HpMethod.Average)
            {
                var optionId = Builder.Data.Strings.InternalOptions.AllowAverageHitPoints;
                var element  = DataManager.Current.ElementsCollection.FirstOrDefault(e => e.Id == optionId);
                if (element != null)
                    CharacterManager.Current.RegisterElement(element);
            }

            string safeName = string.Concat(character.Name
                .Select(c => Path.GetInvalidFileNameChars().Contains(c) ? '_' : c));
            string path = DataManager.Current.GetCombinedCharacterFilePath(safeName);

            // Avoid clobbering an existing file.
            if (File.Exists(path))
            {
                string ts = DateTime.Now.ToString("yyyyMMdd-HHmmss");
                path = DataManager.Current.GetCombinedCharacterFilePath($"{safeName}_{ts}");
            }

            var file = new CharacterFile(path);
            file.Save(character);

            CurrentCharacter     = character;
            CurrentCharacterFile = file;
            return (file, null);
        }
        catch (Exception ex)
        {
            DebugLogService.Instance.LogException(ex, "CharacterService.CreateNewCharacterAsync");
            return (null, $"{ex.GetType().Name}: {ex.Message}");
        }
    }

}
