using System;
using System.Windows.Input;

namespace TodoListWpf.Commands;

/// <summary>
/// Implementación reusable de ICommand para el patrón MVVM.
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

    /// <summary>
    /// Tarea: Construye una instancia del comando asociando la acción y predicado opcional.
    /// </summary>
    public RelayCommand(Action<object?> actEjecutar, Predicate<object?>? predPuedeEjecutar = null)
    {
        _actEjecutar = actEjecutar ?? throw new ArgumentNullException(nameof(actEjecutar));
        _predPuedeEjecutar = predPuedeEjecutar;
    }

    /// <summary>
    /// Tarea: Evalúa si el comando tiene autorización para ejecutarse con el parámetro dado.
    /// </summary>
    public bool CanExecute(object? objParametro)
    {
        return _predPuedeEjecutar == null || _predPuedeEjecutar(objParametro);
    }

    /// <summary>
    /// Tarea: Ejecuta la acción delegada del comando.
    /// </summary>
    public void Execute(object? objParametro)
    {
        _actEjecutar(objParametro);
    }
}
