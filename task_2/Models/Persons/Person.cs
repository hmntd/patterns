using patterns_lab2_2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace patterns_lab2_2.Models
{
    public abstract class Person: ICloneable
    {
        public Point Position { get; private set; }
        public string Type { get; protected set; }
        public string Name { get; set; } = string.Empty;
        public abstract IWeapon Weapon { get; }
        public abstract IMovement Movement { get; }
        public abstract string GetFeatures();
        public void SetPosition(Point newPosition)
        {
            Position = newPosition;
        }
        public abstract void Move(Point destination);
        public object Clone()
        {
            return this.MemberwiseClone(); // MemberwiseClone - поверхностне (неглибоке) копіювання об'єкту
        }
    }
}
