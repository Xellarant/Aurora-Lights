using System.IO;
// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.LauncherWindowViewModel
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core.Events;
using Builder.Core.Logging;
using Builder.Data;
using Builder.Presentation.Events.Data;

using Builder.Presentation.Services;
using Builder.Presentation.Services.Data;
using Builder.Presentation.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable disable
using Builder.Presentation.Properties;
namespace Builder.Presentation.ViewModels;

public class LauncherWindowViewModel : ViewModelBase, ISubscriber<DataManagerProgressChanged>
{
  private string _progressMessage;
  private int _progressPercentage;
  private bool _progressCompleted;
  private string _version;

  public LauncherWindowViewModel()
  {
    this._progressMessage = "Initializing...";
    this._version = Resources.ApplicationVersion;
    if (this.IsInDesignMode)
      this.ProgressPercentage = 67;
    else
      this.SubscribeWithEventAggregator();
  }

  public string ProgressMessage
  {
    get => this._progressMessage;
    set => this.SetProperty<string>(ref this._progressMessage, value, nameof (ProgressMessage));
  }

  public int ProgressPercentage
  {
    get => this._progressPercentage;
    set => this.SetProperty<int>(ref this._progressPercentage, value, nameof (ProgressPercentage));
  }

  public bool ProgressCompleted
  {
    get => this._progressCompleted;
    set => this.SetProperty<bool>(ref this._progressCompleted, value, nameof (ProgressCompleted));
  }

  public string Version
  {
    get => this._version;
    set => this.SetProperty<string>(ref this._version, value, nameof (Version));
  }

  public override async Task InitializeAsync()
  {
    this.ProgressCompleted = false;
    try
    {
      Logger.Initializing((object) "directories");
      DataManager.Current.InitializeDirectories();
    }
    catch (Exception ex)
    {
      Logger.Exception(ex, nameof (InitializeAsync));
      ex.Data[(object) "warning"] = (object) "Failed to initialize directories";
      MessageDialogService.ShowException(ex);
    }
    if (ApplicationManager.Current.IsInDeveloperMode)
    {
      try
      {
        Logger.Initializing((object) "file logger");
        DataManager.Current.InitializeFileLogger();
      }
      catch (Exception ex)
      {
        Logger.Exception(ex, nameof (InitializeAsync));
        MessageDialogService.ShowException(ex);
      }
    }
    try
    {
      Logger.Initializing((object) "image resources");
      DataManager.Current.CopyPortraitsFromResources();
      DataManager.Current.CopyCompanionPortraitsFromResources();
      DataManager.Current.CopySymbolsFromResources();
      DataManager.Current.CopyDragonmarksFromResources();
    }
    catch (Exception ex)
    {
      Logger.Exception(ex, nameof (InitializeAsync));
      ex.Data[(object) "warning"] = (object) "Failed to initialize resources.";
      MessageDialogService.ShowException(ex);
    }
    IEnumerable<ElementBase> elementBases = await DataManager.Current.InitializeElementDataAsync();
    this.ProgressMessage = "Starting Aurora Builder";
    this.ProgressCompleted = true;
  }

  public void OnHandleEvent(DataManagerProgressChanged args)
  {
    this.ProgressMessage = args.ProgressMessage;
    this.ProgressPercentage = args.ProgressPercentage;
  }
}
