// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Views.Development.DiagnosticsWindow
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
using System.Windows.Data;
using System.Windows.Markup;

#nullable disable
namespace Builder.Presentation.Views.Development;

public partial class DiagnosticsWindow : MetroWindow, IComponentConnector
{
  internal StatusBar StatusBar;
  internal MetroProgressBar ProgressBarMain;
  private bool _contentLoaded;

  public DiagnosticsWindow()
  {
    this.InitializeComponent();
    this.Loaded += new RoutedEventHandler(this.DiagnosticsWindow_Loaded);
    this.Closing += new CancelEventHandler(this.DiagnosticsWindow_Closing);
  }

  private void DiagnosticsWindow_Closing(object sender, CancelEventArgs e)
  {
    e.Cancel = true;
    this.Hide();
  }

  private void DiagnosticsWindow_Loaded(object sender, RoutedEventArgs e) => this.ApplyTheme();

  private void ExitClick(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

  private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
  {
    ((TextBoxBase) sender).ScrollToEnd();
  }

  private void FrameworkElement_OnSourceUpdated(object sender, DataTransferEventArgs e)
  {
    try
    {
      ListView listView = (ListView) sender;
      listView.ScrollIntoView((object) listView.Items.IndexOf((object) (listView.Items.Count - 1)));
    }
    catch (Exception ex)
    {
    }
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/Aurora Builder;component/views/development/diagnosticswindow.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    switch (connectionId)
    {
      case 1:
        ((MenuItem) target).Click += new RoutedEventHandler(this.ExitClick);
        break;
      case 2:
        ((FrameworkElement) target).SourceUpdated += new EventHandler<DataTransferEventArgs>(this.FrameworkElement_OnSourceUpdated);
        break;
      case 3:
        this.StatusBar = (StatusBar) target;
        break;
      case 4:
        this.ProgressBarMain = (MetroProgressBar) target;
        break;
      default:
        this._contentLoaded = true;
        break;
    }
  }
}
