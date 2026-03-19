// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.Shell.Items.EquipmentContentViewModel
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Presentation.ViewModels.Base;

#nullable disable
namespace Builder.Presentation.ViewModels.Shell.Items;

public sealed class EquipmentContentViewModel : ViewModelBase
{
  public EquipmentContentViewModel()
  {
    if (this.IsInDesignMode)
      return;
    this.EventAggregator.Subscribe((object) this);
  }
}
