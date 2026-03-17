// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.CharacterSheet.CharacterSheetResourcePage
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using iTextSharp.text.pdf;
using System.IO;
using System.Reflection;

#nullable disable
namespace Builder.Presentation.Models.CharacterSheet;

public class CharacterSheetResourcePage
{
  private readonly string _resourcePath;

  public CharacterSheetResourcePage(string resourcePath) => this._resourcePath = resourcePath;

  public Stream GetResourceStream()
  {
    return Assembly.GetAssembly(typeof (CharacterSheetResourcePage)).GetManifestResourceStream(this._resourcePath);
  }

  public PdfReader CreateReader() => new PdfReader(this.GetResourceStream());
}
