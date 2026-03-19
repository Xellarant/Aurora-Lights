// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.Equipment.InventoryStorage
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core;
using Builder.Presentation.ViewModels.Shell.Items;
using System.Collections.ObjectModel;
using System.Linq;

#nullable disable
namespace Builder.Presentation.Models.Equipment;

public class InventoryStorage : ObservableObject
{
  private string _name;

  public InventoryStorage()
  {
    this.StoredItems = new ObservableCollection<RefactoredEquipmentItem>();
  }

  public string Name
  {
    get => this._name;
    set => this.SetProperty<string>(ref this._name, value, nameof (Name));
  }

  public ObservableCollection<RefactoredEquipmentItem> StoredItems { get; set; }

  public bool IsInUse()
  {
    return !string.IsNullOrWhiteSpace(this.Name) || this.StoredItems.Any<RefactoredEquipmentItem>();
  }

  public override string ToString() => this.Name;
}
