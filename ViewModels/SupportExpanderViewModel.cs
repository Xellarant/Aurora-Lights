// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.SupportExpanderViewModel
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Data.Rules;
using Builder.Presentation.Interfaces;
using Builder.Presentation.Services;
using Builder.Presentation.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace Builder.Presentation.ViewModels;

public abstract class SupportExpanderViewModel : ViewModelBase, ISupportExpanders
{
  private object _lock = new object();
  private ISelectionRuleExpander _selectedExpander;

  protected SupportExpanderViewModel(IEnumerable<string> listings)
  {
    this.Listings = listings;
    this.Name = this.GetType().Name;
    this.Expanders = new ObservableCollection<ISelectionRuleExpander>();
    this.Expanders.CollectionChanged += new NotifyCollectionChangedEventHandler(this.Expanders_CollectionChanged);
    SelectionRuleExpanderHandler.Current.RegisterSupport((ISupportExpanders) this);
  }

  private void Expanders_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
  {
    this.OnPropertyChanged("Selects", "HasExpanders");
    this.OnPropertyChanged("HasExpanders");
  }

  public string Name { get; }

  public IEnumerable<string> Listings { get; set; }

  public ObservableCollection<ISelectionRuleExpander> Expanders { get; set; }

  public void AddExpander(ISelectionRuleExpander expander)
  {
    SelectRule rule = expander.SelectionRule;
    lock (this._lock)
    {
      if (this.Expanders.Count == 0)
      {
        this.Expanders.Add(expander);
      }
      else
      {
        string type = rule.Attributes.IsList ? rule.ElementHeader.Type : rule.Attributes.Type;
        if (type == "Class" || type == "Multiclass")
        {
          this.Expanders.Add(expander);
        }
        else
        {
          int num1 = this.Expanders.Any<ISelectionRuleExpander>((Func<ISelectionRuleExpander, bool>) (x => x.SelectionRule.Attributes.Type == "Multiclass")) ? 1 : 0;
          bool flag1 = this.Expanders.Select<ISelectionRuleExpander, string>((Func<ISelectionRuleExpander, string>) (e => e.SelectionRule.ElementHeader.Id)).Contains<string>(rule.ElementHeader.Id);
          bool flag2 = this.Expanders.Select<ISelectionRuleExpander, string>((Func<ISelectionRuleExpander, string>) (e => e.SelectionRule.Attributes.Type)).Contains<string>(type);
          List<string> list = this.Listings.ToList<string>();
          if (num1 != 0)
          {
            ClassProgressionManager manager = CharacterManager.Current.ClassProgressionManagers.OrderBy<ClassProgressionManager, int>((Func<ClassProgressionManager, int>) (x => x.StartingLevel)).ToList<ClassProgressionManager>().FirstOrDefault<ClassProgressionManager>((Func<ClassProgressionManager, bool>) (x => x.SelectionRules.Contains(rule)));
            if (manager?.ClassElement == null)
            {
              this.Expanders.Add(expander);
            }
            else
            {
              ISelectionRuleExpander selectionRuleExpander = this.Expanders.FirstOrDefault<ISelectionRuleExpander>((Func<ISelectionRuleExpander, bool>) (x => x.SelectionRule == manager.SelectRule));
              int num2 = this.Expanders.IndexOf(selectionRuleExpander);
              if (this.Expanders.Last<ISelectionRuleExpander>() == selectionRuleExpander)
              {
                this.Expanders.Add(expander);
              }
              else
              {
                for (int index1 = num2; index1 < this.Expanders.Count; ++index1)
                {
                  int index2 = index1 + 1;
                  if (index2 == this.Expanders.Count)
                  {
                    this.Expanders.Add(expander);
                    return;
                  }
                  string type1 = this.Expanders[index2].SelectionRule.Attributes.Type;
                  if (type1 == "Multiclass")
                  {
                    this.Expanders.Insert(index2, expander);
                    return;
                  }
                  if (list.IndexOf(rule.Attributes.Type) < list.IndexOf(type1))
                  {
                    this.Expanders.Insert(index2, expander);
                    return;
                  }
                }
                this.Expanders.Add(expander);
              }
            }
          }
          else if (flag2)
          {
            int num3 = this.Expanders.IndexOf(this.Expanders.Last<ISelectionRuleExpander>((Func<ISelectionRuleExpander, bool>) (e => e.SelectionRule.Attributes.Type == type)));
            if (flag1)
              num3 = this.Expanders.IndexOf(this.Expanders.Last<ISelectionRuleExpander>((Func<ISelectionRuleExpander, bool>) (e => e.SelectionRule.ElementHeader.Id == rule.ElementHeader.Id)));
            if (rule.Attributes.IsList)
            {
              ISelectionRuleExpander selectionRuleExpander = this.Expanders.LastOrDefault<ISelectionRuleExpander>((Func<ISelectionRuleExpander, bool>) (e => e.SelectionRule.ElementHeader.Type == type));
              if (selectionRuleExpander != null)
                num3 = this.Expanders.IndexOf(selectionRuleExpander);
            }
            int index = num3 + 1;
            if (index == this.Expanders.Count)
              this.Expanders.Add(expander);
            else
              this.Expanders.Insert(index, expander);
          }
          else
          {
            int num4 = 0;
            if (flag1)
              num4 = this.Expanders.IndexOf(this.Expanders.Last<ISelectionRuleExpander>((Func<ISelectionRuleExpander, bool>) (e => e.SelectionRule.ElementHeader.Id == rule.ElementHeader.Id)));
            for (int index3 = num4; index3 < this.Expanders.Count; ++index3)
            {
              int index4 = index3 + 1;
              if (index4 == this.Expanders.Count)
              {
                this.Expanders.Add(expander);
                return;
              }
              string type2 = this.Expanders[index4].SelectionRule.Attributes.Type;
              if (list.IndexOf(rule.Attributes.Type) < list.IndexOf(type2))
              {
                this.Expanders.Insert(index4, expander);
                return;
              }
            }
            if (Debugger.IsAttached)
              Debugger.Break();
            this.Expanders.Add(expander);
          }
        }
      }
    }
  }

  public string Selects
  {
    get
    {
      return string.Join("\r\n", this.Expanders.Select<ISelectionRuleExpander, string>((Func<ISelectionRuleExpander, string>) (x => $"{x.SelectionRule?.ToString()} [supports:{x.SelectionRule.Attributes.Supports}]")));
    }
  }

  public virtual bool HasExpanders => this.Expanders.Any<ISelectionRuleExpander>();

  public ISelectionRuleExpander SelectedExpander
  {
    get => this._selectedExpander;
    set
    {
      this.SetProperty<ISelectionRuleExpander>(ref this._selectedExpander, value, nameof (SelectedExpander));
    }
  }
}
