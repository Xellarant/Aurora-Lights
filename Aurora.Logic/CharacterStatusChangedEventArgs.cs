// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.CharacterStatusChangedEventArgs
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core.Events;
using Builder.Presentation.Models;

#nullable disable
namespace Builder.Presentation;

public class CharacterStatusChangedEventArgs : EventBase
{
  public Character Character { get; }

  public CharacterStatus Status { get; }

  public CharacterStatusChangedEventArgs(Character character, CharacterStatus status)
  {
    this.Character = character;
    this.Status = status;
  }
}
