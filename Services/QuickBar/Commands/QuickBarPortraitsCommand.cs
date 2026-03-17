// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Services.QuickBar.Commands.QuickBarPortraitsCommand
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Presentation.Events.Shell;
using Builder.Presentation.Services.Data;
using Builder.Presentation.Services.QuickBar.Commands.Base;
using System;
using System.Diagnostics;

#nullable disable
namespace Builder.Presentation.Services.QuickBar.Commands;

public sealed class QuickBarPortraitsCommand : QuickBarCommand
{
  private readonly string[] _parameters;

  public QuickBarPortraitsCommand()
    : base("portraits")
  {
    this._parameters = new string[1]{ "open" };
  }

  public override void Execute(string parameter)
  {
    MainWindowStatusUpdateEvent args = new MainWindowStatusUpdateEvent("");
    try
    {
      switch (parameter)
      {
        case "?":
        case "help":
          args.StatusMessage = $"@{this.CommandName} parameters are: {string.Join(", ", this._parameters)}";
          break;
        case "":
        case "open":
          Process.Start(DataManager.Current.UserDocumentsPortraitsDirectory);
          args.StatusMessage = "opening " + DataManager.Current.UserDocumentsPortraitsDirectory;
          break;
        default:
          args.StatusMessage = $"invalid @{this.CommandName} command ({parameter})";
          args.IsDanger = true;
          break;
      }
    }
    catch (Exception ex)
    {
      args.IsDanger = true;
      args.StatusMessage = ex.Message;
    }
    ApplicationManager.Current.EventAggregator.Send<MainWindowStatusUpdateEvent>(args);
  }
}
