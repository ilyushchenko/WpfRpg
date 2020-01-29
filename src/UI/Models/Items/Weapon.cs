using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Interfaces;

namespace UI.Models.Items
{
    public class Weapon : IWeapon
    {
        public Weapon(String name)
        {
            Name = name;
        }
        public String Name { get; }
        public Int32 Cost { get; set; }
        public Double Weight { get; }
        public String Description { get; set; }
        public Int32 Damage { get; }
        public ImageSource Image { get; set; }
    }
}
