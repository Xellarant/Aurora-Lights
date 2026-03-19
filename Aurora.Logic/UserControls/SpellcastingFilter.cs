// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.UserControls.SpellcastingFilter
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Data;
using Builder.Data.Elements;
using Builder.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Builder.Presentation.UserControls;

public class SpellcastingFilter : ElementFilter
{
  private string _school;
  private bool _includeCantrips;
  private bool _include1;
  private bool _include2;
  private bool _include3;
  private bool _include4;
  private bool _include5;
  private bool _include6;
  private bool _include7;
  private bool _include8;
  private bool _include9;
  private bool _isSchoolFilterAvailable;
  private bool _isRitual;
  private bool _isConcentration;
  private bool _includeComponents;
  private bool _isVerbal;
  private bool _isSomatic;
  private bool _isMaterial;

  public SpellcastingFilter(
    IEnumerable<string> sourceCollection,
    IEnumerable<string> schoolCollection)
    : base(sourceCollection)
  {
    this.SchoolCollection = new List<string>(schoolCollection);
    this.School = "--";
    this.IncludeKeywords = true;
  }

  public bool IsSchoolFilterAvailable
  {
    get => this._isSchoolFilterAvailable;
    set
    {
      this.SetProperty<bool>(ref this._isSchoolFilterAvailable, value, nameof (IsSchoolFilterAvailable));
    }
  }

  public List<string> SchoolCollection { get; }

  public string School
  {
    get => this._school;
    set => this.SetProperty<string>(ref this._school, value, nameof (School));
  }

  public bool IncludeCantrips
  {
    get => this._includeCantrips;
    set => this.SetProperty<bool>(ref this._includeCantrips, value, nameof (IncludeCantrips));
  }

  public bool Include1
  {
    get => this._include1;
    set => this.SetProperty<bool>(ref this._include1, value, nameof (Include1));
  }

  public bool Include2
  {
    get => this._include2;
    set => this.SetProperty<bool>(ref this._include2, value, nameof (Include2));
  }

  public bool Include3
  {
    get => this._include3;
    set => this.SetProperty<bool>(ref this._include3, value, nameof (Include3));
  }

  public bool Include4
  {
    get => this._include4;
    set => this.SetProperty<bool>(ref this._include4, value, nameof (Include4));
  }

  public bool Include5
  {
    get => this._include5;
    set => this.SetProperty<bool>(ref this._include5, value, nameof (Include5));
  }

  public bool Include6
  {
    get => this._include6;
    set => this.SetProperty<bool>(ref this._include6, value, nameof (Include6));
  }

  public bool Include7
  {
    get => this._include7;
    set => this.SetProperty<bool>(ref this._include7, value, nameof (Include7));
  }

  public bool Include8
  {
    get => this._include8;
    set => this.SetProperty<bool>(ref this._include8, value, nameof (Include8));
  }

  public bool Include9
  {
    get => this._include9;
    set => this.SetProperty<bool>(ref this._include9, value, nameof (Include9));
  }

  public bool IsRitual
  {
    get => this._isRitual;
    set => this.SetProperty<bool>(ref this._isRitual, value, nameof (IsRitual));
  }

  public bool IsConcentration
  {
    get => this._isConcentration;
    set => this.SetProperty<bool>(ref this._isConcentration, value, nameof (IsConcentration));
  }

  public bool IncludeComponents
  {
    get => this._includeComponents;
    set => this.SetProperty<bool>(ref this._includeComponents, value, nameof (IncludeComponents));
  }

  public bool IsVerbal
  {
    get => this._isVerbal;
    set => this.SetProperty<bool>(ref this._isVerbal, value, nameof (IsVerbal));
  }

  public bool IsSomatic
  {
    get => this._isSomatic;
    set => this.SetProperty<bool>(ref this._isSomatic, value, nameof (IsSomatic));
  }

  public bool IsMaterial
  {
    get => this._isMaterial;
    set => this.SetProperty<bool>(ref this._isMaterial, value, nameof (IsMaterial));
  }

  public override List<ElementBase> Filter(IEnumerable<ElementBase> input)
  {
    List<ElementBase> source = this.FilterSchool((IEnumerable<ElementBase>) base.Filter(input));
    if (!this.IncludeCantrips)
      source = source.Where<ElementBase>((Func<ElementBase, bool>) (x => x.AsElement<Spell>().Level != 0)).ToList<ElementBase>();
    if (!this.Include1)
      source = source.Where<ElementBase>((Func<ElementBase, bool>) (x => x.AsElement<Spell>().Level != 1)).ToList<ElementBase>();
    if (!this.Include2)
      source = source.Where<ElementBase>((Func<ElementBase, bool>) (x => x.AsElement<Spell>().Level != 2)).ToList<ElementBase>();
    if (!this.Include3)
      source = source.Where<ElementBase>((Func<ElementBase, bool>) (x => x.AsElement<Spell>().Level != 3)).ToList<ElementBase>();
    if (!this.Include4)
      source = source.Where<ElementBase>((Func<ElementBase, bool>) (x => x.AsElement<Spell>().Level != 4)).ToList<ElementBase>();
    if (!this.Include5)
      source = source.Where<ElementBase>((Func<ElementBase, bool>) (x => x.AsElement<Spell>().Level != 5)).ToList<ElementBase>();
    if (!this.Include6)
      source = source.Where<ElementBase>((Func<ElementBase, bool>) (x => x.AsElement<Spell>().Level != 6)).ToList<ElementBase>();
    if (!this.Include7)
      source = source.Where<ElementBase>((Func<ElementBase, bool>) (x => x.AsElement<Spell>().Level != 7)).ToList<ElementBase>();
    if (!this.Include8)
      source = source.Where<ElementBase>((Func<ElementBase, bool>) (x => x.AsElement<Spell>().Level != 8)).ToList<ElementBase>();
    if (!this.Include9)
      source = source.Where<ElementBase>((Func<ElementBase, bool>) (x => x.AsElement<Spell>().Level != 9)).ToList<ElementBase>();
    if (this.IsRitual)
      source = source.Where<ElementBase>((Func<ElementBase, bool>) (x => x.AsElement<Spell>().IsRitual)).ToList<ElementBase>();
    if (this.IsConcentration)
      source = source.Where<ElementBase>((Func<ElementBase, bool>) (x => x.AsElement<Spell>().IsConcentration)).ToList<ElementBase>();
    if (this.IncludeComponents)
    {
      if (!this.IsVerbal)
        source = source.Where<ElementBase>((Func<ElementBase, bool>) (x => !x.AsElement<Spell>().HasVerbalComponent)).ToList<ElementBase>();
      if (!this.IsSomatic)
        source = source.Where<ElementBase>((Func<ElementBase, bool>) (x => !x.AsElement<Spell>().HasSomaticComponent)).ToList<ElementBase>();
      if (!this.IsMaterial)
        source = source.Where<ElementBase>((Func<ElementBase, bool>) (x => !x.AsElement<Spell>().HasMaterialComponent)).ToList<ElementBase>();
    }
    return source;
  }

  protected virtual List<ElementBase> FilterSchool(IEnumerable<ElementBase> input)
  {
    return this._isSchoolFilterAvailable && !string.IsNullOrWhiteSpace(this.School) && !this.School.Equals("--") ? input.Where<ElementBase>((Func<ElementBase, bool>) (x => (x as Spell).MagicSchool.ToLower().Contains(this.School.ToLower().Trim()))).ToList<ElementBase>() : input.ToList<ElementBase>();
  }

  public override void Clear()
  {
    base.Clear();
    this.School = string.Empty;
    this.IncludeCantrips = true;
    this.Include1 = true;
    this.Include2 = true;
    this.Include3 = true;
    this.Include4 = true;
    this.Include5 = true;
    this.Include6 = true;
    this.Include7 = true;
    this.Include8 = true;
    this.Include9 = true;
  }
}
