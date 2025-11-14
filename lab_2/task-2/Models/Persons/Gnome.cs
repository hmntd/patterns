using patterns_lab2_2.Interfaces;
using patterns_lab2_2.Models.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace patterns_lab2_2.Models.Persons
{
    public class Gnome: Person
    {
        public Gnome()
        {
            Type = "Gnome";
        }
        public override IWeapon Weapon => new Axe();
        public override string MovementMethod => "Sneaking";
        public override void Move(Point destination)
        {
            SetPosition(destination);
        }
    }
}
