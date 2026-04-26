using Builder.Data.Elements;
using Builder.Presentation.Interfaces;
using Builder.Presentation.UserControls.Spellcasting;

namespace Aurora.Tests.Helpers;

/// <summary>
/// Test double for ISpellcastingSectionHandler that mirrors the behaviour of
/// MauiSpellcastingSectionHandler. Used in unit tests so we can verify prepare/unprepare
/// logic without depending on MAUI infrastructure.
/// </summary>
public sealed class TestSpellHandler : ISpellcastingSectionHandler
{
    private readonly Dictionary<string, HashSet<string>> _prepared =
        new(StringComparer.OrdinalIgnoreCase);

    public SpellcasterSelectionControlViewModel? GetSpellcasterSectionViewModel(string uniqueIdentifier) => null;

    public bool SetPrepareSpell(SpellcastingInformation information, string elementId)
    {
        if (string.IsNullOrEmpty(elementId)) return false;
        if (!_prepared.TryGetValue(information.Name, out var ids))
            _prepared[information.Name] = ids = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        ids.Add(elementId);
        return true;
    }

    public void UnsetPrepareSpell(string spellcastingName, string elementId)
    {
        if (_prepared.TryGetValue(spellcastingName, out var ids))
            ids.Remove(elementId);
    }

    public void ResetPreparedState() => _prepared.Clear();

    public IReadOnlyCollection<string> GetPreparedIds(string spellcastingName) =>
        _prepared.TryGetValue(spellcastingName, out var ids) ? ids : Array.Empty<string>();

    public int PreparedCount(string spellcastingName) => GetPreparedIds(spellcastingName).Count;
}
