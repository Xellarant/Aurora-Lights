// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Events.ExpanderSelectionChangedEventArgs
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Data;
using Builder.Data.Rules;

#nullable disable
namespace Builder.Presentation.Events;

public class ExpanderSelectionChangedEventArgs
{
  private readonly SelectRule _selectionRule;
  private readonly ElementBase _selectedElement;

  public ExpanderSelectionChangedEventArgs(SelectRule selectionRule, ElementBase selectedElement)
  {
    this._selectionRule = selectionRule;
    this._selectedElement = selectedElement;
  }

  public SelectRule SelectionRule => this._selectionRule;

  public ElementBase SelectedElement => this._selectedElement;
}
