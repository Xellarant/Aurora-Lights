namespace Aurora.Web.Services;

public sealed record ImportedSessionFile(
    string FileName,
    string RelativePath,
    ImportedContentKind Kind,
    long SizeBytes,
    int ElementCount);
