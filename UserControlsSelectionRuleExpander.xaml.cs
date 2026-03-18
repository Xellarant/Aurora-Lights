// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.UserControls.SelectionRuleExpander
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core.Events;
using Builder.Core.Logging;
using Builder.Data;
using Builder.Data.Rules;
using Builder.Presentation.Controls;
using Builder.Presentation.Events.Application;
using Builder.Presentation.Extensions;
using Builder.Presentation.Interfaces;
using Builder.Presentation.Properties;
using Builder.Presentation.ViewModels;
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

public partial class SelectionRuleExpander : 
  UserControl,
  ISelectionRuleExpander,
  ISubscriber<SettingsChangedEvent>,
  IComponentConnector
{
  private bool _toggleHeaders;
  internal Border VisualFocusQue;
  internal AuroraExpander Expander;
  internal DataGrid SelectionElementsDataGrid;
  internal DataGridTextColumn NameColumn;
  internal DataGridTextColumn ShortDescriptionColumn;
  internal DataGridTextColumn PrerequisitesColumn;
  internal DataGridTextColumn SourceColumn;
  private bool _contentLoaded;

  public SelectionRuleExpanderViewModel ViewModel
  {
    get => this.GetViewModel<SelectionRuleExpanderViewModel>();
  }

  public SelectionRuleExpander(SelectRule selectionRule, int number = 1)
  {
    this.SelectionRule = selectionRule != null ? selectionRule : throw new ArgumentNullException(nameof (selectionRule));
    this.Number = number;
    this.InitializeComponent();
    ApplicationManager.Current.EventAggregator.Subscribe((object) this);
  }

  public SelectRule SelectionRule { get; }

  public int Number { get; }

  public string UniqueIdentifier => this.SelectionRule.UniqueIdentifier;

  public bool IsSelectionMade()
  {
    return this.GetViewModel<SelectionRuleExpanderViewModel>().ElementRegistered;
  }

  public void SetSelection(string elementId)
  {
    this.GetViewModel<SelectionRuleExpanderViewModel>().SetSelectionAndRegister(elementId);
  }

  public ElementBase RegisteredElement
  {
    get => this.GetViewModel<SelectionRuleExpanderViewModel>().GetRegisteredElement();
  }

  public void FocusExpander()
  {
    this.Expander.Focus();
    this.Expander.IsExpanded = true;
    this.VisualFocusQue.BringIntoView();
    ApplicationManager.Current.EventAggregator.Send<ExpanderFocusedEvent>(new ExpanderFocusedEvent((ISelectionRuleExpander) this));
  }

  protected override async void OnInitialized(EventArgs e)
  {
    SelectionRuleExpander control = this;
    // ISSUE: reference to a compiler-generated method
    control.<>n__0(e);
    // ISSUE: explicit non-virtual call
    control.DataContext = (object) new SelectionRuleExpanderViewModel(__nonvirtual (control.SelectionRule));
    // ISSUE: explicit non-virtual call
    if (__nonvirtual (control.SelectionRule).Attributes.Type == "Spell")
      control.Expander.IsExpanded = false;
    try
    {
      await control.GetViewModel().InitializeAsync();
      control._toggleHeaders = !Settings.Default.DisplaySelectionExpanderColumnHeaders;
      control.DisplayColumnHeaders(Settings.Default.DisplaySelectionExpanderColumnHeaders);
      switch (Settings.Default.SelectionExpanderGridRowSize)
      {
        case 1:
          control.SelectionElementsDataGrid.MinRowHeight = 17.0;
          break;
        case 2:
          control.SelectionElementsDataGrid.MinRowHeight = 21.0;
          break;
        case 3:
          control.SelectionElementsDataGrid.MinRowHeight = 25.0;
          break;
        default:
          control.SelectionElementsDataGrid.MinRowHeight = 21.0;
          Settings.Default.SelectionExpanderGridRowSize = 2;
          Settings.Default.Save();
          break;
      }
    }
    catch (Exception ex)
    {
      Logger.Exception(ex, nameof (OnInitialized));
    }
    bool display1 = false;
    bool display2 = false;
    bool display3 = true;
    // ISSUE: explicit non-virtual call
    switch (__nonvirtual (control.SelectionRule).Attributes.Type)
    {
      case "Alignment":
        display3 = false;
        break;
      case "Archetype":
      case "Archetype Feature":
      case "Class":
      case "Class Feature":
      case "Companion":
      case "Deity":
      case "Spell":
        display1 = true;
        break;
      case "Background":
        display1 = true;
        break;
      case "Feat":
        display2 = true;
        break;
      case "Multiclass":
        display2 = true;
        break;
    }
    control.DisplayNameColumn();
    control.DisplayShortDescriptionColumn(display1);
    control.DisplayRequirementsColumnColumn(display2);
    control.DisplaySourceColumn(display3);
    if (!control.ViewModel.IsInDebugMode)
      return;
    control.Expander.ToolTip = (object) $"{control} -";
    AuroraExpander expander1 = control.Expander;
    // ISSUE: explicit non-virtual call
    expander1.ToolTip = (object) $"{expander1.ToolTip?.ToString()} [supports:{__nonvirtual (control.SelectionRule).Attributes.Supports}]";
    AuroraExpander expander2 = control.Expander;
    // ISSUE: explicit non-virtual call
    expander2.ToolTip = (object) $"{expander2.ToolTip?.ToString()} [requirements:{__nonvirtual (control.SelectionRule).Attributes.Requirements}]";
  }

  private void DisplayColumnHeaders(bool display = true)
  {
    this.SelectionElementsDataGrid.HeadersVisibility = display ? DataGridHeadersVisibility.Column : DataGridHeadersVisibility.None;
    this._toggleHeaders = !this._toggleHeaders;
  }

  private void DisplayNameColumn(bool display = true)
  {
    this.NameColumn.Visibility = display ? Visibility.Visible : Visibility.Collapsed;
  }

  private void DisplayShortDescriptionColumn(bool display = true)
  {
    this.ShortDescriptionColumn.Visibility = display ? Visibility.Visible : Visibility.Collapsed;
  }

  private void DisplayRequirementsColumnColumn(bool display = true)
  {
    this.PrerequisitesColumn.Visibility = display ? Visibility.Visible : Visibility.Collapsed;
  }

  private void DisplaySourceColumn(bool display = true)
  {
    this.SourceColumn.Visibility = display ? Visibility.Visible : Visibility.Collapsed;
  }

  private void Register()
  {
    this.GetViewModel<SelectionRuleExpanderViewModel>().RegisterElementCommand.Execute((object) null);
  }

  private void Unregister()
  {
    this.GetViewModel<SelectionRuleExpanderViewModel>().UnregisterElementCommand.Execute((object) null);
  }

  private void ToggleColumnHeaders(object sender, RoutedEventArgs e)
  {
    this.DisplayColumnHeaders(!this._toggleHeaders);
  }

  private void ElementsGridKeyDown(object sender, KeyEventArgs e)
  {
    if (e.Key != Key.Return && e.Key != Key.Space)
      return;
    this.Register();
  }

  private void ElementsGridMouseDoubleClick(object sender, MouseButtonEventArgs e)
  {
    this.Register();
  }

  private void UnregisterElement(object sender, RoutedEventArgs e) => this.Unregister();

  void ISubscriber<SettingsChangedEvent>.OnHandleEvent(SettingsChangedEvent args)
  {
    switch (args.Settings.SelectionExpanderGridRowSize)
    {
      case 1:
        this.SelectionElementsDataGrid.MinRowHeight = 17.0;
        break;
      case 2:
        this.SelectionElementsDataGrid.MinRowHeight = 21.0;
        break;
      case 3:
        this.SelectionElementsDataGrid.MinRowHeight = 25.0;
        break;
      default:
        this.SelectionElementsDataGrid.MinRowHeight = 21.0;
        Settings.Default.SelectionExpanderGridRowSize = 2;
        Settings.Default.Save();
        break;
    }
  }

  public override string ToString()
  {
    return $"{nameof (SelectionRuleExpander)} ({this.Number}): ({this.SelectionRule})";
  }

  public void Hide()
  {
    if (this.GetViewModel().IsInDebugMode)
      this.IsEnabled = false;
    else
      this.Visibility = Visibility.Collapsed;
  }

  public void Unhide()
  {
    if (this.GetViewModel().IsInDebugMode)
      this.IsEnabled = true;
    else
      this.Visibility = Visibility.Visible;
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    System.Windows.Application.LoadComponent((object) this, new Uri("/Aurora Builder;component/usercontrols/selectionruleexpander.xaml", UriKind.Relative));
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
        this.VisualFocusQue = (Border) target;
        break;
      case 2:
        this.Expander = (AuroraExpander) target;
        break;
      case 3:
        this.SelectionElementsDataGrid = (DataGrid) target;
        this.SelectionElementsDataGrid.KeyDown += new KeyEventHandler(this.ElementsGridKeyDown);
        this.SelectionElementsDataGrid.MouseDoubleClick += new MouseButtonEventHandler(this.ElementsGridMouseDoubleClick);
        break;
      case 4:
        this.NameColumn = (DataGridTextColumn) target;
        break;
      case 5:
        this.ShortDescriptionColumn = (DataGridTextColumn) target;
        break;
      case 6:
        this.PrerequisitesColumn = (DataGridTextColumn) target;
        break;
      case 7:
        this.SourceColumn = (DataGridTextColumn) target;
        break;
      case 8:
        ((ButtonBase) target).Click += new RoutedEventHandler(this.UnregisterElement);
        break;
      default:
        this._contentLoaded = true;
        break;
    }
  }
}
