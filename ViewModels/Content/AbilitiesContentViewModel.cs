// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.Content.AbilitiesContentViewModel
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core;
using Builder.Core.Events;
using Builder.Core.Logging;
using Builder.Presentation.Events.Application;
using Builder.Presentation.Events.Shell;
using Builder.Presentation.Models;
using Builder.Presentation.Models.Collections;
using Builder.Presentation.Services;
using Builder.Presentation.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

#nullable disable
namespace Builder.Presentation.ViewModels.Content;

public class AbilitiesContentViewModel : ViewModelBase, ISubscriber<SettingsChangedEvent>
{
  private readonly DiceService _dice;
  private AbilitiesGenerationOption _option;
  private bool _isRandomizeGeneration;
  private bool _isArrayGeneration;
  private bool _isPointsGeneration;
  private string _generationDisplayName;

  public AbilitiesContentViewModel()
  {
    if (this.IsInDesignMode)
    {
      this.InitializeDesignData();
    }
    else
    {
      this._dice = new DiceService();
      this.SetGenerationOption();
      this.EventAggregator.Subscribe((object) this);
    }
  }

  public virtual AbilitiesCollection Abilities => CharacterManager.Current.Character.Abilities;

  public bool IsRandomizeGeneration
  {
    get => this._isRandomizeGeneration;
    set
    {
      this.SetProperty<bool>(ref this._isRandomizeGeneration, value, nameof (IsRandomizeGeneration));
    }
  }

  public bool IsArrayGeneration
  {
    get => this._isArrayGeneration;
    set => this.SetProperty<bool>(ref this._isArrayGeneration, value, nameof (IsArrayGeneration));
  }

  public bool IsPointsGeneration
  {
    get => this._isPointsGeneration;
    set => this.SetProperty<bool>(ref this._isPointsGeneration, value, nameof (IsPointsGeneration));
  }

  public string GenerationDisplayName
  {
    get => this._generationDisplayName;
    set
    {
      this.SetProperty<string>(ref this._generationDisplayName, value, nameof (GenerationDisplayName));
    }
  }

  public ICommand GenerateRandomAbilityScoreCommand
  {
    get
    {
      return (ICommand) new RelayCommand<AbilityItem>(new Action<AbilityItem>(this.GenerateRandomAbilityScore));
    }
  }

  public ICommand InitializeStandardArrayCommand
  {
    get => (ICommand) new RelayCommand(new Action(this.InitializeStandardArray));
  }

  private void InitializeStandardArray()
  {
    this.Abilities.Strength.BaseScore = 15;
    this.Abilities.Strength.BaseScore = 14;
    this.Abilities.Strength.BaseScore = 13;
    this.Abilities.Strength.BaseScore = 12;
    this.Abilities.Strength.BaseScore = 10;
    this.Abilities.Strength.BaseScore = 8;
  }

  private async void GenerateRandomAbilityScore(AbilityItem parameter)
  {
    AbilitiesContentViewModel contentViewModel = this;
    try
    {
      switch (contentViewModel._option)
      {
        case AbilitiesGenerationOption.Roll3D6:
          AbilityItem abilityItem = parameter;
          abilityItem.BaseScore = await contentViewModel._dice.D6(3);
          abilityItem = (AbilityItem) null;
          break;
        case AbilitiesGenerationOption.Roll4D6DiscardLowest:
          List<int> intList1 = new List<int>(4);
          List<int> intList2 = intList1;
          intList2.Add(await contentViewModel._dice.D6());
          List<int> intList3 = intList1;
          intList3.Add(await contentViewModel._dice.D6());
          List<int> intList4 = intList1;
          intList4.Add(await contentViewModel._dice.D6());
          List<int> intList5 = intList1;
          intList5.Add(await contentViewModel._dice.D6());
          List<int> source = intList1;
          intList2 = (List<int>) null;
          intList3 = (List<int>) null;
          intList4 = (List<int>) null;
          intList5 = (List<int>) null;
          intList1 = (List<int>) null;
          parameter.BaseScore = source.Sum() - source.Min();
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      contentViewModel.Abilities.IncreaseAbilityCommand.OnCanExecuteChanged();
      contentViewModel.Abilities.DecreaseAbilityCommand.OnCanExecuteChanged();
      contentViewModel.EventAggregator.Send<MainWindowStatusUpdateEvent>(new MainWindowStatusUpdateEvent($"You rolled {parameter.BaseScore} on your {parameter.Name}"));
    }
    catch (Exception ex)
    {
      Logger.Exception(ex, nameof (GenerateRandomAbilityScore));
    }
    try
    {
      contentViewModel.Abilities.CalculateAvailablePoints();
    }
    catch (Exception ex)
    {
      Logger.Exception(ex, nameof (GenerateRandomAbilityScore));
    }
  }

  public ICommand RandomizeCommand
  {
    get => (ICommand) new RelayCommand(new Action(this.RandomizeAbilitiesStatistic));
  }

  public ICommand RandomizeSingleCommand
  {
    get => (ICommand) new RelayCommand<AbilityItem>(new Action<AbilityItem>(this.RandomizeSingle));
  }

  private async void RandomizeSingle(AbilityItem parameter)
  {
    AbilitiesContentViewModel contentViewModel = this;
    AbilityItem abilityItem = parameter;
    abilityItem.BaseScore = await contentViewModel._dice.RandomizeAbilityScore();
    abilityItem = (AbilityItem) null;
    contentViewModel.Abilities.IncreaseAbilityCommand.OnCanExecuteChanged();
    contentViewModel.Abilities.DecreaseAbilityCommand.OnCanExecuteChanged();
    string statusMessage = $"You rolled {parameter.BaseScore} on your {parameter.Name}";
    contentViewModel.EventAggregator.Send<MainWindowStatusUpdateEvent>(new MainWindowStatusUpdateEvent(statusMessage));
    try
    {
      contentViewModel.Abilities.CalculateAvailablePoints();
    }
    catch (Exception ex)
    {
      Logger.Exception(ex, nameof (RandomizeSingle));
    }
  }

  private async void RandomizeAbilitiesStatistic()
  {
    AbilityItem abilityItem = this.Abilities.Strength;
    abilityItem.BaseScore = await this._dice.RandomizeAbilityScore();
    abilityItem = (AbilityItem) null;
    abilityItem = this.Abilities.Dexterity;
    abilityItem.BaseScore = await this._dice.RandomizeAbilityScore();
    abilityItem = (AbilityItem) null;
    abilityItem = this.Abilities.Constitution;
    abilityItem.BaseScore = await this._dice.RandomizeAbilityScore();
    abilityItem = (AbilityItem) null;
    abilityItem = this.Abilities.Intelligence;
    abilityItem.BaseScore = await this._dice.RandomizeAbilityScore();
    abilityItem = (AbilityItem) null;
    abilityItem = this.Abilities.Wisdom;
    abilityItem.BaseScore = await this._dice.RandomizeAbilityScore();
    abilityItem = (AbilityItem) null;
    abilityItem = this.Abilities.Charisma;
    abilityItem.BaseScore = await this._dice.RandomizeAbilityScore();
    abilityItem = (AbilityItem) null;
    this.Abilities.IncreaseAbilityCommand.OnCanExecuteChanged();
    this.Abilities.DecreaseAbilityCommand.OnCanExecuteChanged();
    try
    {
      this.Abilities.CalculateAvailablePoints();
    }
    catch (Exception ex)
    {
      Logger.Exception(ex, nameof (RandomizeAbilitiesStatistic));
    }
  }

  private void SetGenerationOption()
  {
    try
    {
      this._option = (AbilitiesGenerationOption) ApplicationManager.Current.Settings.Settings.AbilitiesGenerationOption;
    }
    catch (Exception ex)
    {
      this._option = AbilitiesGenerationOption.Roll4D6DiscardLowest;
      Logger.Exception(ex, nameof (SetGenerationOption));
    }
    this.GenerationDisplayName = "N/A";
    this.IsRandomizeGeneration = false;
    this.IsArrayGeneration = false;
    this.IsPointsGeneration = false;
    this.Abilities.DisablePointsCalculation = true;
    switch (this._option)
    {
      case AbilitiesGenerationOption.Roll3D6:
        this.IsRandomizeGeneration = true;
        this.GenerationDisplayName = "Roll 3D6";
        break;
      case AbilitiesGenerationOption.Roll4D6DiscardLowest:
        this.IsRandomizeGeneration = true;
        this.GenerationDisplayName = "Roll 4D6, Discard Lowest";
        break;
      case AbilitiesGenerationOption.Array:
        this.IsArrayGeneration = true;
        this.GenerationDisplayName = "Array (Drag & Drop to Switch Scores)";
        break;
      case AbilitiesGenerationOption.Points:
        this.IsPointsGeneration = true;
        this.GenerationDisplayName = "Point Buy";
        this.Abilities.DisablePointsCalculation = false;
        this.Abilities.CalculateAvailablePoints();
        break;
      default:
        throw new ArgumentOutOfRangeException();
    }
  }

  void ISubscriber<SettingsChangedEvent>.OnHandleEvent(SettingsChangedEvent args)
  {
    if (CharacterManager.Current != null && CharacterManager.Current.Status.IsLoaded)
    {
      int num = CharacterManager.Current.Status.HasChanges ? 1 : 0;
      CharacterManager.Current.ReprocessCharacter();
      if (num == 0)
        CharacterManager.Current.Status.HasChanges = false;
    }
    if (this._option == (AbilitiesGenerationOption) args.Settings.AbilitiesGenerationOption)
      return;
    this.SetGenerationOption();
  }

  protected override void InitializeDesignData()
  {
    base.InitializeDesignData();
    this.Abilities.Strength.BaseScore = 8;
    this.Abilities.Dexterity.BaseScore = 15;
    this.Abilities.Constitution.BaseScore = 13;
    this.Abilities.Intelligence.BaseScore = 12;
    this.Abilities.Wisdom.BaseScore = 10;
    this.Abilities.Charisma.BaseScore = 15;
    this.Abilities.Dexterity.AdditionalScore = 3;
    this.Abilities.Constitution.AdditionalScore = 1;
    this.Abilities.CalculateAvailablePoints();
    this.GenerationDisplayName = "Design 4D6";
    this.IsRandomizeGeneration = true;
  }
}
