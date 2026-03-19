// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.Helpers.AttackSectionItem
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core;
using Builder.Core.Logging;
using Builder.Data;
using Builder.Data.Elements;
using Builder.Presentation.Extensions;
using Builder.Presentation.Models.Collections;
using Builder.Presentation.Models.Equipment;
using Builder.Presentation.Models.NewFolder1;
using Builder.Presentation.Services;
using Builder.Presentation.Services.Calculator;
using Builder.Presentation.ViewModels.Shell.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

#nullable disable
namespace Builder.Presentation.Models.Helpers;

public class AttackSectionItem : ObservableObject
{
  private readonly Spell _spell;
  private readonly SpellcastingInformation _information;
  private readonly RefactoredEquipmentItem _equipment;
  private bool _isAutomaticAddition;
  private bool _isDisplayed;
  private bool _isDisplayedAsCard;
  private string _displayCalculatedAttack;
  private string _displayCalculatedDamage;
  private AbilityItem _linkedAbility;

  public bool IsAutomaticAddition
  {
    get => this._isAutomaticAddition;
    set
    {
      this.SetProperty<bool>(ref this._isAutomaticAddition, value, nameof (IsAutomaticAddition));
    }
  }

  public bool IsDisplayed
  {
    get => this._isDisplayed;
    set => this.SetProperty<bool>(ref this._isDisplayed, value, nameof (IsDisplayed));
  }

  public bool IsDisplayedAsCard
  {
    get => this._isDisplayedAsCard;
    set => this.SetProperty<bool>(ref this._isDisplayedAsCard, value, nameof (IsDisplayedAsCard));
  }

  public string DisplayCalculatedAttack
  {
    get => this._displayCalculatedAttack;
    set
    {
      this.SetProperty<string>(ref this._displayCalculatedAttack, value, nameof (DisplayCalculatedAttack));
    }
  }

  public string DisplayCalculatedDamage
  {
    get => this._displayCalculatedDamage;
    set
    {
      this.SetProperty<string>(ref this._displayCalculatedDamage, value, nameof (DisplayCalculatedDamage));
    }
  }

  public ObservableCollection<AbilityItem> Abilities { get; } = new ObservableCollection<AbilityItem>();

  public AbilityItem LinkedAbility
  {
    get => this._linkedAbility;
    set
    {
      this.SetProperty<AbilityItem>(ref this._linkedAbility, value, nameof (LinkedAbility));
      if (this._linkedAbility == null)
        return;
      this.UpdateCalculations();
    }
  }

  public FillableField Name { get; } = new FillableField();

  public FillableField Range { get; } = new FillableField();

  public FillableField Attack { get; } = new FillableField();

  public FillableField Damage { get; } = new FillableField();

  public FillableField Description { get; } = new FillableField();

  public RefactoredEquipmentItem EquipmentItem => this._equipment;

  public AttackSectionItem(string name)
    : this(name, string.Empty)
  {
  }

  public AttackSectionItem(string name, string description)
  {
    this.Name.Content = name;
    this.Description.Content = description;
    this.Range.Content = "";
    this.Attack.Content = "";
    this.Damage.Content = "";
    this.IsDisplayed = true;
    this.IsDisplayedAsCard = false;
  }

  public AttackSectionItem(RefactoredEquipmentItem equipment, bool initializeAbility = true)
  {
    this.Abilities.Add(CharacterManager.Current.Character.Abilities.Strength);
    this.Abilities.Add(CharacterManager.Current.Character.Abilities.Dexterity);
    this.Abilities.Add(CharacterManager.Current.Character.Abilities.Constitution);
    this.Abilities.Add(CharacterManager.Current.Character.Abilities.Intelligence);
    this.Abilities.Add(CharacterManager.Current.Character.Abilities.Wisdom);
    this.Abilities.Add(CharacterManager.Current.Character.Abilities.Charisma);
    this._equipment = equipment;
    this.UpdateCalculations(initializeAbility);
    this.IsDisplayed = true;
    this.IsDisplayedAsCard = equipment.ShowCard;
    this.IsAutomaticAddition = true;
  }

  public AttackSectionItem(Spell spell, SpellcastingInformation information)
  {
    this._spell = spell;
    this._information = information;
    this.Abilities.Add(CharacterManager.Current.Character.Abilities.Strength);
    this.Abilities.Add(CharacterManager.Current.Character.Abilities.Dexterity);
    this.Abilities.Add(CharacterManager.Current.Character.Abilities.Constitution);
    this.Abilities.Add(CharacterManager.Current.Character.Abilities.Intelligence);
    this.Abilities.Add(CharacterManager.Current.Character.Abilities.Wisdom);
    this.Abilities.Add(CharacterManager.Current.Character.Abilities.Charisma);
    this.UpdateCalculations(true);
    this.IsDisplayed = true;
    this.IsDisplayedAsCard = false;
    this.IsAutomaticAddition = true;
  }

  public Dictionary<string, int> GetCalculatedDamage(RefactoredEquipmentItem equipment)
  {
    CharacterManager current = CharacterManager.Current;
    List<Tuple<string, int>> tupleList = new List<Tuple<string, int>>();
    int num = equipment.Item.Supports.Contains("ID_INTERNAL_WEAPON_CATEGORY_SIMPLE_MELEE") || equipment.Item.Supports.Contains("ID_INTERNAL_WEAPON_CATEGORY_MARTIAL_MELEE") ? 1 : (equipment.Item.Supports.Contains("ID_INTERNAL_WEAPON_CATEGORY_MELEE") ? 1 : 0);
    bool flag = equipment.Item.Supports.Contains("ID_INTERNAL_WEAPON_CATEGORY_SIMPLE_RANGED") || equipment.Item.Supports.Contains("ID_INTERNAL_WEAPON_CATEGORY_MARTIAL_RANGED") || equipment.Item.Supports.Contains("ID_INTERNAL_WEAPON_CATEGORY_RANGED") || equipment.Item.Supports.Contains("ID_INTERNAL_WEAPON_CATEGORY_FIREARM") || equipment.Item.Supports.Contains("ID_INTERNAL_WEAPON_CATEGORY_FIREARMS");
    if (this.LinkedAbility != null)
      tupleList.Add(new Tuple<string, int>($"{this.LinkedAbility} Modifier", this.LinkedAbility.Modifier));
    if (num != 0)
    {
      StatisticValuesGroup group = current.StatisticsCalculator.StatisticValues.GetGroup("melee:damage");
      if (group.GetValues().Count > 0)
        tupleList.Add(new Tuple<string, int>(group.GetSummery(false), group.Sum()));
    }
    else if (flag)
    {
      StatisticValuesGroup group = current.StatisticsCalculator.StatisticValues.GetGroup("ranged:damage");
      if (group.GetValues().Count > 0)
        tupleList.Add(new Tuple<string, int>(group.GetSummery(false), group.Sum()));
    }
    StatisticValuesGroup group1 = current.StatisticsCalculator.StatisticValues.GetGroup(equipment.Item.Name.ToLower() + ":damage");
    if (group1.GetValues().Count > 0)
      tupleList.Add(new Tuple<string, int>(group1.GetSummery(false), group1.Sum()));
    int result;
    if (equipment.IsAdorned && int.TryParse(equipment.AdornerItem.Enhancement, out result))
      tupleList.Add(new Tuple<string, int>("Enhancement", result));
    Dictionary<string, int> calculatedDamage = new Dictionary<string, int>();
    foreach (Tuple<string, int> tuple in tupleList)
    {
      if (calculatedDamage.ContainsKey(tuple.Item1))
        calculatedDamage[tuple.Item1] += tuple.Item2;
      else
        calculatedDamage.Add(tuple.Item1, tuple.Item2);
    }
    return calculatedDamage;
  }

  public Dictionary<string, int> GetCalculatedAttackBonus(RefactoredEquipmentItem equipment)
  {
    CharacterManager current = CharacterManager.Current;
    ElementsOrganizer elementsOrganizer = new ElementsOrganizer((IEnumerable<ElementBase>) current.GetElements());
    Dictionary<string, int> calculatedAttackBonus = new Dictionary<string, int>();
    int num1 = 0;
    string str = equipment.Item.ElementSetters.GetSetter("proficiency")?.Value;
    if (str != null && elementsOrganizer.GetProficiencies().Select<Proficiency, string>((Func<Proficiency, string>) (x => x.Id)).Contains<string>(str.Trim()))
    {
      num1 += current.Character.Proficiency;
      calculatedAttackBonus.Add("Proficiency", current.Character.Proficiency);
    }
    int num2 = equipment.Item.Supports.Contains("ID_INTERNAL_WEAPON_CATEGORY_SIMPLE_MELEE") || equipment.Item.Supports.Contains("ID_INTERNAL_WEAPON_CATEGORY_MARTIAL_MELEE") ? 1 : (equipment.Item.Supports.Contains("ID_INTERNAL_WEAPON_CATEGORY_MELEE") ? 1 : 0);
    bool flag = equipment.Item.Supports.Contains("ID_INTERNAL_WEAPON_CATEGORY_SIMPLE_RANGED") || equipment.Item.Supports.Contains("ID_INTERNAL_WEAPON_CATEGORY_MARTIAL_RANGED") || equipment.Item.Supports.Contains("ID_INTERNAL_WEAPON_CATEGORY_RANGED") || equipment.Item.Supports.Contains("ID_INTERNAL_WEAPON_CATEGORY_FIREARM") || equipment.Item.Supports.Contains("ID_INTERNAL_WEAPON_CATEGORY_FIREARMS");
    if (this.LinkedAbility != null)
    {
      num1 += this.LinkedAbility.Modifier;
      calculatedAttackBonus.Add(this.LinkedAbility.Name + " Modifier", this.LinkedAbility.Modifier);
    }
    if (num2 != 0)
    {
      StatisticValuesGroup group = current.StatisticsCalculator.StatisticValues.GetGroup("melee:attack");
      if (group.GetValues().Count > 0)
        calculatedAttackBonus.Add(group.GetSummery(false), group.Sum());
    }
    else if (flag)
    {
      StatisticValuesGroup group = current.StatisticsCalculator.StatisticValues.GetGroup("ranged:attack");
      if (group.GetValues().Count > 0)
        calculatedAttackBonus.Add(group.GetSummery(false), group.Sum());
    }
    StatisticValuesGroup group1 = current.StatisticsCalculator.StatisticValues.GetGroup(equipment.Item.Name.ToLower() + ":attack");
    if (group1.GetValues().Count > 0)
      calculatedAttackBonus.Add(group1.GetSummery(false), group1.Sum());
    int result;
    if (equipment.IsAdorned && int.TryParse(equipment.AdornerItem.Enhancement, out result))
    {
      int num3 = num1 + result;
      calculatedAttackBonus.Add("Enhancement", result);
    }
    return calculatedAttackBonus;
  }

  public Dictionary<string, int> GetCalculatedSpellAttackBonus(SpellcastingInformation information)
  {
    CharacterManager current = CharacterManager.Current;
    Dictionary<string, int> spellAttackBonus = new Dictionary<string, int>();
    StatisticValuesGroup group1 = current.StatisticsCalculator.StatisticValues.GetGroup(information.GetSpellAttackStatisticName());
    spellAttackBonus.Add(group1.GetSummery(false), group1.Sum());
    StatisticValuesGroup group2 = current.StatisticsCalculator.StatisticValues.GetGroup("spellcasting:attack");
    spellAttackBonus.Add(group2.GetSummery(false), group2.Sum());
    return spellAttackBonus;
  }

  public void SetLinkedAbility(string abilityName)
  {
    AbilityItem abilityItem = this.Abilities.FirstOrDefault<AbilityItem>((Func<AbilityItem, bool>) (x => x.Name.Equals(abilityName, StringComparison.OrdinalIgnoreCase)));
    if (abilityItem != null)
      this.LinkedAbility = abilityItem;
    else
      MessageDialogContext.Current?.Show($"unable to set linked ability '{abilityName}' on {this._equipment}");
  }

  private void DeterminePrimaryAbility(RefactoredEquipmentItem equipment)
  {
    AbilitiesCollection abilities = CharacterManager.Current.Character.Abilities;
    bool flag1 = equipment.Item.Supports.Contains("ID_INTERNAL_WEAPON_CATEGORY_SIMPLE_MELEE") || equipment.Item.Supports.Contains("ID_INTERNAL_WEAPON_CATEGORY_MARTIAL_MELEE") || equipment.Item.Supports.Contains("ID_INTERNAL_WEAPON_CATEGORY_MELEE");
    bool flag2 = equipment.Item.Supports.Contains("ID_INTERNAL_WEAPON_CATEGORY_SIMPLE_RANGED") || equipment.Item.Supports.Contains("ID_INTERNAL_WEAPON_CATEGORY_MARTIAL_RANGED") || equipment.Item.Supports.Contains("ID_INTERNAL_WEAPON_CATEGORY_RANGED") || equipment.Item.Supports.Contains("ID_INTERNAL_WEAPON_CATEGORY_FIREARM") || equipment.Item.Supports.Contains("ID_INTERNAL_WEAPON_CATEGORY_FIREARMS");
    if (!flag1 && !flag2)
    {
      flag1 = true;
      this.SetLinkedAbility(abilities.Strength.Name);
    }
    if (flag1)
    {
      if (equipment.Item.Supports.Contains("ID_INTERNAL_WEAPON_PROPERTY_FINESSE") && abilities.Dexterity.Modifier > abilities.Strength.Modifier)
        this.SetLinkedAbility(abilities.Dexterity.Name);
      else
        this.SetLinkedAbility(abilities.Strength.Name);
    }
    else
    {
      if (!flag2)
        return;
      this.SetLinkedAbility(abilities.Dexterity.Name);
    }
  }

  public void UpdateCalculations(bool setLinkedAbility = false)
  {
    try
    {
      if (this._equipment != null)
      {
        if (setLinkedAbility)
          this.DeterminePrimaryAbility(this._equipment);
        else if (this.LinkedAbility == null)
          this.DeterminePrimaryAbility(this._equipment);
        CharacterInventory inventory = CharacterManager.Current.Character.Inventory;
        this.Name.OriginalContent = this._equipment.DisplayName;
        this.Range.OriginalContent = this._equipment.Item.Range ?? "5 ft";
        this.Description.OriginalContent = string.IsNullOrWhiteSpace(this._equipment.Notes) ? this._equipment.Item.DisplayWeaponProperties : this._equipment.Notes;
        Dictionary<string, int> calculatedAttackBonus = this.GetCalculatedAttackBonus(this._equipment);
        this.Attack.OriginalContent = calculatedAttackBonus.Sum<KeyValuePair<string, int>>((Func<KeyValuePair<string, int>, int>) (x => x.Value)).ToValueString() + " vs AC";
        this.DisplayCalculatedAttack = string.Join(", ", calculatedAttackBonus.Where<KeyValuePair<string, int>>((Func<KeyValuePair<string, int>, bool>) (x => x.Value > 0)).Select<KeyValuePair<string, int>, string>((Func<KeyValuePair<string, int>, string>) (x => $"{x.Key} {x.Value}")));
        Dictionary<string, int> calculatedDamage = this.GetCalculatedDamage(this._equipment);
        int num = calculatedDamage.Sum<KeyValuePair<string, int>>((Func<KeyValuePair<string, int>, int>) (x => x.Value));
        bool flag = num < 0;
        this.Damage.OriginalContent = $"{this._equipment.Item.Damage}{(flag ? (object) "" : (object) "+")}{num} {this._equipment.Item.DamageType}";
        this.DisplayCalculatedDamage = string.Join(", ", calculatedDamage.Where<KeyValuePair<string, int>>((Func<KeyValuePair<string, int>, bool>) (x => x.Value > 0)).Select<KeyValuePair<string, int>, string>((Func<KeyValuePair<string, int>, string>) (x => $"{x.Key} {x.Value}")));
        if (!this._equipment.IsEquipped || !this._equipment.Item.HasVersatile || !inventory.IsEquippedVersatile())
          return;
        this.Damage.OriginalContent = $"{this._equipment.Item.Versatile}{(flag ? (object) "" : (object) "+")}{num} {this._equipment.Item.DamageType} (Versatile)";
      }
      else
      {
        if (this._spell == null || this._information == null)
          return;
        this.Name.OriginalContent = this._spell.Name;
        this.Range.OriginalContent = this._spell.Range;
        Dictionary<string, int> spellAttackBonus = this.GetCalculatedSpellAttackBonus(this._information);
        this.Attack.OriginalContent = $"{spellAttackBonus.Sum<KeyValuePair<string, int>>((Func<KeyValuePair<string, int>, int>) (x => x.Value)).ToValueString()} {this._information.AbilityName.Substring(0, 3).ToUpper()} vs AC";
        this.DisplayCalculatedAttack = string.Join(", ", spellAttackBonus.Where<KeyValuePair<string, int>>((Func<KeyValuePair<string, int>, bool>) (x => x.Value > 0)).Select<KeyValuePair<string, int>, string>((Func<KeyValuePair<string, int>, string>) (x => $"{x.Key} {x.Value}")));
      }
    }
    catch (Exception ex)
    {
      Logger.Exception(ex, nameof (UpdateCalculations));
    }
  }

  public override string ToString() => this.Name.ToString();
}
