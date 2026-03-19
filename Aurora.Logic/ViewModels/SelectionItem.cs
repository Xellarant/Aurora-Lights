// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.SelectionItem
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core;

#nullable disable
namespace Builder.Presentation.ViewModels;

public class SelectionItem : ObservableObject
{
  private string _displayName;
  private int _value;

  public SelectionItem(string displayName, int value)
  {
    this._displayName = displayName;
    this._value = value;
  }

  public string DisplayName
  {
    get => this._displayName;
    set => this.SetProperty<string>(ref this._displayName, value, nameof (DisplayName));
  }

  public int Value
  {
    get => this._value;
    set => this.SetProperty<int>(ref this._value, value, nameof (Value));
  }

  public override string ToString() => this.DisplayName;
}
