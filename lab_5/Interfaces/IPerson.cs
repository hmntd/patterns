using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace patterns_lab2_2.Interfaces
{
    public interface IPerson
    {
        string Name { get; }
        Point Position { get; set; }
        string GetInfo();
    }
}
