// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Syndication.SyndicationUpdateResultEventArgs
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Presentation.Syndication.Posts;
using System;
using System.Collections.Generic;

#nullable disable
namespace Builder.Presentation.Syndication;

public class SyndicationUpdateResultEventArgs : EventArgs
{
  public IEnumerable<Post> NewPosts { get; }

  public SyndicationUpdateResultEventArgs(IEnumerable<Post> newPosts) => this.NewPosts = newPosts;
}
