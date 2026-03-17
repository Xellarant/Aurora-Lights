// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Views.Development.DeveloperTools
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Presentation.Extensions;
using MahApps.Metro.Controls;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Media;
using TheArtOfDev.HtmlRenderer.WPF;

#nullable disable
namespace Builder.Presentation.Views.Development;

public partial class DeveloperTools : MetroWindow, IComponentConnector
{
  internal Button UpdateNotificationButton;
  internal TranslateTransform NotificationArrow;
  internal ScrollViewer WebScrollView;
  internal HtmlPanel html;
  internal StatusBar StatusBar;
  internal MetroProgressBar ProgressBarMain;
  private bool _contentLoaded;

  public DeveloperTools()
  {
    this.InitializeComponent();
    this.Loaded += new RoutedEventHandler(this.DeveloperTools_Loaded);
  }

  private void DeveloperTools_Loaded(object sender, RoutedEventArgs e) => this.ApplyTheme();

  private void ExitClick(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

  private void UpdateNotificationButtonClicked(object sender, RoutedEventArgs e)
  {
    this.GetViewModel<DeveloperToolsViewModel>().StatusMessage = "Update Notification Clicked";
  }

  private void OpenSupportTesterWindow(object sender, RoutedEventArgs e)
  {
    new SupportStringTester().Show();
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/Aurora Builder;component/views/development/developertools.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    switch (connectionId)
    {
      case 1:
        this.UpdateNotificationButton = (Button) target;
        this.UpdateNotificationButton.Click += new RoutedEventHandler(this.UpdateNotificationButtonClicked);
        break;
      case 2:
        this.NotificationArrow = (TranslateTransform) target;
        break;
      case 3:
        ((MenuItem) target).Click += new RoutedEventHandler(this.ExitClick);
        break;
      case 4:
        ((MenuItem) target).Click += new RoutedEventHandler(this.OpenSupportTesterWindow);
        break;
      case 5:
        this.WebScrollView = (ScrollViewer) target;
        break;
      case 6:
        this.html = (HtmlPanel) target;
        break;
      case 7:
        this.StatusBar = (StatusBar) target;
        break;
      case 8:
        this.ProgressBarMain = (MetroProgressBar) target;
        break;
      default:
        this._contentLoaded = true;
        break;
    }
  }
}
