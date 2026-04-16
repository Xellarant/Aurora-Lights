using Builder.Presentation;
using Builder.Presentation.Models;

namespace Aurora.App.Services;

/// <summary>
/// Represents a single open character tab. Holds the character file metadata,
/// the loaded Character object (cached so switching tabs doesn't reload),
/// and a dirty flag for unsaved in-memory edits.
/// </summary>
public sealed class CharacterTab
{
    public Guid Id { get; } = Guid.NewGuid();
    public CharacterFile File { get; }
    public Character? Character { get; set; }
    public bool IsDirty { get; set; }

    /// <summary>
    /// Snapshot of class progression data captured at load time.
    /// Stored separately because CharacterManager.Current is a singleton
    /// and gets overwritten when subsequent characters are loaded.
    /// </summary>
    public CharacterSnapshot? Snapshot { get; set; }
    public IReadOnlyList<ClassProgressionSnapshot>? ProgressionSnapshots { get; set; }

    public string DisplayName => File.DisplayName ?? "Character";

    /// <summary>Session state (HP, spell slots, custom resources, etc.) for this character.</summary>
    public SessionState Session { get; set; } = new();

    /// <summary>
    /// True while the character is loading in the background (eager-navigation mode).
    /// Build.razor shows a loading indicator instead of "no character" when this is set.
    /// Cleared by Start.razor once LoadCharacterAsync completes.
    /// </summary>
    public bool IsLoading { get; set; }

    /// <summary>
    /// True when this character was just created (not loaded from an existing file).
    /// Used by Build.razor to show the guided creation banner.
    /// </summary>
    public bool IsNew { get; set; }

    public CharacterTab(CharacterFile file) => File = file;
}

/// <summary>
/// Immutable snapshot of a single class's progression data for display.
/// </summary>
public sealed record ClassProgressionSnapshot(
    string ClassName,
    string HitDie,
    int Level,
    bool IsMainClass,
    IReadOnlyList<FeatureEntry> Features
);
