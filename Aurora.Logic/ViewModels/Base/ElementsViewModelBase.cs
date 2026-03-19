// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.Base.ElementsViewModelBase
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Data;
using Builder.Presentation.Services.Data;
using System.Collections.Generic;

#nullable disable
namespace Builder.Presentation.ViewModels.Base;

public class ElementsViewModelBase : ViewModelBase
{
  private ElementBase _selectedElement;

  public ElementsViewModelBase()
  {
    if (this.IsInDesignMode)
      return;
    this.Elements = new ElementBaseCollection((IEnumerable<ElementBase>) DataManager.Current.ElementsCollection);
  }

  public ElementBaseCollection Elements { get; set; }

  public ElementBase SelectedElement
  {
    get => this._selectedElement;
    set
    {
      this.OnSelectedElementChanged(this.SetProperty<ElementBase>(ref this._selectedElement, value, nameof (SelectedElement)));
    }
  }

  public virtual void OnSelectedElementChanged(bool isChanged)
  {
  }
}
