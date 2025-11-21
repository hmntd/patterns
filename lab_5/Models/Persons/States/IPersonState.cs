using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patterns_lab2_2.Models.Persons.States
{
    public interface IPersonState
    {
        bool CanMoveForward();

        void TakeDamage(Person person);

        void Retreat(Person person);

        string Name { get; }
    }
}
