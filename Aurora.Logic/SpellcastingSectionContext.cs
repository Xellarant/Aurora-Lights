using Builder.Presentation.Interfaces;

namespace Builder.Presentation;

/// <summary>
/// Static bridge for the spellcasting section handler.
/// Set by SpellcastingSectionHandler (WPF) on initialisation so that
/// Aurora.Logic code (CharacterManager) can query spellcasting data
/// without a compile-time WPF dependency.
/// </summary>
public static class SpellcastingSectionContext
{
    public static ISpellcastingSectionHandler? Current { get; set; }
}
