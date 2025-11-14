using patterns_lab2_2.Commands;
using patterns_lab2_2.Interfaces;
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
        public Point Position { get; private set; }
        public event Action PositionChanged;
        public string Type { get; protected set; }
        public string Name { get; set; } = string.Empty;
        public int Health { get; protected set; }
        public abstract IWeapon Weapon { get; }
        public abstract IMovement Movement { get; }
        public abstract string GetFeatures();
        protected IBattleMediator _mediator;
        private Random _random = new Random();

        public void SetMediator(IBattleMediator mediator)
        {
            _mediator = mediator;
        }

        public void SetPosition(Point newPosition)
        {
            Position = newPosition;
            InvokePositionChanged();
        }
        protected void InvokePositionChanged()
        {
            PositionChanged?.Invoke();
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
        protected virtual void MoveLeft()
        {
            SetPosition(new Point(Position.X - Movement.Distance, Position.Y));
        }
        protected virtual void MoveRight()
        {
            SetPosition(new Point(Position.X + Movement.Distance, Position.Y));
        }

        public virtual void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Die();
            }
        }

        protected virtual void Attack()
        {
            if (_mediator == null) return;

            Person enemy = _mediator.FindNearestEnemy(this);
            if (enemy == null) return;

            double distance = (this.Position - enemy.Position).Length;

            if (distance <= this.Weapon.Range)
            {
                enemy.TakeDamage(this.Weapon.Damage);
            }
        }

        protected virtual void Die()
        {
            _mediator?.PersonDied(this);
        }

        public void HandleCommand(SquadCommand command)
        {
            switch (command.Type)
            {
                case CommandType.Up:
                    MoveUp();
                    break;
                case CommandType.Down:
                    MoveDown();
                    break;
                case CommandType.Left:
                    MoveLeft();
                    break;
                case CommandType.Right:
                    MoveRight();
                    break;
                case CommandType.Fight:
                    Attack();
                    break;
            }
        }
    }
}
