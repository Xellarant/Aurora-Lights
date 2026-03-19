// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.SkillItem
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core;
using System.ComponentModel;

#nullable disable
namespace Builder.Presentation.Models;

public class SkillItem : ObservableObject
{
  private int _proficiencyBonus;
  private int _miscBonus;

  public SkillItem(string name, AbilityItem abilityItem)
  {
    this.Name = name;
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
      this.OnPropertyChanged("FinalBonus", "FinalPassiveBonus", "IsProficient");
    }
  }

  public int MiscBonus
  {
    get => this._miscBonus;
    set
    {
      this.SetProperty<int>(ref this._miscBonus, value, nameof (MiscBonus));
      this.OnPropertyChanged("FinalBonus");
      this.OnPropertyChanged("FinalPassiveBonus");
    }
  }

  public int FinalBonus => this.ProficiencyBonus + this.KeyAbility.Modifier + this.MiscBonus;

  public int FinalPassiveBonus => 10 + this.FinalBonus;

  public bool IsProficient => this._proficiencyBonus > 0;

  public bool IsExpertise(int proficiencyBonus)
  {
    return this.IsProficient && this.ProficiencyBonus >= proficiencyBonus * 2;
  }

  private void AbilityPropertyChanged(object sender, PropertyChangedEventArgs e)
  {
    if (!(e.PropertyName == "Modifier"))
      return;
    this.OnPropertyChanged("FinalBonus");
    this.OnPropertyChanged("FinalPassiveBonus");
  }
}
