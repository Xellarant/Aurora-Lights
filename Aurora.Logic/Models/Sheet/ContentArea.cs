// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.Sheet.ContentArea
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using System.Collections.Generic;
using System.Text;

#nullable disable
namespace Builder.Presentation.Models.Sheet;

public class ContentArea : List<ContentLine>
{
  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (ContentLine contentLine in (List<ContentLine>) this)
      stringBuilder.Append((object) contentLine);
    return stringBuilder.ToString();
  }
}
