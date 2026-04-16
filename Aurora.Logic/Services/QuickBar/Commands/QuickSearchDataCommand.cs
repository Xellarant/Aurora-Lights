// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Services.QuickBar.Commands.QuickSearchDataCommand
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Presentation.Services.Data;
using Builder.Presentation.Services.QuickBar.Commands.Base;
#nullable disable
namespace Builder.Presentation.Services.QuickBar.Commands;

public sealed class QuickSearchDataCommand : QuickBarCommand
{
  public QuickSearchDataCommand()
    : base("data")
  {
  }

  public override void Execute(string parameter)
  {
    switch (parameter)
    {
      case "custom":
        ExternalLauncherContext.Current?.OpenPath(DataManager.Current.UserDocumentsCustomElementsDirectory);
        break;
      case "portraits":
        ExternalLauncherContext.Current?.OpenPath(DataManager.Current.UserDocumentsPortraitsDirectory);
        break;
      case "local":
        ExternalLauncherContext.Current?.OpenPath(DataManager.Current.LocalAppDataRootDirectory);
        break;
      case "logs":
        ExternalLauncherContext.Current?.OpenPath(DataManager.Current.LocalAppDataLogsDirectory);
        break;
    }
  }
}
