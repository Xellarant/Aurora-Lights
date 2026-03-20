// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.UserControls.Spellcasting.SpellcasterSelectionControlViewModel
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core;
using Builder.Core.Events;
using Builder.Core.Logging;
using Builder.Data;
using Builder.Data.Elements;
using Builder.Data.Extensions;
using Builder.Data.Rules;
using Builder.Presentation.Events.Character;
using Builder.Presentation.Events.Shell;
using Builder.Presentation.Services;
using Builder.Presentation.Services.Calculator;
using Builder.Presentation.Services.Data;
using Builder.Presentation.Services.Sources;
using Builder.Presentation.Telemetry;
using Builder.Presentation.ViewModels;
using Builder.Presentation.UserControls;
using Builder.Presentation.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

#nullable disable
namespace Builder.Presentation.UserControls.Spellcasting;

public sealed class SpellcasterSelectionControlViewModel : 
  ViewModelBase,
  ISubscriber<CharacterManagerElementRegistered>,
  ISubscriber<CharacterManagerElementUnregistered>
{
  private ExpressionInterpreter _interpreter = new ExpressionInterpreter();
  private ElementBaseCollection _spells;
  private bool _isPrepareSpellsRequired;
  private bool _includeSpellcastingOutput;
  private SelectionElement _selectedKnownSpell;
  private Spell _selectedPreparedSpell;
  private int _prepareCount;
  private int _maxSlot;
  private int _currentPreparedCount;

  public SpellcasterSelectionControlViewModel(SpellcastingInformation spellcastingInformation)
  {
    this.TogglePrepareSpellCommand = new RelayCommand(new Action(this.TogglePrepareSpell), new Func<bool>(this.CanTogglePrepareSpell));
    List<string> list = DataManager.Current.ElementsCollection.Where<ElementBase>((Func<ElementBase, bool>) (x => x.Type.Equals("Spell"))).Select<ElementBase, string>((Func<ElementBase, string>) (x => x.Source)).Distinct<string>().OrderBy<string, string>((Func<string, string>) (x => x)).ToList<string>();
    List<string> schoolCollection = new List<string>((IEnumerable<string>) new string[8]
    {
      "Abjuration",
      "Conjuration",
      "Divination",
      "Enchantment",
      "Evocation",
      "Illusion",
      "Necromancy",
      "Transmutation"
    });
    list.Insert(0, "--");
    schoolCollection.Insert(0, "--");
    SpellcastingFilter spellcastingFilter = new SpellcastingFilter((IEnumerable<string>) list, (IEnumerable<string>) schoolCollection);
    spellcastingFilter.IncludeCantrips = true;
    spellcastingFilter.Include1 = true;
    spellcastingFilter.Include2 = true;
    spellcastingFilter.Include3 = true;
    spellcastingFilter.Include4 = true;
    spellcastingFilter.Include5 = true;
    spellcastingFilter.Include6 = true;
    spellcastingFilter.Include7 = true;
    spellcastingFilter.Include8 = true;
    spellcastingFilter.Include9 = true;
    spellcastingFilter.IsSchoolFilterAvailable = false;
    spellcastingFilter.IsSourceFilterAvailable = true;
    this.SpellcastingFilter = spellcastingFilter;
    this.SpellcastingFilter.IsLocked = false;
    this.SpellcastingFilter.IsSchoolFilterAvailable = true;
    this.SpellcastingFilter.IsSourceFilterAvailable = true;
    this.SpellcastingFilter.School = "--";
    this.SpellcastingFilter.Source = "--";
    this.InformationHeader = new SpellcastingInformationHeader();
    this.Information = spellcastingInformation;
    this._isPrepareSpellsRequired = this.Information.Prepare;
    this._includeSpellcastingOutput = true;
    this._prepareCount = -1;
    this._maxSlot = -1;
    if (this.IsInDesignMode)
    {
      this.InformationHeader.Header = "Wizzy the Wizard";
      this.InformationHeader.SpellcastingAbilityName = "Intelligence";
      this.InformationHeader.SpellAttackModifier = 6;
      this.InformationHeader.SpellSaveDc = 14;
      this.InformationHeader.Slot1 = 4;
      this.InformationHeader.Slot2 = 2;
      this.InformationHeader.Slot3 = 0;
      this.IsPrepareSpellsRequired = true;
      this.InitializeDesignData();
    }
    else
    {
      this.PopulateHeader(this.Information);
      this._spells = new ElementBaseCollection(DataManager.Current.ElementsCollection.Where<ElementBase>((Func<ElementBase, bool>) (x => x.Type.Equals("Spell"))));
      this.PopulateSpellCollection();
      this.SubscribeWithEventAggregator();
      this.Manager.SourcesManager.SourceRestrictionsApplied += new EventHandler(this.SourcesManager_SourceRestrictionsApplied);
    }
  }

  private void SourcesManager_SourceRestrictionsApplied(object sender, EventArgs e)
  {
    if (!this.Manager.Status.IsLoaded)
      return;
    this.PopulateSpellCollection();
  }

  public CharacterManager Manager => CharacterManager.Current;

  public bool IsPrepareSpellsRequired
  {
    get => this._isPrepareSpellsRequired;
    set
    {
      this.SetProperty<bool>(ref this._isPrepareSpellsRequired, value, nameof (IsPrepareSpellsRequired));
    }
  }

  public bool IncludeSpellcastingOutput
  {
    get => this._includeSpellcastingOutput;
    set
    {
      this.SetProperty<bool>(ref this._includeSpellcastingOutput, value, nameof (IncludeSpellcastingOutput));
    }
  }

  public SpellcastingInformation Information { get; }

  public ObservableCollection<SpellcastingInformation> ExtendedInformationList { get; } = new ObservableCollection<SpellcastingInformation>();

  public SpellcastingInformationHeader InformationHeader { get; }

  public SpellcastingFilter SpellcastingFilter { get; }

  private void PopulateHeader(SpellcastingInformation info)
  {
    CharacterManager current = CharacterManager.Current;
    this.InformationHeader.Header = info.Name;
    this.InformationHeader.SpellcastingAbilityName = info.AbilityName;
    this.InformationHeader.SpellAttackModifier = current.StatisticsCalculator.StatisticValues.GetValue(info.GetSpellcasterSpellAttackStatisticName());
    this.InformationHeader.SpellSaveDc = current.StatisticsCalculator.StatisticValues.GetValue(info.GetSpellcasterSpellSaveStatisticName());
    this.InformationHeader.Slot1 = current.StatisticsCalculator.StatisticValues.GetValue(info.GetSlotStatisticName(1));
    this.InformationHeader.Slot2 = current.StatisticsCalculator.StatisticValues.GetValue(info.GetSlotStatisticName(2));
    this.InformationHeader.Slot3 = current.StatisticsCalculator.StatisticValues.GetValue(info.GetSlotStatisticName(3));
    this.InformationHeader.Slot4 = current.StatisticsCalculator.StatisticValues.GetValue(info.GetSlotStatisticName(4));
    this.InformationHeader.Slot5 = current.StatisticsCalculator.StatisticValues.GetValue(info.GetSlotStatisticName(5));
    this.InformationHeader.Slot6 = current.StatisticsCalculator.StatisticValues.GetValue(info.GetSlotStatisticName(6));
    this.InformationHeader.Slot7 = current.StatisticsCalculator.StatisticValues.GetValue(info.GetSlotStatisticName(7));
    this.InformationHeader.Slot8 = current.StatisticsCalculator.StatisticValues.GetValue(info.GetSlotStatisticName(8));
    this.InformationHeader.Slot9 = current.StatisticsCalculator.StatisticValues.GetValue(info.GetSlotStatisticName(9));
  }

  public ObservableCollection<Spell> ListedSpells { get; set; } = new ObservableCollection<Spell>();

  public ObservableCollection<Spell> ExtendedSpells { get; set; } = new ObservableCollection<Spell>();

  public ObservableCollection<SelectionElement> KnownSpells { get; set; } = new ObservableCollection<SelectionElement>();

  public ObservableCollection<Spell> PreparedSpells { get; set; } = new ObservableCollection<Spell>();

  public SelectionElement SelectedKnownSpell
  {
    get => this._selectedKnownSpell;
    set
    {
      this.SetProperty<SelectionElement>(ref this._selectedKnownSpell, value, nameof (SelectedKnownSpell));
      if (this._selectedKnownSpell != null)
        this.EventAggregator.Send<SpellcastingElementDescriptionDisplayRequestEvent>(new SpellcastingElementDescriptionDisplayRequestEvent(this._selectedKnownSpell.Element));
      this.TogglePrepareSpellCommand.RaiseCanExecuteChanged();
    }
  }

  public Spell SelectedPreparedSpell
  {
    get => this._selectedPreparedSpell;
    set
    {
      this.SetProperty<Spell>(ref this._selectedPreparedSpell, value, nameof (SelectedPreparedSpell));
      if (this._selectedPreparedSpell == null)
        return;
      this.EventAggregator.Send<SpellcastingElementDescriptionDisplayRequestEvent>(new SpellcastingElementDescriptionDisplayRequestEvent((ElementBase) this._selectedPreparedSpell));
    }
  }

  public int PrepareCount
  {
    get => this._prepareCount;
    set => this.SetProperty<int>(ref this._prepareCount, value, nameof (PrepareCount));
  }

  public int MaxSlot
  {
    get => this._maxSlot;
    set => this.SetProperty<int>(ref this._maxSlot, value, nameof (MaxSlot));
  }

  private void UpdateStatistics()
  {
    StatisticValuesGroupCollection statisticValues = CharacterManager.Current.StatisticsCalculator.StatisticValues;
    this.PrepareCount = statisticValues.GetValue(this.Information.GetPrepareAmountStatisticName());
    for (int slot = 9; slot >= 0; --slot)
    {
      if (statisticValues.ContainsGroup(this.Information.GetSlotStatisticName(slot)))
      {
        this.MaxSlot = slot;
        break;
      }
    }
    this.InformationHeader.SpellAttackModifier = statisticValues.GetValue(this.Information.GetSpellcasterSpellAttackStatisticName());
    this.InformationHeader.SpellSaveDc = statisticValues.GetValue(this.Information.GetSpellcasterSpellSaveStatisticName());
    this.InformationHeader.Slot1 = statisticValues.GetValue(this.Information.GetSlotStatisticName(1));
    this.InformationHeader.Slot2 = statisticValues.GetValue(this.Information.GetSlotStatisticName(2));
    this.InformationHeader.Slot3 = statisticValues.GetValue(this.Information.GetSlotStatisticName(3));
    this.InformationHeader.Slot4 = statisticValues.GetValue(this.Information.GetSlotStatisticName(4));
    this.InformationHeader.Slot5 = statisticValues.GetValue(this.Information.GetSlotStatisticName(5));
    this.InformationHeader.Slot6 = statisticValues.GetValue(this.Information.GetSlotStatisticName(6));
    this.InformationHeader.Slot7 = statisticValues.GetValue(this.Information.GetSlotStatisticName(7));
    this.InformationHeader.Slot8 = statisticValues.GetValue(this.Information.GetSlotStatisticName(8));
    this.InformationHeader.Slot9 = statisticValues.GetValue(this.Information.GetSlotStatisticName(9));
  }

  private void PopulateSpellCollection()
  {
    this.UpdateStatistics();
    IEnumerable<Spell> source1 = !string.IsNullOrWhiteSpace(this.Information.InitialSupportedSpellsExpression?.Supports) ? this._interpreter.EvaluateSupportsExpression<ElementBase>(this.Information.InitialSupportedSpellsExpression.Supports, (IEnumerable<ElementBase>) this._spells).Cast<Spell>().Where<Spell>((Func<Spell, bool>) (x => x.Level != 0)) : (IEnumerable<Spell>) new List<Spell>();
    this.ExtendedSpells.Clear();
    this.ListedSpells.Clear();
    foreach (Spell spell in (IEnumerable<Spell>) source1.OrderBy<Spell, string>((Func<Spell, string>) (x => x.Name)))
    {
      if (spell.Level <= this.MaxSlot)
        this.ListedSpells.Add(spell);
    }
    List<Spell> source2 = new List<Spell>();
    if (this.Information.InitialSupportedSpellsExpression == null && Debugger.IsAttached)
      Debugger.Break();
    if (this.Information.InitialSupportedSpellsExpression != null && this.Information.InitialSupportedSpellsExpression.Known)
    {
      foreach (Spell listedSpell in (Collection<Spell>) this.ListedSpells)
      {
        if (!source2.Contains(listedSpell))
          source2.Add(listedSpell);
      }
    }
    if (this.Information.PrepareFromSpellList)
    {
      foreach (SpellcastingInformation.SpellcastingList spellsExpression in this.Information.ExtendedSupportedSpellsExpressions)
      {
        SpellcastingInformation.SpellcastingList expression = spellsExpression;
        if (expression.IsId)
        {
          if (this._spells.FirstOrDefault<ElementBase>((Func<ElementBase, bool>) (x => x.Id.Equals(expression.Supports))) is Spell spell && spell.Level != 0 && spell.Level <= this.MaxSlot)
            this.ExtendedSpells.Add(spell);
        }
        else if (Debugger.IsAttached)
          Debugger.Break();
      }
      int num = Debugger.IsAttached ? 1 : 0;
      try
      {
        SpellcastingInformation info = this.Manager.GetSpellcastingInformations().FirstOrDefault<SpellcastingInformation>((Func<SpellcastingInformation, bool>) (x => x.UniqueIdentifier.Equals(this.Information.UniqueIdentifier)));
        ClassProgressionManager progressionManager = this.Manager.ClassProgressionManagers.FirstOrDefault<ClassProgressionManager>((Func<ClassProgressionManager, bool>) (x => x.SpellcastingInformations.Any<SpellcastingInformation>((Func<SpellcastingInformation, bool>) (y => y.UniqueIdentifier.Equals(this.Information.UniqueIdentifier)))));
        IEnumerable<SpellcastingInformation> spellcastingInformations = progressionManager != null ? progressionManager.SpellcastingInformations.Where<SpellcastingInformation>((Func<SpellcastingInformation, bool>) (x => x.IsExtension && x.Name.Equals(info?.Name))) : (IEnumerable<SpellcastingInformation>) null;
        if (spellcastingInformations != null)
        {
          foreach (SpellcastingInformation spellcastingInformation in spellcastingInformations)
          {
            foreach (SpellcastingInformation.SpellcastingList spellcastingList in spellcastingInformation.ExtendedSupportedSpellsExpressions.Where<SpellcastingInformation.SpellcastingList>((Func<SpellcastingInformation.SpellcastingList, bool>) (e => e.Known)))
            {
              foreach (Spell spell in this._interpreter.EvaluateSupportsExpression<ElementBase>(spellcastingList.Supports, (IEnumerable<ElementBase>) this._spells).Cast<Spell>().Where<Spell>((Func<Spell, bool>) (x => x.Level != 0)))
              {
                if (spell.Level <= this.MaxSlot)
                  this.ExtendedSpells.Add(spell);
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        Logger.Exception(ex, nameof (PopulateSpellCollection));
        AnalyticsErrorHelper.Exception(ex, method: nameof (PopulateSpellCollection), line: 540);
      }
      foreach (Spell extendedSpell in (Collection<Spell>) this.ExtendedSpells)
      {
        if (!source2.Contains(extendedSpell))
          source2.Add(extendedSpell);
      }
    }
    List<Spell> list1 = CharacterManager.Current.GetElements().Where<ElementBase>((Func<ElementBase, bool>) (x => x.Type.Equals("Spell"))).Cast<Spell>().OrderBy<Spell, int>((Func<Spell, int>) (x => x.Level)).ThenBy<Spell, string>((Func<Spell, string>) (x => x.Name)).ToList<Spell>();
    List<Spell> list2 = list1.Where<Spell>((Func<Spell, bool>) (x => x.Aquisition.WasGranted && x.Aquisition.GrantRule.Setters.ContainsSetter("spellcasting") && x.Aquisition.GrantRule.Setters.GetSetter("spellcasting").Value.Equals(this.Information.Name, StringComparison.OrdinalIgnoreCase))).ToList<Spell>();
    List<Spell> list3 = list1.Where<Spell>((Func<Spell, bool>) (x => x.Aquisition.WasSelected && x.Aquisition.SelectRule.Attributes.ContainsSpellcastingName() && x.Aquisition.SelectRule.Attributes.SpellcastingName.Equals(this.Information.Name, StringComparison.OrdinalIgnoreCase))).ToList<Spell>();
    List<SelectionElement> selectionElementList1 = new List<SelectionElement>();
    foreach (Spell element in list2)
      selectionElementList1.Add(new SelectionElement((ElementBase) element)
      {
        IsChosen = element.Aquisition.GrantRule.IsAlwaysPrepared()
      });
    foreach (Spell element in list3)
      selectionElementList1.Add(new SelectionElement((ElementBase) element)
      {
        IsChosen = element.Aquisition.SelectRule.IsAlwaysPrepared()
      });
    foreach (Spell element in (IEnumerable<Spell>) source2.OrderBy<Spell, string>((Func<Spell, string>) (x => x.Name)))
    {
      if (!list2.Contains(element) && !list3.Contains(element))
      {
        SelectionElement selectionElement = new SelectionElement((ElementBase) element);
        selectionElementList1.Add(selectionElement);
      }
    }
    int num1 = this.IsPrepareSpellsRequired ? 1 : 0;
    SourcesManager sourcesManager = CharacterManager.Current.SourcesManager;
    List<string> list4 = sourcesManager.GetUndefinedRestrictedSourceNames().ToList<string>();
    List<string> list5 = sourcesManager.GetRestrictedElementIds().ToList<string>();
    List<SelectionElement> selectionElementList2 = new List<SelectionElement>();
    foreach (SelectionElement selectionElement in selectionElementList1)
    {
      if (list5.Contains(selectionElement.Element.Id))
        selectionElementList2.Add(selectionElement);
      else if (list4.Contains(selectionElement.Element.Source))
        selectionElementList2.Add(selectionElement);
    }
    foreach (SelectionElement selectionElement in selectionElementList2)
    {
      if (!selectionElement.IsDefault)
      {
        if (selectionElement.IsChosen)
          ApplicationContext.Current.SendStatusMessage($"'{selectionElement.Element.Name}' is prepared but is restricted by source. Prepare another spell.");
        else
          selectionElementList1.Remove(selectionElement);
      }
    }
    this.KnownSpells.Clear();
    foreach (SelectionElement selectionElement in selectionElementList1)
    {
      if (((IEnumerable<ElementBase>) this.PreparedSpells).Contains<ElementBase>(selectionElement.Element))
        selectionElement.IsChosen = true;
      this.KnownSpells.Add(selectionElement);
    }
    foreach (SpellcastingInformation extendedInformation in (Collection<SpellcastingInformation>) this.ExtendedInformationList)
    {
      if (Debugger.IsAttached)
        Debugger.Break();
    }
  }

  protected override void InitializeDesignData()
  {
  }

  public void OnHandleEvent(CharacterManagerElementRegistered args)
  {
    this.PopulateSpellCollection();
  }

  public void OnHandleEvent(CharacterManagerElementUnregistered args)
  {
    this.PopulateSpellCollection();
  }

  public ICommand PopulateSpellCollectionCommand
  {
    get => (ICommand) new RelayCommand(new Action(this.PopulateSpellCollection));
  }

  public ICommand PrepareCommand => (ICommand) new RelayCommand(new Action(this.Prepare));

  public ICommand UnprepareCommand => (ICommand) new RelayCommand(new Action(this.Unprepare));

  public RelayCommand TogglePrepareSpellCommand { get; set; }

  private void TogglePrepareSpell()
  {
    if (this.SelectedKnownSpell == null)
      return;
    if (this.SelectedKnownSpell.Element.AsElement<Spell>().Level == 0)
    {
      this.SelectedKnownSpell.IsChosen = false;
      MessageDialogContext.Current?.Show(this.SelectedKnownSpell.DisplayName + " (cantrip) is always ready to cast and doesn't need to be prepared.");
    }
    else
    {
      if (this.SelectedKnownSpell.IsChosen)
      {
        if (this.SelectedKnownSpell.Element.Aquisition.WasGranted && this.SelectedKnownSpell.Element.Aquisition.GrantRule.IsAlwaysPrepared())
        {
          MessageDialogContext.Current?.Show(this.SelectedKnownSpell.DisplayName + " is always prepared for you.");
          return;
        }
        if (((IEnumerable<ElementBase>) this.PreparedSpells).Contains<ElementBase>(this.SelectedKnownSpell.Element))
        {
          this.PreparedSpells.Remove(this.SelectedKnownSpell.Element.AsElement<Spell>());
          this.SelectedKnownSpell.IsChosen = false;
          this.EventAggregator.Send<MainWindowStatusUpdateEvent>(new MainWindowStatusUpdateEvent($"{this.Information.Name}: {this.SelectedKnownSpell.DisplayName} is unprepared."));
        }
        else if (Debugger.IsAttached)
          Debugger.Break();
      }
      else if (((IEnumerable<ElementBase>) this.PreparedSpells).Contains<ElementBase>(this.SelectedKnownSpell.Element))
      {
        this.SelectedKnownSpell.IsChosen = true;
      }
      else
      {
        this.PreparedSpells.Add(this.SelectedKnownSpell.Element.AsElement<Spell>());
        this.SelectedKnownSpell.IsChosen = true;
        this.EventAggregator.Send<MainWindowStatusUpdateEvent>(new MainWindowStatusUpdateEvent($"{this.Information.Name}: {this.SelectedKnownSpell.DisplayName} is prepared."));
      }
      int num = 0;
      foreach (Spell preparedSpell in (Collection<Spell>) this.PreparedSpells)
      {
        if (!preparedSpell.Aquisition.WasGranted || !preparedSpell.Aquisition.GrantRule.IsAlwaysPrepared())
          ++num;
      }
      this.CurrentPreparedCount = num;
    }
  }

  private bool CanTogglePrepareSpell()
  {
    return this.SelectedKnownSpell != null && this.SelectedKnownSpell.Element.AsElement<Spell>().Level > 0;
  }

  private void Prepare()
  {
    if (this.SelectedKnownSpell == null)
      return;
    if (this.SelectedKnownSpell.Element.Aquisition.WasGranted && this.SelectedKnownSpell.Element.Aquisition.GrantRule.Setters.GetSetter("prepared") != null)
    {
      MessageDialogContext.Current?.Show("This spell is always prepared and was granted by " + this.SelectedKnownSpell.Element.Aquisition.GrantRule.ElementHeader.Name);
    }
    else
    {
      if (((IEnumerable<ElementBase>) this.PreparedSpells).Contains<ElementBase>(this.SelectedKnownSpell.Element))
      {
        this.PreparedSpells.Remove(this.SelectedPreparedSpell);
      }
      else
      {
        string name = this.SelectedKnownSpell.Element.Name;
        this.PreparedSpells.Add(this.SelectedKnownSpell.Element.AsElement<Spell>());
        this.SelectedKnownSpell.IsChosen = true;
        this.CurrentPreparedCount = this.PreparedSpells.Count;
        this.EventAggregator.Send<MainWindowStatusUpdateEvent>(new MainWindowStatusUpdateEvent($"{this.Information.Name}: {name} added to your prepared spell collection."));
      }
      this.CurrentPreparedCount = this.PreparedSpells.Count<Spell>((Func<Spell, bool>) (x => !x.Aquisition.WasGranted));
    }
  }

  private void Unprepare()
  {
    if (this.SelectedKnownSpell != null)
      this.SelectedKnownSpell.IsChosen = false;
    if (this.SelectedPreparedSpell != null)
    {
      string name = this.SelectedPreparedSpell.Name;
      this.PreparedSpells.Remove(this.SelectedPreparedSpell);
      this.EventAggregator.Send<MainWindowStatusUpdateEvent>(new MainWindowStatusUpdateEvent($"{this.Information.Name}: {name} removed from your prepared spell collection."));
    }
    this.CurrentPreparedCount = this.PreparedSpells.Count<Spell>((Func<Spell, bool>) (x => !x.Aquisition.WasGranted));
  }

  public int CurrentPreparedCount
  {
    get => this._currentPreparedCount;
    set
    {
      this.SetProperty<int>(ref this._currentPreparedCount, value, nameof (CurrentPreparedCount));
    }
  }

  public SelectionNotification FilterNotification { get; } = new SelectionNotification("Filter", "Notification Message");

  private int _filteredElementsCount;
  public int FilteredElementsCount
  {
    get => this._filteredElementsCount;
    set => this.SetProperty<int>(ref this._filteredElementsCount, value, nameof (FilteredElementsCount));
  }

  private int _selectionElementsCount;
  public int SelectionElementsCount
  {
    get => this._selectionElementsCount;
    set => this.SetProperty<int>(ref this._selectionElementsCount, value, nameof (SelectionElementsCount));
  }
}
