// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.Equipment.InventoryWeight
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core;
using System;

#nullable disable
namespace Builder.Presentation.Models.Equipment;

public class InventoryWeight : ObservableObject
{
  private Decimal _weightCarried;
  private Decimal _weightCapacity;
  private Decimal _liftingWeightCapacity;

  public InventoryWeight()
  {
    this._weightCarried = 0M;
    this._weightCapacity = 0M;
    this._liftingWeightCapacity = 0M;
  }

  public Decimal WeightCarried
  {
    get => this._weightCarried;
    set => this.SetProperty<Decimal>(ref this._weightCarried, value, nameof (WeightCarried));
  }

  public Decimal WeightCapacity
  {
    get => this._weightCapacity;
    set => this.SetProperty<Decimal>(ref this._weightCapacity, value, nameof (WeightCapacity));
  }

  public Decimal LiftingWeightCapacity
  {
    get => this._liftingWeightCapacity;
    set
    {
      this.SetProperty<Decimal>(ref this._liftingWeightCapacity, value, nameof (LiftingWeightCapacity));
    }
  }
}
