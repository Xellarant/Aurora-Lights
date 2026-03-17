// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.UserControls.Spellcasting.SpellcastingSectionSlots
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core;

#nullable disable
namespace Builder.Presentation.UserControls.Spellcasting;

public class SpellcastingSectionSlots : ObservableObject
{
  private int _slot1;
  private int _slot2;
  private int _slot3;
  private int _slot4;
  private int _slot5;
  private int _slot6;
  private int _slot7;
  private int _slot8;
  private int _slot9;

  public int Slot1
  {
    get => this._slot1;
    set => this.SetProperty<int>(ref this._slot1, value, nameof (Slot1));
  }

  public int Slot2
  {
    get => this._slot2;
    set => this.SetProperty<int>(ref this._slot2, value, nameof (Slot2));
  }

  public int Slot3
  {
    get => this._slot3;
    set => this.SetProperty<int>(ref this._slot3, value, nameof (Slot3));
  }

  public int Slot4
  {
    get => this._slot4;
    set => this.SetProperty<int>(ref this._slot4, value, nameof (Slot4));
  }

  public int Slot5
  {
    get => this._slot5;
    set => this.SetProperty<int>(ref this._slot5, value, nameof (Slot5));
  }

  public int Slot6
  {
    get => this._slot6;
    set => this.SetProperty<int>(ref this._slot6, value, nameof (Slot6));
  }

  public int Slot7
  {
    get => this._slot7;
    set => this.SetProperty<int>(ref this._slot7, value, nameof (Slot7));
  }

  public int Slot8
  {
    get => this._slot8;
    set => this.SetProperty<int>(ref this._slot8, value, nameof (Slot8));
  }

  public int Slot9
  {
    get => this._slot9;
    set => this.SetProperty<int>(ref this._slot9, value, nameof (Slot9));
  }

  public void Clear()
  {
    this.Slot1 = 0;
    this.Slot2 = 0;
    this.Slot3 = 0;
    this.Slot4 = 0;
    this.Slot5 = 0;
    this.Slot6 = 0;
    this.Slot7 = 0;
    this.Slot8 = 0;
    this.Slot9 = 0;
  }
}
