using patterns_lab2_2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patterns_lab2_2.Models.Weapons
{
    public class Axe: IWeapon
    {
        public string Name => "Axe";
        public int Damage => 7;
        public bool IsMelee => true;
        public int Range => 1;
        public string Attack() => "Chops with an axe";
    }
}
