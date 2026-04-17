using System.Reflection;
using System.Xml.Linq;
using Builder.Presentation.Services.Data;

namespace Aurora.Web.Services;

public sealed class BaselineContentCatalogService
{
    private readonly ILogger<BaselineContentCatalogService> _logger;
    private IReadOnlyList<ContentCatalogEntry>? _cachedEntries;
    private readonly Lock _lock = new();

    public BaselineContentCatalogService(ILogger<BaselineContentCatalogService> logger)
    {
        _logger = logger;
    }

    public IReadOnlyList<ContentCatalogEntry> GetEntries()
    {
        if (_cachedEntries is not null)
            return _cachedEntries;

        lock (_lock)
        {
            if (_cachedEntries is not null)
                return _cachedEntries;

            _cachedEntries = LoadEntries();
            return _cachedEntries;
        }
    }

    private IReadOnlyList<ContentCatalogEntry> LoadEntries()
    {
        const string prefix = "Builder.Presentation.Resources.Data.";
        Assembly assembly = typeof(DataManager).Assembly;

        List<ContentCatalogEntry> entries = [];
        foreach (string resourceName in assembly.GetManifestResourceNames()
                                                .Where(name => name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)
                                                            && name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                                                .OrderBy(name => name, StringComparer.OrdinalIgnoreCase))
        {
            try
            {
                using Stream? stream = assembly.GetManifestResourceStream(resourceName);
                if (stream is null)
                    continue;

                XDocument document = XDocument.Load(stream, LoadOptions.None);
                string fileName = resourceName[prefix.Length..];

                entries.AddRange(document.Root?
                    .Elements("element")
                    .Select(element => new ContentCatalogEntry(
                        element.Attribute("id")?.Value ?? string.Empty,
                        element.Attribute("name")?.Value ?? "(unnamed)",
                        element.Attribute("type")?.Value ?? "(unknown)",
                        element.Attribute("source")?.Value ?? "(unspecified)",
                        fileName,
                        ContentCatalogOrigin.Baseline))
                    ?? []);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to read baseline content resource {ResourceName}", resourceName);
            }
        }

        return entries;
    }
}
