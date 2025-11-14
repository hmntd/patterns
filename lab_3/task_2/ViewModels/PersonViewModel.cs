using patterns_lab2_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace patterns_lab2_2.ViewModels
{
    public class PersonViewModel: BaseViewModel
    {
        private Person _model;
        private bool _isLeader;
        private Brush _color;
        public PersonViewModel(Person model, Brush color)
        {
            _model = model;
            _color = color;
        }
        public Person Model => _model;
        public double X => _model.Position.X;
        public double Y => _model.Position.Y;
        public Brush Color
        {
            get => _color;
            set { _color = value; OnPropertyChanged(); }
        }

        public bool IsLeader
        {
            get => _isLeader;
            set { _isLeader = value; OnPropertyChanged(); }
        }
        public string InfoText =>
            $"{_model.Type} (Pos: {X:F0}, {Y:F0}) - Weapon: {_model.Weapon.Name}";
    }
}
