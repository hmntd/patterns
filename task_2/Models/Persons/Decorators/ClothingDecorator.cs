using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patterns_lab2_2.Models.Persons.Decorators
{
    public class ClothingDecorator: PersonDecorator
    {
        private readonly string _clothingType;

        public ClothingDecorator(Person wrappedPerson, string clothingType)
            : base(wrappedPerson)
        {
            _clothingType = clothingType;
        }

        public override string GetFeatures()
        {
            return base.GetFeatures() + $" (Clothing: {_clothingType})";
        }
    }
}
