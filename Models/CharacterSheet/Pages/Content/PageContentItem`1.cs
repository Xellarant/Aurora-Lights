// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.CharacterSheet.Pages.Content.PageContentItem`1
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

#nullable disable
namespace Builder.Presentation.Models.CharacterSheet.Pages.Content;

public abstract class PageContentItem<T> : IPageContentItem
{
  protected PageContentItem(string key, T content)
  {
    this.Key = key;
    this.Content = content;
  }

  public string Key { get; }

  public T Content { get; }

  public override string ToString() => this.Key;
}
