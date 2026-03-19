using Builder.Data;
using Builder.Data.Elements;
using Builder.Data.Extensions;
using Builder.Presentation.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace Builder.Presentation.Utilities;

/// <summary>
/// Platform-independent helper for generating HTML card headers from element data.
/// Extracted from DescriptionPanelViewModelBase so it can be used in Aurora.Logic
/// without a WPF dependency.
/// </summary>
public static class ElementCardHelper
{
    public static string GenerateHeaderForCard(ElementBase element)
    {
        StringBuilder stringBuilder = new StringBuilder();
        switch (element.Type)
        {
            case "Alignment":
                stringBuilder.AppendLine("<p>");
                stringBuilder.AppendLine($"<b>Abbreviation:</b> {element.ElementSetters.GetSetter("Abbreviation").Value}<br/>");
                stringBuilder.AppendLine("</p>");
                break;
            case "Armor":
                ArmorElement armorElement = element.AsElement<ArmorElement>();
                stringBuilder.AppendLine("<p>");
                stringBuilder.AppendLine(armorElement.ElementSetters.ContainsSetter("armorClass") ? "<br/><b><i>Armor Class. </i></b>" + armorElement.ElementSetters.GetSetter("armorClass").Value : "");
                stringBuilder.AppendLine(armorElement.ElementSetters.ContainsSetter("strength") ? "<br/><b><i>Strength. </i></b>" + armorElement.ElementSetters.GetSetter("strength").Value : "<br/><b><i>Strength. </i></b>\u2014");
                stringBuilder.AppendLine(armorElement.ElementSetters.ContainsSetter("stealth") ? "<br/><b><i>Stealth. </i></b>" + armorElement.ElementSetters.GetSetter("stealth").Value : "<br/><b><i>Stealth. </i></b>\u2014");
                stringBuilder.AppendLine("</p>");
                break;
            case "Companion":
                CompanionElement companionElement = element.AsElement<CompanionElement>();
                stringBuilder.Append($"<p><em>{companionElement.CreatureType}</em></p>");
                break;
            case "Magic Item":
                Item obj = element.AsElement<Item>();
                stringBuilder.Append("<p class=\"underline\"> ");
                if (!string.IsNullOrWhiteSpace(obj.ItemType))
                {
                    string additionAttribute = obj.GetSetterAdditionAttribute("type");
                    if (additionAttribute != null)
                        stringBuilder.Append($"{obj.ItemType} ({additionAttribute}), ");
                    else
                        stringBuilder.Append(obj.ItemType + ", ");
                }
                else
                    stringBuilder.Append("Magic item, ");
                if (!string.IsNullOrWhiteSpace(obj.Rarity))
                    stringBuilder.Append(obj.Rarity.ToLower() + " ");
                if (obj.RequiresAttunement)
                {
                    string additionAttribute = obj.GetSetterAdditionAttribute("attunement");
                    if (additionAttribute != null)
                        stringBuilder.Append($"(requires attunement {additionAttribute})");
                    else
                        stringBuilder.Append("(requires attunement)");
                }
                stringBuilder.Append("</p>");
                break;
            case "Spell":
                Spell spell = element.AsElement<Spell>();
                stringBuilder.Append($"<p class=\"underline\">{spell.GetShortDescription()}</p>");
                stringBuilder.Append("<p>");
                stringBuilder.Append($"<b>Casting Time:</b> {spell.CastingTime}<br/>");
                stringBuilder.Append($"<b>Range:</b> {spell.Range}<br/>");
                stringBuilder.Append($"<b>Components:</b> {spell.GetComponentsString()}<br/>");
                stringBuilder.Append($"<b>Duration:</b> {spell.Duration}<br/>");
                stringBuilder.Append("</p>");
                break;
            case "Weapon":
                WeaponElement weaponElement = element.AsElement<WeaponElement>();
                foreach (ElementBase elementBase in DataManager.Current.ElementsCollection.Where<ElementBase>((Func<ElementBase, bool>)(x => x.Type.Equals("Weapon Category"))).OrderBy<ElementBase, string>((Func<ElementBase, string>)(x => x.Name)).ToList<ElementBase>())
                {
                    if (weaponElement.Supports.Contains(elementBase.Id))
                        stringBuilder.AppendLine($"<p class=\"underline\">{elementBase.Name} Weapon</p>");
                }
                List<ElementBase> list = DataManager.Current.ElementsCollection.Where<ElementBase>((Func<ElementBase, bool>)(x => x.Type.Equals("Weapon Property"))).ToList<ElementBase>();
                stringBuilder.Append("<p>");
                List<string> source = new List<string>();
                foreach (string support1 in weaponElement.Supports)
                {
                    string support = support1;
                    ElementBase elementBase = list.FirstOrDefault<ElementBase>((Func<ElementBase, bool>)(x => x.Id.Equals(support)));
                    if (elementBase != null)
                    {
                        string str = source.Any<string>() ? elementBase.Name.ToLower() : elementBase.Name;
                        switch (elementBase.Id)
                        {
                            case "ID_INTERNAL_WEAPON_PROPERTY_THROWN":
                            case "ID_INTERNAL_WEAPON_PROPERTY_AMMUNITION":
                                source.Add($"{str} ({weaponElement.Range})");
                                continue;
                            case "ID_INTERNAL_WEAPON_PROPERTY_VERSATILE":
                                source.Add($"{str} ({weaponElement.Versatile})");
                                continue;
                            case "ID_INTERNAL_WEAPON_PROPERTY_SPECIAL":
                                if (weaponElement.Supports.Count<string>((Func<string, bool>)(x => x.Contains("WEAPON_PROPERTY_SPECIAL"))) == 1)
                                {
                                    source.Add($"{str} ({weaponElement.Versatile})");
                                    continue;
                                }
                                continue;
                            default:
                                source.Add(str);
                                continue;
                        }
                    }
                }
                stringBuilder.AppendLine(source.Any<string>() ? "<b><i>Properties. </i></b>" + string.Join(", ", (IEnumerable<string>)source.OrderBy<string, string>((Func<string, string>)(x => x))) : "<b><i>Properties. </i></b>\u2014");
                stringBuilder.AppendLine($"<br/><b><i>Damage. </i></b>{weaponElement.Damage} {weaponElement.DamageType}");
                stringBuilder.Append("</p>");
                stringBuilder.AppendLine($"Proficiency with a {weaponElement.Name.ToLower()} allows you to add your proficiency bonus to the attack roll for any attack you make with it.");
                break;
        }
        return stringBuilder.ToString();
    }
}
