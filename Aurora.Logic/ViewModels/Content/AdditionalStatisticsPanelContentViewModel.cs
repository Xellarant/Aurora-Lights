// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.Content.AdditionalStatisticsPanelContentViewModel
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core.Events;
using Builder.Presentation.Events.Character;
using Builder.Presentation.Extensions;
using Builder.Presentation.Services.Calculator;
using Builder.Presentation.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable
namespace Builder.Presentation.ViewModels.Content;

public sealed class AdditionalStatisticsPanelContentViewModel : 
  ViewModelBase,
  ISubscriber<ReprocessCharacterEvent>,
  ISubscriber<CharacterManagerElementRegistered>,
  ISubscriber<CharacterManagerElementUnregistered>
{
  public AdditionalStatisticsPanelContentViewModel()
  {
    this.AdditionalItems = new ObservableCollection<StatisticsPanelItem>();
    if (this.IsInDesignMode)
      this.InitializeDesignData();
    else
      this.EventAggregator.Subscribe((object) this);
  }

  public StatisticsPanelItem Speed { get; } = new StatisticsPanelItem(nameof (Speed));

  public StatisticsPanelItem SpeedFly { get; } = new StatisticsPanelItem("Fly Speed");

  public StatisticsPanelItem SpeedClimb { get; } = new StatisticsPanelItem("Climb Speed");

  public StatisticsPanelItem SpeedSwim { get; } = new StatisticsPanelItem("Swim Speed");

  public StatisticsPanelItem SpeedBurrow { get; } = new StatisticsPanelItem("Burrow Speed");

  public StatisticsPanelItem ArmorClass { get; } = new StatisticsPanelItem("AC");

  public StatisticsPanelItem Initiative { get; } = new StatisticsPanelItem(nameof (Initiative));

  public StatisticsPanelItem Hp { get; } = new StatisticsPanelItem("HP");

  public StatisticsPanelItem KiPoints { get; } = new StatisticsPanelItem("Ki Points");

  public StatisticsPanelItem SorceryPoints { get; } = new StatisticsPanelItem("Sorcery Points");

  public ObservableCollection<StatisticsPanelItem> AdditionalItems { get; }

  public StatisticsPanelItem SneakAttack { get; } = new StatisticsPanelItem("Sneak Attack");

  public StatisticsPanelItem SuperiorityDice { get; } = new StatisticsPanelItem("Superiority Dice");

  public IEnumerable<StatisticsPanelItem> All
  {
    get
    {
      yield return this.Speed;
      yield return this.SpeedFly;
      yield return this.SpeedClimb;
      yield return this.SpeedSwim;
      yield return this.SpeedBurrow;
      yield return this.ArmorClass;
      yield return this.Initiative;
      yield return this.Hp;
      yield return this.KiPoints;
      yield return this.SorceryPoints;
      yield return this.SneakAttack;
      yield return this.SuperiorityDice;
      foreach (StatisticsPanelItem additionalItem in (Collection<StatisticsPanelItem>) this.AdditionalItems)
        yield return additionalItem;
    }
  }

  protected override void InitializeDesignData()
  {
    base.InitializeDesignData();
    this.Speed.Value = 35;
    this.Speed.Exists = this.Speed.IsUpdated = true;
    this.SpeedClimb.Value = 20;
    this.SpeedClimb.Exists = this.SpeedClimb.IsUpdated = true;
    this.ArmorClass.Value = 14;
    this.ArmorClass.Exists = this.ArmorClass.IsUpdated = true;
    this.Initiative.Value = 3;
    this.Initiative.Exists = this.Initiative.IsUpdated = true;
    this.Hp.Value = 123;
    this.Hp.Exists = this.Hp.IsUpdated = true;
    this.Hp.Summery = "All kinds of sources (123)";
    this.AdditionalItems.Add(this.Speed);
    this.AdditionalItems.Add(this.Hp);
  }

  private void Update()
  {
    StatisticValuesGroupCollection statisticValues = CharacterManager.Current.StatisticsCalculator.StatisticValues;
    if (statisticValues == null)
      return;
    foreach (StatisticsPanelItem statisticsPanelItem in this.All)
    {
      statisticsPanelItem.Exists = false;
      statisticsPanelItem.IsUpdated = false;
      statisticsPanelItem.Summery = "n/a";
    }
    this.AdditionalItems.Clear();
    foreach (StatisticValuesGroup group in (List<StatisticValuesGroup>) statisticValues)
    {
      if (group.GroupName.Equals("speed", StringComparison.OrdinalIgnoreCase))
        this.AddItem("Speed", group);
      if (group.Sum() != 0)
      {
        if (group.GroupName.Equals("speed:fly", StringComparison.OrdinalIgnoreCase))
          this.AddItem("Fly", group);
        if (group.GroupName.Equals("speed:climb", StringComparison.OrdinalIgnoreCase))
          this.AddItem("Climb", group);
        if (group.GroupName.Equals("speed:swim", StringComparison.OrdinalIgnoreCase))
          this.AddItem("Swim", group);
        if (group.GroupName.Equals("speed:burrow", StringComparison.OrdinalIgnoreCase))
          this.AddItem("Burrow", group);
      }
    }
    foreach (StatisticValuesGroup group in (List<StatisticValuesGroup>) statisticValues)
    {
      if (group.GroupName.Equals("ac", StringComparison.OrdinalIgnoreCase))
        this.AddItem("AC", group);
      if (group.GroupName.Equals("initiative", StringComparison.OrdinalIgnoreCase))
        this.AddItem("Initiative", group, true);
      if (group.GroupName.Equals("hp", StringComparison.OrdinalIgnoreCase))
        this.AddItem("HP", group);
    }
    foreach (StatisticValuesGroup group in (List<StatisticValuesGroup>) statisticValues)
    {
      if (group.GroupName.Equals("ki:points", StringComparison.OrdinalIgnoreCase))
        this.AddItem("Ki Points", group);
      if (group.GroupName.Equals("sorcery-points", StringComparison.OrdinalIgnoreCase))
        this.AddItem("Sorcery Points", group);
    }
    if (statisticValues.ContainsGroup("sneak-attack:count") && statisticValues.ContainsGroup("sneak-attack:die"))
      this.AdditionalItems.Add(new StatisticsPanelItem("Sneak Attack", $"{statisticValues.GetValue("sneak-attack:count")}d{statisticValues.GetValue("sneak-attack:die")}")
      {
        Exists = true,
        Summery = "Sneak Attack"
      });
    if (!statisticValues.ContainsGroup("superiority dice:amount") || !statisticValues.ContainsGroup("superiority dice:size"))
      return;
    this.AdditionalItems.Add(new StatisticsPanelItem("Superiority Dice", $"{statisticValues.GetValue("superiority dice:amount")}d{statisticValues.GetValue("superiority dice:size")}")
    {
      Exists = true,
      Summery = "Combat Superiority"
    });
  }

  private void AddItem(string displayName, StatisticValuesGroup group, bool toValueString = false)
  {
    StatisticsPanelItem statisticsPanelItem = new StatisticsPanelItem(displayName);
    statisticsPanelItem.Update(group);
    statisticsPanelItem.DisplayValue = toValueString ? statisticsPanelItem.Value.ToValueString() : statisticsPanelItem.Value.ToString();
    this.AdditionalItems.Add(statisticsPanelItem);
  }

  public void OnHandleEvent(ReprocessCharacterEvent args) => this.Update();

  public void OnHandleEvent(CharacterManagerElementRegistered args) => this.Update();

  public void OnHandleEvent(CharacterManagerElementUnregistered args) => this.Update();
}
