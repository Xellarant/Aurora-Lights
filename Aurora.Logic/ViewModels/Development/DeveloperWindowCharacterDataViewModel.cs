// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.Development.DeveloperWindowCharacterDataViewModel
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core.Events;
using Builder.Data;
using Builder.Presentation.Events.Character;
using Builder.Presentation.ViewModels.Base;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Builder.Presentation.ViewModels.Development;

public class DeveloperWindowCharacterDataViewModel : 
  ViewModelBase,
  ISubscriber<CharacterManagerElementRegistered>
{
  private ElementBase _selectedElement;

  public DeveloperWindowCharacterDataViewModel()
  {
    if (this.IsInDesignMode)
      return;
    this.EventAggregator.Subscribe((object) this);
  }

  public Builder.Presentation.Models.Character Character => CharacterManager.Current.Character;

  public ElementBaseCollection Elements { get; set; } = new ElementBaseCollection();

  public ElementBase SelectedElement
  {
    get => this._selectedElement;
    set
    {
      this.SetProperty<ElementBase>(ref this._selectedElement, value, nameof (SelectedElement));
    }
  }

  public void OnHandleEvent(CharacterManagerElementRegistered args)
  {
    this.Elements.Clear();
    this.Elements.AddRange((IEnumerable<ElementBase>) CharacterManager.Current.GetElements().ToList<ElementBase>());
  }
}
