namespace Aurora.Importer;

/// <summary>
/// Public entry point for importing Aurora XML content into the SQLite database.
/// </summary>
public static class AuroraContentImporter
{
    /// <summary>
    /// Returns true if the SQLite database does not exist or is out of date
    /// relative to the XML files in <paramref name="contentDirectory"/>.
    /// </summary>
    public static bool IsStale(string contentDirectory, string sqlitePath) =>
        AuroraSqliteImporter.IsStale(contentDirectory, sqlitePath);

    /// <summary>
    /// Scans <paramref name="contentDirectory"/> for Aurora XML files, then
    /// incrementally updates the SQLite database at <paramref name="sqlitePath"/>.
    /// Only files whose MD5 hash has changed since the last import are re-imported.
    /// </summary>
    public static AuroraImportResult Import(
        string contentDirectory,
        string sqlitePath,
        IProgress<AuroraImportProgress>? progress = null,
        CancellationToken cancellationToken = default)
    {
        var catalog = AuroraXmlCatalogReader.BuildCatalog(contentDirectory);
        return AuroraSqliteImporter.Import(catalog, sqlitePath, progress, cancellationToken);
    }
}
