// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Views.Sliders.Content.BundleContentFlyout
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Presentation.ViewModels.Shell.Start;
using MahApps.Metro.Controls;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

#nullable disable
namespace Builder.Presentation.Views.Sliders.Content;

public partial class BundleContentFlyout : Flyout, IComponentConnector
{
  private bool _contentLoaded;

  public BundleContentFlyout()
  {
    this.InitializeComponent();
    this.IsOpenChanged += new RoutedEventHandler(this.Page1_IsOpenChanged);
  }

  private async void Page1_IsOpenChanged(object sender, RoutedEventArgs e)
  {
    int num = this.IsOpen ? 1 : 0;
  }

  private void PageSaveClicked(object sender, RoutedEventArgs e)
  {
  }

  private void PageCloseClicked(object sender, RoutedEventArgs e) => this.IsOpen = false;

  private void RequestCoreContentOnClick(object sender, RoutedEventArgs e)
  {
    ApplicationManager.Current.EventAggregator.Send<IndexDownloadRequestEvent>(new IndexDownloadRequestEvent(Builder.Presentation.Properties.Resources.AdditionalContentCoreUrl));
  }

  private void RequestSupplementsContentOnClick(object sender, RoutedEventArgs e)
  {
    ApplicationManager.Current.EventAggregator.Send<IndexDownloadRequestEvent>(new IndexDownloadRequestEvent(Builder.Presentation.Properties.Resources.AdditionalContentSupplementsUrl));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/Aurora Builder;component/views/sliders/content/bundlecontentflyout.xaml", UriKind.Relative));
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
    if (connectionId != 1)
    {
      if (connectionId == 2)
        ((ButtonBase) target).Click += new RoutedEventHandler(this.RequestSupplementsContentOnClick);
      else
        this._contentLoaded = true;
    }
    else
      ((ButtonBase) target).Click += new RoutedEventHandler(this.RequestCoreContentOnClick);
  }
}
