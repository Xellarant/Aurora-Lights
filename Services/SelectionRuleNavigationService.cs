// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Services.SelectionRuleNavigationService
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core;
using Builder.Core.Events;
using Builder.Data.Rules;
using Builder.Presentation.Events.Character;
using Builder.Presentation.Events.Global;
using Builder.Presentation.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

#nullable disable
namespace Builder.Presentation.Services;

public class SelectionRuleNavigationService : 
  ObservableObject,
  ISubscriber<CharacterManagerSelectionRuleCreated>,
  ISubscriber<CharacterManagerSelectionRuleDeleted>,
  ISubscriber<CharacterManagerElementRegistered>,
  ISubscriber<CharacterManagerElementUnregistered>,
  ISubscriber<CharacterNameChangedEvent>,
  ISubscriber<ReprocessCharacterEvent>,
  ISubscriber<ListSelectionRuleRegisteredEvent>,
  ISubscriber<ListSelectionRuleUnregisteredEvent>
{
  private readonly IEventAggregator _eventAggregator;
  private bool _isEnabled;
  private bool _isNextAvailable;
  private bool _isPreviousAvailable;
  private int _selectionCount;
  private int _selectionsRemaining;
  private SelectRule _firstRequiredSelectionRule;

  public SelectionRuleNavigationService(IEventAggregator eventAggregator)
  {
    this._eventAggregator = eventAggregator;
    this._eventAggregator.Subscribe((object) this);
    this._isEnabled = true;
  }

  public ICommand NavigateNextCommand => (ICommand) new RelayCommand(new Action(this.NavigateNext));

  public bool IsEnabled
  {
    get => this._isEnabled;
    set
    {
      this.SetProperty<bool>(ref this._isEnabled, value, nameof (IsEnabled));
      if (!this._isEnabled)
        return;
      this.Eval();
    }
  }

  public bool IsNextAvailable
  {
    get => this._isNextAvailable;
    set => this.SetProperty<bool>(ref this._isNextAvailable, value, nameof (IsNextAvailable));
  }

  public bool IsPreviousAvailable
  {
    get => this._isPreviousAvailable;
    set
    {
      this.SetProperty<bool>(ref this._isPreviousAvailable, value, nameof (IsPreviousAvailable));
    }
  }

  public int SelectionCount
  {
    get => this._selectionCount;
    set => this.SetProperty<int>(ref this._selectionCount, value, nameof (SelectionCount));
  }

  public int SelectionsRemaining
  {
    get => this._selectionsRemaining;
    set
    {
      this.SetProperty<int>(ref this._selectionsRemaining, value, nameof (SelectionsRemaining));
    }
  }

  public SelectRule FirstRequiredSelectionRule
  {
    get => this._firstRequiredSelectionRule;
    set
    {
      this.SetProperty<SelectRule>(ref this._firstRequiredSelectionRule, value, nameof (FirstRequiredSelectionRule));
    }
  }

  private void NavigateNext()
  {
    if (!this.IsEnabled)
      return;
    List<IGrouping<string, SelectRule>> list = CharacterManager.Current.SelectionRules.ToList<SelectRule>().GroupBy<SelectRule, string>((Func<SelectRule, string>) (x => x.Attributes.Type)).OrderBy<IGrouping<string, SelectRule>, string>((Func<IGrouping<string, SelectRule>, string>) (x => x.Key)).ToList<IGrouping<string, SelectRule>>();
    bool flag = false;
    foreach (IEnumerable<SelectRule> selectRules in list.Where<IGrouping<string, SelectRule>>((Func<IGrouping<string, SelectRule>, bool>) (x => x.Key == "Race" || x.Key == "Sub Race" || x.Key == "Race Variant" || x.Key == "Racial Trait")))
    {
      foreach (SelectRule rule in selectRules)
      {
        flag = this.TryFocusRule(rule);
        if (flag)
          break;
      }
      if (flag)
        break;
    }
    if (!flag)
    {
      foreach (IEnumerable<SelectRule> selectRules in list.Where<IGrouping<string, SelectRule>>((Func<IGrouping<string, SelectRule>, bool>) (x => x.Key == "Class" || x.Key == "Class Feature")))
      {
        foreach (SelectRule rule in selectRules)
        {
          flag = this.TryFocusRule(rule);
          if (flag)
            break;
        }
        if (flag)
          break;
      }
    }
    if (!flag)
    {
      foreach (IEnumerable<SelectRule> selectRules in list.Where<IGrouping<string, SelectRule>>((Func<IGrouping<string, SelectRule>, bool>) (x => x.Key == "Archetype" || x.Key == "Archetype Feature")))
      {
        foreach (SelectRule rule in selectRules)
        {
          flag = this.TryFocusRule(rule);
          if (flag)
            break;
        }
        if (flag)
          break;
      }
    }
    if (!flag)
    {
      foreach (IEnumerable<SelectRule> selectRules in list.Where<IGrouping<string, SelectRule>>((Func<IGrouping<string, SelectRule>, bool>) (x => x.Key == "Multiclass")))
      {
        foreach (SelectRule rule in selectRules)
        {
          flag = this.TryFocusRule(rule);
          if (flag)
            break;
        }
        if (flag)
          break;
      }
    }
    if (!flag)
    {
      foreach (IEnumerable<SelectRule> selectRules in list.Where<IGrouping<string, SelectRule>>((Func<IGrouping<string, SelectRule>, bool>) (x => x.Key == "Background" || x.Key == "Background Feature" || x.Key == "Background Variant" || x.Key == "Background Characteristics")))
      {
        foreach (SelectRule rule in selectRules)
        {
          flag = this.TryFocusRule(rule);
          if (flag)
            break;
        }
        if (flag)
          break;
      }
    }
    if (!flag)
    {
      foreach (IEnumerable<SelectRule> selectRules in list.Where<IGrouping<string, SelectRule>>((Func<IGrouping<string, SelectRule>, bool>) (x => x.Key == "List")))
      {
        foreach (SelectRule rule in selectRules)
        {
          if (rule.ElementHeader.Type == "Background" || rule.ElementHeader.Type == "Background Characteristics")
            flag = this.TryFocusRule(rule);
          if (flag)
            break;
        }
        if (flag)
          break;
      }
    }
    if (!flag)
    {
      foreach (IEnumerable<SelectRule> selectRules in list.Where<IGrouping<string, SelectRule>>((Func<IGrouping<string, SelectRule>, bool>) (x => x.Key == "Ability Score Improvement")))
      {
        foreach (SelectRule rule in selectRules)
        {
          flag = this.TryFocusRule(rule);
          if (flag)
            break;
        }
        if (flag)
          break;
      }
    }
    if (!flag)
    {
      foreach (IEnumerable<SelectRule> selectRules in list.Where<IGrouping<string, SelectRule>>((Func<IGrouping<string, SelectRule>, bool>) (x => x.Key == "Language")))
      {
        foreach (SelectRule rule in selectRules)
        {
          flag = this.TryFocusRule(rule);
          if (flag)
            break;
        }
        if (flag)
          break;
      }
    }
    if (!flag)
    {
      foreach (IEnumerable<SelectRule> selectRules in list.Where<IGrouping<string, SelectRule>>((Func<IGrouping<string, SelectRule>, bool>) (x => x.Key == "Proficiency")))
      {
        foreach (SelectRule rule in selectRules)
        {
          flag = this.TryFocusRule(rule);
          if (flag)
            break;
        }
        if (flag)
          break;
      }
    }
    if (!flag)
    {
      foreach (IEnumerable<SelectRule> selectRules in list.Where<IGrouping<string, SelectRule>>((Func<IGrouping<string, SelectRule>, bool>) (x => x.Key == "Feat")))
      {
        foreach (SelectRule rule in selectRules)
        {
          flag = this.TryFocusRule(rule);
          if (flag)
            break;
        }
        if (flag)
          break;
      }
    }
    if (!flag)
    {
      foreach (IEnumerable<SelectRule> selectRules in list.Where<IGrouping<string, SelectRule>>((Func<IGrouping<string, SelectRule>, bool>) (x => x.Key == "Companion" || x.Key == "Companion Feature" || x.Key == "Companion Trait" || x.Key == "Companion Action")))
      {
        foreach (SelectRule rule in selectRules)
        {
          flag = this.TryFocusRule(rule);
          if (flag)
            break;
        }
        if (flag)
          break;
      }
    }
    if (!flag)
    {
      foreach (IEnumerable<SelectRule> selectRules in list.Where<IGrouping<string, SelectRule>>((Func<IGrouping<string, SelectRule>, bool>) (x => x.Key == "Spell")))
      {
        foreach (SelectRule rule in selectRules)
        {
          flag = this.TryFocusRule(rule);
          if (flag)
            break;
        }
        if (flag)
          break;
      }
    }
    if (flag)
      return;
    foreach (IEnumerable<SelectRule> selectRules in list)
    {
      foreach (SelectRule rule in selectRules)
      {
        flag = this.TryFocusRule(rule);
        if (flag)
          break;
      }
      if (flag)
        break;
    }
  }

  private bool TryFocusRule(SelectRule rule, int number = 1)
  {
    if (number > 1)
    {
      if (SelectionRuleExpanderHandler.Current.RequiresSelection(rule, number))
      {
        SelectionRuleExpanderHandler.Current.FocusExpander(rule, number);
        ApplicationManager.Current.SendStatusMessage($"The '{rule.Attributes.Name}' sectionselection option.");
        return true;
      }
    }
    else
    {
      for (int index = 0; index < rule.Attributes.Number; ++index)
      {
        if (SelectionRuleExpanderHandler.Current.RequiresSelection(rule, index + 1))
        {
          SelectionRuleExpanderHandler.Current.FocusExpander(rule, index + 1);
          ApplicationManager.Current.SendStatusMessage($"Choose one from the '{rule.Attributes.Name}' selection option.");
          return true;
        }
      }
    }
    return false;
  }

  private async void Eval()
  {
    SelectionRuleNavigationService navigationService = this;
    if (!navigationService.IsEnabled)
      return;
    navigationService.SelectionCount = 0;
    navigationService.SelectionsRemaining = 0;
    bool flag = false;
    foreach (SelectRule selectionRule in (Collection<SelectRule>) CharacterManager.Current.SelectionRules)
    {
      navigationService.SelectionCount += selectionRule.Attributes.Number;
      for (int index = 0; index < selectionRule.Attributes.Number; ++index)
      {
        if (SelectionRuleExpanderHandler.Current.RequiresSelection(selectionRule, index + 1))
        {
          if (!flag)
            navigationService.FirstRequiredSelectionRule = selectionRule;
          flag = true;
          navigationService.SelectionsRemaining++;
        }
      }
    }
    navigationService.IsNextAvailable = navigationService.SelectionsRemaining > 0;
    if (!flag)
      navigationService.FirstRequiredSelectionRule = (SelectRule) null;
    navigationService._eventAggregator.Send<NavigationServiceEvaluationEvent>(new NavigationServiceEvaluationEvent(navigationService.SelectionsRemaining, navigationService.SelectionCount, navigationService.FirstRequiredSelectionRule));
    navigationService.OnPropertyChanged("DisplayNext");
    if (!navigationService.IsNextAvailable || !Settings.Default.AutoNavigateNextSelectionWhenAvailable)
      return;
    await Task.Delay(250);
    navigationService.NavigateNext();
  }

  public void OnHandleEvent(CharacterManagerSelectionRuleCreated args) => this.Eval();

  public void OnHandleEvent(CharacterManagerSelectionRuleDeleted args) => this.Eval();

  public void OnHandleEvent(CharacterManagerElementRegistered args) => this.Eval();

  public void OnHandleEvent(CharacterManagerElementUnregistered args) => this.Eval();

  public string DisplayNext => !this.IsEnabled ? "N/A" : "SELECTION AVAILABLE";

  public string DisplayNextWithCounter
  {
    get
    {
      return !this.IsEnabled ? "N/A" : $"{this.SelectionCount - this.SelectionsRemaining}/{this.SelectionCount} SELECTION AVAILABLE";
    }
  }

  public void OnHandleEvent(CharacterNameChangedEvent args)
  {
  }

  public void OnHandleEvent(ListSelectionRuleRegisteredEvent args) => this.Eval();

  public void OnHandleEvent(ListSelectionRuleUnregisteredEvent args) => this.Eval();

  public void OnHandleEvent(ReprocessCharacterEvent args)
  {
  }

  public override string ToString() => this.DisplayNext;
}
