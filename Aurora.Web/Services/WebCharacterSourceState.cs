using Aurora.Components.Models;

namespace Aurora.Web.Services;

public sealed record WebCharacterSourceState(
    IReadOnlyList<SourceRestrictionGroupModel> Groups,
    string StatusMessage);
