// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.SpellcastingElementDescriptionDisplayRequestEvent
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Data;

#nullable disable
namespace Builder.Presentation.ViewModels;

public sealed class SpellcastingElementDescriptionDisplayRequestEvent(
  ElementBase element,
  string stylesheet = null) : ElementDescriptionDisplayRequestEvent(element, stylesheet)
{
}
