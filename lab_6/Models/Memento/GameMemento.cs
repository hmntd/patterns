using patterns_lab2_2.Models.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patterns_lab2_2.Models.Memento
{
    public class GameMemento
    {
        public List<Clan> ClansState { get; }

        public GameMemento(List<Clan> clans)
        {
            ClansState = new List<Clan>();
            foreach (var clan in clans)
            {
                ClansState.Add(clan.DeepCopy());
            }
        }
    }
}
