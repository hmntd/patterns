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
    public class Elf: Person
    {
        public Elf()
        {
            Type = "Elf";
            Health = 100;
        }
        public override IWeapon Weapon => new Bow();
        public override IMovement Movement => new Sprinting();
        public override string GetFeatures()
        {
            return "Elf";
        }
        public override void Move(Point destination)
        {
            SetPosition(destination);
        }
    }
}
