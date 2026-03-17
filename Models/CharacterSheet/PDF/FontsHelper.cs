// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.CharacterSheet.PDF.FontsHelper
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using iTextSharp.text;
using System;
using System.IO;

#nullable disable
namespace Builder.Presentation.Models.CharacterSheet.PDF;

public static class FontsHelper
{
  public static Font GetRegular(float size = 12f)
  {
    return FontsHelper.GetFont("Calibri", "calibri.ttf", size) ?? FontFactory.GetFont("Helvetica", size);
  }

  public static Font GetBold(float size = 12f)
  {
    return FontsHelper.GetFont("Calibri Bold", "calibrib.ttf", size) ?? FontFactory.GetFont("Helvetica-Bold", size);
  }

  public static Font GetItalic(float size = 12f)
  {
    return FontsHelper.GetFont("Calibri Italic", "calibrii.ttf", size) ?? FontFactory.GetFont("Helvetica-Oblique", size);
  }

  public static Font GetBoldItalic(float size = 12f)
  {
    return FontsHelper.GetFont("Calibri Bold Italic", "calibriz.ttf", size) ?? FontFactory.GetFont("Helvetica-BoldOblique", size);
  }

  private static Font GetFont(string fontName, string filename, float size = 0.0f)
  {
    string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
    if (!FontFactory.IsRegistered(filename))
      FontFactory.Register(Path.Combine(folderPath, filename));
    return FontFactory.GetFont(fontName, "Identity-H", true, size);
  }
}
