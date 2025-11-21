using patterns_lab2_2.Commands;
using patterns_lab2_2.Interfaces;
using patterns_lab2_2.Models.Persons.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace patterns_lab2_2.Models
{
    public abstract class Person: ICloneable, ICommandHandler
    {
        public virtual Point Position { get; set; }
        public event Action PositionChanged;
        public event Action HealthChanged;
        public virtual string Type { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual int Health { get; set; }
        public abstract IWeapon Weapon { get; }
        public abstract IMovement Movement { get; }
        public abstract string GetFeatures();

        protected IBattleMediator _mediator;
        public virtual IPersonState CurrentState { get; private set; } = new NormalState();

        public virtual void SetMediator(IBattleMediator mediator)
        {
            _mediator = mediator;
        }

        public virtual void SetPosition(Point newPosition)
        {
            double clampedX = Math.Max(0, Math.Min(600 - 10, newPosition.X));
            double clampedY = Math.Max(0, Math.Min(600 - 10, newPosition.Y));

            Position = new Point(clampedX, clampedY);
            InvokePositionChanged();
        }

        public virtual void SetState(IPersonState newState)
        {
            CurrentState = newState;
            HealthChanged?.Invoke();
        }

        protected void InvokePositionChanged()
        {
            PositionChanged?.Invoke();
        }

        protected void InvokeHealthChanged()
        {
            HealthChanged?.Invoke();
        }

        public abstract void Move(Point destination);
        
        public object Clone()
        {
            return this.MemberwiseClone(); // MemberwiseClone - поверхностне (неглибоке) копіювання об'єкту
        }

        protected virtual void MoveUp()
        {
            SetPosition(new Point(Position.X, Position.Y - Movement.Distance));
        }
        
        protected virtual void MoveDown()
        {
            SetPosition(new Point(Position.X, Position.Y + Movement.Distance));
        }

        protected virtual void MoveBack()
        {
            CurrentState.Retreat(this);
            if (_mediator == null) return;

            Point leaderPos = _mediator.GetLeaderPosition(this);

            if (leaderPos == this.Position) return;

            Vector direction = leaderPos - this.Position;

            if (direction.Length > 0)
            {
                if (direction.Length <= Movement.Distance)
                {
                    SetPosition(leaderPos);
                }
                else
                {
                    direction.Normalize();
                    Vector moveStep = direction * Movement.Distance;
                    SetPosition(this.Position + moveStep);
                }
            }
        }

        protected virtual void MoveForward()
        {
            if (!CurrentState.CanMoveForward()) return;
            if (_mediator == null) return;

            Person enemy = _mediator.FindNearestEnemy(this);

            if (enemy == null) return;

            Vector direction = enemy.Position - this.Position;

            direction.Normalize();
            Vector moveStep = direction * Movement.Distance;
            Point newPos = this.Position + moveStep;

            SetPosition(newPos);
        }

        public virtual void TakeDamage(int damage)
        {
            CurrentState.TakeDamage(this);

            HealthChanged?.Invoke();
        }

        protected virtual void Attack()
        {
            if (CurrentState is DeadState) return;

            if (_mediator == null) return;
            Person enemy = _mediator.FindNearestEnemy(this);
            if (enemy == null) return;

            double distance = (this.Position - enemy.Position).Length;
            if (distance <= this.Weapon.Range)
            {
                enemy.TakeDamage(this.Weapon.Damage);
            }
        }

        public virtual void Die()
        {
            _mediator?.PersonDied(this);
        }

        public virtual void HandleCommand(SquadCommand command)
        {
            if (CurrentState is DeadState) return;

            switch (command.Type)
            {
                case CommandType.Up:
                    MoveUp();
                    break;
                case CommandType.Down:
                    MoveDown();
                    break;
                case CommandType.Left:
                    MoveBack();
                    break;
                case CommandType.Right:
                    MoveForward();
                    break;
                case CommandType.Fight:
                    Attack();
                    break;
            }
        }

        public virtual Person GetBasePerson()
        {
            return this;
        }
    }
}
