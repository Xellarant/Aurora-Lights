// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Views.Dialogs.EditCharacterGroupWindow
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Presentation.Extensions;
using MahApps.Metro.Controls;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

#nullable disable
namespace Builder.Presentation.Views.Dialogs;

public partial class EditCharacterGroupWindow : MetroWindow, IComponentConnector
{
  private readonly List<string> _existingGroups;
  internal TextBlock SecondaryTextBlock;
  internal ComboBox GroupComboBox;
  private bool _contentLoaded;

  public EditCharacterGroupWindow(string currentGroup, List<string> existingGroups = null)
  {
    this.InitializeComponent();
    this.ApplyTheme();
    this._existingGroups = existingGroups ?? new List<string>();
    this.GroupComboBox.Text = currentGroup;
    this.GroupComboBox.ItemsSource = (IEnumerable) this._existingGroups;
    this.Loaded += new RoutedEventHandler(this.EditCharacterGroupWindow_Loaded);
  }

  private void EditCharacterGroupWindow_Loaded(object sender, RoutedEventArgs e)
  {
  }

  public string NewGroupName { get; set; }

  private void OnAcceptClicked(object sender, RoutedEventArgs e)
  {
    this.NewGroupName = this.GroupComboBox.Text.Trim();
    this.DialogResult = new bool?(true);
  }

  private void OnCancelClicked(object sender, RoutedEventArgs e)
  {
    this.DialogResult = new bool?(false);
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/Aurora Builder;component/views/dialogs/editcharactergroupwindow.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    switch (connectionId)
    {
      case 1:
        this.SecondaryTextBlock = (TextBlock) target;
        break;
      case 2:
        this.GroupComboBox = (ComboBox) target;
        break;
      case 3:
        ((ButtonBase) target).Click += new RoutedEventHandler(this.OnAcceptClicked);
        break;
      case 4:
        ((ButtonBase) target).Click += new RoutedEventHandler(this.OnCancelClicked);
        break;
      default:
        this._contentLoaded = true;
        break;
    }
  }
}
