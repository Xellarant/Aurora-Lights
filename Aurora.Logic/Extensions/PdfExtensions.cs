// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Extensions.PdfExtensions
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using iTextSharp.text.pdf;

#nullable disable
namespace Builder.Presentation.Extensions;

public static class PdfExtensions
{
  public static void SetFontSize(this PdfStamper stamper, float fontsize, params string[] fields)
  {
    foreach (string field in fields)
      stamper.AcroFields.SetFieldProperty(field, "textsize", (object) fontsize, (int[]) null);
  }
}
