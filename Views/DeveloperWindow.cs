// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Views.DeveloperWindow
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core.Logging;
using Builder.Presentation.Views.Development;
using Builder.Presentation.Views.Dialogs;
using MahApps.Metro.Controls;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using TheArtOfDev.HtmlRenderer.WPF;

#nullable disable
namespace Builder.Presentation.Views;

public class DeveloperWindow : MetroWindow, IComponentConnector
{
  internal TextBox BrowseTextBox;
  internal ScrollViewer WebScrollView;
  internal HtmlPanel html;
  internal StatusBar StatusBar;
  internal TextBlock TextBlockStatus;
  internal MetroProgressBar ProgressBarMain;
  private bool _contentLoaded;

  public DeveloperWindow() => this.InitializeComponent();

  private void BrowseHomepageClick(object sender, RoutedEventArgs e)
  {
    Process.Start("http://www.dnd-builder.com/development");
  }

  private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
  {
  }

  private void OpenInformationWindow(object sender, RoutedEventArgs e)
  {
    new ElementInformationWindow().Show();
  }

  private void OpenSettingsWindow(object sender, RoutedEventArgs e) => new SettingsWindow().Show();

  private void OpenIntroductionWindow(object sender, RoutedEventArgs e)
  {
  }

  private void OpenSplash(object sender, RoutedEventArgs e) => new Splash().Show();

  private void Browser_MouseWheel(object sender, MouseWheelEventArgs e)
  {
  }

  private void BrowseTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
  {
  }

  private async void OpenEntry(object sender, RoutedEventArgs e)
  {
    Logger.Info(await new SingleEntryWindow().Show(new EntryInitializationArguments("Character Builder", "Personality Trait", "")));
  }

  private void OpenSupportTestWindow(object sender, RoutedEventArgs e)
  {
    new SupportStringTester().Show();
  }

  private void OpenCompareWindow(object sender, RoutedEventArgs e)
  {
    new ElementCloneWindow().Show();
  }

  private void OpenLauncherWindow(object sender, RoutedEventArgs e) => new Launcher().Show();

  private void MenuItem_OnClick(object sender, RoutedEventArgs e) => new DeveloperTools().Show();

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/Aurora Builder;component/views/_obsolete/developerwindow.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    switch (connectionId)
    {
      case 1:
        ((ButtonBase) target).Click += new RoutedEventHandler(this.BrowseHomepageClick);
        break;
      case 2:
        ((MenuItem) target).Click += new RoutedEventHandler(this.MenuItem_OnClick);
        break;
      case 3:
        ((TextBoxBase) target).TextChanged += new TextChangedEventHandler(this.BrowseTextBox_OnTextChanged);
        break;
      case 4:
        this.BrowseTextBox = (TextBox) target;
        this.BrowseTextBox.TextChanged += new TextChangedEventHandler(this.BrowseTextBox_OnTextChanged);
        break;
      case 5:
        this.WebScrollView = (ScrollViewer) target;
        break;
      case 6:
        this.html = (HtmlPanel) target;
        break;
      case 7:
        ((ButtonBase) target).Click += new RoutedEventHandler(this.OpenInformationWindow);
        break;
      case 8:
        ((ButtonBase) target).Click += new RoutedEventHandler(this.OpenIntroductionWindow);
        break;
      case 9:
        ((ButtonBase) target).Click += new RoutedEventHandler(this.OpenSettingsWindow);
        break;
      case 10:
        ((ButtonBase) target).Click += new RoutedEventHandler(this.OpenSplash);
        break;
      case 11:
        ((ButtonBase) target).Click += new RoutedEventHandler(this.OpenEntry);
        break;
      case 12:
        ((ButtonBase) target).Click += new RoutedEventHandler(this.OpenSupportTestWindow);
        break;
      case 13:
        ((ButtonBase) target).Click += new RoutedEventHandler(this.OpenCompareWindow);
        break;
      case 14:
        ((ButtonBase) target).Click += new RoutedEventHandler(this.OpenLauncherWindow);
        break;
      case 15:
        this.StatusBar = (StatusBar) target;
        break;
      case 16 /*0x10*/:
        this.TextBlockStatus = (TextBlock) target;
        break;
      case 17:
        this.ProgressBarMain = (MetroProgressBar) target;
        break;
      default:
        this._contentLoaded = true;
        break;
    }
  }
}
