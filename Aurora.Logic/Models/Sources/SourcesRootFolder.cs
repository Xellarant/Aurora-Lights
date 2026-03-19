// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.Sources.SourcesRootFolder
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using System.Collections;

#nullable disable
namespace Builder.Presentation.Models.Sources;

public class SourcesRootFolder
{
  public SourcesRootFolder(string name, IEnumerable items = null)
  {
    this.Name = name;
    this.Items = items;
  }

  public string Name { get; set; }

  public IEnumerable Items { get; }

  public override string ToString() => this.Name ?? "";
}
