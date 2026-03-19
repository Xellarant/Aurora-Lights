// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.Shell.Items.EquipmentItemSlots
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core;
using Builder.Data;
using System;
using System.Linq;

#nullable disable
namespace Builder.Presentation.ViewModels.Shell.Items;

[Obsolete]
public class EquipmentItemSlots : ObservableObject
{
  private readonly CharacterManager _manager;
  private RefactoredEquipmentItem _equippedArmor;
  private RefactoredEquipmentItem _equippedPrimary;
  private RefactoredEquipmentItem _equippedSecondary;

  public EquipmentItemSlots() => this._manager = CharacterManager.Current;

  public event EventHandler EquippedChanged;

  public RefactoredEquipmentItem EquippedArmor
  {
    get => this._equippedArmor;
    private set
    {
      this.SetProperty<RefactoredEquipmentItem>(ref this._equippedArmor, value, nameof (EquippedArmor));
      RefactoredEquipmentItem equippedArmor = this._equippedArmor;
      this.OnEquippedChanged();
    }
  }

  public RefactoredEquipmentItem EquippedPrimary
  {
    get => this._equippedPrimary;
    private set
    {
      this.SetProperty<RefactoredEquipmentItem>(ref this._equippedPrimary, value, nameof (EquippedPrimary));
      this.OnPropertyChanged("IsEquippedVersatile");
      this.OnEquippedChanged();
    }
  }

  public RefactoredEquipmentItem EquippedSecondary
  {
    get => this._equippedSecondary;
    private set
    {
      this.SetProperty<RefactoredEquipmentItem>(ref this._equippedSecondary, value, nameof (EquippedSecondary));
      this.OnPropertyChanged("IsEquippedVersatile");
      this.OnPropertyChanged("IsEquippedShield");
      this.OnEquippedChanged();
    }
  }

  public bool IsEquippedTwoHander
  {
    get
    {
      return this.EquippedPrimary != null && this.EquippedSecondary != null && this.EquippedPrimary.Identifier.Equals(this.EquippedSecondary.Identifier);
    }
  }

  public bool IsEquippedVersatile
  {
    get
    {
      return this.EquippedPrimary != null && this.EquippedSecondary != null && this.EquippedPrimary.Identifier.Equals(this.EquippedSecondary.Identifier);
    }
  }

  public bool IsEquippedShield
  {
    get
    {
      return this.EquippedSecondary != null && this.EquippedSecondary.Item.ElementSetters.ContainsSetter("armor") && this.EquippedSecondary.Item.ElementSetters.GetSetter("armor").Value.Equals("shield", StringComparison.OrdinalIgnoreCase);
    }
  }

  public void EquipWeapon(RefactoredEquipmentItem equipment, bool versatile = false)
  {
    string str = equipment.Item.HasMultipleSlots ? equipment.Item.Slots.FirstOrDefault<string>() : equipment.Item.Slot;
    switch (str)
    {
      case null:
        break;
      case "armor":
      case "body":
        this.EquippedArmor = equipment;
        break;
      case "onehand":
        int num1 = versatile ? 1 : 0;
        break;
      default:
        int num2 = str == "twohand" ? 1 : 0;
        break;
    }
  }

  private void Equip(RefactoredEquipmentItem equipment)
  {
    if (equipment == null)
      return;
    this._manager.RegisterElement((ElementBase) equipment.Item);
    if (equipment.IsAdorned)
      this._manager.RegisterElement((ElementBase) equipment.AdornerItem);
    equipment.IsEquipped = true;
    equipment.IsAttuned = true;
  }

  private void Unequip(RefactoredEquipmentItem equipment)
  {
    if (equipment == null)
      return;
    if (equipment.IsAdorned)
      this._manager.UnregisterElement((ElementBase) equipment.AdornerItem);
    this._manager.UnregisterElement((ElementBase) equipment.Item);
    equipment.EquippedLocation = string.Empty;
    equipment.IsEquipped = false;
    equipment.IsAttuned = false;
  }

  protected virtual void OnEquippedChanged()
  {
    EventHandler equippedChanged = this.EquippedChanged;
    if (equippedChanged == null)
      return;
    equippedChanged((object) this, EventArgs.Empty);
  }
}
