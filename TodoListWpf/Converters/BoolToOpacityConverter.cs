using System;
using System.Globalization;
using System.Windows.Data;

namespace TodoListWpf.Converters;

/// <summary>
/// Convierte el estado completado de una tarea a opacidad atenuada para la vista.
/// </summary>
public class BoolToOpacityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolCompletada && boolCompletada)
        {
            return 0.55;
        }

        return 1.0;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
