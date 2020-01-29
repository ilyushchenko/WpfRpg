using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Core.Models.Heroes;
using Core.Models.Units;
using Interfaces;
using UI.Extensions;
using UI.Models.Heroes;
using UI.Resources;

namespace UI.Factories
{
    internal class CUnitTextureFactory
    {
        public static ImageSource GetTexture(IPositionable unit)
        {
            if (unit is CTrader)
            {
                return Images.Trader.ToImageSource();
            }

            //TODO: Optimize
            if (unit is CMage)
            {
                return new BitmapImage(new Uri("../Images/Mage.png", UriKind.Relative));
            }

            if (unit is CPaladin)
            {
                return new BitmapImage(new Uri("../Images/Paladin.png", UriKind.Relative));
            }

            if (unit is CThief)
            {
                return new BitmapImage(new Uri("../Images/Thief.png", UriKind.Relative));
            }

            if (unit is CWarrior)
            {
                return new BitmapImage(new Uri("../Images/Warrior.png", UriKind.Relative));
            }

            if (unit is CWall wall)
            {
                return wall.Orientation == EWallOrientation.Vertical ? Images.Wall_vertical.ToImageSource() : Images.Wall.ToImageSource();
            }

            if (unit is CMonster monster)
            {
                return new BitmapImage(new Uri("../Images/monster.png", UriKind.Relative));
            }

            return null;
        }
    }
}
