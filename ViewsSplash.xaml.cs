// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Views.Splash
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Aurora.Presentation.Controls;
using Builder.Core.Logging;
using Builder.Presentation.Extensions;
using Builder.Presentation.Services;
using Builder.Presentation.Telemetry;
using MahApps.Metro.Controls;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

#nullable disable
namespace Builder.Presentation.Views;

public partial class Splash : MetroWindow, IComponentConnector
{
  private bool _closing;
  private bool _starting;
  internal Button BtnDiagnosticsWindow;
  internal Image BackgroundThemeImage;
  internal Image SplashImage;
  internal Image SplashImageUpperRight;
  internal Image SplashLogo;
  internal Image ThemeLogo;
  internal CommandButton LaunchButton;
  private bool _contentLoaded;

  public Splash() => this.InitializeComponent();

  private async void MetroWindow_Loaded(object sender, RoutedEventArgs e)
  {
    Splash window = this;
    window.ApplyTheme();
    if (ApplicationManager.Current.EnableDiagnostics | await Microsoft.AppCenter.Crashes.Crashes.HasCrashedInLastSessionAsync())
      window.BtnDiagnosticsWindow.Visibility = Visibility.Visible;
    if (ApplicationManager.Current.EnableDiagnostics)
      ApplicationManager.Current.InitializeDiagnosticsWindow();
    try
    {
      await window.GetViewModel().InitializeAsync();
    }
    catch (Exception ex)
    {
      AnalyticsErrorHelper.Exception(ex, method: nameof (MetroWindow_Loaded), line: 105);
      window.BtnDiagnosticsWindow.Visibility = Visibility.Visible;
      Logger.Exception(ex, nameof (MetroWindow_Loaded));
      MessageDialogService.ShowException(ex);
    }
    if (window._closing)
      return;
    window._starting = true;
    window.Launch();
  }

  private void Launch()
  {
    try
    {
      Application.Current.MainWindow = (Window) new ShellWindow();
      Application.Current.MainWindow.Show();
      this.Close();
    }
    catch (Exception ex)
    {
      AnalyticsErrorHelper.Exception(ex, method: nameof (Launch), line: 140);
      Logger.Exception(ex, nameof (Launch));
    }
  }

  private void MetroWindow_Closing(object sender, CancelEventArgs e)
  {
    if (this._starting)
      return;
    this._closing = true;
    Application.Current.Shutdown();
  }

  private void LaunchClick(object sender, RoutedEventArgs e) => this.Launch();

  private void ShowDiagnosticsWindow(object sender, RoutedEventArgs e)
  {
    if (!ApplicationManager.Current.EnableDiagnostics)
      return;
    ApplicationManager.Current.ShowDiagnosticsWindow();
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/Aurora Builder;component/views/splash.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    switch (connectionId)
    {
      case 1:
        ((Window) target).Closing += new CancelEventHandler(this.MetroWindow_Closing);
        ((FrameworkElement) target).Loaded += new RoutedEventHandler(this.MetroWindow_Loaded);
        break;
      case 2:
        this.BtnDiagnosticsWindow = (Button) target;
        this.BtnDiagnosticsWindow.Click += new RoutedEventHandler(this.ShowDiagnosticsWindow);
        break;
      case 3:
        this.BackgroundThemeImage = (Image) target;
        break;
      case 4:
        this.SplashImage = (Image) target;
        break;
      case 5:
        this.SplashImageUpperRight = (Image) target;
        break;
      case 6:
        this.SplashLogo = (Image) target;
        break;
      case 7:
        this.ThemeLogo = (Image) target;
        break;
      case 8:
        this.LaunchButton = (CommandButton) target;
        this.LaunchButton.Click += new RoutedEventHandler(this.LaunchClick);
        break;
      default:
        this._contentLoaded = true;
        break;
    }
  }
}
