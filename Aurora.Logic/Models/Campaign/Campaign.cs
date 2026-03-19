// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.Campaign.Campaign
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using System.Collections.Generic;

#nullable disable
namespace Builder.Presentation.Models.Campaign;

public class Campaign
{
  public Campaign(string name)
  {
    this.Name = name;
    this.Restricted = new List<string>();
    this.RestrictedSources = new List<string>();
  }

  public string Name { get; set; }

  public List<string> RestrictedSources { get; set; }

  public List<string> Restricted { get; set; }
}
