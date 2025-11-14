using patterns_lab2_2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace patterns_lab2_2.Models.Persons.Decorators
{
    public class PersonDecorator: Person
    {
        protected Person _wrappedPerson;
        public PersonDecorator(Person wrappedPerson)
        {
            _wrappedPerson = wrappedPerson;
        }

        public override IWeapon Weapon => _wrappedPerson.Weapon;
        public override IMovement Movement=> _wrappedPerson.Movement;

        public new void SetPosition(Point newPosition)
        {
            _wrappedPerson.SetPosition(newPosition);
        }

        public override void Move(Point destination)
        {
            _wrappedPerson.Move(destination);
        }

        public override string GetFeatures()
        {
            return _wrappedPerson.GetFeatures();
        }
    }
}
