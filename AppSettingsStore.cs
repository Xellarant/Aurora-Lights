// Cross-platform replacement for Builder.Presentation.Properties.Settings
// (System.Configuration / Properties.Settings.Default is .NET Framework only)
//
// Drop-in for Phase 1: ApplicationSettings.cs replaces
//   this.Settings = Builder.Presentation.Properties.Settings.Default;
// with
//   this.Settings = AppSettingsStore.Load();
//
// Settings are stored as JSON in the platform-appropriate AppData folder:
//   Windows : %APPDATA%\AuroraLights\settings.json
//   macOS   : ~/Library/Application Support/AuroraLights/settings.json
//   Linux   : ~/.config/AuroraLights/settings.json

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Builder.Presentation;

/// <summary>
/// Persistent application settings backed by a JSON file.
/// Mirrors every property that was on the old Properties.Settings class
/// so that ApplicationSettings.cs requires minimal changes.
/// </summary>
public sealed class AppSettingsStore
{
    // ----------------------------------------------------------------
    // Storage location
    // ----------------------------------------------------------------

    private static readonly string SettingsDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "AuroraLights");

    private static readonly string SettingsPath =
        Path.Combine(SettingsDirectory, "settings.json");

    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.Never,
        PropertyNameCaseInsensitive = true
    };

    // ----------------------------------------------------------------
    // Settings properties
    // Each property mirrors a field that existed on the old
    // Builder.Presentation.Properties.Settings class.
    // ----------------------------------------------------------------

    public string Accent { get; set; } = "Aurora Default";
    public string Theme { get; set; } = "Aurora Light";
    public string PlayerName { get; set; } = string.Empty;

    /// <summary>
    /// 0 = Roll 3d6
    /// 1 = Roll 4d6 discard lowest
    /// 2 = Standard Array
    /// 3 = Point Buy
    /// </summary>
    public int AbilitiesGenerationOption { get; set; } = 1;

    public bool AllowEditableSheet { get; set; } = false;
    public bool IncludeItemcards { get; set; } = true;
    public bool IncludeSpellcards { get; set; } = true;
    public bool CharacterSheetAbilitiesFlipped { get; set; } = false;
    public bool GenerateSheetOnCharacterChangedRegistered { get; set; } = false;
    public bool AutoNavigateNextSelectionWhenAvailable { get; set; } = true;
    public bool StartupCheckForContentUpdated { get; set; } = true;

    /// <summary>
    /// Preserved for upgrade-path compatibility; always false after first run.
    /// </summary>
    public bool ConfigurationUpgradeRequired { get; set; } = false;

    /// <summary>
    /// 1 = compact (17px), 2 = normal (21px), 3 = large (25px)
    /// </summary>
    public int SelectionExpanderGridRowSize { get; set; } = 2;

    // ----------------------------------------------------------------
    // Load / Save / Reset
    // ----------------------------------------------------------------

    /// <summary>
    /// Loads settings from disk. Returns defaults if the file does not
    /// exist or cannot be read (e.g. first launch or corrupt file).
    /// </summary>
    public static AppSettingsStore Load()
    {
        try
        {
            if (File.Exists(SettingsPath))
            {
                string json = File.ReadAllText(SettingsPath);
                return JsonSerializer.Deserialize<AppSettingsStore>(json, SerializerOptions)
                       ?? new AppSettingsStore();
            }
        }
        catch (Exception ex)
        {
            // Log but do not crash — fall back to defaults
            System.Diagnostics.Debug.WriteLine(
                $"[AppSettingsStore] Failed to load settings: {ex.Message}");
        }

        return new AppSettingsStore();
    }

    /// <summary>
    /// Persists current settings to disk.
    /// Creates the directory if it does not exist.
    /// </summary>
    public void Save()
    {
        try
        {
            Directory.CreateDirectory(SettingsDirectory);
            string json = JsonSerializer.Serialize(this, SerializerOptions);
            File.WriteAllText(SettingsPath, json);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(
                $"[AppSettingsStore] Failed to save settings: {ex.Message}");
            throw; // re-throw so ApplicationSettings.cs can log/report it
        }
    }

    /// <summary>
    /// Reloads settings from disk, discarding any in-memory changes.
    /// Matches the behaviour of Properties.Settings.Default.Reload().
    /// </summary>
    public void Reload()
    {
        AppSettingsStore fresh = Load();

        Accent                                   = fresh.Accent;
        Theme                                    = fresh.Theme;
        PlayerName                               = fresh.PlayerName;
        AbilitiesGenerationOption                = fresh.AbilitiesGenerationOption;
        AllowEditableSheet                       = fresh.AllowEditableSheet;
        IncludeItemcards                         = fresh.IncludeItemcards;
        IncludeSpellcards                        = fresh.IncludeSpellcards;
        CharacterSheetAbilitiesFlipped           = fresh.CharacterSheetAbilitiesFlipped;
        GenerateSheetOnCharacterChangedRegistered = fresh.GenerateSheetOnCharacterChangedRegistered;
        AutoNavigateNextSelectionWhenAvailable   = fresh.AutoNavigateNextSelectionWhenAvailable;
        StartupCheckForContentUpdated            = fresh.StartupCheckForContentUpdated;
        ConfigurationUpgradeRequired             = fresh.ConfigurationUpgradeRequired;
        SelectionExpanderGridRowSize             = fresh.SelectionExpanderGridRowSize;
    }

    /// <summary>
    /// Resets all settings to their defaults and saves immediately.
    /// Matches the behaviour of Properties.Settings.Default.Reset().
    /// </summary>
    public void Reset()
    {
        AppSettingsStore defaults = new AppSettingsStore();

        Accent                                   = defaults.Accent;
        Theme                                    = defaults.Theme;
        PlayerName                               = defaults.PlayerName;
        AbilitiesGenerationOption                = defaults.AbilitiesGenerationOption;
        AllowEditableSheet                       = defaults.AllowEditableSheet;
        IncludeItemcards                         = defaults.IncludeItemcards;
        IncludeSpellcards                        = defaults.IncludeSpellcards;
        CharacterSheetAbilitiesFlipped           = defaults.CharacterSheetAbilitiesFlipped;
        GenerateSheetOnCharacterChangedRegistered = defaults.GenerateSheetOnCharacterChangedRegistered;
        AutoNavigateNextSelectionWhenAvailable   = defaults.AutoNavigateNextSelectionWhenAvailable;
        StartupCheckForContentUpdated            = defaults.StartupCheckForContentUpdated;
        ConfigurationUpgradeRequired             = defaults.ConfigurationUpgradeRequired;
        SelectionExpanderGridRowSize             = defaults.SelectionExpanderGridRowSize;

        Save();
    }

    // ----------------------------------------------------------------
    // Migration helper
    // ----------------------------------------------------------------

    /// <summary>
    /// One-time migration from the old Properties.Settings .config file.
    /// Call this on first launch (when ConfigurationUpgradeRequired == true)
    /// to pull any previously-saved values across to the new JSON store.
    ///
    /// Usage in ApplicationManager.UpgradeConfigurationCheck():
    ///
    ///     if (this.Settings.Settings.ConfigurationUpgradeRequired)
    ///     {
    ///         AppSettingsStore.MigrateFromLegacyConfig(this.Settings.Settings);
    ///     }
    ///
    /// The method is a no-op on macOS / Linux where no legacy file exists.
    /// </summary>
    public static void MigrateFromLegacyConfig(AppSettingsStore target)
    {
        // On macOS and Linux there is no legacy .config file — nothing to migrate.
        if (!OperatingSystem.IsWindows())
        {
            target.ConfigurationUpgradeRequired = false;
            target.Save();
            return;
        }

        // On Windows, the old user.config lives under:
        //   %LOCALAPPDATA%\Aurora_Builder\<assembly-version>\user.config
        // We attempt a best-effort read; if it fails we just clear the flag.
        try
        {
            // The legacy System.Configuration upgrade path no longer exists on
            // .NET 8, so we can only attempt to locate and parse the raw XML.
            string localApp = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string configRoot = Path.Combine(localApp, "Aurora_Builder");

            if (!Directory.Exists(configRoot))
            {
                target.ConfigurationUpgradeRequired = false;
                target.Save();
                return;
            }

            // Find the most recently modified user.config
            string? userConfig = Directory
                .EnumerateFiles(configRoot, "user.config", SearchOption.AllDirectories)
                .OrderByDescending(File.GetLastWriteTimeUtc)
                .FirstOrDefault();

            if (userConfig is null)
            {
                target.ConfigurationUpgradeRequired = false;
                target.Save();
                return;
            }

            // Parse the XML and map known keys to our settings object
            var doc = System.Xml.Linq.XDocument.Load(userConfig);
            var settings = doc
                .Descendants("setting")
                .ToDictionary(
                    el => el.Attribute("name")?.Value ?? string.Empty,
                    el => el.Element("value")?.Value ?? string.Empty);

            if (settings.TryGetValue("Accent", out string? accent) && !string.IsNullOrWhiteSpace(accent))
                target.Accent = accent;

            if (settings.TryGetValue("Theme", out string? theme) && !string.IsNullOrWhiteSpace(theme))
                target.Theme = theme;

            if (settings.TryGetValue("PlayerName", out string? playerName))
                target.PlayerName = playerName ?? string.Empty;

            if (settings.TryGetValue("AbilitiesGenerationOption", out string? agoStr)
                && int.TryParse(agoStr, out int ago))
                target.AbilitiesGenerationOption = ago;

            if (settings.TryGetValue("AllowEditableSheet", out string? aesStr)
                && bool.TryParse(aesStr, out bool aes))
                target.AllowEditableSheet = aes;

            if (settings.TryGetValue("IncludeItemcards", out string? iicStr)
                && bool.TryParse(iicStr, out bool iic))
                target.IncludeItemcards = iic;

            if (settings.TryGetValue("IncludeSpellcards", out string? iscStr)
                && bool.TryParse(iscStr, out bool isc))
                target.IncludeSpellcards = isc;

            if (settings.TryGetValue("SelectionExpanderGridRowSize", out string? segrsStr)
                && int.TryParse(segrsStr, out int segrs))
                target.SelectionExpanderGridRowSize = segrs;

            if (settings.TryGetValue("StartupCheckForContentUpdated", out string? scfcuStr)
                && bool.TryParse(scfcuStr, out bool scfcu))
                target.StartupCheckForContentUpdated = scfcu;

            if (settings.TryGetValue("AutoNavigateNextSelectionWhenAvailable", out string? annswaStr)
                && bool.TryParse(annswaStr, out bool annswa))
                target.AutoNavigateNextSelectionWhenAvailable = annswa;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(
                $"[AppSettingsStore] Legacy migration failed (non-fatal): {ex.Message}");
        }
        finally
        {
            target.ConfigurationUpgradeRequired = false;
            target.Save();
        }
    }
}
