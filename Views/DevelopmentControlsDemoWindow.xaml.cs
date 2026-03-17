// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Views.Development.ControlsDemoWindow
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core.Events;
using Builder.Presentation.Controls;
using Builder.Presentation.Events.Character;
using Builder.Presentation.Services;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

#nullable disable
namespace Builder.Presentation.Views.Development;

public partial class ControlsDemoWindow : 
  Window,
  ISubscriber<CharacterManagerElementRegistered>,
  IComponentConnector
{
  internal TextBox InputBox;
  internal ElementDescriptionPanel DescriptionPanel;
  internal ElementDescriptionPanel DescriptionPanel2;
  private bool _contentLoaded;

  public ControlsDemoWindow()
  {
    this.InitializeComponent();
    ApplicationManager.Current.EventAggregator.Subscribe((object) this);
  }

  private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
  {
    try
    {
      SpeechService.Default.StartSpeech(this.InputBox.SelectedText.Length > 0 ? this.InputBox.SelectedText : this.InputBox.Text);
    }
    catch (Exception ex)
    {
      MessageDialogService.ShowException(ex);
    }
  }

  public void OnHandleEvent(CharacterManagerElementRegistered args)
  {
  }

  private void Button_Click(object sender, RoutedEventArgs e)
  {
    this.DescriptionPanel.GenerateImage();
    this.DescriptionPanel2.GenerateImage();
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/Aurora Builder;component/views/development/controlsdemowindow.xaml", UriKind.Relative));
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
        this.InputBox = (TextBox) target;
        break;
      case 2:
        ((ButtonBase) target).Click += new RoutedEventHandler(this.ButtonBase_OnClick);
        break;
      case 3:
        ((ButtonBase) target).Click += new RoutedEventHandler(this.ButtonBase_OnClick);
        break;
      case 4:
        ((ButtonBase) target).Click += new RoutedEventHandler(this.Button_Click);
        break;
      case 5:
        this.DescriptionPanel = (ElementDescriptionPanel) target;
        break;
      case 6:
        this.DescriptionPanel2 = (ElementDescriptionPanel) target;
        break;
      default:
        this._contentLoaded = true;
        break;
    }
  }
}
