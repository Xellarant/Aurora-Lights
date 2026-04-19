// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Views.Sliders.BundleManagerSliderViewModel
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Data.Files;
using Builder.Presentation.Services.Data;
using Builder.Presentation.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

#nullable disable
namespace Builder.Presentation.Views.Sliders;

public class BundleManagerSliderViewModel : ViewModelBase
{
  private bool _isCheckContentUpdatesOnStartupEnabled;

  public BundleManagerSliderViewModel()
  {
    if (this.IsInDesignMode)
    {
      IndexFile indexFile1 = new IndexFile((FileInfo) null)
      {
        Info = {
          DisplayName = "Core",
          Description = "The content from the core rulebooks.",
          Author = "Wizards of the Coast",
          UpdateFilename = "core.index",
          UpdateUrl = "online/core.index",
          Version = new Version(1, 0, 1)
        }
      };
      IndexFile indexFile2 = new IndexFile((FileInfo) null)
      {
        Info = {
          DisplayName = "Supplements",
          Description = "Supplements from Wizards of the Coast to expand your core gaming elements.",
          Author = "Wizards of the Coast",
          UpdateFilename = "supplements.index",
          UpdateUrl = "online/supplements.index",
          Version = new Version(1, 2, 1)
        }
      };
      foreach (IndexFile file in ((IEnumerable<string>) Directory.GetFiles("C:\\Users\\bas_d\\Documents\\5e Character Builder\\custom", "*.index", SearchOption.AllDirectories)).Select<string, IndexFile>((Func<string, IndexFile>) (x => new IndexFile(new FileInfo(x)))))
      {
        file.Load();
        this.Indices.Add(new ContentFileContainer(file));
      }
    }
    else
    {
      this.LoadIndices();
      this.SubscribeWithEventAggregator();
    }
  }

  public bool IsCheckContentUpdatesOnStartupEnabled
  {
    get => this._isCheckContentUpdatesOnStartupEnabled;
    set
    {
      this.SetProperty<bool>(ref this._isCheckContentUpdatesOnStartupEnabled, value, nameof (IsCheckContentUpdatesOnStartupEnabled));
    }
  }

  public ObservableCollection<ContentFileContainer> Indices { get; } = new ObservableCollection<ContentFileContainer>();

  private void LoadIndices()
  {
    string[] files = Directory.GetFiles(DataManager.Current.UserDocumentsCustomElementsDirectory, "*.index", SearchOption.AllDirectories);
    this.Indices.Clear();
    foreach (IndexFile file in ((IEnumerable<string>) files).Select<string, IndexFile>((Func<string, IndexFile>) (x => new IndexFile(new FileInfo(x)))))
    {
      file.Load();
      if (file.ContainsElementFiles())
        this.Indices.Add(new ContentFileContainer(file));
    }
  }
}
