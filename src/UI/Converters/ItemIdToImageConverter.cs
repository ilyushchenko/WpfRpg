using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Interfaces.Enums;

namespace UI.Converters
{
    public class ItemIdToImageConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            var itemId = (Guid)value;
            try
            {
                var image = new BitmapImage(new Uri($"../Images/Items/{itemId}.png", UriKind.Relative));
                return image;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
