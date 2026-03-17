// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Views.Sliders.BundleManagerSlider
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using MahApps.Metro.Controls;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

#nullable disable
namespace Builder.Presentation.Views.Sliders;

public partial class BundleManagerSlider : Flyout, IComponentConnector
{
  internal BundleManagerSlider Main;
  internal Grid Container;
  private bool _contentLoaded;

  public BundleManagerSlider() => this.InitializeComponent();

  private void ExpandersControlPreviewMouseWheel(object sender, MouseWheelEventArgs e)
  {
    if (e.Handled)
      return;
    e.Handled = true;
    MouseWheelEventArgs mouseWheelEventArgs = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
    mouseWheelEventArgs.RoutedEvent = UIElement.MouseWheelEvent;
    mouseWheelEventArgs.Source = sender;
    MouseWheelEventArgs e1 = mouseWheelEventArgs;
    if (!(((FrameworkElement) sender).Parent is UIElement parent))
      return;
    // ISSUE: explicit non-virtual call
    __nonvirtual (parent.RaiseEvent((RoutedEventArgs) e1));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/Aurora Builder;component/views/sliders/bundlemanagerslider.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  internal Delegate _CreateDelegate(Type delegateType, string handler)
  {
    return Delegate.CreateDelegate(delegateType, (object) this, handler);
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    switch (connectionId)
    {
      case 1:
        this.Main = (BundleManagerSlider) target;
        break;
      case 2:
        this.Container = (Grid) target;
        break;
      case 3:
        ((UIElement) target).PreviewMouseWheel += new MouseWheelEventHandler(this.ExpandersControlPreviewMouseWheel);
        break;
      default:
        this._contentLoaded = true;
        break;
    }
  }
}
