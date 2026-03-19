using Builder.Presentation.Interfaces;

namespace Builder.Presentation;

/// <summary>
/// Static bridge for the selection rule expander handler.
/// Set by SelectionRuleExpanderHandler (WPF) on initialisation so that
/// Aurora.Logic code can interact with UI expanders without a WPF dependency.
/// </summary>
public static class SelectionRuleExpanderContext
{
    public static ISelectionRuleExpanderHandler? Current { get; set; }
}
