// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.CharacterStatus
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core;
using System;

#nullable disable
namespace Builder.Presentation;

public class CharacterStatus : ObservableObject
{
  private bool _isNew;
  private bool _isLoaded;
  private bool _hasChanges;
  private bool _isUserPortrait;
  private bool _hasMainClass;
  private bool _canMulticlass;
  private bool _hasMulticlass;
  private bool _canLevelUp = true;
  private bool _canLevelDown;
  private bool _hasSpellcasting;
  private bool _hasMulticlassSpellSlots;
  private bool _hasCompanion;
  private bool _hasDragonmark;

  public event EventHandler<CharacterStatusChangedEventArgs> StatusChanged;

  public bool IsNew
  {
    get => this._isNew;
    set
    {
      this.SetProperty<bool>(ref this._isNew, value, nameof (IsNew));
      this.OnStatusChanged();
    }
  }

  public bool IsLoaded
  {
    get => this._isLoaded;
    set
    {
      this.SetProperty<bool>(ref this._isLoaded, value, nameof (IsLoaded));
      this.OnStatusChanged();
    }
  }

  public bool HasChanges
  {
    get => this._hasChanges;
    set
    {
      this.SetProperty<bool>(ref this._hasChanges, value, nameof (HasChanges));
      this.OnStatusChanged();
    }
  }

  public bool IsUserPortrait
  {
    get => this._isUserPortrait;
    set
    {
      this.SetProperty<bool>(ref this._isUserPortrait, value, nameof (IsUserPortrait));
      this.OnStatusChanged();
    }
  }

  public bool HasMainClass
  {
    get => this._hasMainClass;
    set
    {
      this.SetProperty<bool>(ref this._hasMainClass, value, nameof (HasMainClass));
      this.OnStatusChanged();
    }
  }

  public bool CanMulticlass
  {
    get => this._canMulticlass;
    set
    {
      this.SetProperty<bool>(ref this._canMulticlass, value, nameof (CanMulticlass));
      this.OnStatusChanged();
    }
  }

  public bool HasMulticlass
  {
    get => this._hasMulticlass;
    set
    {
      this.SetProperty<bool>(ref this._hasMulticlass, value, nameof (HasMulticlass));
      this.OnStatusChanged();
    }
  }

  public bool CanLevelUp
  {
    get => this._canLevelUp;
    set
    {
      this.SetProperty<bool>(ref this._canLevelUp, value, nameof (CanLevelUp));
      this.OnStatusChanged();
    }
  }

  public bool CanLevelDown
  {
    get => this._canLevelDown;
    set
    {
      this.SetProperty<bool>(ref this._canLevelDown, value, nameof (CanLevelDown));
      this.OnStatusChanged();
    }
  }

  public bool HasSpellcasting
  {
    get => this._hasSpellcasting;
    set
    {
      this.SetProperty<bool>(ref this._hasSpellcasting, value, nameof (HasSpellcasting));
      this.OnStatusChanged();
    }
  }

  public bool HasMulticlassSpellSlots
  {
    get => this._hasMulticlassSpellSlots;
    set
    {
      this.SetProperty<bool>(ref this._hasMulticlassSpellSlots, value, nameof (HasMulticlassSpellSlots));
      this.OnStatusChanged();
    }
  }

  public bool HasCompanion
  {
    get => this._hasCompanion;
    set
    {
      this.SetProperty<bool>(ref this._hasCompanion, value, nameof (HasCompanion));
      this.OnStatusChanged();
    }
  }

  public bool HasDragonmark
  {
    get => this._hasDragonmark;
    set => this.SetProperty<bool>(ref this._hasDragonmark, value, nameof (HasDragonmark));
  }

  protected virtual void OnStatusChanged()
  {
    EventHandler<CharacterStatusChangedEventArgs> statusChanged = this.StatusChanged;
    if (statusChanged == null)
      return;
    statusChanged((object) this, new CharacterStatusChangedEventArgs(CharacterManager.Current.Character, this));
  }
}
