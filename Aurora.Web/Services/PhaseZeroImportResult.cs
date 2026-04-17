namespace Aurora.Web.Services;

public sealed record PhaseZeroImportResult(
    PhaseZeroSessionWorkspace Workspace,
    int ImportedFiles,
    int DiscoveredElements,
    int DistinctElementTypes,
    IReadOnlyList<string> Warnings);
