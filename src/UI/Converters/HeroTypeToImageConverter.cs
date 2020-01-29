using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Interfaces.Enums;

namespace UI.Converters
{
    public class CHeroTypeToImageConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            var heroType = (EHeroTypes)value;

            var image = new BitmapImage(new Uri($"../Images/{heroType.ToString()}.png", UriKind.Relative));

            return image;
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
