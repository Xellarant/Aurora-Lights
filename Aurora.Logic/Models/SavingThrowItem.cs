// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.SavingThrowItem
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core;
using System.ComponentModel;

#nullable disable
namespace Builder.Presentation.Models;

public class SavingThrowItem : ObservableObject
{
  private int _proficiencyBonus;
  private int _miscBonus;

  public SavingThrowItem(AbilityItem abilityItem)
  {
    this.Name = abilityItem.Name + " Saving Throw";
    this.KeyAbility = abilityItem;
    this.KeyAbility.PropertyChanged += new PropertyChangedEventHandler(this.AbilityPropertyChanged);
  }

  public string Name { get; }

  public AbilityItem KeyAbility { get; }

  public int ProficiencyBonus
  {
    get => this._proficiencyBonus;
    set
    {
      this.SetProperty<int>(ref this._proficiencyBonus, value, nameof (ProficiencyBonus));
      this.OnPropertyChanged("FinalBonus", "FinalBonusModifierString", "IsProficient");
    }
  }

  public int MiscBonus
  {
    get => this._miscBonus;
    set
    {
      this.SetProperty<int>(ref this._miscBonus, value, nameof (MiscBonus));
      this.OnPropertyChanged("FinalBonus", "FinalBonusModifierString");
    }
  }

  public int FinalBonus => this.ProficiencyBonus + this.KeyAbility.Modifier + this.MiscBonus;

  public bool IsProficient => this._proficiencyBonus > 0;

  private void AbilityPropertyChanged(object sender, PropertyChangedEventArgs e)
  {
    if (!(e.PropertyName == "Modifier"))
      return;
    this.OnPropertyChanged("FinalBonus");
  }

  public string FinalBonusModifierString
  {
    get => $"{(this.FinalBonus >= 0 ? (object) "+" : (object) "")}{this.FinalBonus}";
  }
}
