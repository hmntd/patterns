using patterns_lab_3_1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patterns_lab_3_1.Models
{
    public class FullCalculator: ICalculator
    {
        public double Add(double a, double b)
        {
            Console.WriteLine("FullCalculator: Виконую складне додавання...");
            return a + b;
        }

        public double Subtract(double a, double b)
        {
            Console.WriteLine("FullCalculator: Виконую складне віднімання...");
            return a - b;
        }

        public double Multiply(double a, double b)
        {
            Console.WriteLine("FullCalculator: Виконую складне множення...");
            return a * b;
        }

        public double Divide(double a, double b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("FullCalculator: Ділення на нуль.");
            }

            Console.WriteLine("FullCalculator: Виконую складне ділення...");
            return a / b;
        }
    }
}
