// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.Helpers.Progress
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core;

#nullable disable
namespace Builder.Presentation.Models.Helpers;

public class Progress : ObservableObject
{
  private string _message;
  private int _percentage;
  private bool _inProgress;
  private bool _completed;

  public string Message
  {
    get => this._message;
    set => this.SetProperty<string>(ref this._message, value, nameof (Message));
  }

  public int Percentage
  {
    get => this._percentage;
    set => this.SetProperty<int>(ref this._percentage, value, nameof (Percentage));
  }

  public bool InProgress
  {
    get => this._inProgress;
    set => this.SetProperty<bool>(ref this._inProgress, value, nameof (InProgress));
  }

  public bool Completed
  {
    get => this._completed;
    set => this.SetProperty<bool>(ref this._completed, value, nameof (Completed));
  }
}
