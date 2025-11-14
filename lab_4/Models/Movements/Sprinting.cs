using patterns_lab2_2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patterns_lab2_2.Models.Movements
{
    public class Sprinting: IMovement
    {
        public int Distance => 10;
        public string Move() => "Sprinting";
    }
}
