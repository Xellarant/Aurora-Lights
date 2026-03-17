// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Views.Content.Manage.CreateAttackWindow
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
namespace Builder.Presentation.Views.Content.Manage;

public partial class CreateAttackWindow : MetroWindow, IComponentConnector
{
  private bool _contentLoaded;

  public CreateAttackWindow()
  {
    this.InitializeComponent();
    this.Loaded += new RoutedEventHandler(this.CreateAttackWindow_Loaded);
  }

  private void CreateAttackWindow_Loaded(object sender, RoutedEventArgs e) => this.ApplyTheme();

  private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
  {
    this.DialogResult = new bool?(true);
    this.Close();
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/Aurora Builder;component/views/content/manage/createattackwindow.xaml", UriKind.Relative));
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
    if (connectionId == 1)
      ((ButtonBase) target).Click += new RoutedEventHandler(this.ButtonBase_OnClick);
    else
      this._contentLoaded = true;
  }
}
