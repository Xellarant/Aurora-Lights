// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.Equipment.EquipmentItemCollection
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using System;
using System.Collections.ObjectModel;
using System.Linq;

#nullable disable
namespace Builder.Presentation.Models.Equipment;

public class EquipmentItemCollection : ObservableCollection<EquipmentItem>
{
  public bool Contains(Builder.Data.Elements.Item item)
  {
    return this.Any<EquipmentItem>((Func<EquipmentItem, bool>) (equipmentItem => equipmentItem.Item.Id == item.Id));
  }

  public EquipmentItem GetEquipmentItem(string id)
  {
    return this.First<EquipmentItem>((Func<EquipmentItem, bool>) (x => x.Item.Id == id));
  }

  public void AddItem(Builder.Data.Elements.Item itemElement, int amount = 1)
  {
    if (this.Contains(itemElement))
    {
      if (itemElement.IsStackable)
      {
        this.GetEquipmentItem(itemElement.Id).Amount += amount;
      }
      else
      {
        for (int index = 0; index < amount; ++index)
          this.Add(new EquipmentItem(itemElement));
      }
    }
    else if (itemElement.IsStackable)
    {
      this.Add(new EquipmentItem(itemElement)
      {
        Amount = amount
      });
    }
    else
    {
      for (int index = 0; index < amount; ++index)
        this.Add(new EquipmentItem(itemElement));
    }
  }

  public void DeleteOne(string id)
  {
    EquipmentItem equipmentItem = this.GetEquipmentItem(id);
    if (equipmentItem.Amount > 1)
      --equipmentItem.Amount;
    else
      this.Remove(equipmentItem);
  }

  public void DeleteAll(string id) => this.Remove(this.GetEquipmentItem(id));
}
