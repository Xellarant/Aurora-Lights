// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.ContentItem
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core;
using System.Collections.ObjectModel;

#nullable disable
namespace Builder.Presentation.ViewModels;

public class ContentItem : ObservableObject
{
  private string _displayName;

  public ContentItem(string displayName) => this._displayName = displayName;

  public string DisplayName
  {
    get => this._displayName;
    set => this.SetProperty<string>(ref this._displayName, value, nameof (DisplayName));
  }

  public ObservableCollection<ContentItem> Items { get; } = new ObservableCollection<ContentItem>();
}
