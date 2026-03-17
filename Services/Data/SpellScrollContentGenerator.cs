// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Services.Data.SpellScrollContentGenerator
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core.Logging;
using Builder.Data;
using Builder.Data.Elements;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

#nullable disable
namespace Builder.Presentation.Services.Data;

public class SpellScrollContentGenerator
{
  public List<ElementBase> Generate(IEnumerable<ElementBase> content, MagicItemElement template)
  {
    List<ElementBase> elementBaseList = new List<ElementBase>();
    if (content == null || template == null)
      return elementBaseList;
    foreach (Spell spell in content.Where<ElementBase>((Func<ElementBase, bool>) (x => x.Type.Equals("Spell"))).Cast<Spell>().ToList<Spell>())
    {
      if (!spell.Id.Contains("_SPELL_"))
      {
        Logger.Warning($"{spell.Name} doesn't contain _SPELL_ in the id ({spell.Id})");
      }
      else
      {
        string input = "ID_INTERNAL_MAGIC_ITEM_SPELL_SCROLL_" + (((IEnumerable<string>) Regex.Split(spell.Id, "_SPELL_")).LastOrDefault<string>() ?? "");
        if (!ElementsHelper.ValidateID(input) && Debugger.IsAttached)
          Debugger.Break();
        string id = ElementsHelper.SanitizeID(input);
        MagicItemElement magicItemElement1 = new MagicItemElement();
        magicItemElement1.ElementHeader = new ElementHeader("Spell Scroll, " + spell.Name, template.Type ?? "", spell.Source, id);
        MagicItemElement magicItemElement2 = magicItemElement1;
        magicItemElement2.CalculableWeight = template.CalculableWeight;
        magicItemElement2.Category = "Spell Scrolls";
        magicItemElement2.ItemType = template.ItemType;
        magicItemElement2.IsStackable = true;
        magicItemElement2.Description = template.Description;
        foreach (ElementSetters.Setter elementSetter in (List<ElementSetters.Setter>) template.ElementSetters)
        {
          if (!(elementSetter.Name == "keywords") && !(elementSetter.Name == "rarity") && !string.IsNullOrWhiteSpace(elementSetter.Value))
          {
            ElementSetters.Setter setter = new ElementSetters.Setter(elementSetter.Name, elementSetter.Value);
            foreach (KeyValuePair<string, string> additionalAttribute in elementSetter.AdditionalAttributes)
              setter.AdditionalAttributes.Add(additionalAttribute.Key, additionalAttribute.Value);
            if (setter.Name == "cost")
            {
              switch (spell.Level)
              {
                case 0:
                  setter.Value = "0";
                  break;
                case 2:
                case 3:
                  setter.Value = "0";
                  break;
                case 4:
                case 5:
                  setter.Value = "0";
                  break;
                case 6:
                case 7:
                case 8:
                  setter.Value = "0";
                  break;
                case 9:
                  setter.Value = "0";
                  break;
              }
            }
            magicItemElement2.ElementSetters.Add(setter);
          }
        }
        if (spell.HasSupports)
        {
          foreach (string support in spell.Supports)
            magicItemElement2.Keywords.Add(support);
        }
        magicItemElement2.Keywords.AddRange((IEnumerable<string>) spell.Keywords);
        int num1 = 0;
        int num2 = 0;
        string str = "Common";
        switch (spell.Level)
        {
          case 0:
          case 1:
            str = "Common";
            num1 = 13;
            num2 = 5;
            break;
          case 2:
            str = "Uncommon";
            num1 = 13;
            num2 = 5;
            break;
          case 3:
            str = "Uncommon";
            num1 = 15;
            num2 = 7;
            break;
          case 4:
            str = "Rare";
            num1 = 15;
            num2 = 7;
            break;
          case 5:
            str = "Rare";
            num1 = 17;
            num2 = 9;
            break;
          case 6:
            str = "Very Rare";
            num1 = 17;
            num2 = 9;
            break;
          case 7:
            str = "Very Rare";
            num1 = 18;
            num2 = 10;
            break;
          case 8:
            str = "Very Rare";
            num1 = 18;
            num2 = 10;
            break;
          case 9:
            str = "Legendary";
            num1 = 19;
            num2 = 11;
            break;
        }
        magicItemElement2.ElementSetters.Add(new ElementSetters.Setter("rarity", str));
        magicItemElement2.Rarity = str;
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("<p>A <em>spell scroll</em> bears the words of a single spell, written in a mystical cipher. If the spell is on your class’s spell list, you can use an action to read the scroll and cast its spell without having to provide any of the spell’s components. Otherwise, the scroll is unintelligible.</p>");
        stringBuilder.Append("<p class=\"indent\">If the spell is on your class’s spell list but of a higher level than you can normally cast, you must make an ability check using your spellcasting ability to determine whether you cast it successfully. The DC equals 10 + the spell’s level. On a failed check, the spell disappears from the scroll with no other effect.</p>");
        stringBuilder.Append("<p class=\"indent\">Once the spell is cast, the words on the scroll fade, and the scroll itself crumbles to dust.</p>");
        stringBuilder.Append($"<p class=\"indent\">The level of the spell on the scroll determines the spell’s saving throw DC ({num1}) and attack bonus (+{num2}), as well as the scroll’s rarity ({str}).</p>");
        stringBuilder.Append("<div class=\"reference\">");
        stringBuilder.Append($"<div element=\"{spell.Id}\" />");
        stringBuilder.Append("</div>");
        magicItemElement2.Description = stringBuilder.ToString();
        elementBaseList.Add((ElementBase) magicItemElement2);
      }
    }
    return elementBaseList;
  }
}
