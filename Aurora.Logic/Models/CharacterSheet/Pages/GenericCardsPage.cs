// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.CharacterSheet.Pages.GenericCardsPage
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using System.Collections.Generic;

#nullable disable
namespace Builder.Presentation.Models.CharacterSheet.Pages;

public class GenericCardsPage
{
  public Dictionary<int, GenericCardsPage.GenericCardContent> Cards { get; } = new Dictionary<int, GenericCardsPage.GenericCardContent>();

  public void PopulateCard(int index, GenericCardsPage.GenericCardContent content)
  {
    if (this.Cards.ContainsKey(index))
      this.Cards[index] = content;
    else
      this.Cards.Add(index, content);
  }

  public class GenericCardContent
  {
    public string Title { get; set; } = string.Empty;

    public string Subtitle { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string LeftFooter { get; set; } = string.Empty;

    public string RightFooter { get; set; } = string.Empty;
  }
}
