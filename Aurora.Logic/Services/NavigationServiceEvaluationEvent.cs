// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Services.NavigationServiceEvaluationEvent
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core.Events;
using Builder.Data.Rules;

#nullable disable
namespace Builder.Presentation.Services;

public class NavigationServiceEvaluationEvent : EventBase
{
  public int Remaining { get; }

  public int Count { get; }

  public SelectRule FirstRequiredSelectionRule { get; }

  public NavigationServiceEvaluationEvent(
    int remaining,
    int count,
    SelectRule firstRequiredSelectionRule)
  {
    this.Remaining = remaining;
    this.Count = count;
    this.FirstRequiredSelectionRule = firstRequiredSelectionRule;
  }
}
