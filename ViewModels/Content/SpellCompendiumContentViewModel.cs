// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.Content.SpellCompendiumContentViewModel
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core;
using Builder.Core.Events;
using Builder.Core.Logging;
using Builder.Data;
using Builder.Data.Elements;
using Builder.Presentation.Services.Data;
using Builder.Presentation.ViewModels.Base;
using Builder.Presentation.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

#nullable disable
namespace Builder.Presentation.ViewModels.Content;

public class SpellCompendiumContentViewModel : 
  ViewModelBase,
  ISubscriber<ElementsCollectionPopulatedEvent>,
  ISubscriber<QuickSearchBarEventArgs>
{
  private Spell _selectedSpell;
  private string _filterName;
  private string _selectedLevel;
  private string _selectedSchool;
  private string _selectedClass;
  private string _selectedSource;
  private bool _supressFilter;

  public SpellCompendiumContentViewModel()
  {
    if (this.IsInDesignMode)
      return;
    this.SpellElements = new ObservableCollection<Spell>();
    this.FilteredSpellElements = new ObservableCollection<Spell>();
    this.Levels = new ObservableCollection<string>();
    this.Schools = new ObservableCollection<string>();
    this.Classes = new ObservableCollection<string>();
    this.Sources = new ObservableCollection<string>();
    this.EventAggregator.Subscribe((object) this);
    this.Populate();
  }

  public ObservableCollection<Spell> SpellElements { get; }

  public ObservableCollection<Spell> FilteredSpellElements { get; set; }

  public Spell SelectedSpell
  {
    get => this._selectedSpell;
    set
    {
      this.SetProperty<Spell>(ref this._selectedSpell, value, nameof (SelectedSpell));
      if (this._selectedSpell == null)
        return;
      this.EventAggregator.Send<SpellcastingElementDescriptionDisplayRequestEvent>(new SpellcastingElementDescriptionDisplayRequestEvent((ElementBase) this._selectedSpell));
    }
  }

  public ObservableCollection<string> Levels { get; set; }

  public ObservableCollection<string> Schools { get; set; }

  public ObservableCollection<string> Classes { get; set; }

  public ObservableCollection<string> Sources { get; set; }

  public string SelectedLevel
  {
    get => this._selectedLevel;
    set
    {
      this.SetProperty<string>(ref this._selectedLevel, value, nameof (SelectedLevel));
      this.Filter();
    }
  }

  public string SelectedSchool
  {
    get => this._selectedSchool;
    set
    {
      this.SetProperty<string>(ref this._selectedSchool, value, nameof (SelectedSchool));
      this.Filter();
    }
  }

  public string SelectedClass
  {
    get => this._selectedClass;
    set
    {
      this.SetProperty<string>(ref this._selectedClass, value, nameof (SelectedClass));
      this.Filter();
    }
  }

  public string FilterName
  {
    get => this._filterName;
    set
    {
      this.SetProperty<string>(ref this._filterName, value, nameof (FilterName));
      this.Filter();
    }
  }

  public string SelectedSource
  {
    get => this._selectedSource;
    set
    {
      this.SetProperty<string>(ref this._selectedSource, value, nameof (SelectedSource));
      this.Filter();
    }
  }

  public bool SupressFilter
  {
    get => this._supressFilter;
    set => this.SetProperty<bool>(ref this._supressFilter, value, nameof (SupressFilter));
  }

  public ICommand FilterCommand => (ICommand) new RelayCommand(new Action(this.Filter));

  public ICommand ResetCommand => (ICommand) new RelayCommand(new Action(this.Reset));

  private void Reset()
  {
    this.SupressFilter = true;
    this.SelectedLevel = "--";
    this.SelectedSchool = "--";
    this.SelectedClass = "--";
    this.SelectedSource = "--";
    this.SupressFilter = false;
    this.FilterName = "";
  }

  private void Filter()
  {
    if (this.SupressFilter)
      return;
    this.FilteredSpellElements.Clear();
    foreach (Spell spellElement in (Collection<Spell>) this.SpellElements)
      this.FilteredSpellElements.Add(spellElement);
    if (!string.IsNullOrWhiteSpace(this.SelectedLevel) && this.SelectedLevel != "--")
    {
      List<Spell> list = this.FilteredSpellElements.Where<Spell>((Func<Spell, bool>) (x => x.Level.ToString() == this.SelectedLevel)).ToList<Spell>();
      this.FilteredSpellElements.Clear();
      foreach (Spell spell in list)
        this.FilteredSpellElements.Add(spell);
    }
    if (!string.IsNullOrWhiteSpace(this.SelectedSchool) && this.SelectedSchool != "--")
    {
      List<Spell> list = this.FilteredSpellElements.Where<Spell>((Func<Spell, bool>) (x => x.MagicSchool == this.SelectedSchool)).ToList<Spell>();
      this.FilteredSpellElements.Clear();
      foreach (Spell spell in list)
        this.FilteredSpellElements.Add(spell);
    }
    if (!string.IsNullOrWhiteSpace(this.SelectedClass) && this.SelectedClass != "--")
    {
      List<Spell> list = this.FilteredSpellElements.Where<Spell>((Func<Spell, bool>) (x => x.Supports.Contains(this.SelectedClass))).ToList<Spell>();
      this.FilteredSpellElements.Clear();
      foreach (Spell spell in list)
        this.FilteredSpellElements.Add(spell);
    }
    if (!string.IsNullOrWhiteSpace(this.SelectedSource) && this.SelectedSource != "--")
    {
      List<Spell> list = this.FilteredSpellElements.Where<Spell>((Func<Spell, bool>) (x => x.Source.Equals(this.SelectedSource))).ToList<Spell>();
      this.FilteredSpellElements.Clear();
      foreach (Spell spell in list)
        this.FilteredSpellElements.Add(spell);
    }
    if (string.IsNullOrWhiteSpace(this.FilterName))
      return;
    List<Spell> list1 = this.FilteredSpellElements.Where<Spell>((Func<Spell, bool>) (x => x.Name.ToLower().Contains(this.FilterName.ToLower().Trim()))).ToList<Spell>();
    this.FilteredSpellElements.Clear();
    foreach (Spell spell in list1)
      this.FilteredSpellElements.Add(spell);
  }

  private void Populate()
  {
    foreach (Spell spell in DataManager.Current.ElementsCollection.Where<ElementBase>((Func<ElementBase, bool>) (x => x.Type == "Spell")).OrderBy<ElementBase, string>((Func<ElementBase, string>) (x => x.Name)).Cast<Spell>())
      this.SpellElements.Add(spell);
    this.Levels.Add("--");
    for (int index = 0; index < 10; ++index)
      this.Levels.Add(index.ToString());
    this.Schools.Add("--");
    foreach (string str in (IEnumerable<string>) this.SpellElements.GroupBy<Spell, string>((Func<Spell, string>) (x => x.MagicSchool)).Select<IGrouping<string, Spell>, string>((Func<IGrouping<string, Spell>, string>) (x => x.Key)).OrderBy<string, string>((Func<string, string>) (x => x)))
      this.Schools.Add(str);
    this.Classes.Add("--");
    foreach (string str in (IEnumerable<string>) DataManager.Current.ElementsCollection.Where<ElementBase>((Func<ElementBase, bool>) (x => x.Type == "Class")).Select<ElementBase, string>((Func<ElementBase, string>) (x => x.Name)).OrderBy<string, string>((Func<string, string>) (x => x)))
      this.Classes.Add(str);
    string[] strArray = new string[8]
    {
      "Wizard",
      "Warlock",
      "Bard",
      "Ranger",
      "Monk",
      "Paladin",
      "Sorcerer",
      "Druid"
    };
    foreach (string str in strArray)
    {
      if (!this.Classes.Contains(str))
      {
        this.Classes.Add(str);
        Logger.Debug($"adding {str} to spell filter list");
      }
    }
    this.Sources.Add("--");
    foreach (string str in (IEnumerable<string>) this.SpellElements.GroupBy<Spell, string>((Func<Spell, string>) (x => x.Source)).Select<IGrouping<string, Spell>, string>((Func<IGrouping<string, Spell>, string>) (x => x.Key)).OrderBy<string, string>((Func<string, string>) (x => x)))
      this.Sources.Add(str);
    this.Reset();
  }

  public void OnHandleEvent(ElementsCollectionPopulatedEvent args) => this.Populate();

  public void OnHandleEvent(QuickSearchBarEventArgs args)
  {
    if (!args.IsSearch)
      return;
    this.SupressFilter = true;
    this.Reset();
    this.FilterName = args.SearchCriteria;
    this.SupressFilter = false;
    this.Filter();
    this.SelectedSpell = this.FilteredSpellElements.FirstOrDefault<Spell>((Func<Spell, bool>) (x => x.Name.ToLower().Equals(args.SearchCriteria))) ?? this.FilteredSpellElements.FirstOrDefault<Spell>();
  }
}
