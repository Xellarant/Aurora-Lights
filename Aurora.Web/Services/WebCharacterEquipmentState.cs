using Aurora.Components.Models;

namespace Aurora.Web.Services;

public sealed record WebCharacterEquipmentState(
    ImportedCharacterSummary Summary,
    EquipmentOverviewModel Equipment,
    string StatusMessage);
