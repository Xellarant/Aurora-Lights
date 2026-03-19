// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Events.Shell.MainWindowStatusUpdateEvent
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Presentation.Events.Base;

#nullable disable
namespace Builder.Presentation.Events.Shell;

public sealed class MainWindowStatusUpdateEvent : StatusUpdateEvent
{
  public MainWindowStatusUpdateEvent(string statusMessage)
    : this(statusMessage, -1)
  {
  }

  public MainWindowStatusUpdateEvent(string statusMessage, int progressPercentage)
    : base(statusMessage)
  {
    this.ProgressPercentage = progressPercentage;
  }

  public int ProgressPercentage { get; set; }

  public bool IsIndeterminateProgress { get; set; }

  public bool IsNormal { get; set; } = true;

  public bool IsSuccess { get; set; }

  public bool IsDanger { get; set; }
}
