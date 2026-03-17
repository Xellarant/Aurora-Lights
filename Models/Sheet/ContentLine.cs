// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.Sheet.ContentLine
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using System;

#nullable disable
namespace Builder.Presentation.Models.Sheet;

public class ContentLine
{
  public string Name { get; set; }

  public string Content { get; set; }

  public bool NewLineBefore { get; set; }

  public bool Indent { get; set; }

  public ContentLine(string name, string content)
    : this(name, content, false, false)
  {
  }

  public ContentLine(string name, string content, bool newLineBefore)
    : this(name, content, newLineBefore, false)
  {
  }

  public ContentLine(string name, string content, bool newLineBefore, bool indent)
  {
    this.Name = name;
    this.Content = content;
    this.NewLineBefore = newLineBefore;
    this.Indent = indent;
  }

  public bool HasName() => !string.IsNullOrWhiteSpace(this.Name);

  public bool HasContent() => !string.IsNullOrWhiteSpace(this.Content);

  public override string ToString()
  {
    return $"{(this.NewLineBefore ? Environment.NewLine : "")}{(this.Indent ? "    " : "")}{this.Name}. {this.Content}";
  }
}
