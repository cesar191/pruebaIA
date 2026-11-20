using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TodoListWpf.ViewModels;

/// <summary>
/// Clase base para ViewModels con soporte para notificación de cambios de propiedad.
/// </summary>
public abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? strNombrePropiedad = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(strNombrePropiedad));
    }

    protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? strNombrePropiedad = null)
    {
        if (EqualityComparer<T>.Default.Equals(storage, value))
        {
            return false;
        }

        storage = value;
        OnPropertyChanged(strNombrePropiedad);
        return true;
    }
}
