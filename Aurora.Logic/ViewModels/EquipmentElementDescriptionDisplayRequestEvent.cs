// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.EquipmentElementDescriptionDisplayRequestEvent
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Data;

#nullable disable
namespace Builder.Presentation.ViewModels;

public sealed class EquipmentElementDescriptionDisplayRequestEvent : 
  ElementDescriptionDisplayRequestEvent
{
  public ElementBase Parent { get; }

  public EquipmentElementDescriptionDisplayRequestEvent(
    ElementBase element,
    ElementBase parent = null,
    string stylesheet = null)
    : base(element, stylesheet)
  {
    if (parent == null || element == null || parent.Id.Equals(element.Id))
      return;
    this.Parent = parent;
  }
}
