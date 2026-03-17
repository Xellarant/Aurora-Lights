// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.CharacterOptionsManager
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Data;
using Builder.Data.Strings;
using System;
using System.Linq;

#nullable disable
namespace Builder.Presentation;

[Obsolete]
public class CharacterOptionsManager
{
  private readonly CharacterManager _manager;

  public CharacterOptionsManager(CharacterManager manager) => this._manager = manager;

  public bool ContainsOption(string id)
  {
    return this._manager.GetElements().Select<ElementBase, string>((Func<ElementBase, string>) (x => x.Id)).Contains<string>(id);
  }

  public bool ContainsAverageHitPointsOption()
  {
    return this.ContainsOption(InternalOptions.AllowAverageHitPoints);
  }

  public bool ContainsMulticlassOption() => this.ContainsOption(InternalOptions.AllowMulticlassing);

  public bool ContainsFeatsOption() => this.ContainsOption(InternalOptions.AllowFeats);
}
