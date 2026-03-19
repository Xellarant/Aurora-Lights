// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.HtmlDisplayRequestEvent
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core.Events;

#nullable disable
namespace Builder.Presentation.ViewModels;

public class HtmlDisplayRequestEvent : EventBase
{
  public HtmlDisplayRequestEvent(string html, string stylesheet = null)
  {
    this.Html = html;
    this.Stylesheet = stylesheet;
  }

  public string Html { get; }

  public string Stylesheet { get; }

  public bool ContainsStylesheet => !string.IsNullOrWhiteSpace(this.Stylesheet);
}
