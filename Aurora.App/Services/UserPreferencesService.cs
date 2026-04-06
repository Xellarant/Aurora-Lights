namespace Aurora.App.Services;

/// <summary>
/// Persists user preferences using MAUI's platform Preferences API (registry on Windows).
/// Registered as a singleton so all pages share the same instance.
/// </summary>
public sealed class UserPreferencesService
{
    private const string KeyAutoSave = "build.auto_save";

    /// <summary>
    /// When true, selecting a new option on the Build or Magic page immediately writes
    /// the character file to disk. When false, changes are applied in memory and the tab
    /// is marked dirty so the user can review and save manually.
    /// Default: true.
    /// </summary>
    public bool AutoSaveBuildChanges
    {
        get => Preferences.Default.Get(KeyAutoSave, defaultValue: true);
        set => Preferences.Default.Set(KeyAutoSave, value);
    }
}
