using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace AvaloniaMusicConsole.Controls.Converters
{
    public class DimentionConverter
        : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value;
            //return (object?)( value is decimal nv && nv > 20 ? nv - 20 : value);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
