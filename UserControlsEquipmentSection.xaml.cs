// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.UserControls.EquipmentSection
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core.Events;
using Builder.Presentation.Events.Application;
using Builder.Presentation.Extensions;
using Builder.Presentation.ViewModels.Shell.Items;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

#nullable disable
namespace Builder.Presentation.UserControls;

public partial class EquipmentSection : 
  System.Windows.Controls.UserControl,
  ISubscriber<SettingsChangedEvent>,
  IComponentConnector,
  IStyleConnector
{
  private RefactoredEquipmentSectionViewModel _viewModel;
  internal EquipmentSection UserControl;
  internal DataGrid CategoriesDataGrid;
  internal DataGridTextColumn categoryColumn;
  internal DataGrid ArmorCollectionDataGrid;
  internal DataGridTextColumn armorNameColumn;
  internal DataGrid WeaponsCollectionDataGrid;
  internal DataGridTextColumn weaponsNameColumn;
  internal Grid ItemsGrid;
  internal Border VisualFocusQue;
  internal DataGrid SelectionElementsDataGrid;
  internal DataGridTextColumn NameColumn;
  internal DataGridTextColumn ItemPriceColumn;
  internal DataGridTextColumn ItemWeightColumn;
  internal DataGridTextColumn itemsRarityColumn;
  internal DataGridTextColumn attunementColumn;
  internal DataGridTextColumn slotColumn;
  internal DataGridTextColumn ArmorClassColumn;
  internal DataGridTextColumn ArmorStrengthColumn;
  internal DataGridTextColumn ArmorStealthColumn;
  internal DataGridTextColumn ArmorGroupColumn;
  internal DataGridTextColumn WeaponDamageColumn;
  internal DataGridTextColumn WeaponRangeColumn;
  internal DataGridTextColumn WeaponGroupColumn;
  internal DataGridTextColumn WeaponPropertiesColumn;
  internal DataGridTextColumn SourceColumn;
  internal DataGrid SelectionElementsDataGrid2;
  internal DataGridTextColumn EquipmentItemActiveColumn;
  internal DataGridTextColumn EquipmentItemAttuneColumn;
  internal DataGridTextColumn EquipmentItemNameColumn;
  internal DataGridTextColumn EquipmentItemAdditionColumn;
  internal DataGridTextColumn EquipmentItemAttunementColumn;
  internal DataGridTextColumn EquipmentItemEquippedColumn;
  internal DataGridTextColumn ItemcardVisibleColumn;
  internal DataGridTextColumn EquipmentItemStashColumn;
  private bool _contentLoaded;

  public EquipmentSection()
  {
    this.InitializeComponent();
    this.Loaded += new RoutedEventHandler(this.EquipmentSection_Loaded);
  }

  private void EquipmentSection_Loaded(object sender, RoutedEventArgs e)
  {
    int expanderGridRowHeight = ApplicationManager.Current.Settings.GetSelectionExpanderGridRowHeight();
    this.CategoriesDataGrid.MinRowHeight = (double) expanderGridRowHeight;
    this.ArmorCollectionDataGrid.MinRowHeight = (double) expanderGridRowHeight;
    this.WeaponsCollectionDataGrid.MinRowHeight = (double) expanderGridRowHeight;
    this.SelectionElementsDataGrid.MinRowHeight = (double) expanderGridRowHeight;
    this.SelectionElementsDataGrid2.MinRowHeight = (double) expanderGridRowHeight;
    ApplicationManager.Current.EventAggregator.Subscribe((object) this);
    this._viewModel = this.GetViewModel<RefactoredEquipmentSectionViewModel>();
    this._viewModel.Columns.PropertyChanged += new PropertyChangedEventHandler(this.Columns_PropertyChanged);
  }

  private void Columns_PropertyChanged(object sender, PropertyChangedEventArgs e)
  {
    this.DisplayColumn((DataGridColumn) this.ItemPriceColumn, this._viewModel.Columns.IsPriceColumnVisible);
    this.DisplayColumn((DataGridColumn) this.ItemWeightColumn, this._viewModel.Columns.IsWeightColumnVisible);
    this.DisplayColumn((DataGridColumn) this.attunementColumn, this._viewModel.Columns.IsAttunementColumnVisible);
    this.DisplayColumn((DataGridColumn) this.itemsRarityColumn, this._viewModel.Columns.IsRarityColumnVisible);
    this.DisplayColumn((DataGridColumn) this.ArmorClassColumn, this._viewModel.Columns.IsArmorClassColumnVisible);
    this.DisplayColumn((DataGridColumn) this.ArmorStealthColumn, this._viewModel.Columns.IsArmorStrengthColumnVisible);
    this.DisplayColumn((DataGridColumn) this.ArmorStrengthColumn, this._viewModel.Columns.IsArmorStrengthColumnVisible);
    this.DisplayColumn((DataGridColumn) this.ArmorGroupColumn, this._viewModel.Columns.IsArmorGroupColumnVisible);
    this.DisplayColumn((DataGridColumn) this.WeaponRangeColumn, this._viewModel.Columns.IsWeaponRangeColumnVisible);
    this.DisplayColumn((DataGridColumn) this.WeaponDamageColumn, this._viewModel.Columns.IsWeaponDamageColumnVisible);
    this.DisplayColumn((DataGridColumn) this.WeaponDamageColumn, this._viewModel.Columns.IsWeaponCategoryVisible);
    this.DisplayColumn((DataGridColumn) this.WeaponGroupColumn, this._viewModel.Columns.IsWeaponGroupVisible);
    this.DisplayColumn((DataGridColumn) this.WeaponPropertiesColumn, this._viewModel.Columns.IsWeaponPropertiesVisible);
    this.DisplayColumn((DataGridColumn) this.SourceColumn, this._viewModel.Columns.IsSourceColumnVisible);
    this.DisplayColumn((DataGridColumn) this.ItemcardVisibleColumn, this._viewModel.Columns.IsItemcardsColumnVisible);
  }

  private void DisplayColumn(DataGridColumn column, bool visible)
  {
    column.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
  }

  private void SelectionElementsDataGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
  {
    this._viewModel.AddSelectedItemCommand.Execute((object) null);
  }

  private void SelectionElementsDataGrid_OnPreviewKeyDown(object sender, KeyEventArgs e)
  {
  }

  private void SelectionEquipmentItem_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
  {
    if (!this._viewModel.AllowActivateEquipmentItem)
      return;
    this._viewModel.ActivateEquipmentItemCommand.Execute((object) null);
  }

  private void SelectionEquipmentItem_OnPreviewKeyDown(object sender, KeyEventArgs e)
  {
  }

  private void SelectionElementsDataGrid2_OnKeyDown(object sender, KeyEventArgs e)
  {
    this.SelectionElementsDataGrid2.Focus();
  }

  private void SelectionElementsDataGrid2_OnKeyUp(object sender, KeyEventArgs e)
  {
  }

  void ISubscriber<SettingsChangedEvent>.OnHandleEvent(SettingsChangedEvent args)
  {
    int expanderGridRowHeight = ApplicationManager.Current.Settings.GetSelectionExpanderGridRowHeight();
    this.CategoriesDataGrid.MinRowHeight = (double) expanderGridRowHeight;
    this.ArmorCollectionDataGrid.MinRowHeight = (double) expanderGridRowHeight;
    this.WeaponsCollectionDataGrid.MinRowHeight = (double) expanderGridRowHeight;
    this.SelectionElementsDataGrid.MinRowHeight = (double) expanderGridRowHeight;
    this.SelectionElementsDataGrid2.MinRowHeight = (double) expanderGridRowHeight;
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    System.Windows.Application.LoadComponent((object) this, new Uri("/Aurora Builder;component/usercontrols/equipmentsection.xaml", UriKind.Relative));
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
        this.UserControl = (EquipmentSection) target;
        break;
      case 2:
        this.CategoriesDataGrid = (DataGrid) target;
        break;
      case 3:
        this.categoryColumn = (DataGridTextColumn) target;
        break;
      case 4:
        this.ArmorCollectionDataGrid = (DataGrid) target;
        break;
      case 5:
        this.armorNameColumn = (DataGridTextColumn) target;
        break;
      case 6:
        this.WeaponsCollectionDataGrid = (DataGrid) target;
        break;
      case 7:
        this.weaponsNameColumn = (DataGridTextColumn) target;
        break;
      case 8:
        this.ItemsGrid = (Grid) target;
        break;
      case 9:
        this.VisualFocusQue = (Border) target;
        break;
      case 10:
        this.SelectionElementsDataGrid = (DataGrid) target;
        this.SelectionElementsDataGrid.PreviewKeyDown += new KeyEventHandler(this.SelectionElementsDataGrid_OnPreviewKeyDown);
        break;
      case 12:
        this.NameColumn = (DataGridTextColumn) target;
        break;
      case 13:
        this.ItemPriceColumn = (DataGridTextColumn) target;
        break;
      case 14:
        this.ItemWeightColumn = (DataGridTextColumn) target;
        break;
      case 15:
        this.itemsRarityColumn = (DataGridTextColumn) target;
        break;
      case 16 /*0x10*/:
        this.attunementColumn = (DataGridTextColumn) target;
        break;
      case 17:
        this.slotColumn = (DataGridTextColumn) target;
        break;
      case 18:
        this.ArmorClassColumn = (DataGridTextColumn) target;
        break;
      case 19:
        this.ArmorStrengthColumn = (DataGridTextColumn) target;
        break;
      case 20:
        this.ArmorStealthColumn = (DataGridTextColumn) target;
        break;
      case 21:
        this.ArmorGroupColumn = (DataGridTextColumn) target;
        break;
      case 22:
        this.WeaponDamageColumn = (DataGridTextColumn) target;
        break;
      case 23:
        this.WeaponRangeColumn = (DataGridTextColumn) target;
        break;
      case 24:
        this.WeaponGroupColumn = (DataGridTextColumn) target;
        break;
      case 25:
        this.WeaponPropertiesColumn = (DataGridTextColumn) target;
        break;
      case 26:
        this.SourceColumn = (DataGridTextColumn) target;
        break;
      case 27:
        this.SelectionElementsDataGrid2 = (DataGrid) target;
        this.SelectionElementsDataGrid2.PreviewKeyDown += new KeyEventHandler(this.SelectionEquipmentItem_OnPreviewKeyDown);
        this.SelectionElementsDataGrid2.KeyDown += new KeyEventHandler(this.SelectionElementsDataGrid2_OnKeyDown);
        this.SelectionElementsDataGrid2.KeyUp += new KeyEventHandler(this.SelectionElementsDataGrid2_OnKeyUp);
        break;
      case 29:
        this.EquipmentItemActiveColumn = (DataGridTextColumn) target;
        break;
      case 30:
        this.EquipmentItemAttuneColumn = (DataGridTextColumn) target;
        break;
      case 31 /*0x1F*/:
        this.EquipmentItemNameColumn = (DataGridTextColumn) target;
        break;
      case 32 /*0x20*/:
        this.EquipmentItemAdditionColumn = (DataGridTextColumn) target;
        break;
      case 33:
        this.EquipmentItemAttunementColumn = (DataGridTextColumn) target;
        break;
      case 34:
        this.EquipmentItemEquippedColumn = (DataGridTextColumn) target;
        break;
      case 35:
        this.ItemcardVisibleColumn = (DataGridTextColumn) target;
        break;
      case 36:
        this.EquipmentItemStashColumn = (DataGridTextColumn) target;
        break;
      default:
        this._contentLoaded = true;
        break;
    }
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IStyleConnector.Connect(int connectionId, object target)
  {
    if (connectionId != 11)
    {
      if (connectionId != 28)
        return;
      ((Style) target).Setters.Add((SetterBase) new EventSetter()
      {
        Event = Control.MouseDoubleClickEvent,
        Handler = (Delegate) new MouseButtonEventHandler(this.SelectionEquipmentItem_OnMouseDoubleClick)
      });
    }
    else
      ((Style) target).Setters.Add((SetterBase) new EventSetter()
      {
        Event = Control.MouseDoubleClickEvent,
        Handler = (Delegate) new MouseButtonEventHandler(this.SelectionElementsDataGrid_OnMouseDoubleClick)
      });
  }
}
