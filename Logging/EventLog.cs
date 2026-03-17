// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Logging.EventLog
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core.Events;
using Builder.Core.Logging;

#nullable disable
namespace Builder.Presentation.Logging;

public class EventLog : EventBase
{
  public string Message { get; }

  public Log Level { get; }

  public string Timestamp { get; }

  public EventLog(string timestamp, Log level, string message)
  {
    this.Message = message;
    this.Level = level;
    this.Timestamp = timestamp;
  }
}
