// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.SelectionElement
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core;
using Builder.Data;
using Builder.Data.Elements;
using Builder.Data.Extensions;

#nullable disable
namespace Builder.Presentation.ViewModels;

public class SelectionElement : ObservableObject
{
  private string _shortDescription = string.Empty;
  private bool _isEnabled;
  private bool _isRecommended;
  private bool _isDefault;
  private bool _isHighlighted;
  private bool _isChosen;

  public SelectionElement(ElementBase element, bool isEnabled = true)
  {
    this.Element = element;
    this.IsEnabled = isEnabled;
  }

  public ElementBase Element { get; }

  public string DisplayName => this.Element.Name;

  public string DisplayShortDescription
  {
    get
    {
      if (string.IsNullOrWhiteSpace(this._shortDescription) && this.Element.ElementSetters.ContainsSetter("short"))
        return this.Element.ElementSetters.GetSetter("short").Value;
      switch (this.Element.Type)
      {
        case "Deity":
          return this.Element.AsElement<Deity>().Domains;
        case "Spell":
          return this.Element.AsElement<Spell>().GetShortDescription();
        default:
          return this._shortDescription;
      }
    }
    set => this._shortDescription = value;
  }

  public string DisplayPrerequisites => this.Element.Prerequisite;

  public string DisplaySource
  {
    get
    {
      if (this.Element.Source.StartsWith("Unearthed Arcana: "))
        return this.Element.Source.Replace("Unearthed Arcana: ", "UA: ");
      return this.Element.Source.StartsWith("Adventurers League: ") ? this.Element.Source.Replace("Adventurers League: ", "AL: ") : this.Element.Source;
    }
  }

  public bool IsEnabled
  {
    get => this._isEnabled;
    set => this.SetProperty<bool>(ref this._isEnabled, value, nameof (IsEnabled));
  }

  public bool IsRecommended
  {
    get => this._isRecommended;
    set => this.SetProperty<bool>(ref this._isRecommended, value, nameof (IsRecommended));
  }

  public bool IsDefault
  {
    get => this._isDefault;
    set => this.SetProperty<bool>(ref this._isDefault, value, nameof (IsDefault));
  }

  public bool IsHighlighted
  {
    get => this._isHighlighted;
    set => this.SetProperty<bool>(ref this._isHighlighted, value, nameof (IsHighlighted));
  }

  public bool IsChosen
  {
    get => this._isChosen;
    set => this.SetProperty<bool>(ref this._isChosen, value, nameof (IsChosen));
  }

  public override string ToString() => this.Element.Name + (this.IsEnabled ? "" : " (disabled)");
}
