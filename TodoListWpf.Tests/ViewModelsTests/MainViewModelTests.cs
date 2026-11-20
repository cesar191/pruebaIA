using System.Collections.Generic;
using System.Linq;
using TodoListWpf.Models;
using TodoListWpf.Services;
using TodoListWpf.ViewModels;
using Xunit;

namespace TodoListWpf.Tests.ViewModelsTests;

/// <summary>
/// Implementación en memoria del servicio de persistencia para pruebas aisladas.
/// </summary>
public class FakePersistenciaMemoria : IServicioPersistencia
{
    public List<TareaModel> ColDatos { get; set; } = new();

    /// <summary>
    /// Tarea: Retorna una copia de la lista en memoria simulando lectura de disco.
    /// </summary>
    public List<TareaModel> CargarTareas(string strRutaArchivo)
    {
        return ColDatos.ToList();
    }

    /// <summary>
    /// Tarea: Actualiza los datos en memoria simulando guardado en disco.
    /// </summary>
    public void GuardarTareas(string strRutaArchivo, IEnumerable<TareaModel> colTareasAGuardar)
    {
        ColDatos = colTareasAGuardar.ToList();
    }
}

public class MainViewModelTests
{
    /// <summary>
    /// Tarea: Valida que al agregar una tarea con título válido, se incremente la colección y los contadores.
    /// </summary>
    [Fact]
    public void EjecutarAgregarTarea_ConTituloValido_IncrementaTotalTareas()
    {
        // Arrange
        var srvFake = new FakePersistenciaMemoria();
        var objViewModel = new MainViewModel(srvFake);
        int intTotalInicial = objViewModel.IntTotalTareas;
        objViewModel.StrNuevoTitulo = "Prueba de tarea";
        objViewModel.StrNuevaDescripcion = "Descripción de prueba";

        // Act
        objViewModel.CmdAgregarTarea.Execute(null);

        // Assert
        Assert.Equal(intTotalInicial + 1, objViewModel.IntTotalTareas);
        Assert.Contains(objViewModel.ColTareas, t => t.StrTituloTarea == "Prueba de tarea");
        Assert.True(string.IsNullOrEmpty(objViewModel.StrNuevoTitulo));
    }

    /// <summary>
    /// Tarea: Valida que no se permita agregar tareas si el título está vacío o solo contiene espacios.
    /// </summary>
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void CmdAgregarTarea_ConTituloInvalido_NoPermiteEjecucion(string strTituloInvalido)
    {
        // Arrange
        var srvFake = new FakePersistenciaMemoria();
        var objViewModel = new MainViewModel(srvFake);
        objViewModel.StrNuevoTitulo = strTituloInvalido;

        // Act
        bool boolPuedeEjecutar = objViewModel.CmdAgregarTarea.CanExecute(null);

        // Assert
        Assert.False(boolPuedeEjecutar);
    }

    /// <summary>
    /// Tarea: Valida que al eliminar una tarea existente, se remueva correctamente de la colección.
    /// </summary>
    [Fact]
    public void EjecutarEliminarTarea_TareaExistente_RemueveDeColeccion()
    {
        // Arrange
        var srvFake = new FakePersistenciaMemoria();
        var objViewModel = new MainViewModel(srvFake);
        var objTareaAEliminar = objViewModel.ColTareas.First();
        int intTotalInicial = objViewModel.IntTotalTareas;

        // Act
        objViewModel.CmdEliminarTarea.Execute(objTareaAEliminar);

        // Assert
        Assert.Equal(intTotalInicial - 1, objViewModel.IntTotalTareas);
        Assert.DoesNotContain(objViewModel.ColTareas, t => t.IntIdTarea == objTareaAEliminar.IntIdTarea);
    }

    /// <summary>
    /// Tarea: Valida que alternar el estado completado actualice los contadores correspondientes.
    /// </summary>
    [Fact]
    public void EjecutarAlternarCompletada_CambiaEstadoYActualizaContadores()
    {
        // Arrange
        var srvFake = new FakePersistenciaMemoria();
        var objViewModel = new MainViewModel(srvFake);
        var objTarea = objViewModel.ColTareas.First(t => !t.BoolCompletada);
        int intPendientesIniciales = objViewModel.IntTareasPendientes;
        int intCompletadasIniciales = objViewModel.IntTareasCompletadas;

        // Act
        objViewModel.CmdAlternarCompletada.Execute(objTarea);

        // Assert
        Assert.True(objTarea.BoolCompletada);
        Assert.Equal(intPendientesIniciales - 1, objViewModel.IntTareasPendientes);
        Assert.Equal(intCompletadasIniciales + 1, objViewModel.IntTareasCompletadas);
    }

    /// <summary>
    /// Tarea: Valida que el filtro por estado muestre únicamente las tareas correspondientes.
    /// </summary>
    [Fact]
    public void StrFiltroActual_FiltrarPorPendientes_MuestraSoloPendientes()
    {
        // Arrange
        var srvFake = new FakePersistenciaMemoria();
        var objViewModel = new MainViewModel(srvFake);

        // Act
        objViewModel.StrFiltroActual = "Pendientes";

        // Assert
        Assert.All(objViewModel.ColTareasFiltradas, t => Assert.False(t.BoolCompletada));
    }

    /// <summary>
    /// Tarea: Valida que la búsqueda por texto filtre por título o descripción.
    /// </summary>
    [Fact]
    public void StrBusqueda_FiltraPorCoincidenciaEnTitulo()
    {
        // Arrange
        var srvFake = new FakePersistenciaMemoria();
        var objViewModel = new MainViewModel(srvFake);
        objViewModel.StrNuevoTitulo = "Comprar manzanas";
        objViewModel.CmdAgregarTarea.Execute(null);

        // Act
        objViewModel.StrBusqueda = "manzanas";

        // Assert
        Assert.Single(objViewModel.ColTareasFiltradas);
        Assert.Equal("Comprar manzanas", objViewModel.ColTareasFiltradas.First().StrTituloTarea);
    }

    /// <summary>
    /// Tarea: Valida que limpiar completadas remueva todas las tareas finalizadas.
    /// </summary>
    [Fact]
    public void EjecutarLimpiarCompletadas_RemueveTodasLasCompletadas()
    {
        // Arrange
        var srvFake = new FakePersistenciaMemoria();
        var objViewModel = new MainViewModel(srvFake);

        // Asegurar que hay al menos una completada
        if (!objViewModel.ColTareas.Any(t => t.BoolCompletada))
        {
            objViewModel.ColTareas.First().BoolCompletada = true;
        }

        // Act
        objViewModel.CmdLimpiarCompletadas.Execute(null);

        // Assert
        Assert.Equal(0, objViewModel.IntTareasCompletadas);
        Assert.True(objViewModel.ColTareas.All(t => !t.BoolCompletada));
    }

    /// <summary>
    /// Tarea: Valida que alternar tema conmute el estado booleano de modo oscuro.
    /// </summary>
    [Fact]
    public void EjecutarAlternarTema_ConmutaEstadoModoOscuro()
    {
        // Arrange
        var srvFake = new FakePersistenciaMemoria();
        var objViewModel = new MainViewModel(srvFake);
        bool boolEstadoInicial = objViewModel.BoolModoOscuro;

        // Act
        objViewModel.EjecutarAlternarTema();

        // Assert
        Assert.NotEqual(boolEstadoInicial, objViewModel.BoolModoOscuro);
    }
}
