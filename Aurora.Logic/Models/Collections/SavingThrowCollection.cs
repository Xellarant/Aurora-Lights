// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.Collections.SavingThrowCollection
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using System.Collections.Generic;

#nullable disable
namespace Builder.Presentation.Models.Collections;

public class SavingThrowCollection
{
  private readonly List<SavingThrowItem> _collection;

  public SavingThrowCollection(AbilitiesCollection abilities)
  {
    this.Strength = new SavingThrowItem(abilities.Strength);
    this.Dexterity = new SavingThrowItem(abilities.Dexterity);
    this.Constitution = new SavingThrowItem(abilities.Constitution);
    this.Intelligence = new SavingThrowItem(abilities.Intelligence);
    this.Wisdom = new SavingThrowItem(abilities.Wisdom);
    this.Charisma = new SavingThrowItem(abilities.Charisma);
    this._collection = new List<SavingThrowItem>()
    {
      this.Strength,
      this.Dexterity,
      this.Constitution,
      this.Intelligence,
      this.Wisdom,
      this.Charisma
    };
  }

  public SavingThrowItem Strength { get; }

  public SavingThrowItem Dexterity { get; }

  public SavingThrowItem Constitution { get; }

  public SavingThrowItem Intelligence { get; }

  public SavingThrowItem Wisdom { get; }

  public SavingThrowItem Charisma { get; }

  public IEnumerable<SavingThrowItem> GetCollection()
  {
    return (IEnumerable<SavingThrowItem>) this._collection;
  }
}
