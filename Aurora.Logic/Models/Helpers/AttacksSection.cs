// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.Helpers.AttacksSection
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core;
using System.Collections.ObjectModel;

#nullable disable
namespace Builder.Presentation.Models.Helpers;

public class AttacksSection : ObservableObject
{
  private AttacksSection.AttackObject _attackObject1;
  private AttacksSection.AttackObject _attackObject2;
  private AttacksSection.AttackObject _attackObject3;
  private string _attacksAndSpellcasting;

  public AttacksSection()
  {
    this.AttackObject1 = new AttacksSection.AttackObject("", "", "");
    this.AttackObject2 = new AttacksSection.AttackObject("", "", "");
    this.AttackObject3 = new AttacksSection.AttackObject("", "", "");
    this.AttacksAndSpellcasting = "";
  }

  public AttacksSection.AttackObject AttackObject1
  {
    get => this._attackObject1;
    set
    {
      this.SetProperty<AttacksSection.AttackObject>(ref this._attackObject1, value, nameof (AttackObject1));
    }
  }

  public AttacksSection.AttackObject AttackObject2
  {
    get => this._attackObject2;
    set
    {
      this.SetProperty<AttacksSection.AttackObject>(ref this._attackObject2, value, nameof (AttackObject2));
    }
  }

  public AttacksSection.AttackObject AttackObject3
  {
    get => this._attackObject3;
    set
    {
      this.SetProperty<AttacksSection.AttackObject>(ref this._attackObject3, value, nameof (AttackObject3));
    }
  }

  public string AttacksAndSpellcasting
  {
    get => this._attacksAndSpellcasting;
    set
    {
      this.SetProperty<string>(ref this._attacksAndSpellcasting, value, nameof (AttacksAndSpellcasting));
    }
  }

  public void Reset()
  {
    this.AttackObject1.Reset();
    this.AttackObject2.Reset();
    this.AttackObject3.Reset();
    this.AttacksAndSpellcasting = string.Empty;
    this.Items.Clear();
  }

  public ObservableCollection<AttackSectionItem> Items { get; } = new ObservableCollection<AttackSectionItem>();

  public class AttackObject : ObservableObject
  {
    private string _name;
    private string _bonus;
    private string _damage;

    public AttackObject(string name, string bonus, string damage)
    {
      this.Name = name;
      this.Bonus = bonus;
      this.Damage = damage;
    }

    public string Name
    {
      get => this._name;
      set => this.SetProperty<string>(ref this._name, value, nameof (Name));
    }

    public string Bonus
    {
      get => this._bonus;
      set => this.SetProperty<string>(ref this._bonus, value, nameof (Bonus));
    }

    public string Damage
    {
      get => this._damage;
      set => this.SetProperty<string>(ref this._damage, value, nameof (Damage));
    }

    public void Reset()
    {
      this.Name = "";
      this.Bonus = "";
      this.Damage = "";
    }
  }
}
