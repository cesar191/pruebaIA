using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using TodoListWpf.Commands;
using TodoListWpf.Models;
using TodoListWpf.Services;

namespace TodoListWpf.ViewModels;

/// <summary>
/// ViewModel principal para la gestión de la lista de tareas.
/// </summary>
public class MainViewModel : ViewModelBase
{
    private readonly IServicioPersistencia _srvPersistencia;
    private readonly string _strRutaDatos;

    private string _strNuevoTitulo = string.Empty;
    private string _strNuevaDescripcion = string.Empty;
    private string _strFiltroActual = "Todas";
    private string _strBusqueda = string.Empty;
    private TareaModel? _objTareaSeleccionada;

    private ObservableCollection<TareaModel> _colTareas = new();
    private ObservableCollection<TareaModel> _colTareasFiltradas = new();

    public string StrNuevoTitulo
    {
        get => _strNuevoTitulo;
        set
        {
            if (SetProperty(ref _strNuevoTitulo, value))
            {
                (CmdAgregarTarea as RelayCommand)?.CanExecute(null);
            }
        }
    }

    public string StrNuevaDescripcion
    {
        get => _strNuevaDescripcion;
        set => SetProperty(ref _strNuevaDescripcion, value);
    }

    public string StrFiltroActual
    {
        get => _strFiltroActual;
        set
        {
            if (SetProperty(ref _strFiltroActual, value))
            {
                ActualizarVistaFiltrada();
            }
        }
    }

    public string StrBusqueda
    {
        get => _strBusqueda;
        set
        {
            if (SetProperty(ref _strBusqueda, value))
            {
                ActualizarVistaFiltrada();
            }
        }
    }

    public TareaModel? ObjTareaSeleccionada
    {
        get => _objTareaSeleccionada;
        set => SetProperty(ref _objTareaSeleccionada, value);
    }

    public ObservableCollection<TareaModel> ColTareas
    {
        get => _colTareas;
        private set => SetProperty(ref _colTareas, value);
    }

    public ObservableCollection<TareaModel> ColTareasFiltradas
    {
        get => _colTareasFiltradas;
        private set => SetProperty(ref _colTareasFiltradas, value);
    }

    public int IntTotalTareas => _colTareas.Count;
    public int IntTareasPendientes => _colTareas.Count(t => !t.BoolCompletada);
    public int IntTareasCompletadas => _colTareas.Count(t => t.BoolCompletada);

    public ICommand CmdAgregarTarea { get; }
    public ICommand CmdEliminarTarea { get; }
    public ICommand CmdAlternarCompletada { get; }
    public ICommand CmdLimpiarCompletadas { get; }
    public ICommand CmdCambiarFiltro { get; }

    public MainViewModel() : this(new ServicioPersistenciaJson())
    {
    }

    public MainViewModel(IServicioPersistencia srvPersistencia)
    {
        _srvPersistencia = srvPersistencia;

        string strCarpetaLocal = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "TodoListWpf"
        );
        _strRutaDatos = Path.Combine(strCarpetaLocal, "tareas.json");

        CmdAgregarTarea = new RelayCommand(_ => EjecutarAgregarTarea(), _ => PuedeAgregarTarea());
        CmdEliminarTarea = new RelayCommand(objParam => EjecutarEliminarTarea(objParam as TareaModel));
        CmdAlternarCompletada = new RelayCommand(objParam => EjecutarAlternarCompletada(objParam as TareaModel));
        CmdLimpiarCompletadas = new RelayCommand(_ => EjecutarLimpiarCompletadas(), _ => IntTareasCompletadas > 0);
        CmdCambiarFiltro = new RelayCommand(objParam => StrFiltroActual = objParam?.ToString() ?? "Todas");

        CargarDatosIniciales();
    }

    private bool PuedeAgregarTarea()
    {
        return !string.IsNullOrWhiteSpace(_strNuevoTitulo);
    }

    private void EjecutarAgregarTarea()
    {
        if (string.IsNullOrWhiteSpace(_strNuevoTitulo))
        {
            return;
        }

        int intNuevoId = _colTareas.Any() ? _colTareas.Max(t => t.IntIdTarea) + 1 : 1;
        var objNuevaTarea = new TareaModel(intNuevoId, _strNuevoTitulo.Trim(), _strNuevaDescripcion.Trim());

        objNuevaTarea.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(TareaModel.BoolCompletada))
            {
                ActualizarContadores();
                ActualizarVistaFiltrada();
                GuardarEnDisco();
            }
        };

        _colTareas.Add(objNuevaTarea);

        StrNuevoTitulo = string.Empty;
        StrNuevaDescripcion = string.Empty;

        ActualizarContadores();
        ActualizarVistaFiltrada();
        GuardarEnDisco();
    }

    private void EjecutarEliminarTarea(TareaModel? objTarea)
    {
        if (objTarea == null)
        {
            return;
        }

        _colTareas.Remove(objTarea);
        ActualizarContadores();
        ActualizarVistaFiltrada();
        GuardarEnDisco();
    }

    private void EjecutarAlternarCompletada(TareaModel? objTarea)
    {
        if (objTarea == null)
        {
            return;
        }

        objTarea.BoolCompletada = !objTarea.BoolCompletada;
        ActualizarContadores();
        ActualizarVistaFiltrada();
        GuardarEnDisco();
    }

    private void EjecutarLimpiarCompletadas()
    {
        var colCompletadas = _colTareas.Where(t => t.BoolCompletada).ToList();
        foreach (var objTarea in colCompletadas)
        {
            _colTareas.Remove(objTarea);
        }

        ActualizarContadores();
        ActualizarVistaFiltrada();
        GuardarEnDisco();
    }

    private void ActualizarContadores()
    {
        OnPropertyChanged(nameof(IntTotalTareas));
        OnPropertyChanged(nameof(IntTareasPendientes));
        OnPropertyChanged(nameof(IntTareasCompletadas));
    }

    private void ActualizarVistaFiltrada()
    {
        var query = _colTareas.AsEnumerable();

        if (_strFiltroActual == "Pendientes")
        {
            query = query.Where(t => !t.BoolCompletada);
        }
        else if (_strFiltroActual == "Completadas")
        {
            query = query.Where(t => t.BoolCompletada);
        }

        if (!string.IsNullOrWhiteSpace(_strBusqueda))
        {
            string strTermino = _strBusqueda.Trim().ToLowerInvariant();
            query = query.Where(t =>
                t.StrTituloTarea.ToLowerInvariant().Contains(strTermino) ||
                t.StrDescripcionTarea.ToLowerInvariant().Contains(strTermino)
            );
        }

        ColTareasFiltradas = new ObservableCollection<TareaModel>(query.ToList());
    }

    private void CargarDatosIniciales()
    {
        var colCargadas = _srvPersistencia.CargarTareas(_strRutaDatos);
        _colTareas.Clear();

        if (colCargadas.Count == 0)
        {
            // Datos iniciales de demostración
            _colTareas.Add(new TareaModel(1, "¡Bienvenido a tu Todo List!", "Puedes agregar, completar, buscar y eliminar tareas.", false));
            _colTareas.Add(new TareaModel(2, "Revisar arquitectura MVVM", "Arquitectura desacoplada y fácil de extender.", true));
        }
        else
        {
            foreach (var objItem in colCargadas)
            {
                _colTareas.Add(objItem);
            }
        }

        foreach (var objTarea in _colTareas)
        {
            objTarea.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(TareaModel.BoolCompletada))
                {
                    ActualizarContadores();
                    ActualizarVistaFiltrada();
                    GuardarEnDisco();
                }
            };
        }

        ActualizarContadores();
        ActualizarVistaFiltrada();
    }

    private void GuardarEnDisco()
    {
        _srvPersistencia.GuardarTareas(_strRutaDatos, _colTareas);
    }
}
