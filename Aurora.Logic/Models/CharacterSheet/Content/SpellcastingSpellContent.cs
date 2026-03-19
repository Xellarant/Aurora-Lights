// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.CharacterSheet.Content.SpellcastingSpellContent
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core;

#nullable disable
namespace Builder.Presentation.Models.CharacterSheet.Content;

public class SpellcastingSpellContent : ObservableObject
{
  private string _name;
  private bool _isPrepared;

  public SpellcastingSpellContent(string name, bool isPrepared = false)
  {
    this._name = name;
    this._isPrepared = isPrepared;
  }

  public string Name
  {
    get => this._name;
    set => this.SetProperty<string>(ref this._name, value, nameof (Name));
  }

  public bool IsPrepared
  {
    get => this._isPrepared;
    set => this.SetProperty<bool>(ref this._isPrepared, value, nameof (IsPrepared));
  }
}
