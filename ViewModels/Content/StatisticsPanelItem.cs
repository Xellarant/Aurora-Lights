// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.Content.StatisticsPanelItem
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core;
using Builder.Presentation.Services.Calculator;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Builder.Presentation.ViewModels.Content;

public class StatisticsPanelItem : ObservableObject
{
  private bool _exists;
  private bool _updated;
  private string _displayName;
  private string _displayValue;
  private int _value;
  private string _summery;

  public StatisticsPanelItem(string displayName, string displayValue = "")
  {
    this._displayName = displayName;
    this._displayValue = displayValue;
  }

  public bool Exists
  {
    get => this._exists;
    set => this.SetProperty<bool>(ref this._exists, value, nameof (Exists));
  }

  public bool IsUpdated
  {
    get => this._updated;
    set => this.SetProperty<bool>(ref this._updated, value, nameof (IsUpdated));
  }

  public string DisplayName
  {
    get => this._displayName.ToUpper();
    set => this.SetProperty<string>(ref this._displayName, value, nameof (DisplayName));
  }

  public string DisplayValue
  {
    get => this._displayValue;
    set => this.SetProperty<string>(ref this._displayValue, value, nameof (DisplayValue));
  }

  public int Value
  {
    get => this._value;
    set => this.SetProperty<int>(ref this._value, value, nameof (Value));
  }

  public string Summery
  {
    get => this._summery;
    set => this.SetProperty<string>(ref this._summery, value, nameof (Summery));
  }

  public void Update(StatisticValuesGroup group)
  {
    this.Exists = group != null;
    if (group == null)
      return;
    this.Value = group.Sum();
    this.Summery = string.Join(", ", group.GetValues().Where<KeyValuePair<string, int>>((Func<KeyValuePair<string, int>, bool>) (x => x.Value > 0)).Select<KeyValuePair<string, int>, string>((Func<KeyValuePair<string, int>, string>) (x => $"{x.Key} ({x.Value})")));
    if (this.Value == 0)
      return;
    this.IsUpdated = true;
  }
}
