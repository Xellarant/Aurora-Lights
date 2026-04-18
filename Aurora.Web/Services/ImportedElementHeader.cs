namespace Aurora.Web.Services;

public sealed record ImportedElementHeader(
    string Id,
    string Name,
    string Type,
    string Source,
    string FileName);
