using patterns_lab2_2.Models.Factories;
using patterns_lab2_2.Models.Persons;
using patterns_lab2_2.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace patterns_lab2_2.ViewModels
{
    public class MainViewModel: BaseViewModel
    {
        private const int MapWidth = 600;
        private const int MapHeight = 600;
        private PersonFactory _personFactory;
        private Random _random;
        private List<Clan> _clanModels = new List<Clan>();
        public ObservableCollection<ClanViewModel> Clans { get; } = new ObservableCollection<ClanViewModel>();
        public ObservableCollection<PersonViewModel> AllPersons{ get; } = new ObservableCollection<PersonViewModel>();
        public ICommand GenerateNewClanCommand { get; }
        public ICommand ResetBattleCommand { get; }

        public MainViewModel()
        {
            _personFactory = new PersonFactory();
            _random = new Random();

            GenerateNewClanCommand = new RelayCommand(GenerateNewClan);
            ResetBattleCommand = new RelayCommand(ResetBattle);
        }

        private void ResetBattle()
        {
            Clans.Clear();
            AllPersons.Clear();
            _clanModels.Clear();
        }
        private void GenerateNewClan()
        {
            Window window = new AddClan();

            string clanName = "New Clan";
            Brush clanColor = GetUniqueRandomBrush();
            Clan clan = new Clan(clanName, clanColor);
            
            var vm = new AddClanViewModel(clan);
            window.DataContext = vm;

            if (window.ShowDialog() == true)
            {
                if (_clanModels.Any(c => c.Color.Equals(clan.Color)))
                {
                    MessageBox.Show("This color is already in use. Please choose another.", "Color Error");
                    return;
                }

                _clanModels.Add(clan);
                UpdateTerritoriesAndPersons();
            }
        }

        private void UpdateTerritoriesAndPersons()
        {
            Clans.Clear();
            AllPersons.Clear();

            if (_clanModels.Count == 0) return;

            int totalClans = _clanModels.Count;

            int cols = (int)Math.Ceiling(Math.Sqrt(totalClans));

            int rows = (int)Math.Ceiling((double)totalClans / cols);

            double segmentWidth = (double)MapWidth / cols;
            double segmentHeight = (double)MapHeight / rows;

            for (int i = 0; i < totalClans; i++)
            {
                Clan currentClan = _clanModels[i];

                int colIndex = i % cols;
                int rowIndex = i / cols;

                double x_start = segmentWidth * colIndex;
                double x_end = segmentWidth * (colIndex + 1);
                double y_start = segmentHeight * rowIndex;
                double y_end = segmentHeight * (rowIndex + 1);

                bool isNewClan = currentClan.Squads.Count == 0;

                if (isNewClan)
                {
                    GenerateSquads(currentClan, x_start, x_end, y_start, y_end);
                }
                else
                {
                    MoveExistingPersons(currentClan, x_start, x_end, y_start, y_end);
                }

                double width = x_end - x_start;
                double height = y_end - y_start;
                var clanVM = new ClanViewModel(currentClan, x_start, y_start, width, height);
                Clans.Add(clanVM);
                foreach (var p in clanVM.Persons)
                    AllPersons.Add(p);
            }
        }

        private void MoveExistingPersons(Clan clan, double x_start, double x_end, double y_start, double y_end)
        {
            foreach (var squad in clan.Squads)
            {
                foreach (var person in squad.Persons)
                {
                    Point newPosition = GetRandomPosition(x_start, x_end, y_start, y_end);
                    person.SetPosition(newPosition);
                }
            }
        }

        private void GenerateSquads(Clan clan, double x_start, double x_end, double y_start, double y_end)
        {
            int numSquads = _random.Next(2, 5);
            for (int i = 0; i < numSquads; i++)
            {
                var squad = new Squad();
                int numPersons = _random.Next(2, 4);

                for (int j = 0; j < numPersons; j++)
                {
                    Point position = GetRandomPosition(x_start, x_end, y_start, y_end);
                    squad.Persons.Add(_personFactory.CreateRandomPerson(position));
                }
                clan.Squads.Add(squad);
            }
            clan.AppointRandomLeader();
        }

        private Point GetRandomPosition(double x_start, double x_end, double y_start, double y_end)
        {
            int xMin = (int)x_start;
            int xMax = (int)Math.Max(xMin + 1, (int)x_end - 15);

            int yMin = (int)y_start;
            int yMax = (int)Math.Max(yMin + 1, (int)y_end - 15);

            return new Point(_random.Next(xMin, xMax), _random.Next(yMin, yMax));
        }

        private Brush GetRandomBrush()
        {
            return new SolidColorBrush(Color.FromRgb(
                (byte)_random.Next(100, 256),
                (byte)_random.Next(100, 256),
                (byte)_random.Next(100, 256)));
        }

        private Brush GetUniqueRandomBrush()
        {
            Brush newBrush;
            int maxTries = 50;
            int tries = 0;

            do
            {
                newBrush = new SolidColorBrush(Color.FromRgb(
                    (byte)_random.Next(100, 256),
                    (byte)_random.Next(100, 256),
                    (byte)_random.Next(100, 256)));
                tries++;
            }
            while (tries < maxTries && _clanModels.Any(c => c.Color.Equals(newBrush)));

            return newBrush;
        }
    }
}
