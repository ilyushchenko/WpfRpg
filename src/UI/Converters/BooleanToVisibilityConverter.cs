using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace UI.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            var boolValue = (Boolean) value;
            boolValue = parameter != null ? !boolValue : boolValue;
            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}