// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.UserControls.FillableFieldTextBox
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Presentation.Models.NewFolder1;
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

public partial class FillableFieldTextBox : UserControl, IComponentConnector
{
  public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(nameof (Header), typeof (string), typeof (FillableFieldTextBox), new PropertyMetadata((object) null));
  public static readonly DependencyProperty DisplayValidationProperty = DependencyProperty.Register(nameof (DisplayValidation), typeof (bool), typeof (FillableFieldTextBox), new PropertyMetadata((object) true));
  public static readonly DependencyProperty MinContentHeightProperty = DependencyProperty.Register(nameof (MinContentHeight), typeof (double), typeof (FillableFieldTextBox), new PropertyMetadata((object) 26.0));
  public static readonly DependencyProperty TooltipProperty = DependencyProperty.Register(nameof (Tooltip), typeof (string), typeof (FillableFieldTextBox), new PropertyMetadata((object) null));
  internal FillableFieldTextBox Main;
  private bool _contentLoaded;

  public FillableFieldTextBox() => this.InitializeComponent();

  [Category("Aurora")]
  public string Header
  {
    get => (string) this.GetValue(FillableFieldTextBox.HeaderProperty);
    set => this.SetValue(FillableFieldTextBox.HeaderProperty, (object) value);
  }

  [Category("Aurora")]
  public bool DisplayValidation
  {
    get => (bool) this.GetValue(FillableFieldTextBox.DisplayValidationProperty);
    set => this.SetValue(FillableFieldTextBox.DisplayValidationProperty, (object) value);
  }

  [Category("Aurora")]
  public double MinContentHeight
  {
    get => (double) this.GetValue(FillableFieldTextBox.MinContentHeightProperty);
    set => this.SetValue(FillableFieldTextBox.MinContentHeightProperty, (object) value);
  }

  [Category("Aurora")]
  public string Tooltip
  {
    get => (string) this.GetValue(FillableFieldTextBox.TooltipProperty);
    set => this.SetValue(FillableFieldTextBox.TooltipProperty, (object) value);
  }

  private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
  {
  }

  private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
  {
    if (!(this.DataContext is FillableField dataContext))
      return;
    dataContext.Content = "";
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/Aurora Builder;component/usercontrols/fillablefieldtextbox.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    if (connectionId != 1)
    {
      if (connectionId == 2)
        ((Control) target).MouseDoubleClick += new MouseButtonEventHandler(this.Control_OnMouseDoubleClick);
      else
        this._contentLoaded = true;
    }
    else
      this.Main = (FillableFieldTextBox) target;
  }
}
