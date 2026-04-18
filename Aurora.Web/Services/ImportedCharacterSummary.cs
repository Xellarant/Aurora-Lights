namespace Aurora.Web.Services;

public sealed record ImportedCharacterSummary(
    string RelativePath,
    string FileName,
    string DisplayName,
    string PlayerName,
    string Level,
    string Race,
    string CharacterClass,
    string Background,
    string GroupName,
    string Version,
    long SizeBytes)
{
    public string DisplayBuild => $"Level {Level} {Race} {CharacterClass}".Trim();
}
