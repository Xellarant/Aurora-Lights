// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.UserControls.Inventory.InventoryStorageSection
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

#nullable disable
namespace Builder.Presentation.UserControls.Inventory;

public partial class InventoryStorageSection : UserControl, IComponentConnector
{
  internal DataGrid InventoryDataGrid;
  internal DataGridTextColumn ItemAmountColumn2;
  internal DataGridTextColumn EquipmentItemNameColumn;
  private bool _contentLoaded;

  public InventoryStorageSection() => this.InitializeComponent();

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/Aurora Builder;component/usercontrols/inventory/inventorystoragesection.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    switch (connectionId)
    {
      case 1:
        this.InventoryDataGrid = (DataGrid) target;
        break;
      case 2:
        this.ItemAmountColumn2 = (DataGridTextColumn) target;
        break;
      case 3:
        this.EquipmentItemNameColumn = (DataGridTextColumn) target;
        break;
      default:
        this._contentLoaded = true;
        break;
    }
  }
}
