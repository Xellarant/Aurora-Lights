// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.Shell.Manage.SpellContentViewModel
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Data;
using Builder.Data.Elements;
using Builder.Presentation.Services.Data;
using Builder.Presentation.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace Builder.Presentation.ViewModels.Shell.Manage;

public class SpellContentViewModel : ViewModelBase
{
  private List<Spell> _spells;
  private string _spellcastingClass;
  private string _spellcastingAbility;
  private string _spellcastingAttackModifier;
  private string _spellcastingDifficultyClass;

  public SpellContentViewModel()
  {
    if (!DataManager.Current.IsElementsCollectionPopulated)
      return;
    this.Populate();
  }

  private void SpellcastingCollection_PropertyChanged(object sender, PropertyChangedEventArgs e)
  {
    if (!(e.PropertyName == "SpellcastingClass"))
      return;
    this.Populate();
  }

  public string SpellcastingClass
  {
    get => this._spellcastingClass;
    set => this.SetProperty<string>(ref this._spellcastingClass, value, nameof (SpellcastingClass));
  }

  public string SpellcastingAbility
  {
    get => this._spellcastingAbility;
    set
    {
      this.SetProperty<string>(ref this._spellcastingAbility, value, nameof (SpellcastingAbility));
    }
  }

  public string SpellcastingAttackModifier
  {
    get => this._spellcastingAttackModifier;
    set
    {
      this.SetProperty<string>(ref this._spellcastingAttackModifier, value, nameof (SpellcastingAttackModifier));
    }
  }

  public string SpellcastingDifficultyClass
  {
    get => this._spellcastingDifficultyClass;
    set
    {
      this.SetProperty<string>(ref this._spellcastingDifficultyClass, value, nameof (SpellcastingDifficultyClass));
    }
  }

  public SpellcastingCollection SpellcastingCollection
  {
    get => CharacterManager.Current.Character.SpellcastingCollection;
  }

  public ObservableCollection<string> Cantrips { get; } = new ObservableCollection<string>();

  public ObservableCollection<string> Spells1 { get; } = new ObservableCollection<string>();

  public ObservableCollection<string> Spells2 { get; } = new ObservableCollection<string>();

  public ObservableCollection<string> Spells3 { get; } = new ObservableCollection<string>();

  public ObservableCollection<string> Spells4 { get; } = new ObservableCollection<string>();

  public ObservableCollection<string> Spells5 { get; } = new ObservableCollection<string>();

  public ObservableCollection<string> Spells6 { get; } = new ObservableCollection<string>();

  public ObservableCollection<string> Spells7 { get; } = new ObservableCollection<string>();

  public ObservableCollection<string> Spells8 { get; } = new ObservableCollection<string>();

  public ObservableCollection<string> Spells9 { get; } = new ObservableCollection<string>();

  private void Populate()
  {
    this._spells = DataManager.Current.ElementsCollection.Where<ElementBase>((Func<ElementBase, bool>) (x => x.Type == "Spell")).Cast<Spell>().ToList<Spell>();
    if (!string.IsNullOrWhiteSpace(this.SpellcastingCollection.SpellcastingClass))
      this._spells = this._spells.Where<Spell>((Func<Spell, bool>) (x => x.Supports.Contains(this.SpellcastingCollection.SpellcastingClass))).ToList<Spell>();
    this.Cantrips.Clear();
    this.Spells1.Clear();
    this.Spells2.Clear();
    this.Spells3.Clear();
    this.Spells4.Clear();
    this.Spells5.Clear();
    this.Spells6.Clear();
    this.Spells7.Clear();
    this.Spells8.Clear();
    this.Spells9.Clear();
    this.Cantrips.Add(string.Empty);
    this.Spells1.Add(string.Empty);
    this.Spells2.Add(string.Empty);
    this.Spells3.Add(string.Empty);
    this.Spells4.Add(string.Empty);
    this.Spells5.Add(string.Empty);
    this.Spells6.Add(string.Empty);
    this.Spells7.Add(string.Empty);
    this.Spells8.Add(string.Empty);
    this.Spells9.Add(string.Empty);
    foreach (ElementBase elementBase in this._spells.Where<Spell>((Func<Spell, bool>) (x => x.Level == 0)))
      this.Cantrips.Add(elementBase.Name);
    foreach (ElementBase elementBase in this._spells.Where<Spell>((Func<Spell, bool>) (x => x.Level == 1)))
      this.Spells1.Add(elementBase.Name);
    foreach (ElementBase elementBase in this._spells.Where<Spell>((Func<Spell, bool>) (x => x.Level == 2)))
      this.Spells2.Add(elementBase.Name);
    foreach (ElementBase elementBase in this._spells.Where<Spell>((Func<Spell, bool>) (x => x.Level == 3)))
      this.Spells3.Add(elementBase.Name);
    foreach (ElementBase elementBase in this._spells.Where<Spell>((Func<Spell, bool>) (x => x.Level == 4)))
      this.Spells4.Add(elementBase.Name);
    foreach (ElementBase elementBase in this._spells.Where<Spell>((Func<Spell, bool>) (x => x.Level == 5)))
      this.Spells5.Add(elementBase.Name);
    foreach (ElementBase elementBase in this._spells.Where<Spell>((Func<Spell, bool>) (x => x.Level == 6)))
      this.Spells6.Add(elementBase.Name);
    foreach (ElementBase elementBase in this._spells.Where<Spell>((Func<Spell, bool>) (x => x.Level == 7)))
      this.Spells7.Add(elementBase.Name);
    foreach (ElementBase elementBase in this._spells.Where<Spell>((Func<Spell, bool>) (x => x.Level == 8)))
      this.Spells8.Add(elementBase.Name);
    foreach (ElementBase elementBase in this._spells.Where<Spell>((Func<Spell, bool>) (x => x.Level == 9)))
      this.Spells9.Add(elementBase.Name);
  }
}
