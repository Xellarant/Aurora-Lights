// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.UserControls.CharacterPanel
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Presentation.Controls;
using MahApps.Metro.Controls;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

#nullable disable
namespace Builder.Presentation.UserControls;

public partial class CharacterPanel : UserControl, IComponentConnector
{
  private bool _asAccordeon;
  internal AuroraExpander ExpanderDebug;
  internal AuroraExpander ExpanderSavingThrows;
  internal AuroraExpander ExpanderSkills;
  internal AuroraExpander ExpanderSpells;
  private bool _contentLoaded;

  public CharacterPanel()
  {
    this.InitializeComponent();
    this._asAccordeon = false;
  }

  private void ExpanderSpells_OnExpanded(object sender, RoutedEventArgs e)
  {
    if (!this._asAccordeon)
      return;
    foreach (Expander child in this.FindChildren<Expander>())
    {
      if (!object.Equals((object) child, (object) (sender as Expander)))
        child.IsExpanded = false;
    }
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/Aurora Builder;component/usercontrols/characterpanel.xaml", UriKind.Relative));
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
        this.ExpanderDebug = (AuroraExpander) target;
        break;
      case 2:
        this.ExpanderSavingThrows = (AuroraExpander) target;
        break;
      case 3:
        this.ExpanderSkills = (AuroraExpander) target;
        break;
      case 4:
        this.ExpanderSpells = (AuroraExpander) target;
        break;
      default:
        this._contentLoaded = true;
        break;
    }
  }
}
