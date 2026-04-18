namespace Aurora.Components.Models;

public sealed record SourceRestrictionGroupModel(
    string Id,
    string Name,
    string Summary,
    bool AllowUnchecking,
    bool? IsChecked,
    IReadOnlyList<SourceRestrictionItemModel> Sources);
