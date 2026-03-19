using Builder.Presentation.Interfaces;

namespace Builder.Presentation;

/// <summary>
/// Static bridge for the character sheet generator.
/// Set by CharacterSheetGenerator (WPF) so that CharacterManager can
/// request sheet generation without a compile-time WPF dependency.
/// </summary>
public static class CharacterSheetGeneratorContext
{
    public static ICharacterSheetGenerator? Current { get; set; }
}
