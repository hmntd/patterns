using patterns_lab2_2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patterns_lab2_2.Models.Weapons
{
    public class Bow: IWeapon
    {
        public string Name => "Bow";
        public int Damage => 3;
        public bool IsMelee => false;
        public int Range => 5;
        public string Attack() => "Shoots a bow";
    }
}
