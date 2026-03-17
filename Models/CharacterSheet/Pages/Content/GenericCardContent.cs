// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.CharacterSheet.Pages.Content.GenericCardContent
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

#nullable disable
namespace Builder.Presentation.Models.CharacterSheet.Pages.Content;

public class GenericCardContent
{
  public string Title { get; set; }

  public string Subtitle { get; set; }

  public string Description { get; set; }

  public string LeftFooter { get; set; }

  public string RightFooter { get; set; }

  public GenericCardContent(
    string title,
    string subtitle,
    string description = "",
    string left = "",
    string right = "")
  {
    this.Title = title;
    this.Subtitle = subtitle;
    this.Description = description;
    this.LeftFooter = left;
    this.RightFooter = right;
    this.DescriptionHtml = "";
  }

  public string DescriptionHtml { get; set; }
}
