using System.Collections;

namespace Aurora.Importer.Models;

public class AuroraTextCollection : IEnumerable<string>
{
    public string? raw { get; set; }
    public List<string> values { get; set; } = new();

    public int Count => values?.Count ?? 0;
    public string this[int index] => values[index];

    public void Add(string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
            values.Add(value.Trim());
    }

    public void AddRange(IEnumerable<string> newValues)
    {
        if (newValues == null) return;
        foreach (var value in newValues.Where(x => !string.IsNullOrWhiteSpace(x)))
            values.Add(value.Trim());
    }

    public IEnumerator<string> GetEnumerator() => values.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class AuroraSetterEntry
{
    public string? name { get; set; }
    public string? value { get; set; }
    public Dictionary<string, string> attributes { get; set; } = new(StringComparer.OrdinalIgnoreCase);

    public string? GetAttribute(string attributeName) =>
        attributes.TryGetValue(attributeName, out var v) ? v : null;
}

public class AuroraItemEntry
{
    public string? value { get; set; }
    public Dictionary<string, string> attributes { get; set; } = new(StringComparer.OrdinalIgnoreCase);

    public string? GetAttribute(string attributeName) =>
        attributes.TryGetValue(attributeName, out var v) ? v : null;
}

public class AuroraBlockEntry
{
    public string? name { get; set; }
    public string? value { get; set; }
    public string? rawXml { get; set; }
    public Dictionary<string, string> attributes { get; set; } = new(StringComparer.OrdinalIgnoreCase);

    public string? GetAttribute(string attributeName) =>
        attributes.TryGetValue(attributeName, out var v) ? v : null;
}
