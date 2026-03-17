// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Events.ExpanderSelectionChanged
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core.Events;
using Builder.Data;

#nullable disable
namespace Builder.Presentation.Events;

public class ExpanderSelectionChanged : EventBase
{
  public ExpanderSelectionChanged(ElementBase element) => this.Element = element;

  public ElementBase Element { get; }
}
