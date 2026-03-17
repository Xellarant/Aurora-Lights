// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ElementContainer
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core;
using Builder.Data;
using Builder.Presentation.Models.NewFolder1;
using System.Linq;

#nullable disable
namespace Builder.Presentation;

public class ElementContainer : ObservableObject
{
  private bool _isEnabled;
  private bool _isNested;

  public ElementContainer(ElementBase element)
  {
    if (element == null)
      return;
    this.Element = element;
    this.Name.OriginalContent = this.Element.Name;
    if (this.Element.SheetDescription.HasAlternateName)
      this.Name.OriginalContent = this.Element.SheetDescription.AlternateName;
    this.Description.OriginalContent = element.SheetDescription.FirstOrDefault<ElementSheetDescriptions.SheetDescription>()?.Description ?? "n/a";
    this.IsEnabled = element.SheetDescription.DisplayOnSheet;
  }

  public ElementBase Element { get; }

  public FillableField Name { get; set; } = new FillableField();

  public FillableField Description { get; set; } = new FillableField();

  public bool IsEnabled
  {
    get => this._isEnabled;
    set => this.SetProperty<bool>(ref this._isEnabled, value, nameof (IsEnabled));
  }

  public bool IsNested
  {
    get => this._isNested;
    set => this.SetProperty<bool>(ref this._isNested, value, nameof (IsNested));
  }

  public override string ToString() => this.Name.Content;
}
