// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.CharacterSheet.Content.CharacterSheetSpellcastingPageExportContent
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using System.Collections.Generic;

#nullable disable
namespace Builder.Presentation.Models.CharacterSheet.Content;

public class CharacterSheetSpellcastingPageExportContent
{
  public CharacterSheetSpellcastingPageExportContent()
  {
    this.SpellcastingClass = "";
    this.SpellcastingArchetype = "";
    this.Ability = "";
    this.AttackBonus = "";
    this.Save = "";
    this.PrepareCount = "";
    this.Notes = "";
    this.Notes = "";
    this.IncludeHeader = true;
  }

  public bool IncludeHeader { get; set; }

  public string SpellcastingClass { get; set; }

  public string SpellcastingArchetype { get; set; }

  public string Ability { get; set; }

  public string AttackBonus { get; set; }

  public string Save { get; set; }

  public string PrepareCount { get; set; }

  public string Notes { get; set; }

  public bool IsMulticlassSpellcaster { get; set; }

  public CharacterSheetSpellcastingPageExportContent.SpellcastingLevelExportContent Cantrips { get; set; } = new CharacterSheetSpellcastingPageExportContent.SpellcastingLevelExportContent();

  public CharacterSheetSpellcastingPageExportContent.SpellcastingLevelExportContent Spells1 { get; set; } = new CharacterSheetSpellcastingPageExportContent.SpellcastingLevelExportContent();

  public CharacterSheetSpellcastingPageExportContent.SpellcastingLevelExportContent Spells2 { get; set; } = new CharacterSheetSpellcastingPageExportContent.SpellcastingLevelExportContent();

  public CharacterSheetSpellcastingPageExportContent.SpellcastingLevelExportContent Spells3 { get; set; } = new CharacterSheetSpellcastingPageExportContent.SpellcastingLevelExportContent();

  public CharacterSheetSpellcastingPageExportContent.SpellcastingLevelExportContent Spells4 { get; set; } = new CharacterSheetSpellcastingPageExportContent.SpellcastingLevelExportContent();

  public CharacterSheetSpellcastingPageExportContent.SpellcastingLevelExportContent Spells5 { get; set; } = new CharacterSheetSpellcastingPageExportContent.SpellcastingLevelExportContent();

  public CharacterSheetSpellcastingPageExportContent.SpellcastingLevelExportContent Spells6 { get; set; } = new CharacterSheetSpellcastingPageExportContent.SpellcastingLevelExportContent();

  public CharacterSheetSpellcastingPageExportContent.SpellcastingLevelExportContent Spells7 { get; set; } = new CharacterSheetSpellcastingPageExportContent.SpellcastingLevelExportContent();

  public CharacterSheetSpellcastingPageExportContent.SpellcastingLevelExportContent Spells8 { get; set; } = new CharacterSheetSpellcastingPageExportContent.SpellcastingLevelExportContent();

  public CharacterSheetSpellcastingPageExportContent.SpellcastingLevelExportContent Spells9 { get; set; } = new CharacterSheetSpellcastingPageExportContent.SpellcastingLevelExportContent();

  public override string ToString()
  {
    return string.IsNullOrWhiteSpace(this.SpellcastingArchetype) || this.SpellcastingClass.Equals(this.SpellcastingArchetype) ? this.SpellcastingClass : $"{this.SpellcastingClass}, {this.SpellcastingArchetype}";
  }

  public class SpellcastingLevelExportContent
  {
    public int Level { get; set; }

    public int AvailableSlots { get; set; }

    public int ExpendedSlots { get; set; }

    public List<CharacterSheetSpellcastingPageExportContent.SpellExportContent> Spells { get; set; } = new List<CharacterSheetSpellcastingPageExportContent.SpellExportContent>();

    public CharacterSheetSpellcastingPageExportContent.SpellExportContent Get(int index)
    {
      if (this.Spells.Count == 0)
        return (CharacterSheetSpellcastingPageExportContent.SpellExportContent) null;
      return this.Spells.Count - 1 < index ? (CharacterSheetSpellcastingPageExportContent.SpellExportContent) null : this.Spells[index];
    }
  }

  public class SpellExportContent
  {
    public bool IsPrepared { get; set; }

    public bool AlwaysPrepared { get; set; }

    public string Name { get; set; }

    public string Subtitle { get; set; }

    public string Description { get; set; }

    public string CastingTime { get; set; }

    public string Range { get; set; }

    public string Duration { get; set; }

    public string Components { get; set; }

    public string Level { get; set; }

    public string School { get; set; }

    public bool Ritual { get; set; }

    public bool Concentration { get; set; }

    public string GetDisplayName() => this.ToString();

    public override string ToString()
    {
      return !this.AlwaysPrepared ? this.Name : this.Name + " (Always Prepared)";
    }
  }
}
