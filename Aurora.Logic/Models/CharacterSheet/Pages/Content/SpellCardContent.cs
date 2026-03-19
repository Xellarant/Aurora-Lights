// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.CharacterSheet.Pages.Content.SpellCardContent
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

#nullable disable
namespace Builder.Presentation.Models.CharacterSheet.Pages.Content;

public class SpellCardContent(
  string title,
  string subtitle,
  string description = "",
  string left = "",
  string right = "") : GenericCardContent(title, subtitle, description, left, right)
{
  public string CastingTime { get; set; }

  public string Range { get; set; }

  public string Duration { get; set; }

  public string Components { get; set; }
}
