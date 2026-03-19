// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.CharacterSheet.Content.CompanionSheetExportContent
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

#nullable disable
namespace Builder.Presentation.Models.CharacterSheet.Content;

public class CompanionSheetExportContent
{
  public string Name { get; set; }

  public string Kind { get; set; }

  public string Build { get; set; }

  public string Skills { get; set; }

  public string CompanionAppearance { get; set; }

  public string CompanionPortrait { get; set; }

  public string CompanionProficiency { get; set; }

  public CharacterSheetExportContent.ArmorClassExportContent CompanionArmorClassContent { get; } = new CharacterSheetExportContent.ArmorClassExportContent();

  public CharacterSheetExportContent.AbilitiesExportContent CompanionAbilitiesContent { get; } = new CharacterSheetExportContent.AbilitiesExportContent();

  public CharacterSheetExportContent.SkillsExportContent CompanionSkillsContent { get; } = new CharacterSheetExportContent.SkillsExportContent();

  public CharacterSheetExportContent.ConditionsExportContent CompanionConditionsContent { get; } = new CharacterSheetExportContent.ConditionsExportContent();

  public CharacterSheetExportContent.HitPointsExportContent CompanionHitPointsContent { get; } = new CharacterSheetExportContent.HitPointsExportContent();
}
