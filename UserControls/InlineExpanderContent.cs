// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.UserControls.InlineExpanderContent
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

#nullable disable
namespace Builder.Presentation.UserControls;

public class InlineExpanderContent : UserControl, IComponentConnector
{
  private bool _contentLoaded;

  public InlineExpanderContent() => this.InitializeComponent();

  private void InlineExpanderContentPreviewMouseWheel(object sender, MouseWheelEventArgs e)
  {
    int num = e.Handled ? 1 : 0;
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/Aurora Builder;component/usercontrols/content/inlineexpandercontent.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    if (connectionId == 1)
      ((UIElement) target).PreviewMouseWheel += new MouseWheelEventHandler(this.InlineExpanderContentPreviewMouseWheel);
    else
      this._contentLoaded = true;
  }
}
