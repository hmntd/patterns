using patterns_lab2_2.Commands;
using patterns_lab2_2.Interfaces;
using patterns_lab2_2.Models.Persons.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace patterns_lab2_2.Models.Persons.Decorators
{
    public class PersonDecorator: Person
    {
        protected Person _wrappedPerson;
        public PersonDecorator(Person wrappedPerson)
        {
            _wrappedPerson = wrappedPerson;
            _wrappedPerson.PositionChanged += OnWrappedPersonPositionChanged;
            _wrappedPerson.HealthChanged += OnWrappedPersonHealthChanged;
        }
        public override int Health
        {
            get => _wrappedPerson.Health;
            set => _wrappedPerson.Health = value;
        }

        public override Point Position
        {
            get => _wrappedPerson.Position; 
            set => _wrappedPerson.Position = value;
        }

        public override string Type { get => _wrappedPerson.Type; set => _wrappedPerson.Type = value; }
        
        public override IPersonState CurrentState { get => _wrappedPerson.CurrentState; }

        public override IWeapon Weapon => _wrappedPerson.Weapon;
        public override IMovement Movement => _wrappedPerson.Movement;

        private void OnWrappedPersonPositionChanged()
        {
            InvokePositionChanged();
        }

        private void OnWrappedPersonHealthChanged()
        {
            InvokeHealthChanged();
        }

        public override void SetMediator(IBattleMediator mediator)
        {
            base.SetMediator(mediator);
            _wrappedPerson.SetMediator(mediator);
        }

        public override void SetPosition(Point newPosition)
        {
            _wrappedPerson.SetPosition(newPosition);
        }

        public override void Move(Point destination)
        {
            _wrappedPerson.Move(destination);
        }

        public override string GetFeatures()
        {
            return _wrappedPerson.GetFeatures();
        }

        public override void HandleCommand(SquadCommand command)
        {
            _wrappedPerson.HandleCommand(command);
        }

        public override void TakeDamage(int damage)
        {
            _wrappedPerson.TakeDamage(damage);
        }

        public override void SetState(IPersonState newState)
        {
            _wrappedPerson.SetState(newState);
        }

        public override void Die()
        {
            _wrappedPerson.Die();
        }

        public override Person GetBasePerson()
        {
            return _wrappedPerson.GetBasePerson();
        }

        public override Person DeepCopy()
        {
            var clone = (PersonDecorator)this.MemberwiseClone();

            clone._wrappedPerson = _wrappedPerson.DeepCopy();

            clone._wrappedPerson.PositionChanged += clone.OnWrappedPersonPositionChanged;
            clone._wrappedPerson.HealthChanged += clone.OnWrappedPersonHealthChanged;

            clone.ClearEvents();

            return clone;
        }
    }
}
