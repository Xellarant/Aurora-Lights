// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.Content.RaceContentViewModel
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using System.Collections.Generic;

#nullable disable
namespace Builder.Presentation.ViewModels.Content;

public class RaceContentViewModel : SupportExpanderViewModel
{
  public RaceContentViewModel()
    : base((IEnumerable<string>) new string[6]
    {
      "Race",
      "Race Variant",
      "Sub Race",
      "Racial Trait",
      "Dragonmark",
      "Dragonmark Feature"
    })
  {
    this.Listings = (IEnumerable<string>) new List<string>()
    {
      "Race",
      "Race Variant",
      "Sub Race",
      "Racial Trait",
      "Dragonmark",
      "Dragonmark Feature"
    };
  }
}
