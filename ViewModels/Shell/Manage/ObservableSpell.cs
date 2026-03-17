// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.Shell.Manage.ObservableSpell
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core;

#nullable disable
namespace Builder.Presentation.ViewModels.Shell.Manage;

public class ObservableSpell : ObservableObject
{
  private string _name;
  private bool _isPrepared;

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

  public void Reset()
  {
    this.Name = "";
    this.IsPrepared = false;
  }
}
