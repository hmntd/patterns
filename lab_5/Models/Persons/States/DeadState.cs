using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patterns_lab2_2.Models.Persons.States
{
    public class DeadState: IPersonState
    {
        public string Name => "Dead";

        public bool CanMoveForward() => false;
        public void Retreat(Person person) { }
        public void TakeDamage(Person person) { }
    }
}
