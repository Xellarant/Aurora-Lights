// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.CharacterSheet.Pages.DetailsPageGenerator
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Presentation.Models.CharacterSheet.Content;
using iTextSharp.text;
using iTextSharp.text.pdf;

#nullable disable
namespace Builder.Presentation.Models.CharacterSheet.Pages;

public class DetailsPageGenerator : PageGenerator
{
  private readonly PdfReader _detailsPageReader;
  private readonly PdfReader _backgroundPageReader;

  public DetailsPageGenerator()
    : base()
  {
    this._detailsPageReader = CharacterSheetResources.GetDetailsPage().CreateReader();
    this._backgroundPageReader = CharacterSheetResources.GetBackgroundPage().CreateReader();
  }

  public void AddDetails(CharacterSheetExportContent content)
  {
    this.PlacePage(this._detailsPageReader, new Rectangle(0.0f, 0.0f, (float) this.PageWidth, (float) this.PageHeight));
  }

  public void AddBackground(CharacterSheetExportContent content)
  {
    this.PlacePage(this._backgroundPageReader, new Rectangle(0.0f, 0.0f, (float) this.PageWidth, (float) this.PageHeight));
  }

  public override void Dispose()
  {
    base.Dispose();
    this._detailsPageReader?.Dispose();
  }
}
