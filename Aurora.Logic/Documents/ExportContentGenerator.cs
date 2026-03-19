// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Documents.ExportContentGenerator
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Aurora.Documents.ExportContent;
using Aurora.Documents.ExportContent.Equipment;
using Aurora.Documents.ExportContent.Notes;
using Aurora.Documents.Sheets;
using Builder.Core.Logging;
using Builder.Data;
using Builder.Data.Strings;
using Builder.Presentation.Models.Equipment;
using Builder.Presentation.ViewModels.Shell.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

#nullable disable
namespace Builder.Presentation.Documents;

public class ExportContentGenerator : IExportContentProvider
{
  private readonly CharacterManager _manager;
  private readonly CharacterSheetConfiguration _configuration;

  public ExportContentGenerator(CharacterManager manager, CharacterSheetConfiguration configuration)
  {
    this._manager = manager;
    this._configuration = configuration;
  }

  public EquipmentExportContent GetEquipmentContent()
  {
    EquipmentExportContent equipmentContent = new EquipmentExportContent();
    CharacterInventory inventory = this._manager.Character.Inventory;
    foreach (RefactoredEquipmentItem refactoredEquipmentItem in this.GetItems(inventory.Items.Where<RefactoredEquipmentItem>((Func<RefactoredEquipmentItem, bool>) (x => x.IncludeInEquipmentPageInventory))))
    {
      if (!refactoredEquipmentItem.IsStored)
      {
        InventoryItemExportContent itemExportContent1 = new InventoryItemExportContent(refactoredEquipmentItem.GetEquipmentPageName());
        itemExportContent1.Amount = refactoredEquipmentItem.Amount.ToString();
        Decimal num1 = refactoredEquipmentItem.GetWeight();
        itemExportContent1.Weight = num1.ToString("#.##", (IFormatProvider) CultureInfo.InvariantCulture);
        itemExportContent1.IsEquipped = refactoredEquipmentItem.IsActivated;
        InventoryItemExportContent item = itemExportContent1;
        item.ReferenceId = refactoredEquipmentItem.Item.Id;
        if (refactoredEquipmentItem.Item.CalculableWeight == 0M)
          item.Weight = "—";
        if (!refactoredEquipmentItem.IsStackable && (refactoredEquipmentItem.Item.Type == "Weapon" || refactoredEquipmentItem.Item.Type == "Armor") && !refactoredEquipmentItem.IsAdorned && !refactoredEquipmentItem.IsEquipped)
        {
          InventoryItemExportContent itemExportContent2 = equipmentContent.AdventuringGear.FirstOrDefault<InventoryItemExportContent>((Func<InventoryItemExportContent, bool>) (x => x.ReferenceId.Equals(item.ReferenceId) && !x.IsEquipped));
          if (itemExportContent2 != null)
          {
            int num2 = int.Parse(itemExportContent2.Amount);
            itemExportContent2.Amount = (num2 + 1).ToString();
            InventoryItemExportContent itemExportContent3 = itemExportContent2;
            num1 = (Decimal) (num2 + 1) * refactoredEquipmentItem.Item.CalculableWeight;
            string str = num1.ToString("#.##", (IFormatProvider) CultureInfo.InvariantCulture);
            itemExportContent3.Weight = str;
            continue;
          }
        }
        if (this._configuration.IsFormFillable && !this._configuration.IncludeFormatting && item.IsEquipped)
          item.Name = item.Name ?? "";
        if (refactoredEquipmentItem.IncludeInTreasure)
          equipmentContent.Valuables.Add(item);
        else if (refactoredEquipmentItem.Item.Type.Equals("Magic Item") || refactoredEquipmentItem.IsAdorned)
        {
          equipmentContent.MagicItems.Add(item);
        }
        else
        {
          switch (refactoredEquipmentItem.Item.Category)
          {
            case "Art":
            case "Art Objects":
            case "Gemstones":
            case "Treasure":
            case "Valuables":
              Logger.Warning($"should include {item.Name} in treasure, but it's not flagged as 'IncludeInTreasure'");
              equipmentContent.Valuables.Add(item);
              continue;
            default:
              equipmentContent.AdventuringGear.Add(item);
              continue;
          }
        }
      }
    }
    CoinageExportContent coinage1 = equipmentContent.Coinage;
    long num = inventory.Coins.Copper;
    string str1 = num.ToString();
    coinage1.Copper = str1;
    CoinageExportContent coinage2 = equipmentContent.Coinage;
    num = inventory.Coins.Silver;
    string str2 = num.ToString();
    coinage2.Silver = str2;
    CoinageExportContent coinage3 = equipmentContent.Coinage;
    num = inventory.Coins.Electrum;
    string str3 = num.ToString();
    coinage3.Electrum = str3;
    CoinageExportContent coinage4 = equipmentContent.Coinage;
    num = inventory.Coins.Gold;
    string str4 = num.ToString();
    coinage4.Gold = str4;
    CoinageExportContent coinage5 = equipmentContent.Coinage;
    num = inventory.Coins.Platinum;
    string str5 = num.ToString();
    coinage5.Platinum = str5;
    Decimal carryingCapacity = this.CalculateCarryingCapacity();
    Decimal dragCapacity = this.CalculateDragCapacity(carryingCapacity);
    equipmentContent.CarryingCapacity = carryingCapacity.ToString("#.#", (IFormatProvider) CultureInfo.InvariantCulture) + " lb";
    equipmentContent.DragCapacity = dragCapacity.ToString("#.#", (IFormatProvider) CultureInfo.InvariantCulture) + " lb";
    equipmentContent.WeightCarried = inventory.EquipmentWeight.ToString("#.#", (IFormatProvider) CultureInfo.InvariantCulture) + " lb";
    equipmentContent.AdditionalTreasure = inventory.Treasure;
    EquipmentExportContent equipmentExportContent1 = equipmentContent;
    int index = inventory.AttunedItemCount;
    string str6 = index.ToString();
    equipmentExportContent1.AttunementCurrent = str6;
    EquipmentExportContent equipmentExportContent2 = equipmentContent;
    index = inventory.MaxAttunedItemCount;
    string str7 = index.ToString();
    equipmentExportContent2.AttunementMaximum = str7;
    StringBuilder stringBuilder1 = new StringBuilder();
    foreach (RefactoredEquipmentItem refactoredEquipmentItem in inventory.Items.Where<RefactoredEquipmentItem>((Func<RefactoredEquipmentItem, bool>) (x => x.IncludeInEquipmentPageDescriptionSidebar)))
    {
      if (!string.IsNullOrWhiteSpace(stringBuilder1.ToString()))
        stringBuilder1.AppendLine("<p>&nbsp;</p>");
      string equipmentPageName = refactoredEquipmentItem.GetEquipmentPageName();
      string str8 = refactoredEquipmentItem.IsAdorned ? refactoredEquipmentItem.AdornerItem.Description : refactoredEquipmentItem.Item.Description;
      if (str8.StartsWith("<p>"))
        str8 = str8.Substring(3, str8.Length - 3);
      if (str8.Contains("<p class=\"indent\">"))
        str8 = str8.Replace("<p class=\"indent\">", "<p class=\"indent\">&nbsp;     &nbsp;");
      if (!string.IsNullOrWhiteSpace(refactoredEquipmentItem.Notes))
      {
        StringBuilder stringBuilder2 = new StringBuilder();
        string[] strArray = Regex.Split(refactoredEquipmentItem.Notes, Environment.NewLine);
        for (index = 0; index < strArray.Length; ++index)
        {
          string str9 = strArray[index];
          if (string.IsNullOrWhiteSpace(str9))
            stringBuilder2.Append("<p>&nbsp;</p>");
          else
            stringBuilder2.Append($"<p>{str9}</p>");
        }
        str8 = stringBuilder2.ToString().Substring(3, stringBuilder2.Length - 3);
      }
      stringBuilder1.Append($"<p><b><i>{equipmentPageName}.</i></b> {str8}");
    }
    equipmentContent.AttunedMagicItems = stringBuilder1.ToString();
    StoredItemsExportContent itemsExportContent1 = new StoredItemsExportContent(inventory.StoredItems1.Name);
    foreach (RefactoredEquipmentItem storedItem in (Collection<RefactoredEquipmentItem>) inventory.StoredItems1.StoredItems)
    {
      InventoryItemExportContent itemExportContent = new InventoryItemExportContent(storedItem.GetEquipmentPageName())
      {
        Amount = storedItem.Amount.ToString(),
        Weight = storedItem.GetWeight().ToString("#.##", (IFormatProvider) CultureInfo.InvariantCulture)
      };
      if (string.IsNullOrWhiteSpace(itemExportContent.Weight))
        itemExportContent.Weight = "—";
      itemsExportContent1.Items.Add(itemExportContent);
    }
    StoredItemsExportContent itemsExportContent2 = new StoredItemsExportContent(inventory.StoredItems2.Name);
    foreach (RefactoredEquipmentItem storedItem in (Collection<RefactoredEquipmentItem>) inventory.StoredItems2.StoredItems)
    {
      InventoryItemExportContent itemExportContent = new InventoryItemExportContent(storedItem.GetEquipmentPageName())
      {
        Amount = storedItem.Amount.ToString(),
        Weight = (storedItem.Item.CalculableWeight * (Decimal) storedItem.Amount).ToString("#.##", (IFormatProvider) CultureInfo.InvariantCulture)
      };
      if (string.IsNullOrWhiteSpace(itemExportContent.Weight))
        itemExportContent.Weight = "—";
      itemsExportContent2.Items.Add(itemExportContent);
    }
    equipmentContent.StorageLocations.Add(itemsExportContent1);
    equipmentContent.StorageLocations.Add(itemsExportContent2);
    equipmentContent.QuestItems = inventory.QuestItems;
    return equipmentContent;
  }

  public NotesExportContent GetNotesContent()
  {
    return new NotesExportContent()
    {
      LeftNotesColumn = this._manager.Character.Notes1,
      RightNotesColumn = this._manager.Character.Notes2
    };
  }

  public string GetItemEquippedAffix(RefactoredEquipmentItem equipment)
  {
    List<string> values = new List<string>();
    if (equipment.IsEquipped)
      values.Add("Equipped");
    if (equipment.IsAttuned)
      values.Add("Attuned");
    return string.Join(", ", (IEnumerable<string>) values);
  }

  private IEnumerable<RefactoredEquipmentItem> GetItems(
    IEnumerable<RefactoredEquipmentItem> inventoryItems)
  {
    List<RefactoredEquipmentItem> items = new List<RefactoredEquipmentItem>();
    List<RefactoredEquipmentItem> collection1 = new List<RefactoredEquipmentItem>();
    List<RefactoredEquipmentItem> collection2 = new List<RefactoredEquipmentItem>();
    List<RefactoredEquipmentItem> collection3 = new List<RefactoredEquipmentItem>();
    List<RefactoredEquipmentItem> collection4 = new List<RefactoredEquipmentItem>();
    List<RefactoredEquipmentItem> collection5 = new List<RefactoredEquipmentItem>();
    foreach (RefactoredEquipmentItem inventoryItem in inventoryItems)
    {
      if (inventoryItem.IncludeInTreasure)
        collection5.Add(inventoryItem);
      else if (inventoryItem.Item.Type.Equals("Magic Item"))
      {
        switch (inventoryItem.Item.ItemType)
        {
          case "Potion":
            collection3.Add(inventoryItem);
            continue;
          case "Scroll":
            collection4.Add(inventoryItem);
            continue;
          default:
            collection2.Add(inventoryItem);
            continue;
        }
      }
      else
      {
        if (inventoryItem.Item.Type.Equals("Item"))
        {
          switch (inventoryItem.Item.Category)
          {
            case "Art":
            case "Art Objects":
            case "Gemstones":
            case "Treasure":
            case "Valuable":
            case "Valuables":
              collection5.Add(inventoryItem);
              continue;
          }
        }
        collection1.Add(inventoryItem);
      }
    }
    items.AddRange((IEnumerable<RefactoredEquipmentItem>) collection1);
    items.AddRange((IEnumerable<RefactoredEquipmentItem>) collection2);
    items.AddRange((IEnumerable<RefactoredEquipmentItem>) collection3);
    items.AddRange((IEnumerable<RefactoredEquipmentItem>) collection4);
    items.AddRange((IEnumerable<RefactoredEquipmentItem>) collection5);
    return (IEnumerable<RefactoredEquipmentItem>) items;
  }

  public Decimal GetCarryMultiplierBasedOnSize()
  {
    ElementBase elementBase = this._manager.GetElements().FirstOrDefault<ElementBase>((Func<ElementBase, bool>) (x => x.Type.Equals("Size")));
    if (elementBase != null)
    {
      bool flag = this._manager.GetElements().Where<ElementBase>((Func<ElementBase, bool>) (x => x.Type.Equals("Grants"))).ToList<ElementBase>().Any<ElementBase>((Func<ElementBase, bool>) (x => x.Id.Equals(InternalGrants.WeightCapacityCountAsLargerSize)));
      switch (elementBase.Id)
      {
        case "ID_SIZE_TINY":
          return !flag ? 7.5M : 15M;
        case "ID_SIZE_MEDIUM":
          if (flag)
            return 30M;
          break;
        case "ID_SIZE_LARGE":
        case "ID_SIZE_HUGE":
        case "ID_SIZE_GARGANTUAN":
        case "ID_SIZE_COLOSSAL":
          return 30M;
      }
    }
    else
      Logger.Warning("no size element available to calculate carry capacity based on size, defaulting to 15 (medium)");
    return 15M;
  }

  private Decimal CalculateCarryingCapacity()
  {
    Decimal multiplierBasedOnSize = this.GetCarryMultiplierBasedOnSize();
    List<ElementBase> list = this._manager.GetElements().Where<ElementBase>((Func<ElementBase, bool>) (x => x.Type.Equals("Grants"))).ToList<ElementBase>();
    if (list.Any<ElementBase>((Func<ElementBase, bool>) (x => x.Id.Equals(InternalGrants.WeightCapacityDoubled))))
      multiplierBasedOnSize *= 2M;
    if (list.Any<ElementBase>((Func<ElementBase, bool>) (x => x.Id.Equals(InternalGrants.WeightCapacityHalved))))
      multiplierBasedOnSize /= 2M;
    return (Decimal) this._manager.Character.Abilities.Strength.FinalScore * multiplierBasedOnSize;
  }

  private Decimal CalculateDragCapacity(Decimal carryCapacity) => carryCapacity * 2M;
}
