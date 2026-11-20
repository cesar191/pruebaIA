using System.Globalization;
using System.Windows;
using TodoListWpf.Converters;
using Xunit;

namespace TodoListWpf.Tests.ConvertersTests;

public class ConvertersTests
{
    /// <summary>
    /// Tarea: Valida que BoolToOpacityConverter retorne 0.55 cuando la tarea está completada.
    /// </summary>
    [Fact]
    public void BoolToOpacityConverter_Completada_RetornaOpacidadAtenuada()
    {
        // Arrange
        var conv = new BoolToOpacityConverter();

        // Act
        var objResultado = conv.Convert(true, typeof(double), null, CultureInfo.InvariantCulture);

        // Assert
        Assert.Equal(0.55, objResultado);
    }

    /// <summary>
    /// Tarea: Valida que BoolToOpacityConverter retorne 1.0 cuando la tarea está pendiente.
    /// </summary>
    [Fact]
    public void BoolToOpacityConverter_Pendiente_RetornaOpacidadCompleta()
    {
        // Arrange
        var conv = new BoolToOpacityConverter();

        // Act
        var objResultado = conv.Convert(false, typeof(double), null, CultureInfo.InvariantCulture);

        // Assert
        Assert.Equal(1.0, objResultado);
    }

    /// <summary>
    /// Tarea: Valida que BoolToStrikethroughConverter retorne Strikethrough si la tarea está completada.
    /// </summary>
    [Fact]
    public void BoolToStrikethroughConverter_Completada_RetornaStrikethrough()
    {
        // Arrange
        var conv = new BoolToStrikethroughConverter();

        // Act
        var objResultado = conv.Convert(true, typeof(TextDecorationCollection), null, CultureInfo.InvariantCulture);

        // Assert
        Assert.Equal(TextDecorations.Strikethrough, objResultado);
    }

    /// <summary>
    /// Tarea: Valida que BoolToStrikethroughConverter retorne null si la tarea está pendiente.
    /// </summary>
    [Fact]
    public void BoolToStrikethroughConverter_Pendiente_RetornaNull()
    {
        // Arrange
        var conv = new BoolToStrikethroughConverter();

        // Act
        var objResultado = conv.Convert(false, typeof(TextDecorationCollection), null, CultureInfo.InvariantCulture);

        // Assert
        Assert.Null(objResultado);
    }
}
