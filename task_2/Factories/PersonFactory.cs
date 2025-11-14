using patterns_lab2_2.Interfaces;
using patterns_lab2_2.Models.Persons;
using patterns_lab2_2.Models.Persons.Decorators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace patterns_lab2_2.Models.Factories
{
    public class PersonFactory
    {
        private readonly Dictionary<string, Person> _prototypes = new Dictionary<string, Person>();
        private readonly Random _random = new Random();

        private readonly string[] _heights = { "Short", "Average", "Tall" };
        private readonly string[] _clothings = { "Cloth", "Leather", "Mail", "Plate" };

        public PersonFactory()
        {
            var playerTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => !t.IsAbstract &&
                            typeof(Person).IsAssignableFrom(t) &&
                            !typeof(PersonDecorator).IsAssignableFrom(t)
                );

            foreach (var type in playerTypes)
            {
                var instance = (Person)Activator.CreateInstance(type);
                _prototypes[type.Name] = instance;
            }
        }

        public Person Create(string typeName, Point position)
        {
            if (!_prototypes.TryGetValue(typeName, out var prototype))
                throw new ArgumentException($"Unknown person type: {typeName}");

            var person = (Person)prototype.Clone();
            person.SetPosition(position);
            return person;
        }

        public Person CreateRandomPerson(Point position)
        {
            var key = _prototypes.Keys.ElementAt(_random.Next(_prototypes.Count));

            Person basePerson = Create(key, position);

            if (_random.Next(2) == 0)
            {
                string height = _heights[_random.Next(_heights.Length)];
                basePerson = new HeightDecorator(basePerson, height);
            }

            if (_random.Next(2) == 0)
            {
                string clothing = _clothings[_random.Next(_clothings.Length)];
                basePerson = new ClothingDecorator(basePerson, clothing);
            }

            basePerson.SetPosition(position);

            return basePerson;
        }

    }
}
