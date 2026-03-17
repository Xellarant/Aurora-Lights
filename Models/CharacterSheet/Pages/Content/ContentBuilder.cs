// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.CharacterSheet.Pages.Content.ContentBuilder
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Presentation.Models.CharacterSheet.PDF;
using iTextSharp.text;
using System;

#nullable disable
namespace Builder.Presentation.Models.CharacterSheet.Pages.Content;

public class ContentBuilder
{
  private readonly Font _regular;
  private readonly Font _bold;
  private readonly Font _italic;
  private readonly Font _boldItalic;
  private readonly Paragraph _content;

  public ContentBuilder()
  {
    this._regular = FontsHelper.GetRegular();
    this._bold = FontsHelper.GetBold();
    this._italic = FontsHelper.GetItalic();
    this._boldItalic = FontsHelper.GetBoldItalic();
    this._content = new Paragraph();
  }

  public ContentBuilder Append(string value, bool newLine = false)
  {
    return this.Append(value, this._regular, newLine);
  }

  public ContentBuilder AppendBold(string value, bool newLine = false)
  {
    return this.Append(value, this._bold, newLine);
  }

  public ContentBuilder AppendItalic(string value, bool newLine = false)
  {
    return this.Append(value, this._italic, newLine);
  }

  public ContentBuilder AppendBoldItalic(string value, bool newLine = false)
  {
    return this.Append(value, this._boldItalic, newLine);
  }

  public ContentBuilder AppendLine(string value) => this.Append(value, this._regular, true);

  public Paragraph GetContent(int alignment = 0)
  {
    this._content.Alignment = alignment;
    return this._content;
  }

  public override string ToString() => this._content.Content;

  private ContentBuilder Append(string value, Font font, bool newLine = false)
  {
    this._content.Add((IElement) new Chunk(value + (newLine ? Environment.NewLine : ""), font));
    return this;
  }
}
