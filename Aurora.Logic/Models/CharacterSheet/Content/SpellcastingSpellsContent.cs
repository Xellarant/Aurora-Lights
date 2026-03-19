// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.CharacterSheet.Content.SpellcastingSpellsContent
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core;
using System.Collections.ObjectModel;

#nullable disable
namespace Builder.Presentation.Models.CharacterSheet.Content;

public class SpellcastingSpellsContent : ObservableObject
{
  private int _slotsCount;
  private int _expendedSlotsCount;
  private int _remainingSlotsCount;
  private int _maximumListableCount;

  public SpellcastingSpellsContent(int maximumListableCount)
  {
    this._slotsCount = 0;
    this._expendedSlotsCount = 0;
    this._remainingSlotsCount = 0;
    this._maximumListableCount = maximumListableCount;
  }

  public int SlotsCount
  {
    get => this._slotsCount;
    set => this.SetProperty<int>(ref this._slotsCount, value, nameof (SlotsCount));
  }

  public int ExpendedSlotsCount
  {
    get => this._expendedSlotsCount;
    set => this.SetProperty<int>(ref this._expendedSlotsCount, value, nameof (ExpendedSlotsCount));
  }

  public int RemainingSlotsCount
  {
    get => this._remainingSlotsCount;
    set
    {
      this.SetProperty<int>(ref this._remainingSlotsCount, value, nameof (RemainingSlotsCount));
    }
  }

  public int MaximumListableCount
  {
    get => this._maximumListableCount;
    set
    {
      this.SetProperty<int>(ref this._maximumListableCount, value, nameof (MaximumListableCount));
    }
  }

  public ObservableCollection<SpellcastingSpellContent> Collection { get; } = new ObservableCollection<SpellcastingSpellContent>();

  public SpellcastingSpellContent GetSpell(int index, bool returnEmpty = false)
  {
    if (this.Collection.Count > index)
      return this.Collection[index];
    return !returnEmpty ? (SpellcastingSpellContent) null : new SpellcastingSpellContent("");
  }
}
