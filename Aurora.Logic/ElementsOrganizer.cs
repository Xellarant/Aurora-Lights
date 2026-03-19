// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ElementsOrganizer
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core.Logging;
using Builder.Data;
using Builder.Data.Elements;
using Builder.Data.Rules;
using Builder.Presentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Builder.Presentation;

public class ElementsOrganizer
{
  public IEnumerable<ElementBase> Elements { get; }

  public ElementsOrganizer(IEnumerable<ElementBase> elements)
  {
    this.Elements = (IEnumerable<ElementBase>) new List<ElementBase>(elements);
  }

  public bool ContainsType(string type)
  {
    return this.Elements.Any<ElementBase>((Func<ElementBase, bool>) (x => x.Type == type));
  }

  public IEnumerable<ElementBase> GetTypes(string type, bool includeDuplicates = true)
  {
    return includeDuplicates ? this.Elements.Where<ElementBase>((Func<ElementBase, bool>) (element => element.Type == type)) : this.Elements.Where<ElementBase>((Func<ElementBase, bool>) (element => element.Type == type)).GroupBy<ElementBase, string>((Func<ElementBase, string>) (x => x.Id)).Select<IGrouping<string, ElementBase>, ElementBase>((Func<IGrouping<string, ElementBase>, ElementBase>) (x => x.First<ElementBase>()));
  }

  public IEnumerable<T> GetTypes<T>(string type, bool includeDuplicates = true) where T : ElementBase
  {
    return includeDuplicates ? this.Elements.Where<ElementBase>((Func<ElementBase, bool>) (element => element.Type == type)).Cast<T>() : this.Elements.Where<ElementBase>((Func<ElementBase, bool>) (element => element.Type == type)).GroupBy<ElementBase, string>((Func<ElementBase, string>) (x => x.Id)).Select<IGrouping<string, ElementBase>, ElementBase>((Func<IGrouping<string, ElementBase>, ElementBase>) (x => x.First<ElementBase>())).Cast<T>();
  }

  public IEnumerable<Language> GetLanguages(bool includeDuplicates = true)
  {
    return this.GetTypes<Language>("Language", includeDuplicates);
  }

  public IEnumerable<Proficiency> GetProficiencies(bool includeDuplicates = true)
  {
    return this.GetTypes<Proficiency>("Proficiency", includeDuplicates);
  }

  public IEnumerable<Proficiency> GetWeaponProficiencies(bool includeDuplicates = true, bool trimName = false)
  {
    return this.GetTypes<Proficiency>("Proficiency", includeDuplicates).Where<Proficiency>((Func<Proficiency, bool>) (x => x.Name.StartsWith("Weapon")));
  }

  public IEnumerable<Proficiency> GetArmorProficiencies(bool includeDuplicates = true)
  {
    return this.GetTypes<Proficiency>("Proficiency", includeDuplicates).Where<Proficiency>((Func<Proficiency, bool>) (x => x.Name.StartsWith("Armor")));
  }

  public IEnumerable<Proficiency> GetToolProficiencies(bool includeDuplicates = true)
  {
    return this.GetTypes<Proficiency>("Proficiency", includeDuplicates).Where<Proficiency>((Func<Proficiency, bool>) (x => x.Name.StartsWith("Tool")));
  }

  public IEnumerable<Race> GetRaces(bool includeDuplicates = true)
  {
    return this.GetTypes<Race>("Race", includeDuplicates);
  }

  public IEnumerable<Class> GetClasses(bool includeDuplicates = true)
  {
    return this.GetTypes<Class>("Class", includeDuplicates);
  }

  public IEnumerable<ElementBase> GetBackgrounds(bool includeDuplicates = true)
  {
    return this.GetTypes<ElementBase>("Background", includeDuplicates);
  }

  public IEnumerable<Spell> GetSpells(bool includeDuplicates = true)
  {
    return this.GetTypes<Spell>("Spell", includeDuplicates);
  }

  public IEnumerable<ElementBase> GetInternalSupports() => this.GetTypes<ElementBase>("Support");

  public IEnumerable<ElementBase> GetSortedFeatures(List<ElementBase> children)
  {
    List<ElementBase> list = this.Elements.Where<ElementBase>((Func<ElementBase, bool>) (x => x.Type == "Vision" || x.Type == "Race" || x.Type == "Sub Race" || x.Type == "Race Variant" || x.Type == "Racial Trait" || x.Type == "Language Feature" || x.Type == "Class" || x.Type == "Class Feature" || x.Type == "Archetype" || x.Type == "Archetype Feature" || x.Type == "Feat" || x.Type == "Feat Feature")).Where<ElementBase>((Func<ElementBase, bool>) (x => !x.Name.StartsWith("Ability Score Increase"))).Where<ElementBase>((Func<ElementBase, bool>) (x => !x.Name.StartsWith("Ability Score Improvement"))).ToList<ElementBase>();
    Logger.Info("====================features post sorting====================");
    List<ElementBase> sortedFeatures = new List<ElementBase>();
    foreach (ElementBase elementBase1 in list)
    {
      if (!sortedFeatures.Contains(elementBase1))
      {
        sortedFeatures.Add(elementBase1);
        Logger.Info($"{elementBase1}");
      }
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
                if (!sortedFeatures.Contains(elementBase2))
                {
                  ElementBase elementBase3 = CharacterManager.Current.GetElements().Single<ElementBase>((Func<ElementBase, bool>) (x => x.Id == rule.ElementHeader.Id));
                  sortedFeatures.Add(elementBase2);
                  if (!rule.ElementHeader.Name.StartsWith("Ability Score Increase") && rule.ElementHeader.Type != "Race" || !rule.ElementHeader.Id.StartsWith("ID_CLASS_FEATURE_FEAT_") && rule.ElementHeader.Type != "Class Feature")
                  {
                    if (elementBase3.SheetDescription.DisplayOnSheet)
                    {
                      children.Add(elementBase2);
                      Logger.Info($"\t{elementBase2}");
                    }
                    else
                      Logger.Info($"\t{elementBase2}");
                  }
                  else
                    Logger.Info($"{elementBase2}");
                }
              }
            }
          }
        }
      }
    }
    return (IEnumerable<ElementBase>) sortedFeatures;
  }

  public IEnumerable<ElementBase> GetSortedFeaturesExcludingRacialTraits(List<ElementBase> children)
  {
    List<ElementBase> list = this.Elements.Where<ElementBase>((Func<ElementBase, bool>) (x => x.Type == "Vision" || x.Type == "Language Feature" || x.Type == "Class" || x.Type == "Class Feature" || x.Type == "Archetype" || x.Type == "Archetype Feature" || x.Type == "Feat" || x.Type == "Feat Feature")).Where<ElementBase>((Func<ElementBase, bool>) (x => !x.Name.StartsWith("Ability Score Increase"))).Where<ElementBase>((Func<ElementBase, bool>) (x => !x.Name.StartsWith("Ability Score Improvement"))).ToList<ElementBase>();
    Logger.Info("====================features post sorting====================");
    List<ElementBase> excludingRacialTraits = new List<ElementBase>();
    foreach (ElementBase elementBase1 in list)
    {
      if (!excludingRacialTraits.Contains(elementBase1))
      {
        excludingRacialTraits.Add(elementBase1);
        Logger.Info($"{elementBase1}");
      }
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
                if (!excludingRacialTraits.Contains(elementBase2))
                {
                  ElementBase elementBase3 = CharacterManager.Current.GetElements().Single<ElementBase>((Func<ElementBase, bool>) (x => x.Id == rule.ElementHeader.Id));
                  excludingRacialTraits.Add(elementBase2);
                  if (!rule.ElementHeader.Name.StartsWith("Ability Score Increase") && rule.ElementHeader.Type != "Race" || !rule.ElementHeader.Id.StartsWith("ID_CLASS_FEATURE_FEAT_") && rule.ElementHeader.Type != "Class Feature")
                  {
                    if (elementBase3.SheetDescription.DisplayOnSheet)
                    {
                      children.Add(elementBase2);
                      Logger.Info($"\t{elementBase2}");
                    }
                    else
                      Logger.Info($"\t{elementBase2}");
                  }
                  else
                    Logger.Info($"{elementBase2}");
                }
              }
            }
          }
        }
      }
    }
    return (IEnumerable<ElementBase>) excludingRacialTraits;
  }

  public IEnumerable<ElementBase> GetSortedRacialTraits(List<ElementBase> children)
  {
    List<ElementBase> list = this.Elements.Where<ElementBase>((Func<ElementBase, bool>) (x => x.Type == "Race" || x.Type == "Sub Race" || x.Type == "Race Variant" || x.Type == "Racial Trait")).Where<ElementBase>((Func<ElementBase, bool>) (x => !x.Name.StartsWith("Ability Score Increase"))).Where<ElementBase>((Func<ElementBase, bool>) (x => !x.Name.StartsWith("Ability Score Improvement"))).ToList<ElementBase>();
    Logger.Info("====================features post sorting====================");
    List<ElementBase> sortedRacialTraits = new List<ElementBase>();
    foreach (ElementBase elementBase1 in list)
    {
      if (!sortedRacialTraits.Contains(elementBase1))
      {
        sortedRacialTraits.Add(elementBase1);
        Logger.Info($"{elementBase1}");
      }
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
                if (!sortedRacialTraits.Contains(elementBase2))
                {
                  ElementBase elementBase3 = CharacterManager.Current.GetElements().Single<ElementBase>((Func<ElementBase, bool>) (x => x.Id == rule.ElementHeader.Id));
                  sortedRacialTraits.Add(elementBase2);
                  if (!rule.ElementHeader.Name.StartsWith("Ability Score Increase") && rule.ElementHeader.Type != "Race" || !rule.ElementHeader.Id.StartsWith("ID_CLASS_FEATURE_FEAT_") && rule.ElementHeader.Type != "Class Feature")
                  {
                    if (elementBase3.SheetDescription.DisplayOnSheet)
                    {
                      children.Add(elementBase2);
                      Logger.Info($"\t{elementBase2}");
                    }
                    else
                      Logger.Info($"\t{elementBase2}");
                  }
                  else
                    Logger.Info($"{elementBase2}");
                }
              }
            }
          }
        }
      }
    }
    return (IEnumerable<ElementBase>) sortedRacialTraits;
  }
}
