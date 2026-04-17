using Aurora.Components.Models;

namespace Aurora.Web.Services;

public sealed record WebCharacterInfoState(
    ImportedCharacterSummary Summary,
    EditableCharacterInfoModel Info,
    string StatusMessage);
