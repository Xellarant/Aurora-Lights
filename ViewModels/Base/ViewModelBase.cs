// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.Base.ViewModelBase
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core;
using Builder.Core.Events;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

#nullable disable
namespace Builder.Presentation.ViewModels.Base;

public class ViewModelBase : ObservableObject
{
  protected IEventAggregator EventAggregator => ApplicationManager.Current.EventAggregator;

  protected ViewModelBase()
  {
  }

  public ApplicationSettings Settings => ApplicationManager.Current.Settings;

  public bool IsInDebugMode => Debugger.IsAttached;

  public bool IsInDesignMode => DesignerProperties.GetIsInDesignMode(new DependencyObject());

  public bool IsInDeveloperMode => ApplicationManager.Current.IsInDeveloperMode;

  protected void SubscribeWithEventAggregator() => this.EventAggregator.Subscribe((object) this);

  public virtual Task InitializeAsync() => this.InitializeAsync((InitializationArguments) null);

  public virtual async Task InitializeAsync(InitializationArguments args)
  {
    int num = await Task.FromResult<bool>(true) ? 1 : 0;
  }

  protected virtual void InitializeDesignData()
  {
  }
}
