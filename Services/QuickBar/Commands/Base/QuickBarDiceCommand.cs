// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Services.QuickBar.Commands.Base.QuickBarDiceCommand
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core.Logging;
using Builder.Presentation.Events.Shell;
using Builder.Presentation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace Builder.Presentation.Services.QuickBar.Commands.Base;

public class QuickBarDiceCommand : QuickBarCommand
{
  private DiceService _dice;

  public QuickBarDiceCommand()
    : base("roll")
  {
    this._dice = new DiceService();
  }

  public override async void Execute(string parameter)
  {
    MainWindowStatusUpdateEvent args = new MainWindowStatusUpdateEvent("rolling " + parameter);
    try
    {
      ApplicationManager.Current.EventAggregator.Send<MainWindowStatusUpdateEvent>(args);
      List<string> list = ((IEnumerable<string>) parameter.Split('d')).Where<string>((Func<string, bool>) (x => !string.IsNullOrWhiteSpace(x))).ToList<string>();
      int amount = int.Parse(list[0]);
      string size = list[1];
      int bonus = 0;
      if (list[1].Contains("+"))
      {
        size = list[1].Split('+')[0];
        bonus = int.Parse(list[1].Split('+')[1]);
      }
      else if (list[1].Contains("-"))
      {
        size = list[1].Split('-')[0];
        bonus = -int.Parse(list[1].Split('-')[1]);
      }
      int total = 0;
      List<int> results = new List<int>();
      for (int i = 0; i < amount; ++i)
      {
        List<int> intList;
        if (size != null)
        {
          switch (size)
          {
            case "4":
              intList = results;
              intList.Add(await this._dice.D4());
              intList = (List<int>) null;
              break;
            case "6":
              intList = results;
              intList.Add(await this._dice.D6());
              intList = (List<int>) null;
              break;
            case "8":
              intList = results;
              intList.Add(await this._dice.D8());
              intList = (List<int>) null;
              break;
            case "10":
              intList = results;
              intList.Add(await this._dice.D10());
              intList = (List<int>) null;
              break;
            case "12":
              intList = results;
              intList.Add(await this._dice.D12());
              intList = (List<int>) null;
              break;
            case "20":
              intList = results;
              intList.Add(await this._dice.D20());
              intList = (List<int>) null;
              break;
          }
        }
        args.ProgressPercentage = (i + 1).IsPercetageOf(amount);
        ApplicationManager.Current.EventAggregator.Send<MainWindowStatusUpdateEvent>(args);
        await Task.Delay(100);
      }
      total += results.Sum();
      total += bonus;
      args.StatusMessage = $"You rolled a total of {total} on {parameter}. (individual rolls: {string.Join<int>(",", (IEnumerable<int>) results)})";
      args.ProgressPercentage = 100;
      ApplicationManager.Current.EventAggregator.Send<MainWindowStatusUpdateEvent>(args);
      size = (string) null;
      results = (List<int>) null;
    }
    catch (Exception ex)
    {
      Logger.Exception(ex, nameof (Execute));
      args.StatusMessage = ex.Message;
      args.IsDanger = true;
    }
    ApplicationManager.Current.EventAggregator.Send<MainWindowStatusUpdateEvent>(args);
    args = (MainWindowStatusUpdateEvent) null;
  }
}
