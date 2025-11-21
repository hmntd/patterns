using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace patterns_lab2_2.Models.Persons.States
{
    public class WoundedState: IPersonState
    {
        public string Name => "Wounded";

        public bool CanMoveForward() => false;

        public void Retreat(Person person)
        {
            person.SetState(new NormalState());
        }

        public void TakeDamage(Person person)
        {
            person.SetState(new DeadState());
            person.Die();
        }
    }
}
