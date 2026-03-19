// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Events.Character.CharacterPortraitChangedEvent
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core.Events;

#nullable disable
namespace Builder.Presentation.Events.Character;

public class CharacterPortraitChangedEvent : EventBase
{
  public string Filename { get; }

  public CharacterPortraitChangedEvent(string filename) => this.Filename = filename;
}
