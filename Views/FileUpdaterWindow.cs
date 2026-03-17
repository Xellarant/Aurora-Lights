// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Views.FileUpdaterWindow
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
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

#nullable disable
namespace Builder.Presentation.Views;

public class FileUpdaterWindow : MetroWindow, IComponentConnector
{
  internal StatusBar StatusBar;
  internal MetroProgressBar ProgressBarMain;
  private bool _contentLoaded;

  public FileUpdaterWindow()
  {
    this.InitializeComponent();
    this.ApplyTheme();
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/Aurora Builder;component/views/_obsolete/fileupdaterwindow.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    if (connectionId != 1)
    {
      if (connectionId == 2)
        this.ProgressBarMain = (MetroProgressBar) target;
      else
        this._contentLoaded = true;
    }
    else
      this.StatusBar = (StatusBar) target;
  }
}
