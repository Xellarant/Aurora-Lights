// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.CharacterContentOrganizer
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Data;
using Builder.Data.Rules;
using Builder.Presentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Builder.Presentation;

public class CharacterContentOrganizer
{
  public CharacterManager Manager => CharacterManager.Current;

  public IEnumerable<ElementBase> GetFeatures(List<ElementBase> elements)
  {
    List<ElementBase> source = new List<ElementBase>();
    source.AddRange(elements.Where<ElementBase>((Func<ElementBase, bool>) (x => x.Type == "Vision")));
    source.AddRange(elements.Where<ElementBase>((Func<ElementBase, bool>) (x => x.Type == "Race")));
    source.AddRange(elements.Where<ElementBase>((Func<ElementBase, bool>) (x => x.Type == "Race Variant")));
    source.AddRange(elements.Where<ElementBase>((Func<ElementBase, bool>) (x => x.Type == "Sub Race")));
    source.AddRange(elements.Where<ElementBase>((Func<ElementBase, bool>) (x => x.Type == "Racial Trait")));
    source.AddRange(elements.Where<ElementBase>((Func<ElementBase, bool>) (x => x.Type == "Class")));
    source.AddRange(elements.Where<ElementBase>((Func<ElementBase, bool>) (x => x.Type == "Class Feature")));
    source.AddRange(elements.Where<ElementBase>((Func<ElementBase, bool>) (x => x.Type == "Archetype")));
    source.AddRange(elements.Where<ElementBase>((Func<ElementBase, bool>) (x => x.Type == "Archetype Feature")));
    source.AddRange(elements.Where<ElementBase>((Func<ElementBase, bool>) (x => x.Type == "Feat")));
    source.AddRange(elements.Where<ElementBase>((Func<ElementBase, bool>) (x => x.Type == "Feat Feature")));
    return source.Where<ElementBase>((Func<ElementBase, bool>) (x => !x.Name.StartsWith("Ability Score Increase"))).Where<ElementBase>((Func<ElementBase, bool>) (x => !x.Name.StartsWith("Ability Score Improvement")));
  }

  public IEnumerable<ElementContainer> GetContainers(List<ElementBase> elements)
  {
    List<ElementBase> list = this.GetFeatures(elements).ToList<ElementBase>();
    List<ElementBase> elementBaseList1 = new List<ElementBase>();
    List<ElementBase> elementBaseList2 = new List<ElementBase>();
    foreach (ElementBase elementBase1 in list)
    {
      if (!elementBaseList1.Contains(elementBase1))
        elementBaseList1.Add(elementBase1);
      if (elementBase1.ContainsSelectRules)
      {
        foreach (SelectRule selectRule in elementBase1.GetSelectRules())
        {
          SelectRule rule = selectRule;
          for (int number = 1; number <= rule.Attributes.Number; ++number)
          {
            if (SelectionRuleExpanderContext.Current.HasExpander(rule.UniqueIdentifier, number))
            {
              ElementBase registeredElement = SelectionRuleExpanderContext.Current.GetRegisteredElement(rule, number) as ElementBase;
              if (list.Contains(registeredElement))
              {
                ElementBase elementBase2 = list.First<ElementBase>((Func<ElementBase, bool>) (x => x.Id == registeredElement.Id));
                if (!elementBaseList1.Contains(elementBase2))
                {
                  ElementBase elementBase3 = CharacterManager.Current.GetElements().Single<ElementBase>((Func<ElementBase, bool>) (x => x.Id == rule.ElementHeader.Id));
                  elementBaseList1.Add(elementBase2);
                  if ((!rule.ElementHeader.Name.StartsWith("Ability Score Increase") && rule.ElementHeader.Type != "Race" || !rule.ElementHeader.Id.StartsWith("ID_CLASS_FEATURE_FEAT_") && rule.ElementHeader.Type != "Class Feature") && elementBase3.SheetDescription.DisplayOnSheet)
                    elementBaseList2.Add(elementBase2);
                }
              }
            }
          }
        }
      }
    }
    List<ElementContainer> containers = new List<ElementContainer>();
    foreach (ElementBase element in elementBaseList1)
    {
      ElementContainer elementContainer = new ElementContainer(element)
      {
        IsNested = elementBaseList2.Contains(element)
      };
      containers.Add(elementContainer);
    }
    return (IEnumerable<ElementContainer>) containers;
  }

  public void UpdateContainers(
    IEnumerable<ElementContainer> containers,
    IEnumerable<ElementContainer> existingContainers)
  {
    foreach (ElementContainer container1 in containers)
    {
      ElementContainer container = container1;
      ElementContainer elementContainer = existingContainers.FirstOrDefault<ElementContainer>((Func<ElementContainer, bool>) (x => x.Element.Id == container.Element.Id));
      if (elementContainer != null)
      {
        container.Name.Content = elementContainer.Name.Content;
        container.Description.Content = elementContainer.Description.Content;
        container.IsEnabled = elementContainer.IsEnabled;
        container.IsNested = elementContainer.IsNested;
      }
    }
  }
}
