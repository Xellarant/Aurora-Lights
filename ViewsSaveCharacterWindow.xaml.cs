// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Views.SaveCharacterWindow
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Presentation.Extensions;
using Builder.Presentation.Models;
using Builder.Presentation.ViewModels.Base;
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

public partial class SaveCharacterWindow : MetroWindow, IComponentConnector
{
  private readonly CharacterFile _characterFile;
  private bool _contentLoaded;

  public SaveCharacterWindow(CharacterFile characterFile)
  {
    this._characterFile = characterFile;
    this.InitializeComponent();
    this.ApplyTheme();
    this.Loaded += new RoutedEventHandler(this.SaveCharacterWindow_Loaded);
  }

  private async void SaveCharacterWindow_Loaded(object sender, RoutedEventArgs e)
  {
    SaveCharacterWindow window = this;
    await window.GetViewModel().InitializeAsync(new InitializationArguments((object) window._characterFile));
  }

  private void Save(object sender, RoutedEventArgs e)
  {
    this.DialogResult = new bool?(true);
    this.Close();
  }

  private void Cancel(object sender, RoutedEventArgs e)
  {
    this.DialogResult = new bool?(false);
    this.Close();
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/Aurora Builder;component/views/savecharacterwindow.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    if (connectionId != 1)
    {
      if (connectionId == 2)
        ((ButtonBase) target).Click += new RoutedEventHandler(this.Cancel);
      else
        this._contentLoaded = true;
    }
    else
      ((ButtonBase) target).Click += new RoutedEventHandler(this.Save);
  }
}
