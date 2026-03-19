// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Syndication.Posts.Post
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core;
using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Xml.Serialization;

#nullable disable
namespace Builder.Presentation.Syndication.Posts;

[XmlType("post")]
[Serializable]
public class Post : ObservableObject
{
  private bool _isNew;
  private bool _isDismissed;

  public Post() => this._isNew = true;

  [XmlAttribute("id")]
  public string Identifier { get; set; }

  [XmlElement("title")]
  public string Title { get; set; }

  [XmlElement("content")]
  public string Content { get; set; }

  [XmlElement("url")]
  public string Url { get; set; }

  [XmlElement("date")]
  public string Date { get; set; }

  [XmlElement("image")]
  public string Image { get; set; }

  [XmlIgnore]
  public bool IsNew
  {
    get => this._isNew;
    set => this.SetProperty<bool>(ref this._isNew, value, nameof (IsNew));
  }

  [XmlAttribute("dismissed")]
  public bool IsDismissed
  {
    get => this._isDismissed;
    set => this.SetProperty<bool>(ref this._isDismissed, value, nameof (IsDismissed));
  }

  public ICommand DismissCommand
  {
    get
    {
      return (ICommand) new RelayCommand(new Action(this.Dismiss), (Func<bool>) (() => !this.IsDismissed));
    }
  }

  public ICommand ViewCommand
  {
    get
    {
      return (ICommand) new RelayCommand((Action) (() =>
      {
        Process.Start(this.Url);
        this.MarkAsRead();
      }));
    }
  }

  public void Dismiss()
  {
    this.IsDismissed = true;
    this.MarkAsRead();
  }

  public void MarkAsRead() => this.IsNew = false;

  public bool ShouldSerializeIsDismissed() => this.IsDismissed;

  [XmlElement("meta")]
  public PostMeta Meta { get; set; } = new PostMeta();
}
