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
    public class Warrior: Person
    {
        public Warrior()
        {
            Type = "Warrior";
        }
        public override IWeapon Weapon => new Sword();
        public override IMovement Movement => new Sprinting();
        public override string GetFeatures()
        {
            return "Warrior";
        }
        public override void Move(Point destination)
        {
            SetPosition(destination);
        }
    }
}
