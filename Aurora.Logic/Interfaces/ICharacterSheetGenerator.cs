namespace Builder.Presentation.Interfaces;

/// <summary>
/// Cross-platform contract for generating a character sheet PDF.
/// The WPF implementation (CharacterSheetGenerator) uses iTextSharp and
/// WPF-specific spellcasting controls. This interface lets CharacterManager
/// trigger sheet generation without a compile-time WPF dependency.
/// Registered via CharacterSheetGeneratorContext.SetCurrent().
/// </summary>
public interface ICharacterSheetGenerator
{
    FileInfo GenerateNewSheet(string outputPath, bool isPreview);
}
