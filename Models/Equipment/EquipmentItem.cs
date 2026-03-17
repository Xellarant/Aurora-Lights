// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.Equipment.EquipmentItem
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core;
using Builder.Data;
using Builder.Data.Elements;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

#nullable disable
namespace Builder.Presentation.Models.Equipment;

public class EquipmentItem : ObservableObject
{
  private string _name;
  private bool _isStackable;
  private bool _isSilvered;
  private int _amount;
  private bool _isEquippable;
  private bool _isEquipped;
  private string _equippedLocation;

  public EquipmentItem(Item item, Item adorner = null)
  {
    if (item == null)
      throw new ArgumentNullException(nameof (item));
    this.Identifier = Guid.NewGuid().ToString();
    Item obj1 = item.Copy<Item>();
    Item obj2 = adorner != null ? adorner.Copy<Item>() : (Item) null;
    this.Item = obj1;
    this.AdornerItem = obj2;
    this.Name = item.Name;
    this.Amount = 1;
    this.IsStackable = item.IsStackable;
    this.IsEquippable = item.IsEquippable;
  }

  public string Identifier { get; protected set; }

  public Item Item { get; }

  public string Name
  {
    get => this._name;
    set
    {
      this.SetProperty<string>(ref this._name, value, nameof (Name));
      this.OnPropertyChanged("DisplayName");
    }
  }

  public bool IsStackable
  {
    get => this._isStackable;
    set => this.SetProperty<bool>(ref this._isStackable, value, nameof (IsStackable));
  }

  public bool IsSilvered
  {
    get => this._isSilvered;
    set => this.SetProperty<bool>(ref this._isSilvered, value, nameof (IsSilvered));
  }

  public int Amount
  {
    get => this._amount;
    set
    {
      this.SetProperty<int>(ref this._amount, value, nameof (Amount));
      this.OnPropertyChanged("DisplayName");
    }
  }

  public bool IsEquippable
  {
    get => this._isEquippable;
    set => this.SetProperty<bool>(ref this._isEquippable, value, nameof (IsEquippable));
  }

  public bool IsEquipped
  {
    get => this._isEquipped;
    set
    {
      this.SetProperty<bool>(ref this._isEquipped, value, nameof (IsEquipped));
      this.OnPropertyChanged("IsActivated");
    }
  }

  public string EquippedLocation
  {
    get => this._equippedLocation;
    set => this.SetProperty<string>(ref this._equippedLocation, value, nameof (EquippedLocation));
  }

  public virtual string DisplayName => this.Name;

  public ObservableCollection<EquipmentItem> StashedItems { get; } = (ObservableCollection<EquipmentItem>) new EquipmentItemCollection();

  public Item AdornerItem { get; set; }

  public bool IsAdorned => this.AdornerItem != null;

  public override string ToString()
  {
    return this.Name + (this.IsSilvered ? " (silver)" : "") + (this.IsStackable ? $" ({this.Amount})" : "") + (Debugger.IsAttached ? " " + this.Identifier : "");
  }
}
