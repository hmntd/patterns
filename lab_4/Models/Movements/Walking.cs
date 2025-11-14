using patterns_lab2_2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patterns_lab2_2.Models.Movements
{
    public class Walking: IMovement
    {
        public int Distance => 7;
        public string Move() => "Walking";
    }
}
