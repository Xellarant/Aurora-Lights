// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.Content.ConditionsPanelContentViewModel
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core.Events;
using Builder.Data;
using Builder.Presentation.Events.Character;
using Builder.Presentation.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Builder.Presentation.ViewModels.Content;

public sealed class ConditionsPanelContentViewModel : 
  ViewModelBase,
  ISubscriber<CharacterManagerElementRegistered>,
  ISubscriber<CharacterManagerElementUnregistered>
{
  private string _displayResistances;

  public ConditionsPanelContentViewModel()
  {
    this.Resistances = new ElementBaseCollection();
    if (this.IsInDesignMode)
      this.InitializeDesignData();
    else
      this.EventAggregator.Subscribe((object) this);
  }

  public ElementBaseCollection Resistances { get; }

  public bool HasConditions => this.Resistances.Any<ElementBase>();

  public string DisplayResistances
  {
    get => this._displayResistances;
    set
    {
      this.SetProperty<string>(ref this._displayResistances, value, nameof (DisplayResistances));
    }
  }

  public void OnHandleEvent(CharacterManagerElementRegistered args) => this.Handle();

  public void OnHandleEvent(CharacterManagerElementUnregistered args) => this.Handle();

  private void Handle()
  {
    List<ElementBase> list = CharacterManager.Current.GetElements().Where<ElementBase>((Func<ElementBase, bool>) (x => x.Type.Equals("Condition"))).OrderBy<ElementBase, string>((Func<ElementBase, string>) (x => x.Name)).ToList<ElementBase>();
    IEnumerable<ElementBase> elements1 = list.Where<ElementBase>((Func<ElementBase, bool>) (x => x.Supports.Contains("Resistance")));
    IEnumerable<ElementBase> elements2 = list.Where<ElementBase>((Func<ElementBase, bool>) (x => x.Supports.Contains("Vulnerability")));
    IEnumerable<ElementBase> elements3 = list.Where<ElementBase>((Func<ElementBase, bool>) (x => x.Supports.Contains("Immunity")));
    this.Resistances.Clear();
    this.Resistances.AddRange(elements1);
    this.Resistances.AddRange(elements2);
    this.Resistances.AddRange(elements3);
    this.DisplayResistances = string.Join(", ", list.Select<ElementBase, string>((Func<ElementBase, string>) (x => x.Name)));
    this.OnPropertyChanged("HasConditions");
  }

  protected override void InitializeDesignData()
  {
    base.InitializeDesignData();
    this.DisplayResistances = "Acid, Fire, Slashing";
  }
}
