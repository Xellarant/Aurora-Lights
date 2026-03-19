// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.Content.CompanionTraitsPanelContentViewModel
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core.Events;
using Builder.Data;
using Builder.Data.Elements;
using Builder.Presentation.Events.Character;
using Builder.Presentation.Services.Data;
using Builder.Presentation.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

#nullable disable
namespace Builder.Presentation.ViewModels.Content;

public sealed class CompanionTraitsPanelContentViewModel : 
  ViewModelBase,
  ISubscriber<CharacterManagerElementRegistered>,
  ISubscriber<CharacterManagerElementUnregistered>
{
  public CompanionTraitsPanelContentViewModel()
  {
    this.Traits = new ElementBaseCollection();
    this.Actions = new ElementBaseCollection();
    this.Reactions = new ElementBaseCollection();
    if (this.IsInDesignMode)
      this.InitializeDesignData();
    else
      this.EventAggregator.Subscribe((object) this);
  }

  public ElementBaseCollection Traits { get; }

  public ElementBaseCollection Actions { get; }

  public ElementBaseCollection Reactions { get; }

  public bool HasTraits => this.Traits.Any<ElementBase>();

  public bool HasActions => this.Actions.Any<ElementBase>();

  public bool HasReactions => this.Reactions.Any<ElementBase>();

  public void OnHandleEvent(CharacterManagerElementRegistered args) => this.Handle();

  public void OnHandleEvent(CharacterManagerElementUnregistered args) => this.Handle();

  private void Handle()
  {
    this.Traits.Clear();
    this.Actions.Clear();
    this.Reactions.Clear();
    if (CharacterManager.Current.GetElements().FirstOrDefault<ElementBase>((Func<ElementBase, bool>) (x => x.Type.Equals("Companion"))) is CompanionElement companionElement)
    {
      if (companionElement.Traits.Any<string>())
      {
        List<ElementBase> list = DataManager.Current.ElementsCollection.Where<ElementBase>((Func<ElementBase, bool>) (x => x.Type.Equals("Companion Trait"))).ToList<ElementBase>();
        foreach (string trait in companionElement.Traits)
        {
          string companionTrait = trait;
          ElementBase elementBase = list.FirstOrDefault<ElementBase>((Func<ElementBase, bool>) (x => x.Id.Equals(companionTrait)));
          if (elementBase != null)
            this.Traits.Add(elementBase);
        }
      }
      if (companionElement.Actions.Any<string>())
      {
        List<ElementBase> list = DataManager.Current.ElementsCollection.Where<ElementBase>((Func<ElementBase, bool>) (x => x.Type.Equals("Companion Action"))).ToList<ElementBase>();
        foreach (string action in companionElement.Actions)
        {
          string companionTrait = action;
          ElementBase elementBase = list.FirstOrDefault<ElementBase>((Func<ElementBase, bool>) (x => x.Id.Equals(companionTrait)));
          if (elementBase != null)
            this.Actions.Add(elementBase);
        }
      }
      if (companionElement.Reactions.Any<string>())
      {
        List<ElementBase> list = DataManager.Current.ElementsCollection.Where<ElementBase>((Func<ElementBase, bool>) (x => x.Type.Equals("Companion Reaction"))).ToList<ElementBase>();
        foreach (string reaction in companionElement.Reactions)
        {
          string companionTrait = reaction;
          ElementBase elementBase = list.FirstOrDefault<ElementBase>((Func<ElementBase, bool>) (x => x.Id.Equals(companionTrait)));
          if (elementBase != null)
            this.Reactions.Add(elementBase);
        }
      }
    }
    this.OnPropertyChanged("HasTraits");
    this.OnPropertyChanged("HasActions");
    this.OnPropertyChanged("HasReactions");
  }

  protected override void InitializeDesignData()
  {
    base.InitializeDesignData();
    this.Traits.Add(new ElementBase("Keen Smell", "Companion Trait", "", ""));
    this.Traits.Add(new ElementBase("Keen Smells", "Companion Trait", "", ""));
    this.Actions.Add(new ElementBase("Ink Cloud", "Companion Trait", "", ""));
    this.Actions.Add(new ElementBase("Talons", "Companion Trait", "", ""));
    this.Reactions.Add(new ElementBase("Ink Cloud", "Companion Trait", "", ""));
    this.Reactions.Add(new ElementBase("Talons", "Companion Trait", "", ""));
    foreach (ElementBase trait in (Collection<ElementBase>) this.Traits)
      trait.SheetDescription.Add(new ElementSheetDescriptions.SheetDescription("The bear has advantage on Wisdom (Perception) checks that rely on smell."));
    this.Actions[0].SheetDescription.Usage = "Recharges after a Short or Long Rest";
    this.Actions[0].SheetDescription.Add(new ElementSheetDescriptions.SheetDescription("A 5-foot-radius cloud of ink extends all around the octopus if it is underwater. The area is heavily obscured for 1 minute, although a significant current can disperse the ink. After releasing the ink, the octopus can use the Dash action as a bonus action."));
    this.Actions[1].SheetDescription.Add(new ElementSheetDescriptions.SheetDescription("Melee Weapon Attack: +4 to hit, reach 5 ft., one target. Hit: 4 (1d4 + 2) slashing damage."));
    this.Reactions[0].SheetDescription.Usage = "Recharges after a Short or Long Rest";
    this.Reactions[0].SheetDescription.Add(new ElementSheetDescriptions.SheetDescription("A 5-foot-radius cloud of ink extends all around the octopus if it is underwater. The area is heavily obscured for 1 minute, although a significant current can disperse the ink. After releasing the ink, the octopus can use the Dash action as a bonus action."));
    this.Reactions[1].SheetDescription.Add(new ElementSheetDescriptions.SheetDescription("Melee Weapon Attack: +4 to hit, reach 5 ft., one target. Hit: 4 (1d4 + 2) slashing damage."));
  }
}
