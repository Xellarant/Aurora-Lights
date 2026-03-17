// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.SelectionElementCollection
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

#nullable disable
namespace Builder.Presentation.ViewModels;

public class SelectionElementCollection : ObservableCollection<SelectionElement>
{
  public void Initialize(IEnumerable<SelectionElement> selectionElements, bool recommendedFirst = false)
  {
    this.Clear();
    if (recommendedFirst)
    {
      foreach (SelectionElement selectionElement in selectionElements.Where<SelectionElement>((Func<SelectionElement, bool>) (x => x.IsRecommended)))
        this.Add(selectionElement);
      foreach (SelectionElement selectionElement in selectionElements.Where<SelectionElement>((Func<SelectionElement, bool>) (x => !x.IsRecommended)))
        this.Add(selectionElement);
    }
    else
    {
      foreach (SelectionElement selectionElement in selectionElements)
        this.Add(selectionElement);
    }
  }
}
