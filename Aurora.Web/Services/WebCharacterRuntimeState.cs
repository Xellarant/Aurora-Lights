namespace Aurora.Web.Services;

public sealed record WebCharacterRuntimeState(
    ImportedCharacterSummary Summary,
    bool IsNew,
    string StatusMessage);
