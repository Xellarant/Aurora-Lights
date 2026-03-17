// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Elements.ElementCollectionEventArgs
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace Builder.Presentation.Elements;

public class ElementCollectionEventArgs : EventArgs
{
  public List<ElementBase> Elements { get; }

  public ElementCollectionEventArgs(List<ElementBase> elements) => this.Elements = elements;
}
