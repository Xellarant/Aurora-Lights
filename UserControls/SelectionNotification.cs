// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.UserControls.SelectionNotification
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core;

#nullable disable
namespace Builder.Presentation.UserControls;

public class SelectionNotification : ObservableObject
{
  private string _title;
  private string _message;
  private bool _isActive;

  public SelectionNotification(string title, string message)
  {
    this._title = title;
    this._message = message;
  }

  public string Title
  {
    get => this._title;
    set => this.SetProperty<string>(ref this._title, value, nameof (Title));
  }

  public string Message
  {
    get => this._message;
    set => this.SetProperty<string>(ref this._message, value, nameof (Message));
  }

  public bool IsActive
  {
    get => this._isActive;
    set => this.SetProperty<bool>(ref this._isActive, value, nameof (IsActive));
  }
}
