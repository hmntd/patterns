using patterns_lab2_2.Commands;
using patterns_lab2_2.Interfaces;
using patterns_lab2_2.Models.Persons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace patterns_lab2_2.ViewModels
{
    public class ClanViewModel: BaseViewModel
    {
        public Clan Model { get; }
        private readonly IBattleMediator _mediator;
        public double TerritoryX { get; }
        public double TerritoryY { get; }
        public double TerritoryWidth { get; }
        public double TerritoryHeight { get; }
        public PersonViewModel Leader { get; private set; }
        public ObservableCollection<PersonViewModel> Persons { get; } = new ObservableCollection<PersonViewModel>();
        public ObservableCollection<SquadViewModel> SquadVMs { get; } = new ObservableCollection<SquadViewModel>();
        private SquadViewModel _selectedSquad;
        public SquadViewModel SelectedSquad
        {
            get => _selectedSquad;
            set {
                _selectedSquad = value;
                
                OnPropertyChanged();
                UpdateSelection();
            }
        }
        public string Name => Model.Name;
        public Brush ClanBrush { get; }
        public ICommand SendCommandUp { get; }
        public ICommand SendCommandDown { get; }
        public ICommand SendCommandLeft { get; }
        public ICommand SendCommandRight { get; }
        public ICommand SendCommandFight { get; }

        public ClanViewModel(Clan model, double x, double y, double width, double height, IBattleMediator mediator)
        {
            Model = model;
            _mediator = mediator;
            ClanBrush = model.Color;

            TerritoryX = x;
            TerritoryY = y;
            TerritoryWidth = width;
            TerritoryHeight = height;

            UpdatePersonsAndLeader();

            for (int i = 0; i < Model.Squads.Count; i++)
            {
                SquadVMs.Add(new SquadViewModel(Model.Squads[i], i));
            }
            SelectedSquad = SquadVMs.FirstOrDefault();

            SendCommandUp = new RelayCommand(
                () => _mediator.SendCommand(Leader?.Model, SelectedSquad?.Model, new SquadCommand(CommandType.Up)),
                () => Leader != null && SelectedSquad != null
            );
            SendCommandDown = new RelayCommand(
                () => _mediator.SendCommand(Leader?.Model, SelectedSquad?.Model, new SquadCommand(CommandType.Down)),
                () => Leader != null && SelectedSquad != null
            );
            SendCommandLeft = new RelayCommand(
                () => _mediator.SendCommand(Leader?.Model, SelectedSquad?.Model, new SquadCommand(CommandType.Left)),
                () => Leader != null && SelectedSquad != null
            );
            SendCommandRight = new RelayCommand(
                () => _mediator.SendCommand(Leader?.Model, SelectedSquad?.Model, new SquadCommand(CommandType.Right)),
                () => Leader != null && SelectedSquad != null
            );
            SendCommandFight = new RelayCommand(
                () => _mediator.SendCommand(Leader?.Model, SelectedSquad?.Model, new SquadCommand(CommandType.Fight)),
                () => Leader != null && SelectedSquad != null
            );
        }

        private void UpdatePersonsAndLeader()
        {
            Persons.Clear();
            Leader = null;

            foreach (var person in Model.Squads.SelectMany(s => s.Persons))
            {
                var pvm = new PersonViewModel(person, Model.Color);
                if (Model.Leader == person)
                {
                    Leader = pvm;
                    pvm.IsLeader = true;
                }
                Persons.Add(pvm);
            }

            OnPropertyChanged(nameof(Leader));
            OnPropertyChanged(nameof(LeaderInfo));
        }

        private void UpdateSelection()
        {
            if (SelectedSquad == null) return;

            foreach (var pvm in Persons)
            {
                var basePersonModel = pvm.Model.GetBasePerson();

                bool isInSquad = SelectedSquad.Model.Persons.Any(p => p.GetBasePerson() == basePersonModel);

                pvm.IsSelected = isInSquad;
            }
        }

        public void NotifyLeaderChanged()
        {
            UpdatePersonsAndLeader();
        }

        public string LeaderInfo
        {
            get
            {
                if (Leader == null) return "No leader";
                return $"Leader: {Leader.Model.GetFeatures()} @ ({Leader.X:F0}, {Leader.Y:F0}) " +
                       $"| Weapon: {Leader.Model.Weapon.Name} " +
                       $"| Move Dist: {Leader.Model.Movement.Distance}";
            }
        }
    }
}
