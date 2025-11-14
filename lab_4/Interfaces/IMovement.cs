using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patterns_lab2_2.Interfaces
{
    public interface IMovement
    {
        int Distance { get; }
        string Move();
    }
}
