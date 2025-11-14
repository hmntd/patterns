using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using patterns_lab2_2.Interfaces;

namespace patterns_lab2_2.Models.Weapons
{
    public class Sword: IWeapon
    {
        public string Name => "Sword";
        public int Damage => 5;
        public bool IsMelee => true;
        public int Range => 2;
    }
}
