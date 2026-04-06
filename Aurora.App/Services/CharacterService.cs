using Builder.Core.Events;
using Builder.Presentation;
using Builder.Presentation.Events.Shell;
using Builder.Presentation.Models;
using Builder.Presentation.Services.Data;
using Builder.Presentation.Views.Sliders;

namespace Aurora.App.Services;

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
    private readonly SemaphoreSlim _elementLock = new(1, 1);

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

    public async Task<(bool Success, string Message)> LoadCharacterAsync(CharacterFile file)
    {
        await EnsureElementsLoadedAsync();
        LoadingPercent = 0;
        LoadingStatus  = "";
        try
        {
            // Clear prepared spell state from any previous character load.
            if (SpellcastingSectionContext.Current is MauiSpellcastingSectionHandler spellHandler)
                spellHandler.Reset();

            var result = await Task.Run(async () => await file.Load());
            var character = CharacterManager.Current?.Character;
            if (character != null)
            {
                CurrentCharacter     = character;
                CurrentCharacterFile = file;
            }
            return (result.Success || CurrentCharacter != null,
                    result.Success ? string.Empty
                        : $"⚠ Partial load: {result.Message}\n\nElements loaded: {ElementCount}\n{_initDiagnostic}\nCustom dir: {CustomElementsDirectory}");
        }
        catch (Exception ex)
        {
            return (false, $"{ex.GetType().Name}: {ex.Message}\n\n{ex.StackTrace}");
        }
    }

    public string CharactersDirectory => DataManager.Current.UserDocumentsRootDirectory;
}
