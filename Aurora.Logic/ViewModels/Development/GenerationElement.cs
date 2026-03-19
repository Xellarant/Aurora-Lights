// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.Development.GenerationElement
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable
namespace Builder.Presentation.ViewModels.Development;

public class GenerationElement : ObservableObject
{
  private string _name = "";
  private string _type = "";
  private string _id = "";
  private string _source = "";

  public string Name
  {
    get => this._name;
    set => this.SetProperty<string>(ref this._name, value, nameof (Name));
  }

  public string Type
  {
    get => this._type;
    set => this.SetProperty<string>(ref this._type, value, nameof (Type));
  }

  public string Id
  {
    get => this._id;
    set => this.SetProperty<string>(ref this._id, value, nameof (Id));
  }

  public string Source
  {
    get => this._source;
    set => this.SetProperty<string>(ref this._source, value, nameof (Source));
  }

  public ObservableCollection<string> Supports { get; } = new ObservableCollection<string>();

  public ObservableCollection<GenerationElement.SetterItem> Setters { get; } = new ObservableCollection<GenerationElement.SetterItem>();

  public class SetterItem
  {
    public string Name { get; set; }

    public string Value { get; set; }

    public KeyValuePair<string, string>[] Attributes { get; set; }

    public SetterItem(string name, string value, params KeyValuePair<string, string>[] attributes)
    {
      this.Name = name;
      this.Value = value;
      this.Attributes = attributes;
    }

    public string GetXmlNode()
    {
      foreach (int num in "")
        ;
      return $"<set name=\"{this.Name}\">{this.Value}</set>";
    }
  }
}
