// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.Development.DeveloperWindowGenerateViewModel
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Data;
using Builder.Presentation.Services.Data;
using Builder.Presentation.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

#nullable disable
namespace Builder.Presentation.ViewModels.Development;

public class DeveloperWindowGenerateViewModel : ViewModelBase
{
  private string _selectedType;
  private string _selectedSource;
  private string _name;
  private string _id;
  private bool _autoGenerateId;

  public DeveloperWindowGenerateViewModel()
  {
    if (this.IsInDesignMode)
      return;
    this.InitializeElementCollections();
  }

  private void InitializeElementCollections()
  {
    IEnumerable<string> collection1 = DataManager.Current.ElementsCollection.GroupBy<ElementBase, string>((Func<ElementBase, string>) (e => e.Type)).Select<IGrouping<string, ElementBase>, string>((Func<IGrouping<string, ElementBase>, string>) (g => g.First<ElementBase>().Type));
    IEnumerable<string> collection2 = DataManager.Current.ElementsCollection.GroupBy<ElementBase, string>((Func<ElementBase, string>) (e => e.Source)).Select<IGrouping<string, ElementBase>, string>((Func<IGrouping<string, ElementBase>, string>) (g => g.First<ElementBase>().Source));
    this.ElementTypes = new ObservableCollection<string>(collection1);
    this.ElementSources = new ObservableCollection<string>(collection2);
    this.SelectedType = this.ElementTypes.First<string>();
    this.SelectedSource = this.ElementSources.First<string>();
    this.Name = string.Empty;
    this.Id = string.Empty;
    this.AutoGenerateId = true;
  }

  public ObservableCollection<string> ElementTypes { get; private set; }

  public ObservableCollection<string> ElementSources { get; private set; }

  public string SelectedType
  {
    get => this._selectedType;
    set
    {
      this.SetProperty<string>(ref this._selectedType, value, nameof (SelectedType));
      if (!this._autoGenerateId)
        return;
      this.Id = this.GenerateUniqueId(this._name, this._selectedType);
    }
  }

  public string SelectedSource
  {
    get => this._selectedSource;
    set => this.SetProperty<string>(ref this._selectedSource, value, nameof (SelectedSource));
  }

  public string Name
  {
    get => this._name;
    set
    {
      this.SetProperty<string>(ref this._name, value, nameof (Name));
      if (!this._autoGenerateId)
        return;
      this.Id = this.GenerateUniqueId(this._name, this._selectedType);
    }
  }

  public string Id
  {
    get => this._id;
    set => this.SetProperty<string>(ref this._id, value, nameof (Id));
  }

  public bool AutoGenerateId
  {
    get => this._autoGenerateId;
    set => this.SetProperty<bool>(ref this._autoGenerateId, value, nameof (AutoGenerateId));
  }

  private string GenerateUniqueId(string elementName, string type)
  {
    string[] strArray = new string[3]{ " ", "/", "'" };
    foreach (string oldValue in strArray)
      elementName = elementName.Replace(oldValue, "");
    return $"ID_{type.Replace(" ", "").ToUpper()}_{elementName.Trim().ToUpper()}";
  }
}
