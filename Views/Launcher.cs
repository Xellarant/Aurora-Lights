// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Views.Launcher
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
using System.Windows.Markup;

#nullable disable
namespace Builder.Presentation.Views;

public class Launcher : MetroWindow, IComponentConnector
{
  private bool _closing;
  private bool _starting;
  private bool _contentLoaded;

  public Launcher() => this.InitializeComponent();

  private async void MetroWindow_Loaded(object sender, RoutedEventArgs e) => this.ApplyTheme();

  private void MetroWindow_Closing(object sender, CancelEventArgs e)
  {
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/Aurora Builder;component/views/_obsolete/launcher.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    if (connectionId == 1)
    {
      ((Window) target).Closing += new CancelEventHandler(this.MetroWindow_Closing);
      ((FrameworkElement) target).Loaded += new RoutedEventHandler(this.MetroWindow_Loaded);
    }
    else
      this._contentLoaded = true;
  }
}
