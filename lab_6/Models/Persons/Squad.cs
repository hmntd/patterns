using patterns_lab2_2.Commands;
using patterns_lab2_2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patterns_lab2_2.Models.Persons
{
    public class Squad: ICommandHandler
    {
        public List<Person> Persons { get; private set; } = new List<Person>();

        public void HandleCommand(SquadCommand command)
        {
            var currentPersons = new List<Person>(Persons);

            foreach (var person in currentPersons)
            {
                person.HandleCommand(command);
            }
        }

        public Squad DeepCopy()
        {
            var clone = new Squad();
            foreach (var person in Persons)
            {
                clone.Persons.Add(person.DeepCopy());
            }
            return clone;
        }
    }
}
