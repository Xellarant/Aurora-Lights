// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.UserControls.FeaturesTreeCollection
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core.Events;
using Builder.Data;
using Builder.Presentation.Events.Character;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

#nullable disable
namespace Builder.Presentation.UserControls;

public partial class FeaturesTreeCollection : 
  UserControl,
  ISubscriber<CharacterManagerElementRegistered>,
  IComponentConnector
{
  internal TreeView FeaturesTree;
  private bool _contentLoaded;

  public FeaturesTreeCollection()
  {
    this.InitializeComponent();
    ApplicationManager.Current.EventAggregator.Subscribe((object) this);
  }

  public void OnHandleEvent(CharacterManagerElementRegistered args) => this.PopulateFeaturesTree();

  public void PopulateFeaturesTree()
  {
    this.FeaturesTree.Items.Clear();
    IEnumerable<ElementContainer> containers = new CharacterContentOrganizer().GetContainers(CharacterManager.Current.GetElements().ToList<ElementBase>());
    TreeViewItem treeViewItem = new TreeViewItem();
    treeViewItem.Header = (object) "Features & Traits";
    TreeViewItem newItem1 = treeViewItem;
    foreach (ElementBase elementBase in containers.Select<ElementContainer, ElementBase>((Func<ElementContainer, ElementBase>) (x => x.Element)))
    {
      ItemCollection items = newItem1.Items;
      TreeViewItem newItem2 = new TreeViewItem();
      newItem2.Header = (object) elementBase.Name;
      items.Add((object) newItem2);
    }
    this.FeaturesTree.Items.Add((object) newItem1);
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/Aurora Builder;component/usercontrols/featurestreecollection.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    if (connectionId == 1)
      this.FeaturesTree = (TreeView) target;
    else
      this._contentLoaded = true;
  }
}
