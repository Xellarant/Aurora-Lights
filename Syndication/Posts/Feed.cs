// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Syndication.Posts.Feed
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

#nullable disable
namespace Builder.Presentation.Syndication.Posts;

[XmlRoot("feed")]
[Serializable]
public class Feed
{
  public Feed()
  {
    this.Version = "1.19.309";
    this.Collection = new ObservableCollection<Post>();
  }

  [XmlAttribute("version")]
  public string Version { get; set; }

  [XmlElement("updated")]
  public string Updated { get; set; }

  [XmlArray("posts")]
  public ObservableCollection<Post> Collection { get; set; }

  public void Dismiss()
  {
    foreach (Post post in (System.Collections.ObjectModel.Collection<Post>) this.Collection)
      post.Dismiss();
  }

  public void MarkAsRead()
  {
    foreach (Post post in (System.Collections.ObjectModel.Collection<Post>) this.Collection)
      post.MarkAsRead();
  }
}
