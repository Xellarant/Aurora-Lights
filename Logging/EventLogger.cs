// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Logging.EventLogger
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core.Events;
using Builder.Core.Logging;
using System;

#nullable disable
namespace Builder.Presentation.Logging;

public class EventLogger : ILogger
{
  private readonly IEventAggregator _eventAggregator;

  public EventLogger(IEventAggregator eventAggregator) => this._eventAggregator = eventAggregator;

  public void Debug(string message, params object[] args)
  {
    this._eventAggregator.Send<EventLog>(new EventLog(DateTime.Now.ToString(), Log.Debug, string.Format(message, args)));
  }

  public void Info(string message, params object[] args)
  {
    this._eventAggregator.Send<EventLog>(new EventLog(DateTime.Now.ToString(), Log.Info, string.Format(message, args)));
  }

  public void Warning(string message, params object[] args)
  {
    this._eventAggregator.Send<EventLog>(new EventLog(DateTime.Now.ToString(), Log.Warning, string.Format(message, args)));
  }

  public void Exception(System.Exception ex)
  {
    this._eventAggregator.Send<EventLog>(new EventLog(DateTime.Now.ToString(), Log.Exception, ex.Message));
  }
}
