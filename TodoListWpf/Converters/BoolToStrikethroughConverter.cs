using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TodoListWpf.Converters;

/// <summary>
/// Convierte un valor booleano en decoración de texto tachado cuando la tarea está completada.
/// </summary>
public class BoolToStrikethroughConverter : IValueConverter
{
    /// <summary>
    /// Tarea: Retorna decoración Strikethrough si la tarea está completada, o null si está pendiente.
    /// </summary>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolCompletada && boolCompletada)
        {
            return TextDecorations.Strikethrough;
        }

        return null;
    }

    /// <summary>
    /// Tarea: Operación inversa no requerida para el enlace de decoración de texto.
    /// </summary>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
