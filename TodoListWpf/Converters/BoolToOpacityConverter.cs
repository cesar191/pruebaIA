using System;
using System.Globalization;
using System.Windows.Data;

namespace TodoListWpf.Converters;

/// <summary>
/// Convierte el estado completado de una tarea a opacidad atenuada para la vista.
/// </summary>
public class BoolToOpacityConverter : IValueConverter
{
    /// <summary>
    /// Tarea: Convierte el estado booleano de completado en un nivel de opacidad (0.55 o 1.0).
    /// </summary>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolCompletada && boolCompletada)
        {
            return 0.55;
        }

        return 1.0;
    }

    /// <summary>
    /// Tarea: Operación inversa no requerida para el enlace unidireccional de opacidad.
    /// </summary>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
