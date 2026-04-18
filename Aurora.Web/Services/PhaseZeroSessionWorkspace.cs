namespace Aurora.Web.Services;

public sealed class PhaseZeroSessionWorkspace
{
    public required Guid WorkspaceId { get; init; }
    public required string WorkspacePath { get; init; }
    public required DateTimeOffset CreatedUtc { get; init; }
    public DateTimeOffset LastTouchedUtc { get; set; }
    public List<ImportedSessionFile> ImportedFiles { get; } = [];
    public SessionContentIndex Index { get; } = new();

    public int XmlFileCount => ImportedFiles.Count(file => file.Kind == ImportedContentKind.Xml);
    public int CharacterFileCount => ImportedFiles.Count(file => file.Kind == ImportedContentKind.CharacterFile);
    public int ArchiveCount => ImportedFiles.Count(file => file.Kind == ImportedContentKind.Archive);
}
