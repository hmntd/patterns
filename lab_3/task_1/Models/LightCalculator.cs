using patterns_lab_3_1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patterns_lab_3_1.Models
{
    public class LightCalculator: ICalculator
    {
        protected ICalculator _fullCalculator;

        public LightCalculator(ICalculator calc)
        {
            _fullCalculator = calc;     
        }

        public new double Add(double a, double b)
        {
            if (b == 0)
            {
                Console.WriteLine("LightCalculator: A + 0 = A");
                return a;
            }

            if (a == 0)
            {
                Console.WriteLine("LightCalculator: 0 + A = A");
                return b;
            }

            return _fullCalculator.Add(a, b);
        }

        public new double Subtract(double a, double b)
        {
            if (b == 0)
            {
                Console.WriteLine("LightCalculator: A - 0 = A");
                return a;
            }

            if (a == 0)
            {
                Console.WriteLine("LightCalculator: 0 - A = -A");
                return -b;
            }

            return _fullCalculator.Subtract(a, b);
        }

        public new double Multiply(double a, double b)
        {
            if (a == 0 || b == 0)
            {
                Console.WriteLine("LightCalculator: Множення на 0 = 0");
                return 0;
            }

            return _fullCalculator.Multiply(a, b);
        }

        public new double Divide(double a, double b)
        {
            if (b == 0)
            {
                Console.WriteLine("LightCalculator: Спроба ділення на нуль!");
                throw new DivideByZeroException("Ділення на нуль заборонено.");
            }

            if (a == 0)
            {
                Console.WriteLine("LightCalculator: 0 / B = 0");
                return 0;
            }

            return _fullCalculator.Divide(a, b);
        }
    }
}
