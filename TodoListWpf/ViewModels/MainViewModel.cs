using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TodoListWpf.Commands;
using TodoListWpf.Models;
using TodoListWpf.Services;

namespace TodoListWpf.ViewModels;

/// <summary>
/// ViewModel principal para la gestión reactiva de tareas, filtros, temas y estado visual.
/// </summary>
public class MainViewModel : ViewModelBase
{
    private readonly IServicioPersistencia _srvPersistencia;
    private readonly string _strRutaDatos;

    private string _strNuevoTitulo = string.Empty;
    private string _strNuevaDescripcion = string.Empty;
    private string _strFiltroActual = "Todas";
    private string _strBusqueda = string.Empty;
    private bool _boolModoOscuro;
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
                OnPropertyChanged(nameof(BoolEsFiltroTodas));
                OnPropertyChanged(nameof(BoolEsFiltroPendientes));
                OnPropertyChanged(nameof(BoolEsFiltroCompletadas));
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

    public bool BoolModoOscuro
    {
        get => _boolModoOscuro;
        set => SetProperty(ref _boolModoOscuro, value);
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

    public bool BoolTieneTareasFiltradas => _colTareasFiltradas.Count > 0;
    public bool BoolListaVacia => _colTareasFiltradas.Count == 0;

    public bool BoolEsFiltroTodas => _strFiltroActual == "Todas";
    public bool BoolEsFiltroPendientes => _strFiltroActual == "Pendientes";
    public bool BoolEsFiltroCompletadas => _strFiltroActual == "Completadas";

    public ICommand CmdAgregarTarea { get; }
    public ICommand CmdEliminarTarea { get; }
    public ICommand CmdAlternarCompletada { get; }
    public ICommand CmdLimpiarCompletadas { get; }
    public ICommand CmdCambiarFiltro { get; }
    public ICommand CmdAlternarTema { get; }
    public ICommand CmdLimpiarBusqueda { get; }

    /// <summary>
    /// Tarea: Constructor por defecto que inicializa el servicio de persistencia JSON.
    /// </summary>
    public MainViewModel() : this(new ServicioPersistenciaJson())
    {
    }

    /// <summary>
    /// Tarea: Inicializa los comandos, rutas y carga los datos de inicio.
    /// </summary>
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
        CmdAlternarTema = new RelayCommand(_ => EjecutarAlternarTema());
        CmdLimpiarBusqueda = new RelayCommand(_ => StrBusqueda = string.Empty, _ => !string.IsNullOrEmpty(StrBusqueda));

        CargarDatosIniciales();
    }

    /// <summary>
    /// Tarea: Valida si la tarea contiene un título no vacío antes de permitir la inserción.
    /// </summary>
    private bool PuedeAgregarTarea()
    {
        return !string.IsNullOrWhiteSpace(_strNuevoTitulo);
    }

    /// <summary>
    /// Tarea: Agrega una nueva tarea a la colección, limpia los campos y guarda los cambios.
    /// </summary>
    public void EjecutarAgregarTarea()
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

        _colTareas.Insert(0, objNuevaTarea);

        StrNuevoTitulo = string.Empty;
        StrNuevaDescripcion = string.Empty;

        ActualizarContadores();
        ActualizarVistaFiltrada();
        GuardarEnDisco();
    }

    /// <summary>
    /// Tarea: Elimina la tarea especificada de la colección y persiste el cambio.
    /// </summary>
    public void EjecutarEliminarTarea(TareaModel? objTarea)
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

    /// <summary>
    /// Tarea: Alterna el estado de completado de una tarea.
    /// </summary>
    public void EjecutarAlternarCompletada(TareaModel? objTarea)
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

    /// <summary>
    /// Tarea: Remueve todas las tareas que se encuentren marcadas como completadas.
    /// </summary>
    public void EjecutarLimpiarCompletadas()
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

    /// <summary>
    /// Tarea: Conmuta el tema visual entre Claro y Oscuro aplicando el ResourceDictionary correspondiente.
    /// </summary>
    public void EjecutarAlternarTema()
    {
        BoolModoOscuro = !BoolModoOscuro;
        string strRutaTema = BoolModoOscuro ? "Themes/Theme.Dark.xaml" : "Themes/Theme.Light.xaml";

        try
        {
            if (Application.Current != null)
            {
                var objNuevoTema = new ResourceDictionary
                {
                    Source = new Uri(strRutaTema, UriKind.Relative)
                };

                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(objNuevoTema);
            }
        }
        catch
        {
            // Ignorar en entornos de pruebas unitarias sin UI thread
        }
    }

    /// <summary>
    /// Tarea: Notifica los cambios en las propiedades de conteo de tareas.
    /// </summary>
    private void ActualizarContadores()
    {
        OnPropertyChanged(nameof(IntTotalTareas));
        OnPropertyChanged(nameof(IntTareasPendientes));
        OnPropertyChanged(nameof(IntTareasCompletadas));
    }

    /// <summary>
    /// Tarea: Filtra la lista de tareas según el estado seleccionado y el término de búsqueda.
    /// </summary>
    public void ActualizarVistaFiltrada()
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
        OnPropertyChanged(nameof(BoolTieneTareasFiltradas));
        OnPropertyChanged(nameof(BoolListaVacia));
    }

    /// <summary>
    /// Tarea: Carga las tareas desde el servicio de persistencia o genera elementos de ejemplo iniciales.
    /// </summary>
    private void CargarDatosIniciales()
    {
        var colCargadas = _srvPersistencia.CargarTareas(_strRutaDatos);
        _colTareas.Clear();

        if (colCargadas.Count == 0)
        {
            _colTareas.Add(new TareaModel(1, "¡Bienvenido a tu Todo List!", "Organiza tu día con este gestor rápido, elegante y moderno.", false));
            _colTareas.Add(new TareaModel(2, "Explorar el Modo Oscuro", "Haz clic en el botón 🎨 Tema en la esquina superior derecha.", false));
            _colTareas.Add(new TareaModel(3, "Completar primera tarea", "Haz clic en el círculo de verificación a la izquierda para tacharla.", true));
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

    /// <summary>
    /// Tarea: Guarda la colección actual de tareas en el almacenamiento persistente.
    /// </summary>
    private void GuardarEnDisco()
    {
        _srvPersistencia.GuardarTareas(_strRutaDatos, _colTareas);
    }
}
