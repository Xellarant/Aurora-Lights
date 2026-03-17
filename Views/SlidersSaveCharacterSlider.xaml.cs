// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Views.Sliders.SaveCharacterSlider
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
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

#nullable disable
namespace Builder.Presentation.Views.Sliders;

public partial class SaveCharacterSlider : Flyout, IComponentConnector
{
  private CharacterFile _file;
  internal Button PortraitImage;
  private bool _contentLoaded;

  public SaveCharacterSlider()
  {
    this.InitializeComponent();
    this.IsOpenChanged += new RoutedEventHandler(this.Page1_IsOpenChanged);
  }

  public void SetFile(CharacterFile file) => this._file = file;

  private async void Page1_IsOpenChanged(object sender, RoutedEventArgs e)
  {
    SaveCharacterSlider window = this;
    if (window.IsOpen)
    {
      if (window._file != null)
        await window.GetViewModel().InitializeAsync(new InitializationArguments((object) window._file));
      else
        await window.GetViewModel().InitializeAsync(new InitializationArguments((object) new CharacterFile("")
        {
          IsNew = true
        }));
    }
    else
      window._file = (CharacterFile) null;
  }

  private void PageSaveClicked(object sender, RoutedEventArgs e) => this.IsOpen = false;

  private void PageCloseClicked(object sender, RoutedEventArgs e) => this.IsOpen = false;

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/Aurora Builder;component/views/sliders/savecharacterslider.xaml", UriKind.Relative));
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
        this.PortraitImage = (Button) target;
        break;
      case 2:
        ((ButtonBase) target).Click += new RoutedEventHandler(this.PageCloseClicked);
        break;
      case 3:
        ((ButtonBase) target).Click += new RoutedEventHandler(this.PageCloseClicked);
        break;
      default:
        this._contentLoaded = true;
        break;
    }
  }
}
