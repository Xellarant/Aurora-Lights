// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.Shell.CharacterCollectionSectionViewModel
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Presentation.ViewModels.Base;
using System.Threading.Tasks;

#nullable disable
namespace Builder.Presentation.ViewModels.Shell;

public class CharacterCollectionSectionViewModel : ViewModelBase
{
  public CharacterCollectionSectionViewModel()
  {
    int num = this.IsInDesignMode ? 1 : 0;
  }

  public override Task InitializeAsync(InitializationArguments args) => base.InitializeAsync(args);

  protected override void InitializeDesignData() => base.InitializeDesignData();
}
