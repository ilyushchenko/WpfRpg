using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace UI.Extensions
{
    internal static class CBitmapExtensions
    {
        public static ImageSource ToImageSource(this Bitmap imageToConvert)
        {
            var bmp = new Bitmap(imageToConvert);
            var ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Png);

            var image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
    }
}