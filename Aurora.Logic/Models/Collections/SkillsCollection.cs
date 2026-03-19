// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.Collections.SkillsCollection
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using System.Collections.Generic;

#nullable disable
namespace Builder.Presentation.Models.Collections;

public class SkillsCollection
{
  private readonly List<SkillItem> _collection;

  public SkillsCollection(AbilitiesCollection abilities)
  {
    this.Acrobatics = new SkillItem(nameof (Acrobatics), abilities.Dexterity);
    this.AnimalHandling = new SkillItem("Animal Handling", abilities.Wisdom);
    this.Arcana = new SkillItem(nameof (Arcana), abilities.Intelligence);
    this.Athletics = new SkillItem(nameof (Athletics), abilities.Strength);
    this.Deception = new SkillItem(nameof (Deception), abilities.Charisma);
    this.History = new SkillItem(nameof (History), abilities.Intelligence);
    this.Insight = new SkillItem(nameof (Insight), abilities.Wisdom);
    this.Intimidation = new SkillItem(nameof (Intimidation), abilities.Charisma);
    this.Investigation = new SkillItem(nameof (Investigation), abilities.Intelligence);
    this.Medicine = new SkillItem(nameof (Medicine), abilities.Wisdom);
    this.Nature = new SkillItem(nameof (Nature), abilities.Intelligence);
    this.Perception = new SkillItem(nameof (Perception), abilities.Wisdom);
    this.Performance = new SkillItem(nameof (Performance), abilities.Charisma);
    this.Persuasion = new SkillItem(nameof (Persuasion), abilities.Charisma);
    this.Religion = new SkillItem(nameof (Religion), abilities.Intelligence);
    this.SleightOfHand = new SkillItem("Sleight of Hand", abilities.Dexterity);
    this.Stealth = new SkillItem(nameof (Stealth), abilities.Dexterity);
    this.Survival = new SkillItem(nameof (Survival), abilities.Wisdom);
    this._collection = new List<SkillItem>()
    {
      this.Acrobatics,
      this.AnimalHandling,
      this.Arcana,
      this.Athletics,
      this.Deception,
      this.History,
      this.Insight,
      this.Intimidation,
      this.Investigation,
      this.Medicine,
      this.Nature,
      this.Perception,
      this.Performance,
      this.Persuasion,
      this.Religion,
      this.SleightOfHand,
      this.Stealth,
      this.Survival
    };
  }

  public SkillItem Acrobatics { get; }

  public SkillItem AnimalHandling { get; }

  public SkillItem Arcana { get; }

  public SkillItem Athletics { get; }

  public SkillItem Deception { get; }

  public SkillItem History { get; }

  public SkillItem Insight { get; }

  public SkillItem Intimidation { get; }

  public SkillItem Investigation { get; }

  public SkillItem Medicine { get; }

  public SkillItem Nature { get; }

  public SkillItem Perception { get; }

  public SkillItem Performance { get; }

  public SkillItem Persuasion { get; }

  public SkillItem Religion { get; }

  public SkillItem SleightOfHand { get; }

  public SkillItem Stealth { get; }

  public SkillItem Survival { get; }

  public IEnumerable<SkillItem> GetCollection() => (IEnumerable<SkillItem>) this._collection;
}
