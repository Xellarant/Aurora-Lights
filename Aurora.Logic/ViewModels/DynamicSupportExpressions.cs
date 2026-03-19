// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.DynamicSupportExpressions
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

#nullable disable
namespace Builder.Presentation.ViewModels;

public static class DynamicSupportExpressions
{
  public const string SpellcastingList = "$(spellcasting:list)";
  public const string SpellcastingSlots = "$(spellcasting:slots)";

  public static string[] All
  {
    get
    {
      return new string[2]
      {
        "$(spellcasting:list)",
        "$(spellcasting:slots)"
      };
    }
  }
}
