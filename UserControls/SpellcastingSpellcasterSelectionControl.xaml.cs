// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.UserControls.Spellcasting.SpellcasterSelectionControl
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Presentation.Extensions;
using Builder.Presentation.Services;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

#nullable disable
namespace Builder.Presentation.UserControls.Spellcasting;

public partial class SpellcasterSelectionControl : UserControl, IComponentConnector
{
  internal Border VisualFocusQue;
  internal DataGrid SelectionElementsDataGrid;
  internal DataGridTextColumn registeredColumn;
  internal DataGridTextColumn NameColumn;
  internal DataGridTextColumn ShortDescriptionColumn;
  internal DataGridTextColumn verbal;
  internal DataGridTextColumn somatic;
  internal DataGridTextColumn material;
  internal DataGridTextColumn SourceColumn;
  private bool _contentLoaded;

  public SpellcasterSelectionControl() => this.InitializeComponent();

  private void ListView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
  {
  }

  private void ListViewKnown_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
  {
  }

  private void ListView2_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
  {
  }

  private void SelectionElementsDataGrid_OnPreviewKeyDown(object sender, KeyEventArgs e)
  {
    SpellcasterSelectionControlViewModel viewModel = this.GetViewModel<SpellcasterSelectionControlViewModel>();
    Key key = e.Key;
    if (key <= Key.Escape)
    {
      if (key == Key.Tab)
        ;
    }
    else if (key != Key.Space)
    {
      if (key == Key.Delete)
        ;
    }
    else
      viewModel.TogglePrepareSpellCommand.Execute((object) null);
  }

  private void SelectionElementsDataGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
  {
    try
    {
      if (e.OriginalSource.GetType() == typeof (ScrollViewer))
        return;
      this.GetViewModel<SpellcasterSelectionControlViewModel>().TogglePrepareSpellCommand.Execute((object) null);
    }
    catch (Exception ex)
    {
      MessageDialogService.ShowException(ex);
    }
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/Aurora Builder;component/usercontrols/spellcasting/spellcasterselectioncontrol.xaml", UriKind.Relative));
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
        this.SelectionElementsDataGrid = (DataGrid) target;
        this.SelectionElementsDataGrid.MouseDoubleClick += new MouseButtonEventHandler(this.SelectionElementsDataGrid_OnMouseDoubleClick);
        this.SelectionElementsDataGrid.PreviewKeyDown += new KeyEventHandler(this.SelectionElementsDataGrid_OnPreviewKeyDown);
        break;
      case 3:
        this.registeredColumn = (DataGridTextColumn) target;
        break;
      case 4:
        this.NameColumn = (DataGridTextColumn) target;
        break;
      case 5:
        this.ShortDescriptionColumn = (DataGridTextColumn) target;
        break;
      case 6:
        this.verbal = (DataGridTextColumn) target;
        break;
      case 7:
        this.somatic = (DataGridTextColumn) target;
        break;
      case 8:
        this.material = (DataGridTextColumn) target;
        break;
      case 9:
        this.SourceColumn = (DataGridTextColumn) target;
        break;
      default:
        this._contentLoaded = true;
        break;
    }
  }
}
