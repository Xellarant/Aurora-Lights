using System.Windows.Input;

namespace Builder.Presentation.Utilities;

/// <summary>
/// Cross-platform ICommand implementation that replaces
/// FirstFloor.ModernUI.Presentation.RelayCommand used in the legacy WPF codebase.
/// Supports both parameterless and parameterised forms.
/// </summary>
public sealed class RelayCommand : ICommand
{
    private readonly Action<object?> _execute;
    private readonly Func<object?, bool>? _canExecute;

    public RelayCommand(Action execute, Func<bool>? canExecute = null)
        : this(_ => execute(), canExecute is null ? null : _ => canExecute()) { }

    public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => _canExecute is null || _canExecute(parameter);

    public void Execute(object? parameter) => _execute(parameter);

    public void RaiseCanExecuteChanged() =>
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);

    /// <summary>API alias matching FirstFloor.ModernUI.Presentation.RelayCommand.</summary>
    public void OnCanExecuteChanged() => RaiseCanExecuteChanged();
}
