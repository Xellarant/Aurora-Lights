namespace Aurora.App.Services;

public enum HpMethod { Average, Rolled }

/// <summary>
/// Persists user preferences using MAUI's platform Preferences API (registry on Windows).
/// Registered as a singleton so all pages share the same instance.
/// </summary>
public sealed class UserPreferencesService
{
    private const string KeyAutoSave      = "build.auto_save";
    private const string KeyHpMethod      = "build.hp_method";
    private const string KeyDevMode       = "dev.mode";
    private const string KeyMruCharacter  = "app.mru_character";
    private const string KeyLightMode     = "app.light_mode";

    public event Action? ThemeChanged;

    /// <summary>
    /// When true, the app uses a light colour palette instead of the default dark theme.
    /// Default: false.
    /// </summary>
    public bool LightMode
    {
        get => Preferences.Default.Get(KeyLightMode, defaultValue: false);
        set { Preferences.Default.Set(KeyLightMode, value); ThemeChanged?.Invoke(); }
    }

    // Character sheet page/card options — read directly by MauiCharacterSheetGenerator as well.
    public const string KeySpellCards      = "sheet.spellcards";
    public const string KeyItemCards       = "sheet.itemcards";
    public const string KeyAttackCards     = "sheet.attackcards";
    public const string KeyFeatureCards    = "sheet.featurecards";
    public const string KeyBackgroundPage  = "sheet.backgroundpage";
    public const string KeyEquipmentPage   = "sheet.equipmentpage";

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

    /// <summary>
    /// When true, a Console page is available in the nav menu showing captured
    /// runtime exceptions and diagnostic messages.
    /// Default: false.
    /// </summary>
    public bool DevMode
    {
        get => Preferences.Default.Get(KeyDevMode, defaultValue: false);
        set => Preferences.Default.Set(KeyDevMode, value);
    }

    /// <summary>
    /// Global default for how HP is assigned on level-up.
    /// Applied when creating a new character; can be overridden per-character on the Build page.
    /// Default: Average.
    /// </summary>
    public HpMethod DefaultHpMethod
    {
        get => Preferences.Default.Get(KeyHpMethod, defaultValue: (int)HpMethod.Average) == (int)HpMethod.Rolled
               ? HpMethod.Rolled : HpMethod.Average;
        set => Preferences.Default.Set(KeyHpMethod, (int)value);
    }

    /// <summary>
    /// File path of the most recently opened character. Used to preload that character
    /// in the background so it is ready before the user clicks it.
    /// </summary>
    public string? MruCharacterPath
    {
        get { var v = Preferences.Default.Get(KeyMruCharacter, ""); return v.Length > 0 ? v : null; }
        set => Preferences.Default.Set(KeyMruCharacter, value ?? "");
    }

    // ── Character sheet card pages ────────────────────────────────────────────

    /// <summary>Include a spell-cards page at the end of the generated PDF.</summary>
    public bool IncludeSpellCards
    {
        get => Preferences.Default.Get(KeySpellCards,   defaultValue: false);
        set => Preferences.Default.Set(KeySpellCards,   value);
    }

    /// <summary>Include an equipment/item-cards page at the end of the generated PDF.</summary>
    public bool IncludeItemCards
    {
        get => Preferences.Default.Get(KeyItemCards,    defaultValue: false);
        set => Preferences.Default.Set(KeyItemCards,    value);
    }

    /// <summary>Include an attack-cards page at the end of the generated PDF.</summary>
    public bool IncludeAttackCards
    {
        get => Preferences.Default.Get(KeyAttackCards,  defaultValue: false);
        set => Preferences.Default.Set(KeyAttackCards,  value);
    }

    /// <summary>Include a feature-cards page at the end of the generated PDF.</summary>
    public bool IncludeFeatureCards
    {
        get => Preferences.Default.Get(KeyFeatureCards, defaultValue: false);
        set => Preferences.Default.Set(KeyFeatureCards, value);
    }

    // ── Character sheet page toggles ──────────────────────────────────────────

    /// <summary>Include the background/biography page in the generated PDF. Default: true.</summary>
    public bool IncludeBackgroundPage
    {
        get => Preferences.Default.Get(KeyBackgroundPage, defaultValue: true);
        set => Preferences.Default.Set(KeyBackgroundPage, value);
    }

    /// <summary>Include the equipment/inventory page in the generated PDF. Default: true.</summary>
    public bool IncludeEquipmentPage
    {
        get => Preferences.Default.Get(KeyEquipmentPage, defaultValue: true);
        set => Preferences.Default.Set(KeyEquipmentPage, value);
    }
}
