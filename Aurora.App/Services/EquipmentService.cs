using Builder.Presentation;
using Builder.Presentation.Models;
using Builder.Presentation.Services.Data;
using Builder.Presentation.ViewModels.Shell.Items;

namespace Aurora.App.Services;

public enum GearSlot { Armor, MainHand, OffHand }

/// <summary>
/// Service for adding, removing, and equipping inventory items on the active character.
/// </summary>
public static class EquipmentService
{
    private static readonly HashSet<string> ItemTypes = new(StringComparer.OrdinalIgnoreCase)
    {
        "Item", "Weapon", "Armor", "Magic Item", "Ammunition",
        "Tool", "Mount", "Vehicle", "Pack", "Gear", "Adventuring Gear"
    };

    // ── General inventory ───────────────────────────────────────────────────────

    /// <summary>
    /// Searches all loaded elements for those that can be added to inventory.
    /// Returns at most 200 results ordered by name.
    /// </summary>
    public static IReadOnlyList<ItemSearchResult> SearchItems(string query)
    {
        IEnumerable<Builder.Data.ElementBase> source =
            DataManager.Current.ElementsCollection.Where(e =>
                ItemTypes.Contains(e.Type)
                // Exclude Aurora's internal ability-enabling pseudo-items (e.g. "Additional Arcane
                // Dilettante Spell, Fire Shield"). These are named "Additional …" and are designed
                // to stay hidden from the character sheet — they are not real inventory items.
                && !e.Name.StartsWith("Additional ", StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(query))
            source = source.Where(e => e.Name.Contains(query, StringComparison.OrdinalIgnoreCase));

        return source
            .OrderBy(e => e.Name)
            .Take(200)
            .Select(e => new ItemSearchResult(e.Id, e.Name, e.Type, GetDescription(e), e.Source ?? ""))
            .ToList();
    }

    /// <summary>
    /// Searches elements compatible with the given gear slot.
    /// </summary>
    public static IReadOnlyList<ItemSearchResult> SearchItemsForSlot(GearSlot slot, string query)
    {
        IEnumerable<Builder.Data.ElementBase> source =
            DataManager.Current.ElementsCollection.Where(e => IsElementCompatibleWithSlot(e, slot));

        if (!string.IsNullOrWhiteSpace(query))
            source = source.Where(e => e.Name.Contains(query, StringComparison.OrdinalIgnoreCase));

        return source
            .OrderBy(e => e.Name)
            .Take(200)
            .Select(e => new ItemSearchResult(e.Id, e.Name, e.Type, GetDescription(e), e.Source ?? ""))
            .ToList();
    }

    /// <summary>
    /// Returns inventory items compatible with the given gear slot, ordered by name.
    /// </summary>
    public static IReadOnlyList<InventoryItemOption> GetInventoryItemsForSlot(Character character, GearSlot slot) =>
        character.Inventory.Items
            .Where(i => IsItemCompatibleWithSlot(i, slot))
            .OrderBy(i => i.DisplayName ?? i.Name ?? "")
            .Select(i => new InventoryItemOption(i.Identifier, i.DisplayName ?? i.Name ?? ""))
            .ToList();

    // ── Add / remove ─────────────────────────────────────────────────────────────

    /// <summary>
    /// Adds an item to inventory by element ID. Uses reflection because
    /// Builder.Data.Elements.Item cannot be named from Aurora.App.
    /// </summary>
    public static bool AddItem(Character character, string elementId, int amount = 1)
    {
        var element = DataManager.Current.ElementsCollection.GetElement(elementId);
        if (element == null) return false;

        try
        {
            var elementType = element.GetType();

            // Find the constructor whose first parameter accepts this element type,
            // with all remaining parameters optional (has default values).
            var ctor = typeof(RefactoredEquipmentItem)
                .GetConstructors()
                .FirstOrDefault(c =>
                {
                    var ps = c.GetParameters();
                    return ps.Length >= 1
                        && ps[0].ParameterType.IsAssignableFrom(elementType)
                        && ps.Skip(1).All(p => p.HasDefaultValue);
                });

            if (ctor == null) return false;

            // Build the argument list: first arg is the element, rest use their defaults.
            var ps = ctor.GetParameters();
            var args = ps.Select((p, i) => i == 0 ? (object?)element : p.DefaultValue).ToArray();

            var item = (RefactoredEquipmentItem)ctor.Invoke(args);
            item.Amount = Math.Max(1, amount);
            character.Inventory.Items.Add(item);
            return true;
        }
        catch { return false; }
    }

    /// <summary>
    /// Removes the item with the given identifier from inventory (deactivates first).
    /// </summary>
    public static bool RemoveItem(Character character, string identifier)
    {
        var item = character.Inventory.Items.FirstOrDefault(i => i.Identifier == identifier);
        if (item == null) return false;

        // If this item is in a gear slot, unequip it cleanly via the inventory methods.
        if (item.IsEquipped)
        {
            var loc = item.EquippedLocation ?? "";
            if (loc == "Armor")
                character.Inventory.UnequipArmor();
            else if (loc is "Primary Hand" or "Two-Handed" or "Two-Handed (Versatile)")
                character.Inventory.UnequipPrimary();
            else if (loc == "Secondary Hand")
                character.Inventory.UnequipSecondary();
            else
                item.Deactivate();
        }
        else
        {
            item.Deactivate();
        }

        character.Inventory.Items.Remove(item);
        character.Inventory.CalculateAttunedItemCount();
        return true;
    }

    /// <summary>Updates the stack amount for a specific inventory item.</summary>
    public static void SetAmount(Character character, string identifier, int amount)
    {
        var item = character.Inventory.Items.FirstOrDefault(i => i.Identifier == identifier);
        if (item != null)
            item.Amount = Math.Max(1, amount);
    }

    // ── Gear slots ────────────────────────────────────────────────────────────────

    /// <summary>
    /// Equips an existing inventory item to the specified gear slot.
    /// Unequips whatever is currently in the slot first.
    /// </summary>
    public static bool EquipToSlot(Character character, GearSlot slot, string identifier)
    {
        var item = character.Inventory.Items.FirstOrDefault(i => i.Identifier == identifier);
        if (item == null) return false;

        switch (slot)
        {
            case GearSlot.Armor:
                if (character.Inventory.EquippedArmor != null)
                    character.Inventory.UnequipArmor();
                character.Inventory.EquipArmor(item);
                break;

            case GearSlot.MainHand:
                if (character.Inventory.EquippedPrimary != null)
                    character.Inventory.UnequipPrimary();
                character.Inventory.EquipPrimary(item, item.IsTwoHandTarget());
                break;

            case GearSlot.OffHand:
                if (character.Inventory.EquippedSecondary != null)
                    character.Inventory.UnequipSecondary();
                character.Inventory.EquipSecondary(item);
                break;
        }

        character.Inventory.CalculateAttunedItemCount();
        return true;
    }

    /// <summary>
    /// Adds a new item by element ID, then equips it to the given slot.
    /// </summary>
    public static bool AddAndEquipToSlot(Character character, GearSlot slot, string elementId)
    {
        if (!AddItem(character, elementId)) return false;
        var item = character.Inventory.Items.Last();
        return EquipToSlot(character, slot, item.Identifier);
    }

    /// <summary>Unequips the item in the given gear slot (item stays in inventory).</summary>
    public static void UnequipSlot(Character character, GearSlot slot)
    {
        switch (slot)
        {
            case GearSlot.Armor:    character.Inventory.UnequipArmor();     break;
            case GearSlot.MainHand: character.Inventory.UnequipPrimary();   break;
            case GearSlot.OffHand:  character.Inventory.UnequipSecondary(); break;
        }
        character.Inventory.CalculateAttunedItemCount();
    }

    // ── Slot compatibility ────────────────────────────────────────────────────────

    private static bool IsItemCompatibleWithSlot(RefactoredEquipmentItem item, GearSlot slot) => slot switch
    {
        GearSlot.Armor    => item.IsArmorTarget(),
        GearSlot.MainHand => item.IsOneHandTarget() || item.IsTwoHandTarget() || item.IsPrimaryTarget(),
        GearSlot.OffHand  => item.IsSecondaryTarget() || item.IsOneHandTarget(),
        _                 => false,
    };

    private static bool IsElementCompatibleWithSlot(Builder.Data.ElementBase e, GearSlot slot)
    {
        try
        {
            dynamic d = e;
            string slotVal  = ((string?)d.Slot  ?? "").ToLower();
            var slotsRaw    = (IEnumerable<string>?)d.Slots ?? [];
            var slots       = slotsRaw.Select(s => s.ToLower()).ToHashSet();

            return slot switch
            {
                GearSlot.Armor    => e.Type == "Armor"
                                     && (slots.Contains("armor") || slots.Contains("body")
                                         || slotVal == "armor"  || slotVal == "body"),

                GearSlot.MainHand => e.Type == "Weapon"
                                     && (slots.Contains("onehand") || slots.Contains("twohand")
                                         || slots.Contains("primary")
                                         || slotVal is "onehand" or "twohand"),

                GearSlot.OffHand  => (e.Type == "Weapon"
                                         && (slots.Contains("onehand") || slots.Contains("secondary")))
                                     || (e.Type == "Armor"
                                         && (slots.Contains("secondary")
                                             || slotVal.Contains("secondary"))),
                _ => false,
            };
        }
        catch { return false; }
    }

    // ── Description helper ────────────────────────────────────────────────────────

    public static string GetDescription(Builder.Data.ElementBase e)
    {
        try
        {
            if (e.SheetDescription?.Count > 0)
            {
                var sd = e.SheetDescription[0].Description?.Trim();
                if (!string.IsNullOrEmpty(sd)) return sd;
            }
        }
        catch { }
        try
        {
            if (!string.IsNullOrWhiteSpace(e.Description))
                return Builder.Presentation.Utilities.ElementDescriptionGenerator
                    .GeneratePlainDescription(e.Description).Trim();
        }
        catch { }
        return "";
    }
}

public sealed record ItemSearchResult(string Id, string Name, string Type, string Description, string Source);
public sealed record ItemPickerResult(string ElementId, int Amount);
public sealed record InventoryItemOption(string Identifier, string Name);
public sealed record GearPickerResult(string? Identifier, string? ElementId, bool IsNew);
