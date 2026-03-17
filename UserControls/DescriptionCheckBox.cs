// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.UserControls.DescriptionCheckBox
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

#nullable disable
namespace Builder.Presentation.UserControls;

public class DescriptionCheckBox : UserControl, IComponentConnector
{
  public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(nameof (Header), typeof (string), typeof (DescriptionCheckBox), new PropertyMetadata((object) nameof (Header)));
  public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(nameof (Description), typeof (string), typeof (DescriptionCheckBox), new PropertyMetadata((object) null));
  public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register(nameof (IsChecked), typeof (bool), typeof (DescriptionCheckBox), new PropertyMetadata((object) false));
  internal DescriptionCheckBox Main;
  private bool _contentLoaded;

  public DescriptionCheckBox() => this.InitializeComponent();

  public string Header
  {
    get => (string) this.GetValue(DescriptionCheckBox.HeaderProperty);
    set => this.SetValue(DescriptionCheckBox.HeaderProperty, (object) value);
  }

  public string Description
  {
    get => (string) this.GetValue(DescriptionCheckBox.DescriptionProperty);
    set => this.SetValue(DescriptionCheckBox.DescriptionProperty, (object) value);
  }

  public bool IsChecked
  {
    get => (bool) this.GetValue(DescriptionCheckBox.IsCheckedProperty);
    set => this.SetValue(DescriptionCheckBox.IsCheckedProperty, (object) value);
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/Aurora Builder;component/usercontrols/common/descriptioncheckbox.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    if (connectionId == 1)
      this.Main = (DescriptionCheckBox) target;
    else
      this._contentLoaded = true;
  }
}
