namespace Aurora.Web.Services;

public sealed class SessionContentIndex
{
    private readonly List<ImportedElementHeader> _elements = [];
    private readonly Dictionary<string, int> _elementTypes = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, int> _sources = new(StringComparer.OrdinalIgnoreCase);

    public IReadOnlyList<ImportedElementHeader> Elements => _elements;
    public IReadOnlyDictionary<string, int> ElementTypes => _elementTypes;
    public IReadOnlyDictionary<string, int> Sources => _sources;
    public int TotalElements => _elements.Count;

    public void AddRange(IEnumerable<ImportedElementHeader> elements)
    {
        foreach (var element in elements)
        {
            _elements.Add(element);

            if (_elementTypes.TryGetValue(element.Type, out int typeCount))
                _elementTypes[element.Type] = typeCount + 1;
            else
                _elementTypes[element.Type] = 1;

            if (_sources.TryGetValue(element.Source, out int sourceCount))
                _sources[element.Source] = sourceCount + 1;
            else
                _sources[element.Source] = 1;
        }
    }

    public void Clear()
    {
        _elements.Clear();
        _elementTypes.Clear();
        _sources.Clear();
    }
}
