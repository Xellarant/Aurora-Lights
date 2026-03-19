// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ViewModels.Shell.StartSectionViewModel
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Presentation.ViewModels.Base;

#nullable disable
namespace Builder.Presentation.ViewModels.Shell;

public class StartSectionViewModel : ViewModelBase
{
  private bool _isStartSectionEnabled;
  private bool _isBuildSectionEnabled;
  private bool _isMagicSectionEnabled;
  private bool _isEquipmentSectionEnabled;
  private bool _isManageCharacterSectionEnabled;
  private bool _isCharacterSheetSectionEnabled;

  public StartSectionViewModel()
  {
    this._isStartSectionEnabled = false;
    this._isBuildSectionEnabled = false;
    this._isMagicSectionEnabled = false;
    this._isEquipmentSectionEnabled = false;
    this._isManageCharacterSectionEnabled = false;
    this._isCharacterSheetSectionEnabled = false;
  }

  public bool IsStartSectionEnabled
  {
    get => this._isStartSectionEnabled;
    set
    {
      this.SetProperty<bool>(ref this._isStartSectionEnabled, value, nameof (IsStartSectionEnabled));
    }
  }

  public bool IsBuildSectionEnabled
  {
    get => this._isBuildSectionEnabled;
    set
    {
      this.SetProperty<bool>(ref this._isBuildSectionEnabled, value, nameof (IsBuildSectionEnabled));
    }
  }

  public bool IsMagicSectionEnabled
  {
    get => this._isMagicSectionEnabled;
    set
    {
      this.SetProperty<bool>(ref this._isMagicSectionEnabled, value, nameof (IsMagicSectionEnabled));
    }
  }

  public bool IsEquipmentSectionEnabled
  {
    get => this._isEquipmentSectionEnabled;
    set
    {
      this.SetProperty<bool>(ref this._isEquipmentSectionEnabled, value, nameof (IsEquipmentSectionEnabled));
    }
  }

  public bool IsManageCharacterSectionEnabled
  {
    get => this._isManageCharacterSectionEnabled;
    set
    {
      this.SetProperty<bool>(ref this._isManageCharacterSectionEnabled, value, nameof (IsManageCharacterSectionEnabled));
    }
  }

  public bool IsCharacterSheetSectionEnabled
  {
    get => this._isCharacterSheetSectionEnabled;
    set
    {
      this.SetProperty<bool>(ref this._isCharacterSheetSectionEnabled, value, nameof (IsCharacterSheetSectionEnabled));
    }
  }
}
