// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.Shell.Manage.SpellcastingCollection
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core;

#nullable disable
namespace Builder.Presentation.ViewModels.Shell.Manage;

public class SpellcastingCollection : ObservableObject
{
  private string _cantripSlot1;
  private string _cantripSlot2;
  private string _cantripSlot3;
  private string _cantripSlot4;
  private string _cantripSlot5;
  private string _cantripSlot6;
  private string _cantripSlot7;
  private string _cantripSlot8;
  private string _spellcastingClass;
  private string _spellcastingAbility;
  private string _spellcastingDifficultyClass;
  private string _spellcastingAttackBonus;

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

  public string SpellcastingDifficultyClass
  {
    get => this._spellcastingDifficultyClass;
    set
    {
      this.SetProperty<string>(ref this._spellcastingDifficultyClass, value, nameof (SpellcastingDifficultyClass));
    }
  }

  public string SpellcastingAttackBonus
  {
    get => this._spellcastingAttackBonus;
    set
    {
      this.SetProperty<string>(ref this._spellcastingAttackBonus, value, nameof (SpellcastingAttackBonus));
    }
  }

  public string CantripSlot1
  {
    get => this._cantripSlot1;
    set => this.SetProperty<string>(ref this._cantripSlot1, value, nameof (CantripSlot1));
  }

  public string CantripSlot2
  {
    get => this._cantripSlot2;
    set => this.SetProperty<string>(ref this._cantripSlot2, value, nameof (CantripSlot2));
  }

  public string CantripSlot3
  {
    get => this._cantripSlot3;
    set => this.SetProperty<string>(ref this._cantripSlot3, value, nameof (CantripSlot3));
  }

  public string CantripSlot4
  {
    get => this._cantripSlot4;
    set => this.SetProperty<string>(ref this._cantripSlot4, value, nameof (CantripSlot4));
  }

  public string CantripSlot5
  {
    get => this._cantripSlot5;
    set => this.SetProperty<string>(ref this._cantripSlot5, value, nameof (CantripSlot5));
  }

  public string CantripSlot6
  {
    get => this._cantripSlot6;
    set => this.SetProperty<string>(ref this._cantripSlot6, value, nameof (CantripSlot6));
  }

  public string CantripSlot7
  {
    get => this._cantripSlot7;
    set => this.SetProperty<string>(ref this._cantripSlot7, value, nameof (CantripSlot7));
  }

  public string CantripSlot8
  {
    get => this._cantripSlot8;
    set => this.SetProperty<string>(ref this._cantripSlot8, value, nameof (CantripSlot8));
  }

  public ObservableSpell Spell1Slot1 { get; } = new ObservableSpell();

  public ObservableSpell Spell1Slot2 { get; } = new ObservableSpell();

  public ObservableSpell Spell1Slot3 { get; } = new ObservableSpell();

  public ObservableSpell Spell1Slot4 { get; } = new ObservableSpell();

  public ObservableSpell Spell1Slot5 { get; } = new ObservableSpell();

  public ObservableSpell Spell1Slot6 { get; } = new ObservableSpell();

  public ObservableSpell Spell1Slot7 { get; } = new ObservableSpell();

  public ObservableSpell Spell1Slot8 { get; } = new ObservableSpell();

  public ObservableSpell Spell1Slot9 { get; } = new ObservableSpell();

  public ObservableSpell Spell1Slot10 { get; } = new ObservableSpell();

  public ObservableSpell Spell1Slot11 { get; } = new ObservableSpell();

  public ObservableSpell Spell1Slot12 { get; } = new ObservableSpell();

  public ObservableSpell Spell2Slot1 { get; } = new ObservableSpell();

  public ObservableSpell Spell2Slot2 { get; } = new ObservableSpell();

  public ObservableSpell Spell2Slot3 { get; } = new ObservableSpell();

  public ObservableSpell Spell2Slot4 { get; } = new ObservableSpell();

  public ObservableSpell Spell2Slot5 { get; } = new ObservableSpell();

  public ObservableSpell Spell2Slot6 { get; } = new ObservableSpell();

  public ObservableSpell Spell2Slot7 { get; } = new ObservableSpell();

  public ObservableSpell Spell2Slot8 { get; } = new ObservableSpell();

  public ObservableSpell Spell2Slot9 { get; } = new ObservableSpell();

  public ObservableSpell Spell2Slot10 { get; } = new ObservableSpell();

  public ObservableSpell Spell2Slot11 { get; } = new ObservableSpell();

  public ObservableSpell Spell2Slot12 { get; } = new ObservableSpell();

  public ObservableSpell Spell2Slot13 { get; } = new ObservableSpell();

  public ObservableSpell Spell3Slot1 { get; } = new ObservableSpell();

  public ObservableSpell Spell3Slot2 { get; } = new ObservableSpell();

  public ObservableSpell Spell3Slot3 { get; } = new ObservableSpell();

  public ObservableSpell Spell3Slot4 { get; } = new ObservableSpell();

  public ObservableSpell Spell3Slot5 { get; } = new ObservableSpell();

  public ObservableSpell Spell3Slot6 { get; } = new ObservableSpell();

  public ObservableSpell Spell3Slot7 { get; } = new ObservableSpell();

  public ObservableSpell Spell3Slot8 { get; } = new ObservableSpell();

  public ObservableSpell Spell3Slot9 { get; } = new ObservableSpell();

  public ObservableSpell Spell3Slot10 { get; } = new ObservableSpell();

  public ObservableSpell Spell3Slot11 { get; } = new ObservableSpell();

  public ObservableSpell Spell3Slot12 { get; } = new ObservableSpell();

  public ObservableSpell Spell3Slot13 { get; } = new ObservableSpell();

  public ObservableSpell Spell4Slot1 { get; } = new ObservableSpell();

  public ObservableSpell Spell4Slot2 { get; } = new ObservableSpell();

  public ObservableSpell Spell4Slot3 { get; } = new ObservableSpell();

  public ObservableSpell Spell4Slot4 { get; } = new ObservableSpell();

  public ObservableSpell Spell4Slot5 { get; } = new ObservableSpell();

  public ObservableSpell Spell4Slot6 { get; } = new ObservableSpell();

  public ObservableSpell Spell4Slot7 { get; } = new ObservableSpell();

  public ObservableSpell Spell4Slot8 { get; } = new ObservableSpell();

  public ObservableSpell Spell4Slot9 { get; } = new ObservableSpell();

  public ObservableSpell Spell4Slot10 { get; } = new ObservableSpell();

  public ObservableSpell Spell4Slot11 { get; } = new ObservableSpell();

  public ObservableSpell Spell4Slot12 { get; } = new ObservableSpell();

  public ObservableSpell Spell4Slot13 { get; } = new ObservableSpell();

  public ObservableSpell Spell5Slot1 { get; } = new ObservableSpell();

  public ObservableSpell Spell5Slot2 { get; } = new ObservableSpell();

  public ObservableSpell Spell5Slot3 { get; } = new ObservableSpell();

  public ObservableSpell Spell5Slot4 { get; } = new ObservableSpell();

  public ObservableSpell Spell5Slot5 { get; } = new ObservableSpell();

  public ObservableSpell Spell5Slot6 { get; } = new ObservableSpell();

  public ObservableSpell Spell5Slot7 { get; } = new ObservableSpell();

  public ObservableSpell Spell5Slot8 { get; } = new ObservableSpell();

  public ObservableSpell Spell5Slot9 { get; } = new ObservableSpell();

  public ObservableSpell Spell6Slot1 { get; } = new ObservableSpell();

  public ObservableSpell Spell6Slot2 { get; } = new ObservableSpell();

  public ObservableSpell Spell6Slot3 { get; } = new ObservableSpell();

  public ObservableSpell Spell6Slot4 { get; } = new ObservableSpell();

  public ObservableSpell Spell6Slot5 { get; } = new ObservableSpell();

  public ObservableSpell Spell6Slot6 { get; } = new ObservableSpell();

  public ObservableSpell Spell6Slot7 { get; } = new ObservableSpell();

  public ObservableSpell Spell6Slot8 { get; } = new ObservableSpell();

  public ObservableSpell Spell6Slot9 { get; } = new ObservableSpell();

  public ObservableSpell Spell7Slot1 { get; } = new ObservableSpell();

  public ObservableSpell Spell7Slot2 { get; } = new ObservableSpell();

  public ObservableSpell Spell7Slot3 { get; } = new ObservableSpell();

  public ObservableSpell Spell7Slot4 { get; } = new ObservableSpell();

  public ObservableSpell Spell7Slot5 { get; } = new ObservableSpell();

  public ObservableSpell Spell7Slot6 { get; } = new ObservableSpell();

  public ObservableSpell Spell7Slot7 { get; } = new ObservableSpell();

  public ObservableSpell Spell7Slot8 { get; } = new ObservableSpell();

  public ObservableSpell Spell7Slot9 { get; } = new ObservableSpell();

  public ObservableSpell Spell8Slot1 { get; } = new ObservableSpell();

  public ObservableSpell Spell8Slot2 { get; } = new ObservableSpell();

  public ObservableSpell Spell8Slot3 { get; } = new ObservableSpell();

  public ObservableSpell Spell8Slot4 { get; } = new ObservableSpell();

  public ObservableSpell Spell8Slot5 { get; } = new ObservableSpell();

  public ObservableSpell Spell8Slot6 { get; } = new ObservableSpell();

  public ObservableSpell Spell8Slot7 { get; } = new ObservableSpell();

  public ObservableSpell Spell9Slot1 { get; } = new ObservableSpell();

  public ObservableSpell Spell9Slot2 { get; } = new ObservableSpell();

  public ObservableSpell Spell9Slot3 { get; } = new ObservableSpell();

  public ObservableSpell Spell9Slot4 { get; } = new ObservableSpell();

  public ObservableSpell Spell9Slot5 { get; } = new ObservableSpell();

  public ObservableSpell Spell9Slot6 { get; } = new ObservableSpell();

  public ObservableSpell Spell9Slot7 { get; } = new ObservableSpell();

  public void Reset()
  {
    this.SpellcastingClass = "";
    this.SpellcastingAbility = "";
    this.SpellcastingDifficultyClass = "";
    this.SpellcastingAttackBonus = "";
    this.CantripSlot1 = "";
    this.CantripSlot2 = "";
    this.CantripSlot3 = "";
    this.CantripSlot4 = "";
    this.CantripSlot5 = "";
    this.CantripSlot6 = "";
    this.CantripSlot7 = "";
    this.CantripSlot8 = "";
    this.Spell1Slot1.Reset();
    this.Spell1Slot2.Reset();
    this.Spell1Slot3.Reset();
    this.Spell1Slot4.Reset();
    this.Spell1Slot5.Reset();
    this.Spell1Slot6.Reset();
    this.Spell1Slot7.Reset();
    this.Spell1Slot8.Reset();
    this.Spell1Slot9.Reset();
    this.Spell1Slot10.Reset();
    this.Spell1Slot11.Reset();
    this.Spell1Slot12.Reset();
    this.Spell2Slot1.Reset();
    this.Spell2Slot2.Reset();
    this.Spell2Slot3.Reset();
    this.Spell2Slot4.Reset();
    this.Spell2Slot5.Reset();
    this.Spell2Slot6.Reset();
    this.Spell2Slot7.Reset();
    this.Spell2Slot8.Reset();
    this.Spell2Slot9.Reset();
    this.Spell2Slot10.Reset();
    this.Spell2Slot11.Reset();
    this.Spell2Slot12.Reset();
    this.Spell2Slot13.Reset();
    this.Spell3Slot1.Reset();
    this.Spell3Slot2.Reset();
    this.Spell3Slot3.Reset();
    this.Spell3Slot4.Reset();
    this.Spell3Slot5.Reset();
    this.Spell3Slot6.Reset();
    this.Spell3Slot7.Reset();
    this.Spell3Slot8.Reset();
    this.Spell3Slot9.Reset();
    this.Spell3Slot10.Reset();
    this.Spell3Slot11.Reset();
    this.Spell3Slot12.Reset();
    this.Spell3Slot13.Reset();
    this.Spell4Slot1.Reset();
    this.Spell4Slot2.Reset();
    this.Spell4Slot3.Reset();
    this.Spell4Slot4.Reset();
    this.Spell4Slot5.Reset();
    this.Spell4Slot6.Reset();
    this.Spell4Slot7.Reset();
    this.Spell4Slot8.Reset();
    this.Spell4Slot9.Reset();
    this.Spell4Slot10.Reset();
    this.Spell4Slot11.Reset();
    this.Spell4Slot12.Reset();
    this.Spell4Slot13.Reset();
    this.Spell5Slot1.Reset();
    this.Spell5Slot2.Reset();
    this.Spell5Slot3.Reset();
    this.Spell5Slot4.Reset();
    this.Spell5Slot5.Reset();
    this.Spell5Slot6.Reset();
    this.Spell5Slot7.Reset();
    this.Spell5Slot8.Reset();
    this.Spell5Slot9.Reset();
    this.Spell6Slot1.Reset();
    this.Spell6Slot2.Reset();
    this.Spell6Slot3.Reset();
    this.Spell6Slot4.Reset();
    this.Spell6Slot5.Reset();
    this.Spell6Slot6.Reset();
    this.Spell6Slot7.Reset();
    this.Spell6Slot8.Reset();
    this.Spell6Slot9.Reset();
    this.Spell7Slot1.Reset();
    this.Spell7Slot2.Reset();
    this.Spell7Slot3.Reset();
    this.Spell7Slot4.Reset();
    this.Spell7Slot5.Reset();
    this.Spell7Slot6.Reset();
    this.Spell7Slot7.Reset();
    this.Spell7Slot8.Reset();
    this.Spell7Slot9.Reset();
    this.Spell8Slot1.Reset();
    this.Spell8Slot2.Reset();
    this.Spell8Slot3.Reset();
    this.Spell8Slot4.Reset();
    this.Spell8Slot5.Reset();
    this.Spell8Slot6.Reset();
    this.Spell8Slot7.Reset();
    this.Spell9Slot1.Reset();
    this.Spell9Slot2.Reset();
    this.Spell9Slot3.Reset();
    this.Spell9Slot4.Reset();
    this.Spell9Slot5.Reset();
    this.Spell9Slot6.Reset();
    this.Spell9Slot7.Reset();
  }
}
