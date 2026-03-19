// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ContentGenerator
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Data.Elements;
using Builder.Presentation.Models.Sheet;
using System.Collections.Generic;

#nullable disable
namespace Builder.Presentation;

public class ContentGenerator
{
  private readonly ElementsOrganizer _organizer;

  public ContentGenerator(ElementsOrganizer organizer) => this._organizer = organizer;

  public ContentField GetLanguagesField()
  {
    IEnumerable<Language> languages = this._organizer.GetLanguages();
    ContentBuilder contentBuilder = new ContentBuilder("lang");
    contentBuilder.Append("Languages", string.Join<Language>(", ", languages), false).AppendNewLine().Append("WP", "-", false).AppendNewLine().Append("AP", "-", false).AppendNewLine().Append("TP", "-", false);
    return contentBuilder.GetContentField();
  }
}
