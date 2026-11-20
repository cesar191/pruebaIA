using System;
using System.Collections.Generic;
using System.IO;
using TodoListWpf.Models;
using TodoListWpf.Services;
using Xunit;

namespace TodoListWpf.Tests.ServicesTests;

public class ServicioPersistenciaJsonTests : IDisposable
{
    private readonly string _strRutaTemporal;

    /// <summary>
    /// Tarea: Prepara un archivo temporal único para cada prueba de persistencia.
    /// </summary>
    public ServicioPersistenciaJsonTests()
    {
        _strRutaTemporal = Path.Combine(Path.GetTempPath(), $"test_tareas_{Guid.NewGuid():N}.json");
    }

    /// <summary>
    /// Tarea: Limpia el archivo temporal creado tras la ejecución de las pruebas.
    /// </summary>
    public void Dispose()
    {
        if (File.Exists(_strRutaTemporal))
        {
            try
            {
                File.Delete(_strRutaTemporal);
            }
            catch
            {
                // Silencioso en caso de bloqueo
            }
        }
    }

    /// <summary>
    /// Tarea: Valida que guardar y cargar tareas en JSON preserve la información íntegra.
    /// </summary>
    [Fact]
    public void GuardarYCargar_TareasValidas_PersisteInformacionCorrectamente()
    {
        // Arrange
        var srvPersistencia = new ServicioPersistenciaJson();
        var colTareasAGuardar = new List<TareaModel>
        {
            new(1, "Tarea 1", "Detalle 1", false),
            new(2, "Tarea 2", "Detalle 2", true)
        };

        // Act
        srvPersistencia.GuardarTareas(_strRutaTemporal, colTareasAGuardar);
        var colTareasRecuperadas = srvPersistencia.CargarTareas(_strRutaTemporal);

        // Assert
        Assert.Equal(2, colTareasRecuperadas.Count);
        Assert.Equal("Tarea 1", colTareasRecuperadas[0].StrTituloTarea);
        Assert.False(colTareasRecuperadas[0].BoolCompletada);
        Assert.Equal("Tarea 2", colTareasRecuperadas[1].StrTituloTarea);
        Assert.True(colTareasRecuperadas[1].BoolCompletada);
    }

    /// <summary>
    /// Tarea: Valida que si el archivo no existe, retorne una lista vacía sin arrojar excepciones.
    /// </summary>
    [Fact]
    public void CargarTareas_ArchivoInexistente_RetornaColeccionVacia()
    {
        // Arrange
        var srvPersistencia = new ServicioPersistenciaJson();
        string strRutaInexistente = Path.Combine(Path.GetTempPath(), $"no_existe_{Guid.NewGuid():N}.json");

        // Act
        var colResultado = srvPersistencia.CargarTareas(strRutaInexistente);

        // Assert
        Assert.NotNull(colResultado);
        Assert.Empty(colResultado);
    }
}
