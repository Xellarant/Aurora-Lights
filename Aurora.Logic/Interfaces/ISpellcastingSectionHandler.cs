using Builder.Presentation.UserControls.Spellcasting;

namespace Builder.Presentation.Interfaces;

/// <summary>
/// Cross-platform contract for spellcasting section management.
/// The WPF implementation (SpellcastingSectionHandler) holds a collection of
/// SpellcasterSelectionControl UserControls; this interface exposes only the
/// ViewModel surface so that Aurora.Logic code (CharacterManager,
/// CharacterSheetGenerator) can query spell data without a WPF dependency.
/// </summary>
public interface ISpellcastingSectionHandler
{
    SpellcasterSelectionControlViewModel? GetSpellcasterSectionViewModel(string uniqueIdentifier);
    bool SetPrepareSpell(Builder.Data.Elements.SpellcastingInformation information, string elementId);

    /// <summary>
    /// Clears any cached prepared-spell state before loading a different character.
    /// WPF derives this from live controls, so the default implementation is a no-op.
    /// </summary>
    void ResetPreparedState() { }

    /// <summary>
    /// Returns the element IDs of spells that the user has manually prepared for the
    /// given spellcasting class name (e.g. "Artificer"). Used when no WPF ViewModel
    /// is available to derive prepared state. Returns an empty collection by default.
    /// </summary>
    IReadOnlyCollection<string> GetPreparedIds(string spellcastingName)
        => Array.Empty<string>();
}
