// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Commands.ApplicationCommands
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core.Events;

#nullable disable
namespace Builder.Presentation.Commands;

public abstract class ApplicationCommands
{
  protected ApplicationCommands(IEventAggregator eventAggregator, CharacterManager manager)
  {
    this.EventAggregator = eventAggregator;
    this.Manager = manager;
  }

  protected IEventAggregator EventAggregator { get; }

  protected CharacterManager Manager { get; }
}
