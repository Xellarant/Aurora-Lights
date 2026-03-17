// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.Content.SpellsPanelContentViewModel
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core.Events;
using Builder.Data;
using Builder.Data.Elements;
using Builder.Presentation.Events.Character;
using Builder.Presentation.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Builder.Presentation.ViewModels.Content;

public sealed class SpellsPanelContentViewModel : 
  ViewModelBase,
  ISubscriber<CharacterManagerElementRegistered>,
  ISubscriber<CharacterManagerElementUnregistered>
{
  private string _displayCantrips;
  private string _displaySpells;

  public SpellsPanelContentViewModel()
  {
    if (this.IsInDesignMode)
      this.InitializeDesignData();
    else
      this.EventAggregator.Subscribe((object) this);
  }

  public List<Spell> Spells { get; } = new List<Spell>();

  public string DisplayCantrips
  {
    get => this._displayCantrips;
    set => this.SetProperty<string>(ref this._displayCantrips, value, nameof (DisplayCantrips));
  }

  public string DisplaySpells
  {
    get => this._displaySpells;
    set => this.SetProperty<string>(ref this._displaySpells, value, nameof (DisplaySpells));
  }

  public void OnHandleEvent(CharacterManagerElementRegistered args) => this.Handle();

  public void OnHandleEvent(CharacterManagerElementUnregistered args) => this.Handle();

  private void Handle()
  {
    this.DisplayCantrips = string.Join(", ", CharacterManager.Current.GetElements().Where<ElementBase>((Func<ElementBase, bool>) (x => x.Type.Equals("Spell"))).Cast<Spell>().Where<Spell>((Func<Spell, bool>) (x => x.Level == 0)).OrderBy<Spell, string>((Func<Spell, string>) (x => x.Name)).Select<Spell, string>((Func<Spell, string>) (x => x.Name)));
    this.DisplaySpells = string.Join(", ", CharacterManager.Current.GetElements().Where<ElementBase>((Func<ElementBase, bool>) (x => x.Type.Equals("Spell"))).Cast<Spell>().Where<Spell>((Func<Spell, bool>) (x => x.Level > 0)).OrderBy<Spell, int>((Func<Spell, int>) (x => x.Level)).ThenBy<Spell, string>((Func<Spell, string>) (x => x.Name)).Select<Spell, string>((Func<Spell, string>) (x => x.Name)));
  }

  protected override void InitializeDesignData()
  {
    base.InitializeDesignData();
    this.DisplayCantrips = "Eldritch Blast, Shocking Grasp, Firebolt";
    this.DisplaySpells = "Fireball, Lightning Bolt, Fly, Teleport";
  }
}
