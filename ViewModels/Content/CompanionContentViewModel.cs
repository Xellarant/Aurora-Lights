// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.Content.CompanionContentViewModel
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using System.Collections.Generic;

#nullable disable
namespace Builder.Presentation.ViewModels.Content;

public class CompanionContentViewModel : SupportExpanderViewModel
{
  public CompanionContentViewModel()
    : base((IEnumerable<string>) new string[6]
    {
      "Companion",
      "Companion Feature",
      "Companion Trait",
      "Companion Action",
      "Monster",
      "Monster Trait"
    })
  {
  }
}
