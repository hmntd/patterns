using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace patterns_lab2_2.Models.Persons
{
    public class Clan
    {
        public string Name { get; set; }
        public Brush Color { get; set; }
        public List<Squad> Squads { get; } = new List<Squad>();
        public Person Leader { get; private set; }
        public Clan(string name, Brush color)
        {
            Name = name;
            Color = color;
        }
        public void AppointRandomLeader()
        {
            var allPersons = Squads.SelectMany(s => s.Persons).ToList();

            if (allPersons.Count == 0)
            {
                Leader = null;
                return;
            }

            Random random = new Random();
            int index = random.Next(allPersons.Count);
            Leader = allPersons[index];
        }

        public void RemovePerson(Person personToRemove)
        {
            var baseToRemove = personToRemove.GetBasePerson();

            foreach (var squad in Squads)
            {
                var found = squad.Persons.FirstOrDefault(p => p.GetBasePerson() == baseToRemove);

                if (found != null)
                {
                    squad.Persons.Remove(found);
                    break;
                }
            }

            // Якщо лідер помер
            if (Leader != null && Leader.GetBasePerson() == baseToRemove)
            {
                AppointRandomLeader();
            }
        }
    }
}
