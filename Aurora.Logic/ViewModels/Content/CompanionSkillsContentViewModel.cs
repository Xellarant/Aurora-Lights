// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.Content.CompanionSkillsContentViewModel
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Presentation.Events.Character;
using Builder.Presentation.Models.Collections;
using Builder.Presentation.Services.Calculator;
using System.ComponentModel;

#nullable disable
namespace Builder.Presentation.ViewModels.Content;

public class CompanionSkillsContentViewModel : SkillsContentViewModel
{
  public CompanionSkillsContentViewModel()
  {
    this.Character.Companion.Statistics.PropertyChanged += new PropertyChangedEventHandler(this.Statistics_PropertyChanged);
  }

  private void Statistics_PropertyChanged(object sender, PropertyChangedEventArgs e)
  {
    this.OnPropertyChanged("IsAcrobaticsExpert");
    this.OnPropertyChanged("IsAnimalHandlingExpert");
    this.OnPropertyChanged("IsArcanaExpert");
    this.OnPropertyChanged("IsAthleticsExpert");
    this.OnPropertyChanged("IsDeceptionExpert");
    this.OnPropertyChanged("IsHistoryExpert");
    this.OnPropertyChanged("IsInsightExpert");
    this.OnPropertyChanged("IsIntimidationExpert");
    this.OnPropertyChanged("IsInvestigationExpert");
    this.OnPropertyChanged("IsMedicineExpert");
    this.OnPropertyChanged("IsNatureExpert");
    this.OnPropertyChanged("IsPerceptionExpert");
    this.OnPropertyChanged("IsPerformanceExpert");
    this.OnPropertyChanged("IsPersuasionExpert");
    this.OnPropertyChanged("IsReligionExpert");
    this.OnPropertyChanged("IsSleightOfHandExpert");
    this.OnPropertyChanged("IsStealthExpert");
    this.OnPropertyChanged("IsSurvivalExpert");
  }

  public new SkillsCollection Skills => CharacterManager.Current.Character.Companion.Skills;

  public bool IsAcrobaticsExpert
  {
    get => this.Skills.Acrobatics.IsExpertise(this.Character.Companion.Statistics.Proficiency);
  }

  public bool IsAnimalHandlingExpert
  {
    get => this.Skills.AnimalHandling.IsExpertise(this.Character.Companion.Statistics.Proficiency);
  }

  public bool IsArcanaExpert
  {
    get => this.Skills.Arcana.IsExpertise(this.Character.Companion.Statistics.Proficiency);
  }

  public bool IsAthleticsExpert
  {
    get => this.Skills.Athletics.IsExpertise(this.Character.Companion.Statistics.Proficiency);
  }

  public bool IsDeceptionExpert
  {
    get => this.Skills.Deception.IsExpertise(this.Character.Companion.Statistics.Proficiency);
  }

  public bool IsHistoryExpert
  {
    get => this.Skills.History.IsExpertise(this.Character.Companion.Statistics.Proficiency);
  }

  public bool IsInsightExpert
  {
    get => this.Skills.Insight.IsExpertise(this.Character.Companion.Statistics.Proficiency);
  }

  public bool IsIntimidationExpert
  {
    get => this.Skills.Intimidation.IsExpertise(this.Character.Companion.Statistics.Proficiency);
  }

  public bool IsInvestigationExpert
  {
    get => this.Skills.Investigation.IsExpertise(this.Character.Companion.Statistics.Proficiency);
  }

  public bool IsMedicineExpert
  {
    get => this.Skills.Medicine.IsExpertise(this.Character.Companion.Statistics.Proficiency);
  }

  public bool IsNatureExpert
  {
    get => this.Skills.Nature.IsExpertise(this.Character.Companion.Statistics.Proficiency);
  }

  public bool IsPerceptionExpert
  {
    get => this.Skills.Perception.IsExpertise(this.Character.Companion.Statistics.Proficiency);
  }

  public bool IsPerformanceExpert
  {
    get => this.Skills.Performance.IsExpertise(this.Character.Companion.Statistics.Proficiency);
  }

  public bool IsPersuasionExpert
  {
    get => this.Skills.Persuasion.IsExpertise(this.Character.Companion.Statistics.Proficiency);
  }

  public bool IsReligionExpert
  {
    get => this.Skills.Religion.IsExpertise(this.Character.Companion.Statistics.Proficiency);
  }

  public bool IsSleightOfHandExpert
  {
    get => this.Skills.SleightOfHand.IsExpertise(this.Character.Companion.Statistics.Proficiency);
  }

  public bool IsStealthExpert
  {
    get => this.Skills.Stealth.IsExpertise(this.Character.Companion.Statistics.Proficiency);
  }

  public bool IsSurvivalExpert
  {
    get => this.Skills.Survival.IsExpertise(this.Character.Companion.Statistics.Proficiency);
  }

  public override void OnHandleEvent(ReprocessCharacterEvent args)
  {
    StatisticValuesGroupCollection statisticValues = CharacterManager.Current.StatisticsCalculator.StatisticValues;
    if (statisticValues == null)
      return;
    StatisticValuesGroup group = statisticValues.GetGroup("companion:perception:passive", false);
    if (group != null)
      this.PassivePerception = group.Sum() + this.Skills.Perception.FinalBonus;
    else
      this.PassivePerception = 10 + this.Skills.Perception.FinalBonus;
    this.OnPropertyChanged("IsAcrobaticsExpert");
    this.OnPropertyChanged("IsAnimalHandlingExpert");
    this.OnPropertyChanged("IsArcanaExpert");
    this.OnPropertyChanged("IsAthleticsExpert");
    this.OnPropertyChanged("IsDeceptionExpert");
    this.OnPropertyChanged("IsHistoryExpert");
    this.OnPropertyChanged("IsInsightExpert");
    this.OnPropertyChanged("IsIntimidationExpert");
    this.OnPropertyChanged("IsInvestigationExpert");
    this.OnPropertyChanged("IsMedicineExpert");
    this.OnPropertyChanged("IsNatureExpert");
    this.OnPropertyChanged("IsPerceptionExpert");
    this.OnPropertyChanged("IsPerformanceExpert");
    this.OnPropertyChanged("IsPersuasionExpert");
    this.OnPropertyChanged("IsReligionExpert");
    this.OnPropertyChanged("IsSleightOfHandExpert");
    this.OnPropertyChanged("IsStealthExpert");
    this.OnPropertyChanged("IsSurvivalExpert");
  }

  public override void OnHandleEvent(CharacterManagerElementRegistered args)
  {
    StatisticValuesGroup group = CharacterManager.Current.StatisticsCalculator.StatisticValues.GetGroup("companion:perception:passive", false);
    if (group != null)
      this.PassivePerception = group.Sum() + this.Skills.Perception.FinalBonus;
    else
      this.PassivePerception = 10 + this.Skills.Perception.FinalBonus;
    this.OnPropertyChanged("IsAcrobaticsExpert");
    this.OnPropertyChanged("IsAnimalHandlingExpert");
    this.OnPropertyChanged("IsArcanaExpert");
    this.OnPropertyChanged("IsAthleticsExpert");
    this.OnPropertyChanged("IsDeceptionExpert");
    this.OnPropertyChanged("IsHistoryExpert");
    this.OnPropertyChanged("IsInsightExpert");
    this.OnPropertyChanged("IsIntimidationExpert");
    this.OnPropertyChanged("IsInvestigationExpert");
    this.OnPropertyChanged("IsMedicineExpert");
    this.OnPropertyChanged("IsNatureExpert");
    this.OnPropertyChanged("IsPerceptionExpert");
    this.OnPropertyChanged("IsPerformanceExpert");
    this.OnPropertyChanged("IsPersuasionExpert");
    this.OnPropertyChanged("IsReligionExpert");
    this.OnPropertyChanged("IsSleightOfHandExpert");
    this.OnPropertyChanged("IsStealthExpert");
    this.OnPropertyChanged("IsSurvivalExpert");
  }

  public override void OnHandleEvent(CharacterManagerElementUnregistered args)
  {
    StatisticValuesGroup group = CharacterManager.Current.StatisticsCalculator.StatisticValues.GetGroup("companion:perception:passive", false);
    if (group != null)
      this.PassivePerception = group.Sum() + this.Skills.Perception.FinalBonus;
    else
      this.PassivePerception = 10 + this.Skills.Perception.FinalBonus;
    this.OnPropertyChanged("IsAcrobaticsExpert");
    this.OnPropertyChanged("IsAnimalHandlingExpert");
    this.OnPropertyChanged("IsArcanaExpert");
    this.OnPropertyChanged("IsAthleticsExpert");
    this.OnPropertyChanged("IsDeceptionExpert");
    this.OnPropertyChanged("IsHistoryExpert");
    this.OnPropertyChanged("IsInsightExpert");
    this.OnPropertyChanged("IsIntimidationExpert");
    this.OnPropertyChanged("IsInvestigationExpert");
    this.OnPropertyChanged("IsMedicineExpert");
    this.OnPropertyChanged("IsNatureExpert");
    this.OnPropertyChanged("IsPerceptionExpert");
    this.OnPropertyChanged("IsPerformanceExpert");
    this.OnPropertyChanged("IsPersuasionExpert");
    this.OnPropertyChanged("IsReligionExpert");
    this.OnPropertyChanged("IsSleightOfHandExpert");
    this.OnPropertyChanged("IsStealthExpert");
    this.OnPropertyChanged("IsSurvivalExpert");
  }
}
