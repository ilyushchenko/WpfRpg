using System.Drawing;
using System.Windows.Media;
using Core.Models.Terrains;
using Interfaces;
using UI.Extensions;
using UI.Resources;

namespace UI.Factories
{
    public class CTerrainTextureFactory
    {
        public static ImageSource GetTexture(ITerrain terrain)
        {
            //TODO: Fill factory
            Bitmap grassImage = Images.Grass;
            ImageSource image;
            if (terrain.GetType() == typeof(CDirt))
            {
                image = Images.Dirt.ToImageSource();
                return image;
            }
            else
            {
                image = grassImage.ToImageSource();
            }
            return image;
        }
    }
}