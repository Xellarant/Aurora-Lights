// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.ContentManagerViewModel
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Presentation.ViewModels.Base;
using System.Collections.ObjectModel;

#nullable disable
namespace Builder.Presentation.ViewModels;

public sealed class ContentManagerViewModel : ViewModelBase
{
  public ContentManagerViewModel()
  {
    for (int index1 = 0; index1 < 5; ++index1)
    {
      ContentItem contentItem = new ContentItem($"Index {index1 + 1}");
      for (int index2 = 0; index2 < 10; ++index2)
        contentItem.Items.Add(new ContentItem($"Elements {index2 + 1}"));
      this.Items.Add(contentItem);
    }
  }

  public ObservableCollection<ContentItem> Items { get; } = new ObservableCollection<ContentItem>();
}
