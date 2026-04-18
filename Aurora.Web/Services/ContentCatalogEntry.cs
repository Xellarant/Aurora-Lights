namespace Aurora.Web.Services;

public sealed record ContentCatalogEntry(
    string Id,
    string Name,
    string Type,
    string Source,
    string FileName,
    ContentCatalogOrigin Origin);
