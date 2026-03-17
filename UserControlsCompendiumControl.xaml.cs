// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.UserControls.CompendiumControl
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Presentation.Extensions;
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

public partial class CompendiumControl : UserControl, IComponentConnector
{
  internal DataGrid SearchResultsDataGrid;
  internal DataGridTextColumn NameColumn;
  internal DataGridTextColumn TypeColumn;
  internal DataGridTextColumn SourceColumn;
  private bool _contentLoaded;

  public CompendiumControl() => this.InitializeComponent();

  private void SearchResultsDataGrid_OnKeyDown(object sender, KeyEventArgs e)
  {
    if (e.Key != Key.F1)
      return;
    this.GetViewModel<CompendiumControlViewModel>().DeveloperCopyDetailsCommand.Execute((object) null);
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/Aurora Builder;component/usercontrols/compendiumcontrol.xaml", UriKind.Relative));
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
        this.SearchResultsDataGrid = (DataGrid) target;
        this.SearchResultsDataGrid.KeyDown += new KeyEventHandler(this.SearchResultsDataGrid_OnKeyDown);
        break;
      case 2:
        this.NameColumn = (DataGridTextColumn) target;
        break;
      case 3:
        this.TypeColumn = (DataGridTextColumn) target;
        break;
      case 4:
        this.SourceColumn = (DataGridTextColumn) target;
        break;
      default:
        this._contentLoaded = true;
        break;
    }
  }
}
