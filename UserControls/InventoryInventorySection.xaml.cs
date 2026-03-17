// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.UserControls.Inventory.InventorySection
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Presentation.Extensions;
using Builder.Presentation.ViewModels.Shell.Items;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

#nullable disable
namespace Builder.Presentation.UserControls.Inventory;

public partial class InventorySection : UserControl, IComponentConnector
{
  private RefactoredEquipmentSectionViewModel _viewModel;
  internal InventorySection ParentControl;
  internal DataGrid InventoryDataGrid;
  internal DataGridTextColumn EquipmentItemActiveColumn;
  internal DataGridTextColumn EquipmentItemAttuneColumn;
  internal DataGridTextColumn EquipmentItemNameColumn;
  internal DataGridTextColumn EquipmentItemAdditionalColumn;
  internal DataGridTextColumn ItemAmountColumn;
  internal DataGridTextColumn ItemValueColumn;
  internal DataGridTextColumn ItemWeightColumn;
  internal DataGridTextColumn EquipmentItemAttunementColumn;
  internal DataGridTextColumn EquipmentItemEquippedColumn;
  internal DataGridTextColumn StorageLocationColumn;
  internal DataGridTextColumn ItemcardVisibleColumn;
  internal DataGridTextColumn EquipmentItemStashColumn;
  internal DataGridTextColumn EquipmentItemAttuneColumn2;
  private bool _contentLoaded;

  public InventorySection()
  {
    this.InitializeComponent();
    this.Loaded += new RoutedEventHandler(this.InventorySection_Loaded);
  }

  private void InventorySection_Loaded(object sender, RoutedEventArgs e)
  {
    this._viewModel = this.GetViewModel<RefactoredEquipmentSectionViewModel>();
    this._viewModel.PropertyChanged += new PropertyChangedEventHandler(this.InventorySection_PropertyChanged);
    this.ItemcardVisibleColumn.Visibility = this.GetVisibility(this._viewModel.ShowInventoryItemCardColumn);
  }

  private void InventorySection_PropertyChanged(object sender, PropertyChangedEventArgs e)
  {
    string str = "ShowInventoryItemCardColumn";
    if (e.PropertyName.Equals(str))
      this.ItemcardVisibleColumn.Visibility = this.GetVisibility(this._viewModel.ShowInventoryItemCardColumn);
    e.PropertyName.Equals("ShowInventoryItemCardColumn");
  }

  private Visibility GetVisibility(bool visible)
  {
    return !visible ? Visibility.Collapsed : Visibility.Visible;
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/Aurora Builder;component/usercontrols/inventory/inventorysection.xaml", UriKind.Relative));
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
        this.ParentControl = (InventorySection) target;
        break;
      case 2:
        this.InventoryDataGrid = (DataGrid) target;
        break;
      case 3:
        this.EquipmentItemActiveColumn = (DataGridTextColumn) target;
        break;
      case 4:
        this.EquipmentItemAttuneColumn = (DataGridTextColumn) target;
        break;
      case 5:
        this.EquipmentItemNameColumn = (DataGridTextColumn) target;
        break;
      case 6:
        this.EquipmentItemAdditionalColumn = (DataGridTextColumn) target;
        break;
      case 7:
        this.ItemAmountColumn = (DataGridTextColumn) target;
        break;
      case 8:
        this.ItemValueColumn = (DataGridTextColumn) target;
        break;
      case 9:
        this.ItemWeightColumn = (DataGridTextColumn) target;
        break;
      case 10:
        this.EquipmentItemAttunementColumn = (DataGridTextColumn) target;
        break;
      case 11:
        this.EquipmentItemEquippedColumn = (DataGridTextColumn) target;
        break;
      case 12:
        this.StorageLocationColumn = (DataGridTextColumn) target;
        break;
      case 13:
        this.ItemcardVisibleColumn = (DataGridTextColumn) target;
        break;
      case 14:
        this.EquipmentItemStashColumn = (DataGridTextColumn) target;
        break;
      case 15:
        this.EquipmentItemAttuneColumn2 = (DataGridTextColumn) target;
        break;
      default:
        this._contentLoaded = true;
        break;
    }
  }
}
