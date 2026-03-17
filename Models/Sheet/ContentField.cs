// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.Sheet.ContentField
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using System.Collections.Generic;

#nullable disable
namespace Builder.Presentation.Models.Sheet;

public class ContentField
{
  public string Key { get; set; }

  public List<ContentLine> Lines { get; set; }

  public ContentField(string key = "")
  {
    this.Key = key;
    this.Lines = new List<ContentLine>();
  }
}
