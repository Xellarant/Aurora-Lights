// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Services.SelectionRuleNavigationArgs
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core.Events;
using System;

#nullable disable
namespace Builder.Presentation.Services;

[Obsolete("rename to generic navigation for main tab application")]
public class SelectionRuleNavigationArgs : EventBase
{
  public NavigationLocation Location { get; }

  public SelectionRuleNavigationArgs(NavigationLocation location) => this.Location = location;
}
