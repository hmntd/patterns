using patterns_lab2_2.Interfaces;
using patterns_lab2_2.Models.Movements;
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
            Health = 200;
        }
        public override IWeapon Weapon => new Stick();
        public override IMovement Movement => new Stepping();
        public override string GetFeatures()
        {
            return "Ogre";
        }
        public override void Move(Point destination)
        {
            SetPosition(destination);
        }
    }
}
