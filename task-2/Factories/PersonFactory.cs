using patterns_lab2_2.Models.Persons;
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

        public PersonFactory()
        {
            var playerTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => !t.IsAbstract && typeof(Person).IsAssignableFrom(t));

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
            return Create(key, position);
        }
    }
}
