using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patterns_lab2_2.Models.Persons.Decorators
{
    public class HeightDecorator: PersonDecorator
    {
        private readonly string _height;

        public HeightDecorator(Person wrappedPerson, string height)
            : base(wrappedPerson)
        {
            _height = height;
        }

        public override string GetFeatures()
        {
            return base.GetFeatures() + $" (Height: {_height})";
        }
    }
}
