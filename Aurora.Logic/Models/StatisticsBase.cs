// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.StatisticsBase
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core;
using Builder.Presentation.Models.Collections;

#nullable disable
namespace Builder.Presentation.Models;

public class StatisticsBase : ObservableObject
{
  private string _name;
  private string _displayName;
  private string _displayBuild;

  public StatisticsBase()
  {
    this.Abilities = new AbilitiesCollection();
    this.Skills = new SkillsCollection(this.Abilities);
    this.SavingThrows = new SavingThrowCollection(this.Abilities);
  }

  public AbilitiesCollection Abilities { get; }

  public SkillsCollection Skills { get; }

  public SavingThrowCollection SavingThrows { get; }

  public string DisplayName
  {
    get => this._displayName;
    set => this.SetProperty<string>(ref this._displayName, value, nameof (DisplayName));
  }

  public string DisplayBuild
  {
    get => this._displayBuild;
    set => this.SetProperty<string>(ref this._displayBuild, value, nameof (DisplayBuild));
  }

  public string Name
  {
    get => this._name;
    set => this.SetProperty<string>(ref this._name, value, nameof (Name));
  }

  public virtual void Reset()
  {
    this.Name = "";
    this.DisplayName = "";
    this.DisplayBuild = "";
    this.Abilities.Reset();
  }
}
