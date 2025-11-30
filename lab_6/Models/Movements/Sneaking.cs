using patterns_lab2_2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patterns_lab2_2.Models.Movements
{
    public class Sneaking: IMovement
    {
        public int Distance => 5;
        public string Move() => "Sneaking";
    }
}
