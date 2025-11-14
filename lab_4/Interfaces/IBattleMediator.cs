using patterns_lab2_2.Commands;
using patterns_lab2_2.Models.Persons;
using patterns_lab2_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patterns_lab2_2.Interfaces
{
    public interface IBattleMediator
    {
        void SendCommand(Person leader, Squad targetSquad, SquadCommand command);
        void RegisterPerson(Person person, Clan clan);
        void PersonDied(Person person);
        void UnregisterPerson(Person person);
        Person FindNearestEnemy(Person attacker);
    }
}
