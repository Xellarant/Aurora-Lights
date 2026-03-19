// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ContentBuilder
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Presentation.Models.Sheet;

#nullable disable
namespace Builder.Presentation;

public class ContentBuilder
{
  private readonly ContentField _field;

  public ContentBuilder(string key = "") => this._field = new ContentField(key);

  public string Key => this._field.Key;

  public ContentBuilder Append(string name, string content, bool indent)
  {
    this._field.Lines.Add(new ContentLine(name, content, false, indent));
    return this;
  }

  public ContentBuilder AppendNewLine() => this;

  public ContentField GetContentField() => this._field;
}
