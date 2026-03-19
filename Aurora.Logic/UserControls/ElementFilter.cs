// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.UserControls.ElementFilter
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core;
using Builder.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

#nullable disable
namespace Builder.Presentation.UserControls;

public class ElementFilter : ObservableObject
{
  private bool _isLocked;
  private bool _isSourceFilterAvailable;
  private string _name = "";
  private string _source = "";
  private bool _includeKeywords;

  public ElementFilter(IEnumerable<string> sourceCollection)
  {
    this.SourceCollection = new ObservableCollection<string>(sourceCollection);
    this.Source = "--";
  }

  public ObservableCollection<string> SourceCollection { get; }

  public bool IncludeKeywords
  {
    get => this._includeKeywords;
    set => this.SetProperty<bool>(ref this._includeKeywords, value, nameof (IncludeKeywords));
  }

  public bool IsLocked
  {
    get => this._isLocked;
    set => this.SetProperty<bool>(ref this._isLocked, value, nameof (IsLocked));
  }

  public bool IsSourceFilterAvailable
  {
    get => this._isSourceFilterAvailable;
    set
    {
      this.SetProperty<bool>(ref this._isSourceFilterAvailable, value, nameof (IsSourceFilterAvailable));
    }
  }

  public string Name
  {
    get => this._name;
    set => this.SetProperty<string>(ref this._name, value, nameof (Name));
  }

  public string Source
  {
    get => this._source;
    set => this.SetProperty<string>(ref this._source, value, nameof (Source));
  }

  public virtual List<ElementBase> Filter(IEnumerable<ElementBase> input)
  {
    return this.FilterSource((IEnumerable<ElementBase>) this.FilterName(input));
  }

  private bool Include(string target, IEnumerable<string> criteriaCollection)
  {
    bool flag = false;
    foreach (string criteria in criteriaCollection)
    {
      char[] chArray = new char[1]{ ' ' };
      foreach (string str in ((IEnumerable<string>) criteria.Split(chArray)).Where<string>((Func<string, bool>) (x => !string.IsNullOrWhiteSpace(x))).Select<string, string>((Func<string, string>) (x => x.Trim().ToLower())).ToList<string>())
      {
        flag = target.ToLower().Contains(str);
        if (!flag)
          break;
      }
      if (flag)
        break;
    }
    return flag;
  }

  protected virtual List<ElementBase> FilterName(IEnumerable<ElementBase> input)
  {
    List<ElementBase> elementBaseList = new List<ElementBase>();
    if (string.IsNullOrWhiteSpace(this.Name))
      return new List<ElementBase>(input);
    List<string> list = ((IEnumerable<string>) this.Name.Split('+')).Where<string>((Func<string, bool>) (x => !string.IsNullOrWhiteSpace(x))).Select<string, string>((Func<string, string>) (x => x.Trim().ToLower())).ToList<string>();
    foreach (ElementBase elementBase in input)
    {
      bool flag = this.Include(elementBase.Name, (IEnumerable<string>) list);
      if (!flag && this.IncludeKeywords)
      {
        foreach (string keyword in elementBase.Keywords)
        {
          flag = this.Include(keyword, (IEnumerable<string>) list);
          if (flag)
            break;
        }
      }
      if (flag)
        elementBaseList.Add(elementBase);
    }
    return elementBaseList;
  }

  protected virtual List<ElementBase> FilterSource(IEnumerable<ElementBase> input)
  {
    List<ElementBase> source = new List<ElementBase>(input);
    if (this._isSourceFilterAvailable && !string.IsNullOrWhiteSpace(this.Source) && !this.Source.Equals("--"))
      source = source.Where<ElementBase>((Func<ElementBase, bool>) (x => x.Source.ToLower().Contains(this.Source.ToLower().Trim()))).ToList<ElementBase>();
    return source;
  }

  public virtual void Clear()
  {
    this.Name = "";
    this.Source = "";
  }
}
