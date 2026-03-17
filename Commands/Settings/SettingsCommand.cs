// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Commands.Settings.SettingsCommand
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using System;
using System.Windows.Input;

#nullable disable
namespace Builder.Presentation.Commands.Settings;

internal abstract class SettingsCommand : ICommand
{
  protected Builder.Presentation.Properties.Settings Settings { get; }

  protected SettingsCommand() => this.Settings = Builder.Presentation.Properties.Settings.Default;

  public abstract bool CanExecute(object parameter);

  public abstract void Execute(object parameter);

  public event EventHandler CanExecuteChanged
  {
    add => CommandManager.RequerySuggested += value;
    remove => CommandManager.RequerySuggested -= value;
  }
}
