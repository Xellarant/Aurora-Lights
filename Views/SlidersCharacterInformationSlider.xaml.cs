// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Views.Sliders.CharacterInformationSlider
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Presentation.Controls;
using Builder.Presentation.Properties;
using MahApps.Metro.Controls;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

#nullable disable
namespace Builder.Presentation.Views.Sliders;

public partial class CharacterInformationSlider : Flyout, IComponentConnector
{
  internal AuroraExpander ExpanderSkills;
  internal AuroraExpander ExpanderSavingThrows;
  internal AuroraExpander CompanionExpanderSkills;
  private bool _contentLoaded;

  public CharacterInformationSlider()
  {
    this.InitializeComponent();
    this.Loaded += new RoutedEventHandler(this.CharacterInformationSlider_Loaded);
    this.IsOpenChanged += new RoutedEventHandler(this.Page1IsOpenChanged);
  }

  private void CharacterInformationSlider_Loaded(object sender, RoutedEventArgs e)
  {
    // ISSUE: variable of a compiler-generated type
    Settings settings = ApplicationManager.Current.Settings.Settings;
    this.ExpanderSavingThrows.IsExpanded = settings.CharacterPanelSavingThrowsExpanded;
    this.ExpanderSkills.IsExpanded = settings.CharacterPanelSkillsExpanded;
  }

  private void Page1IsOpenChanged(object sender, RoutedEventArgs e)
  {
  }

  private void CharacterPortraitButtonClick(object sender, RoutedEventArgs e)
  {
    ApplicationManager.Current.EventAggregator.Send<ShowSliderEvent>(new ShowSliderEvent(Slider.Gallery));
  }

  private void CompanionClicked(object sender, RoutedEventArgs e)
  {
    ApplicationManager.Current.EventAggregator.Send<ShowSliderEvent>(new ShowSliderEvent(Slider.CompanionGallery));
  }

  private void OrganizationSymbolClicked(object sender, RoutedEventArgs e)
  {
    ApplicationManager.Current.EventAggregator.Send<ShowSliderEvent>(new ShowSliderEvent(Slider.OrganizationSymbolsGallery));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/Aurora Builder;component/views/sliders/characterinformationslider.xaml", UriKind.Relative));
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
        this.ExpanderSkills = (AuroraExpander) target;
        break;
      case 2:
        this.ExpanderSavingThrows = (AuroraExpander) target;
        break;
      case 3:
        this.CompanionExpanderSkills = (AuroraExpander) target;
        break;
      default:
        this._contentLoaded = true;
        break;
    }
  }
}
