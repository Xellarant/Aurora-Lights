// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.UserControls.RefactoredAbilitiesContent
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core.Logging;
using Builder.Presentation.Events.Application;
using Builder.Presentation.Events.Shell;
using Builder.Presentation.Models;
using Builder.Presentation.Properties;
using Builder.Presentation.Views;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;

#nullable disable
namespace Builder.Presentation.UserControls;

public class RefactoredAbilitiesContent : UserControl, IComponentConnector
{
  private bool _isDragging;
  private bool _contentLoaded;

  public RefactoredAbilitiesContent() => this.InitializeComponent();

  private void UIElement_OnMouseMove(object sender, MouseEventArgs e)
  {
    if (e.LeftButton == MouseButtonState.Pressed)
    {
      Logger.Info("entered UIElement_OnMouseMove");
      try
      {
        if (this._isDragging)
          return;
        this._isDragging = true;
        AbilitiesSpinner abilitiesSpinner = (AbilitiesSpinner) sender;
        if (abilitiesSpinner == null)
          return;
        int num = (int) DragDrop.DoDragDrop((DependencyObject) abilitiesSpinner, (object) abilitiesSpinner, DragDropEffects.Move);
      }
      catch (Exception ex)
      {
        ApplicationManager.Current.EventAggregator.Send<MainWindowStatusUpdateEvent>(new MainWindowStatusUpdateEvent(ex.Message));
      }
    }
    else
      this._isDragging = false;
  }

  private void UIElement_OnDrop(object sender, DragEventArgs e)
  {
    try
    {
      Logger.Info("entered UIElement_OnDrop");
      AbilityItem abilityItem1 = ((AbilitiesSpinner) sender)?.GetAbilityItem();
      if (abilityItem1 == null)
        return;
      AbilityItem abilityItem2 = e.Data.GetData(typeof (AbilitiesSpinner)) is AbilitiesSpinner data ? data.GetAbilityItem() : (AbilityItem) null;
      if (abilityItem2 == null)
        return;
      int baseScore1 = abilityItem1.BaseScore;
      int baseScore2 = abilityItem2.BaseScore;
      abilityItem1.BaseScore = baseScore2;
      abilityItem2.BaseScore = baseScore1;
      this._isDragging = false;
      ApplicationManager.Current.EventAggregator.Send<MainWindowStatusUpdateEvent>(new MainWindowStatusUpdateEvent($"You switched your base ability score of {abilityItem2.Name} and {abilityItem1.Name}"));
      CharacterManager.Current.ReprocessCharacter();
    }
    catch (Exception ex)
    {
      ApplicationManager.Current.EventAggregator.Send<MainWindowStatusUpdateEvent>(new MainWindowStatusUpdateEvent(ex.Message));
    }
  }

  private void UIElement_OnDragEnter(object sender, DragEventArgs e)
  {
  }

  private void UIElement_OnDragLeave(object sender, DragEventArgs e)
  {
  }

  private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
  {
    new PreferencesWindow().ShowDialog();
  }

  private void SwitchGenerateionButtonBase_OnClick(object sender, RoutedEventArgs e)
  {
    Settings.Default.AbilitiesGenerationOption = 3;
    ApplicationManager.Current.EventAggregator.Send<SettingsChangedEvent>(new SettingsChangedEvent());
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    System.Windows.Application.LoadComponent((object) this, new Uri("/Aurora Builder;component/usercontrols/content/refactoredabilitiescontent.xaml", UriKind.Relative));
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
      ((ButtonBase) target).Click += new RoutedEventHandler(this.SwitchGenerateionButtonBase_OnClick);
    else
      this._contentLoaded = true;
  }
}
