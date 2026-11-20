using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TodoListWpf.Models;

/// <summary>
/// Modelo observable que representa una tarea individual en la lista.
/// </summary>
public class TareaModel : INotifyPropertyChanged
{
    private int _intIdTarea;
    private string _strTituloTarea = string.Empty;
    private string _strDescripcionTarea = string.Empty;
    private bool _boolCompletada;
    private DateTime _dtmFechaCreacion = DateTime.Now;

    public event PropertyChangedEventHandler? PropertyChanged;

    public int IntIdTarea
    {
        get => _intIdTarea;
        set => SetProperty(ref _intIdTarea, value);
    }

    public string StrTituloTarea
    {
        get => _strTituloTarea;
        set => SetProperty(ref _strTituloTarea, value);
    }

    public string StrDescripcionTarea
    {
        get => _strDescripcionTarea;
        set => SetProperty(ref _strDescripcionTarea, value);
    }

    public bool BoolCompletada
    {
        get => _boolCompletada;
        set => SetProperty(ref _boolCompletada, value);
    }

    public DateTime DtmFechaCreacion
    {
        get => _dtmFechaCreacion;
        set => SetProperty(ref _dtmFechaCreacion, value);
    }

    /// <summary>
    /// Tarea: Inicializa una tarea vacía para serialización.
    /// </summary>
    public TareaModel()
    {
    }

    /// <summary>
    /// Tarea: Inicializa una nueva tarea con título, descripción y estado inicial.
    /// </summary>
    public TareaModel(int intId, string strTitulo, string strDescripcion = "", bool boolCompletada = false)
    {
        _intIdTarea = intId;
        _strTituloTarea = strTitulo;
        _strDescripcionTarea = strDescripcion;
        _boolCompletada = boolCompletada;
        _dtmFechaCreacion = DateTime.Now;
    }

    /// <summary>
    /// Tarea: Asigna un valor y notifica el cambio a la vista si difiere del actual.
    /// </summary>
    protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string? strNombrePropiedad = null)
    {
        if (!EqualityComparer<T>.Default.Equals(field, value))
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(strNombrePropiedad));
        }
    }
}
