using System;
using System.Windows.Input;

namespace TodoListWpf.Commands;

/// <summary>
/// Implementación simple y reutilizable de ICommand para el patrón MVVM.
/// </summary>
public class RelayCommand : ICommand
{
    private readonly Action<object?> _actEjecutar;
    private readonly Predicate<object?>? _predPuedeEjecutar;

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public RelayCommand(Action<object?> actEjecutar, Predicate<object?>? predPuedeEjecutar = null)
    {
        _actEjecutar = actEjecutar ?? throw new ArgumentNullException(nameof(actEjecutar));
        _predPuedeEjecutar = predPuedeEjecutar;
    }

    public bool CanExecute(object? objParametro)
    {
        return _predPuedeEjecutar == null || _predPuedeEjecutar(objParametro);
    }

    public void Execute(object? objParametro)
    {
        _actEjecutar(objParametro);
    }
}
