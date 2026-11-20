using TodoListWpf.Models;
using Xunit;

namespace TodoListWpf.Tests.ModelsTests;

public class TareaModelTests
{
    /// <summary>
    /// Tarea: Valida que el cambio de propiedad notifique el evento PropertyChanged.
    /// </summary>
    [Fact]
    public void TareaModel_CambioDeTitulo_DisparaPropertyChanged()
    {
        // Arrange
        var objTarea = new TareaModel(1, "Original", "Notas", false);
        string? strPropiedadNotificada = null;
        objTarea.PropertyChanged += (s, e) => strPropiedadNotificada = e.PropertyName;

        // Act
        objTarea.StrTituloTarea = "Modificado";

        // Assert
        Assert.Equal(nameof(TareaModel.StrTituloTarea), strPropiedadNotificada);
        Assert.Equal("Modificado", objTarea.StrTituloTarea);
    }

    /// <summary>
    /// Tarea: Valida que el cambio de estado completado dispare PropertyChanged.
    /// </summary>
    [Fact]
    public void TareaModel_CambioDeEstadoCompletada_DisparaPropertyChanged()
    {
        // Arrange
        var objTarea = new TareaModel(1, "Original", "Notas", false);
        string? strPropiedadNotificada = null;
        objTarea.PropertyChanged += (s, e) => strPropiedadNotificada = e.PropertyName;

        // Act
        objTarea.BoolCompletada = true;

        // Assert
        Assert.Equal(nameof(TareaModel.BoolCompletada), strPropiedadNotificada);
        Assert.True(objTarea.BoolCompletada);
    }
}
