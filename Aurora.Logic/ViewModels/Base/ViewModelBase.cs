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

#nullable disable
namespace Builder.Presentation.ViewModels.Base;

public class ViewModelBase : ObservableObject
{
  protected IEventAggregator EventAggregator => ApplicationContext.Current.EventAggregator;

  protected ViewModelBase()
  {
  }

  public AppSettingsStore Settings => ApplicationContext.Current.Settings;

  public bool IsInDebugMode => Debugger.IsAttached;

  // Design-mode detection without WPF DependencyObject — always false at runtime.
  public bool IsInDesignMode => false;

  public bool IsInDeveloperMode => ApplicationContext.Current.IsInDeveloperMode;

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
