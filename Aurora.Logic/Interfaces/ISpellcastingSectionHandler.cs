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
}
