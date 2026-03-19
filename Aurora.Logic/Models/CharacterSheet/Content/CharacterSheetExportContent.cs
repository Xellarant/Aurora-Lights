// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.CharacterSheet.Content.CharacterSheetExportContent
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace Builder.Presentation.Models.CharacterSheet.Content;

public class CharacterSheetExportContent
{
  public string PlayerName { get; set; }

  public string CharacterName { get; set; }

  public string Race { get; set; }

  public string Alignment { get; set; }

  public string Deity { get; set; }

  public string Domain { get; set; }

  public string Experience { get; set; }

  public string ProficiencyBonus { get; set; }

  public string Level { get; set; }

  public string Initiative { get; set; }

  public string AttacksCount { get; set; }

  public bool Inspiration { get; set; }

  public bool InitiativeAdvantage { get; set; }

  public CharacterSheetExportContent.ClassExportContent MainClass { get; } = new CharacterSheetExportContent.ClassExportContent();

  public bool IsMulticlass { get; set; }

  public List<CharacterSheetExportContent.ClassExportContent> Multiclass { get; } = new List<CharacterSheetExportContent.ClassExportContent>();

  public List<CharacterSheetExportContent.AttackExportContent> AttacksContent { get; } = new List<CharacterSheetExportContent.AttackExportContent>();

  public string AttackAndSpellcastingField { get; set; }

  public string Languages { get; set; }

  public string ArmorProficiencies { get; set; }

  public string WeaponProficiencies { get; set; }

  public string ToolProficiencies { get; set; }

  public string Features { get; set; }

  public string TemporaryRacialTraits { get; set; }

  public string AlliesAndOrganizations { get; set; }

  public string OrganizationName { get; set; }

  public string OrganizationSymbol { get; set; }

  public string AdditionalFeaturesAndTraits { get; set; }

  public string Treasure { get; set; }

  public CharacterSheetExportContent.AbilitiesExportContent AbilitiesContent { get; } = new CharacterSheetExportContent.AbilitiesExportContent();

  public CharacterSheetExportContent.SkillsExportContent SkillsContent { get; } = new CharacterSheetExportContent.SkillsExportContent();

  public CharacterSheetExportContent.ArmorClassExportContent ArmorClassContent { get; } = new CharacterSheetExportContent.ArmorClassExportContent();

  public CharacterSheetExportContent.HitPointsExportContent HitPointsContent { get; } = new CharacterSheetExportContent.HitPointsExportContent();

  public CharacterSheetExportContent.ConditionsExportContent ConditionsContent { get; } = new CharacterSheetExportContent.ConditionsExportContent();

  public CharacterSheetExportContent.EquipmentExportContent EquipmentContent { get; } = new CharacterSheetExportContent.EquipmentExportContent();

  public CharacterSheetExportContent.BackgroundExportContent BackgroundContent { get; } = new CharacterSheetExportContent.BackgroundExportContent();

  public CharacterSheetExportContent.AppearanceExportContent AppearanceContent { get; } = new CharacterSheetExportContent.AppearanceExportContent();

  public string CompanionName { get; set; }

  public string CompanionKind { get; set; }

  public string CompanionBuild { get; set; }

  public string CompanionOwner { get; set; }

  public string CompanionChallenge { get; set; }

  public string CompanionAppearance { get; set; }

  public string CompanionPortrait { get; set; }

  public string CompanionSkills { get; set; }

  public string CompanionProficiency { get; set; }

  public string CompanionInitiative { get; set; }

  public string CompanionFeatures { get; set; }

  public string CompanionStats { get; set; }

  public string CompanionSpeedString { get; set; }

  public CharacterSheetExportContent.ArmorClassExportContent CompanionArmorClassContent { get; } = new CharacterSheetExportContent.ArmorClassExportContent();

  public CharacterSheetExportContent.AbilitiesExportContent CompanionAbilitiesContent { get; } = new CharacterSheetExportContent.AbilitiesExportContent();

  public CharacterSheetExportContent.SkillsExportContent CompanionSkillsContent { get; } = new CharacterSheetExportContent.SkillsExportContent();

  public CharacterSheetExportContent.ConditionsExportContent CompanionConditionsContent { get; } = new CharacterSheetExportContent.ConditionsExportContent();

  public CharacterSheetExportContent.HitPointsExportContent CompanionHitPointsContent { get; } = new CharacterSheetExportContent.HitPointsExportContent();

  public string GetClassBuild()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append($"Level {this.Level} {this.Race} ");
    if (this.IsMulticlass)
    {
      if (this.Multiclass.Count >= 2)
      {
        stringBuilder.Append($"{this.MainClass.Name} ({this.MainClass.Level})");
        foreach (CharacterSheetExportContent.ClassExportContent classExportContent in this.Multiclass)
          stringBuilder.Append($" / {classExportContent.Name} ({classExportContent.Level})");
      }
      else
      {
        stringBuilder.Append($"{this.MainClass} ({this.MainClass.Level})");
        foreach (CharacterSheetExportContent.ClassExportContent classExportContent in this.Multiclass)
          stringBuilder.Append($" / {classExportContent} ({classExportContent.Level})");
      }
    }
    else
      stringBuilder.Append((object) this.MainClass);
    return stringBuilder.ToString();
  }

  public class AbilitiesExportContent
  {
    public string Strength { get; set; }

    public string Dexterity { get; set; }

    public string Constitution { get; set; }

    public string Intelligence { get; set; }

    public string Wisdom { get; set; }

    public string Charisma { get; set; }

    public string StrengthModifier { get; set; }

    public string DexterityModifier { get; set; }

    public string ConstitutionModifier { get; set; }

    public string IntelligenceModifier { get; set; }

    public string WisdomModifier { get; set; }

    public string CharismaModifier { get; set; }

    public string StrengthSave { get; set; }

    public string DexteritySave { get; set; }

    public string ConstitutionSave { get; set; }

    public string IntelligenceSave { get; set; }

    public string WisdomSave { get; set; }

    public string CharismaSave { get; set; }

    public bool StrengthSaveProficient { get; set; }

    public bool DexteritySaveProficient { get; set; }

    public bool ConstitutionSaveProficient { get; set; }

    public bool IntelligenceSaveProficient { get; set; }

    public bool WisdomSaveProficient { get; set; }

    public bool CharismaSaveProficient { get; set; }

    public string ConditionalSave { get; set; }
  }

  public class SkillsExportContent
  {
    public string Acrobatics { get; set; }

    public string AnimalHandling { get; set; }

    public string Athletics { get; set; }

    public string Deception { get; set; }

    public string History { get; set; }

    public string Insight { get; set; }

    public string Intimidation { get; set; }

    public string Investigation { get; set; }

    public string Arcana { get; set; }

    public string Perception { get; set; }

    public string Nature { get; set; }

    public string Performance { get; set; }

    public string Medicine { get; set; }

    public string Religion { get; set; }

    public string Stealth { get; set; }

    public string Persuasion { get; set; }

    public string SleightOfHand { get; set; }

    public string Survival { get; set; }

    public bool AcrobaticsProficient { get; set; }

    public bool AnimalHandlingProficient { get; set; }

    public bool AthleticsProficient { get; set; }

    public bool DeceptionProficient { get; set; }

    public bool HistoryProficient { get; set; }

    public bool InsightProficient { get; set; }

    public bool IntimidationProficient { get; set; }

    public bool InvestigationProficient { get; set; }

    public bool ArcanaProficient { get; set; }

    public bool PerceptionProficient { get; set; }

    public bool NatureProficient { get; set; }

    public bool PerformanceProficient { get; set; }

    public bool MedicineProficient { get; set; }

    public bool ReligionProficient { get; set; }

    public bool StealthProficient { get; set; }

    public bool PersuasionProficient { get; set; }

    public bool SleightOfHandProficient { get; set; }

    public bool SurvivalProficient { get; set; }

    public bool AcrobaticsExpertise { get; set; }

    public bool AnimalHandlingExpertise { get; set; }

    public bool AthleticsExpertise { get; set; }

    public bool DeceptionExpertise { get; set; }

    public bool HistoryExpertise { get; set; }

    public bool InsightExpertise { get; set; }

    public bool IntimidationExpertise { get; set; }

    public bool InvestigationExpertise { get; set; }

    public bool ArcanaExpertise { get; set; }

    public bool PerceptionExpertise { get; set; }

    public bool NatureExpertise { get; set; }

    public bool PerformanceExpertise { get; set; }

    public bool MedicineExpertise { get; set; }

    public bool ReligionExpertise { get; set; }

    public bool StealthExpertise { get; set; }

    public bool PersuasionExpertise { get; set; }

    public bool SleightOfHandExpertise { get; set; }

    public bool SurvivalExpertise { get; set; }

    public string PerceptionPassive { get; set; }
  }

  public class ArmorClassExportContent
  {
    public string ArmorClass { get; set; }

    public string EquippedArmor { get; set; }

    public string EquippedShield { get; set; }

    public bool StealthDisadvantage { get; set; }

    public string ConditionalArmorClass { get; set; }
  }

  public class HitPointsExportContent
  {
    public string Current { get; set; }

    public string Maximum { get; set; }

    public string Temporary { get; set; }

    public string HitDice { get; set; }

    public bool DeathSavingThrowSuccess1 { get; set; }

    public bool DeathSavingThrowSuccess2 { get; set; }

    public bool DeathSavingThrowSuccess3 { get; set; }

    public bool DeathSavingThrowFailure1 { get; set; }

    public bool DeathSavingThrowFailure2 { get; set; }

    public bool DeathSavingThrowFailure3 { get; set; }
  }

  public class ConditionsExportContent
  {
    public string WalkingSpeed { get; set; }

    public string FlySpeed { get; set; }

    public string ClimbSpeed { get; set; }

    public string SwimSpeed { get; set; }

    public string BurrowSpeed { get; set; }

    public string Vision { get; set; }

    public string Exhaustion { get; set; }

    public string Resistances { get; set; }

    public string DamageResistances { get; set; }

    public string ConditionResistances { get; set; }
  }

  public class EquipmentExportContent
  {
    public string Copper { get; set; }

    public string Silver { get; set; }

    public string Electrum { get; set; }

    public string Gold { get; set; }

    public string Platinum { get; set; }

    public string Weight { get; set; }

    public List<Tuple<string, string>> Equipment { get; } = new List<Tuple<string, string>>();

    public string AdditionalTreasure { get; set; }
  }

  public class BackgroundExportContent
  {
    public string Name { get; set; }

    public string PersonalityTrait { get; set; }

    public string Ideal { get; set; }

    public string Bond { get; set; }

    public string Flaw { get; set; }

    public string FeatureName { get; set; }

    public string FeatureDescription { get; set; }

    public string Trinket { get; set; }

    public string Story { get; set; }
  }

  public class AppearanceExportContent
  {
    public string Gender { get; set; }

    public string Portrait { get; set; }

    public string Age { get; set; }

    public string Height { get; set; }

    public string Weight { get; set; }

    public string Eyes { get; set; }

    public string Skin { get; set; }

    public string Hair { get; set; }

    public string Description { get; set; }
  }

  public class ClassExportContent
  {
    public string Name { get; set; }

    public string Archetype { get; set; }

    public string Level { get; set; }

    public string HitDie { get; set; }

    public override string ToString()
    {
      return string.IsNullOrWhiteSpace(this.Archetype) ? this.Name ?? "" : $"{this.Name}, {this.Archetype}";
    }
  }

  public class AttackExportContent
  {
    public string Name { get; set; }

    public string Underline { get; set; }

    public string Range { get; set; }

    public string Bonus { get; set; }

    public string Damage { get; set; }

    public string Misc { get; set; }

    public string Description { get; set; }

    public string AdditionalEffects { get; set; }

    public string AttackBreakdown { get; set; }

    public string DamageBreakdown { get; set; }

    public bool AsCard { get; set; }
  }
}
