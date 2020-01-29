using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace UI.Factories
{
    internal class CItemsTextureFactory
    {
        public static ImageSource GetImage(String fileName)
        {
            var uri = new Uri($"../Images/Items/{fileName}", UriKind.Relative);
            return new BitmapImage(uri);
        }
    }
}