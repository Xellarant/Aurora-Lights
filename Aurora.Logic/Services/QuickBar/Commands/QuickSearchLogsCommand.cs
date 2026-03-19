// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Services.QuickBar.Commands.QuickSearchLogsCommand
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Presentation.Services.Data;
using Builder.Presentation.Services.QuickBar.Commands.Base;
using System.IO;

#nullable disable
namespace Builder.Presentation.Services.QuickBar.Commands;

public sealed class QuickSearchLogsCommand : QuickBarCommand
{
  public QuickSearchLogsCommand()
    : base("logs")
  {
  }

  public override void Execute(string parameter)
  {
    switch (parameter)
    {
      case "clear":
        this.ExecuteClearCommand();
        break;
    }
  }

  private void ExecuteClearCommand()
  {
    if (!Directory.Exists(DataManager.Current.LocalAppDataLogsDirectory))
      return;
    foreach (string file in Directory.GetFiles(DataManager.Current.LocalAppDataLogsDirectory))
    {
      if (File.Exists(file))
        File.Delete(file);
    }
  }
}
