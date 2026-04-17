namespace Aurora.Web.Services;

public sealed class MergedContentCatalog
{
    public required IReadOnlyList<ContentCatalogEntry> Elements { get; init; }
    public required IReadOnlyDictionary<string, int> ElementTypes { get; init; }
    public required IReadOnlyDictionary<string, int> Sources { get; init; }
    public required IReadOnlyDictionary<ContentCatalogOrigin, int> Origins { get; init; }
    public int BaselineElementCount => Origins.TryGetValue(ContentCatalogOrigin.Baseline, out int count) ? count : 0;
    public int SessionElementCount => Origins.TryGetValue(ContentCatalogOrigin.Session, out int count) ? count : 0;
    public int TotalElements => Elements.Count;
}
