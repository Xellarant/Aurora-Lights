using Aurora.Components.Models;

namespace Aurora.Web.Services;

public sealed record WebCharacterMagicState(
    ImportedCharacterSummary Summary,
    MagicOverviewModel Magic,
    string StatusMessage);
