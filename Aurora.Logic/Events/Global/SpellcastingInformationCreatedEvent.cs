// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Events.Global.SpellcastingInformationCreatedEvent
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core.Events;
using Builder.Data.Elements;

#nullable disable
namespace Builder.Presentation.Events.Global;

public class SpellcastingInformationCreatedEvent : EventBase
{
  public SpellcastingInformation Information { get; }

  public SpellcastingInformationCreatedEvent(SpellcastingInformation information)
  {
    this.Information = information;
  }
}
