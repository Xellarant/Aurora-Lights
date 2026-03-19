using Builder.Data.Rules;

namespace Builder.Presentation.Interfaces;

/// <summary>
/// Cross-platform contract for managing selection rule expanders.
/// The WPF implementation (SelectionRuleExpanderHandler) holds references
/// to WPF SelectionRuleExpander UserControls. This interface lets
/// Aurora.Logic services interact with the expander system without a
/// compile-time WPF dependency. Registered via SelectionRuleExpanderContext.
/// </summary>
public interface ISelectionRuleExpanderHandler
{
    void RegisterSupport(ISupportExpanders support);
    bool HasExpander(string uniqueIdentifier);
    bool HasExpander(string uniqueIdentifier, int number);
    object GetRegisteredElement(SelectRule selectionRule, int number = 1);
    void SetRegisteredElement(SelectRule selectionRule, string id, int number = 1);
    int GetExpandersCount();
    void FocusExpander(SelectRule rule, int number = 1);
    void RetrainSpellExpander(SelectRule rule, int number, int retrainLevel);
    void RemoveAllExpanders();
    bool RequiresSelection(SelectRule rule, int number = 1);
    int GetRetrainLevel(SelectRule rule, int number);
}
