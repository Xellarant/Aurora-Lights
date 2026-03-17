// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Events.Data.DataManagerProgressChanged
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core.Events;

#nullable disable
namespace Builder.Presentation.Events.Data;

public class DataManagerProgressChanged : EventBase
{
  public string ProgressMessage { get; set; }

  public int ProgressPercentage { get; set; }

  public bool InProgress { get; set; }

  public DataManagerProgressChanged(
    string progressMessage,
    int progressPercentage,
    bool inProgress)
  {
    this.InProgress = inProgress;
    this.ProgressMessage = progressMessage;
    this.ProgressPercentage = progressPercentage;
  }
}
