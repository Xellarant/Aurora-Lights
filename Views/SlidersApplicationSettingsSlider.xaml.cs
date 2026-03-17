// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Views.Sliders.ApplicationSettingsSlider
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
using System.Windows.Markup;

#nullable disable
namespace Builder.Presentation.Views.Sliders;

public partial class ApplicationSettingsSlider : Flyout, IComponentConnector
{
  internal Expander ExpanderSavingThrows;
  internal Expander ExpanderSkills;
  private bool _contentLoaded;

  public ApplicationSettingsSlider()
  {
    this.InitializeComponent();
    this.IsOpenChanged += new RoutedEventHandler(this.ApplicationSettingsSlider_IsOpenChanged);
  }

  private void ApplicationSettingsSlider_IsOpenChanged(object sender, RoutedEventArgs e)
  {
  }

  private void ButtonBase_OnClick(object sender, RoutedEventArgs e) => this.IsOpen = false;

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/Aurora Builder;component/views/sliders/applicationsettingsslider.xaml", UriKind.Relative));
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
        this.ExpanderSkills = (Expander) target;
      else
        this._contentLoaded = true;
    }
    else
      this.ExpanderSavingThrows = (Expander) target;
  }
}
