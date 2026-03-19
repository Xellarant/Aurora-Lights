// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.AbilityItem
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core;

using System;

#nullable disable
namespace Builder.Presentation.Models;

public class AbilityItem : ObservableObject
{
  private string _name;
  private int _baseScore;
  private int _additionalScore;
  private int _maximumScore;
  private bool _useAbilityScoreMaximum;
  private int _overrideScore;
  private string _additionalSummery = "";

  public AbilityItem(string name, int baseScore = 8)
  {
    this._name = name;
    this._baseScore = baseScore;
    this._useAbilityScoreMaximum = true;
    this._maximumScore = 20;
  }

  [Obsolete("needs to be implemented")]
  public bool UseAbilityScoreMaximum
  {
    get => this._useAbilityScoreMaximum;
    set
    {
      this.SetProperty<bool>(ref this._useAbilityScoreMaximum, value, nameof (UseAbilityScoreMaximum));
    }
  }

  public string Name
  {
    get => this._name;
    set
    {
      this.SetProperty<string>(ref this._name, value, nameof (Name));
      this.OnPropertyChanged("Abbreviation");
    }
  }

  public string Abbreviation
  {
    get
    {
      return !string.IsNullOrWhiteSpace(this.Name) ? this.Name.Substring(0, this.Name.Length >= 3 ? 3 : this.Name.Length) : string.Empty;
    }
  }

  public int BaseScore
  {
    get => this._baseScore;
    set
    {
      this.SetProperty<int>(ref this._baseScore, value, nameof (BaseScore));
      this.OnPropertyChanged("FinalScore", "Modifier", "ModifierString", "AbilityAndModifierString", "ExceedsMaximumScore");
    }
  }

  public int AdditionalScore
  {
    get => this._additionalScore;
    set
    {
      this.SetProperty<int>(ref this._additionalScore, value, nameof (AdditionalScore));
      this.OnPropertyChanged("FinalScore", "Modifier", "ModifierString", "AbilityAndModifierString", "ExceedsMaximumScore");
    }
  }

  public int FinalScore
  {
    get
    {
      int num = this.BaseScore + this.AdditionalScore;
      if (this.OverrideScore > num)
        return this.OverrideScore;
      return this.ExceedsMaximumScore && ApplicationContext.Current.Settings.UseDefaultAbilityScoreMaximum ? this.MaximumScore : num;
    }
  }

  public int Modifier
  {
    get
    {
      int finalScore = this.FinalScore;
      return finalScore < 10 ? (finalScore - 11) / 2 : (finalScore - 10) / 2;
    }
  }

  public string ModifierString
  {
    get => $"{(this.Modifier >= 0 ? (object) "+" : (object) "")}{this.Modifier}";
  }

  public string AbilityAndModifierString
  {
    get
    {
      return $"{this.FinalScore} ({(this.Modifier >= 0 ? (object) "+" : (object) "")}{this.Modifier})";
    }
  }

  public int OverrideScore
  {
    get => this._overrideScore;
    set
    {
      this.SetProperty<int>(ref this._overrideScore, value, nameof (OverrideScore));
      this.OnPropertyChanged("FinalScore", "Modifier", "ModifierString", "AbilityAndModifierString", "ExceedsMaximumScore");
    }
  }

  public bool UseOverrideScore() => this.OverrideScore > this.BaseScore + this.AdditionalScore;

  public string AdditionalSummery
  {
    get => this._additionalSummery;
    set => this.SetProperty<string>(ref this._additionalSummery, value, nameof (AdditionalSummery));
  }

  public bool ExceedsMaximumScore
  {
    get
    {
      int num = this.BaseScore + this.AdditionalScore;
      if (this.OverrideScore > num)
        num = this.OverrideScore;
      return num > this.MaximumScore;
    }
  }

  public int MaximumScore
  {
    get => this._maximumScore;
    set
    {
      this.SetProperty<int>(ref this._maximumScore, value, nameof (MaximumScore));
      this.OnPropertyChanged("FinalScore", "Modifier", "ModifierString", "AbilityAndModifierString", "ExceedsMaximumScore");
    }
  }

  public override string ToString() => this.Name;
}
