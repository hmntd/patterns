using patterns_lab2_2.Models.Persons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace patterns_lab2_2.ViewModels
{
    public class ClanViewModel: BaseViewModel
    {
        private Clan _model;
        public double TerritoryX { get; }
        public double TerritoryY { get; }
        public double TerritoryWidth { get; }
        public double TerritoryHeight { get; }
        public PersonViewModel Leader { get; private set; }
        public ObservableCollection<PersonViewModel> Persons { get; } = new ObservableCollection<PersonViewModel>();
        public string Name => _model.Name;
        public Brush ClanBrush { get; }

        public ClanViewModel(Clan model, double x, double y, double width, double height)
        {
            _model = model;
            ClanBrush = model.Color;
            TerritoryX = x;
            TerritoryY = y;
            TerritoryWidth = width;
            TerritoryHeight = height;

            foreach (var player in _model.Squads.SelectMany(s => s.Persons))
            {
                var pvm = new PersonViewModel(player, model.Color);
                if (_model.Leader == player)
                {
                    Leader = pvm;
                    pvm.IsLeader = true;
                }
                Persons.Add(pvm);
            }
        }

        public string LeaderInfo =>
            $"Leader: {Leader.Model.Type} @ ({Leader.X:F0}, {Leader.Y:F0}) " +
            $"| Weapon: {Leader.Model.Weapon.Name} " +
            $"| Moves by: {Leader.Model.MovementMethod}";
    }
}
