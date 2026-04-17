namespace Aurora.Web.Services;

public sealed class WebContentCatalogService
{
    private readonly BaselineContentCatalogService _baselineCatalog;
    private readonly PhaseZeroSessionWorkspaceService _sessionWorkspace;

    public WebContentCatalogService(
        BaselineContentCatalogService baselineCatalog,
        PhaseZeroSessionWorkspaceService sessionWorkspace)
    {
        _baselineCatalog = baselineCatalog;
        _sessionWorkspace = sessionWorkspace;
    }

    public async Task<MergedContentCatalog> GetCatalogAsync()
    {
        Dictionary<string, ContentCatalogEntry> merged = new(StringComparer.OrdinalIgnoreCase);

        foreach (ContentCatalogEntry entry in _baselineCatalog.GetEntries())
            merged[entry.Id] = entry;

        PhaseZeroSessionWorkspace workspace = await _sessionWorkspace.GetWorkspaceAsync();
        foreach (ImportedElementHeader header in workspace.Index.Elements)
        {
            merged[header.Id] = new ContentCatalogEntry(
                header.Id,
                header.Name,
                header.Type,
                header.Source,
                header.FileName,
                ContentCatalogOrigin.Session);
        }

        List<ContentCatalogEntry> ordered = merged.Values
            .OrderBy(entry => entry.Type, StringComparer.OrdinalIgnoreCase)
            .ThenBy(entry => entry.Name, StringComparer.OrdinalIgnoreCase)
            .ToList();

        Dictionary<string, int> types = ordered
            .GroupBy(entry => entry.Type, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(group => group.Key, group => group.Count(), StringComparer.OrdinalIgnoreCase);

        Dictionary<string, int> sources = ordered
            .GroupBy(entry => entry.Source, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(group => group.Key, group => group.Count(), StringComparer.OrdinalIgnoreCase);

        Dictionary<ContentCatalogOrigin, int> origins = ordered
            .GroupBy(entry => entry.Origin)
            .ToDictionary(group => group.Key, group => group.Count());

        return new MergedContentCatalog
        {
            Elements = ordered,
            ElementTypes = types,
            Sources = sources,
            Origins = origins
        };
    }
}
