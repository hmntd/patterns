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
    public class Ogre: Person
    {
        public Ogre()
        {
            Type = "Ogre";
        }
        public override IWeapon Weapon => new Stick();
        public override string MovementMethod => "Stepping";
        public override void Move(Point destination)
        {
            SetPosition(destination);
        }
    }
}
