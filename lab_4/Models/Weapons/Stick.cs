using patterns_lab2_2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patterns_lab2_2.Models.Weapons
{
    public class Stick: IWeapon
    {
        public string Name => "Stick";
        public int Damage => 1;
        public bool IsMelee => true;
        public int Range => 1;
        public string Attack() => "Beats with a stick";
    }
}
