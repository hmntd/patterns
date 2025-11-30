using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patterns_lab2_2.Models.Persons.States
{
    public class NormalState: IPersonState
    {
        public string Name => "Normal";

        public bool CanMoveForward() => true;

        public void Retreat(Person person)
        {
        }

        public void TakeDamage(Person person)
        {
            person.SetState(new WoundedState());
        }
    }
}
