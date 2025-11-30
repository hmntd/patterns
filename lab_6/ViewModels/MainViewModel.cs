using patterns_lab2_2.Commands;
using patterns_lab2_2.Interfaces;
using patterns_lab2_2.Models;
using patterns_lab2_2.Models.Bridge;
using patterns_lab2_2.Models.Factories;
using patterns_lab2_2.Models.Memento;
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
using System.Windows.Media.Media3D;

namespace patterns_lab2_2.ViewModels
{
    public class MainViewModel : BaseViewModel, IBattleMediator
    {
        private const int MapWidth = 600;
        private const int MapHeight = 600;
        private PersonFactory _personFactory;
        private Random _random;
        private List<Clan> _clanModels = new List<Clan>();
        private readonly Dictionary<Person, Clan> _personClanMap = new Dictionary<Person, Clan>();
        public ObservableCollection<ClanViewModel> Clans { get; } = new ObservableCollection<ClanViewModel>();
        public ObservableCollection<PersonViewModel> AllPersons { get; } = new ObservableCollection<PersonViewModel>();
        private readonly Stack<GameMemento> _history = new Stack<GameMemento>();
        public ICommand GenerateNewClanCommand { get; }
        public ICommand ResetBattleCommand { get; }
        public ICommand GenerateTextReportCommand { get; }
        public ICommand GeneratePseudoGraphicsReportCommand { get; }
        public ICommand SaveCheckpointCommand { get; }
        public ICommand LoadCheckpointCommand { get; }

        private string _generatedReport;
        public string GeneratedReport
        {
            get => _generatedReport;
            set { _generatedReport = value; OnPropertyChanged(); }
        }

        public MainViewModel()
        {
            _personFactory = new PersonFactory();
            _random = new Random();

            GenerateNewClanCommand = new RelayCommand(GenerateNewClan);
            ResetBattleCommand = new RelayCommand(ResetBattle);
            GenerateTextReportCommand = new RelayCommand(ShowTextReport);
            GeneratePseudoGraphicsReportCommand = new RelayCommand(ShowPseudoGraphicsReport);

            SaveCheckpointCommand = new RelayCommand(SaveCheckpoint);
            LoadCheckpointCommand = new RelayCommand(LoadCheckpoint, () => _history.Count > 0);
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

            if (_clanModels.Count == 0)
                return;

            int total = _clanModels.Count;

            int cols = (int)Math.Ceiling(Math.Sqrt(total));
            int rows = (int)Math.Ceiling((double)total / cols);

            double segmentWidth = MapWidth / cols;
            double segmentHeight = MapHeight / rows;

            for (int i = 0; i < total; i++)
            {
                Clan clan = _clanModels[i];

                int col = i % cols;
                int row = i / cols;

                double xStart = segmentWidth * col;
                double yStart = segmentHeight * row;

                double width = segmentWidth;
                double height = segmentHeight;

                if (width < 20) width = 20;
                if (height < 20) height = 20;

                if (clan.Squads.Count == 0)
                {
                    GenerateSquads(clan, xStart, yStart, width, height);
                }
                else
                {
                    MoveExistingPersons(clan, xStart, yStart, width, height);
                }

                var vm = new ClanViewModel(clan, xStart, yStart, width, height, this);
                Clans.Add(vm);

                foreach (var person in vm.Persons)
                    AllPersons.Add(person);
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

        private void GenerateSquads(Clan clan, double x, double y, double w, double h)
        {
            int numSquads = _random.Next(2, 5);

            for (int i = 0; i < numSquads; i++)
            {
                var squad = new Squad();
                int numPersons = _random.Next(2, 4);

                for (int j = 0; j < numPersons; j++)
                {
                    Point p = GetRandomPosition(x, y, w, h);
                    squad.Persons.Add(_personFactory.CreateRandomPerson(p, this, clan));
                }

                clan.Squads.Add(squad);
            }

            clan.AppointRandomLeader();
        }

        private Point GetRandomPosition(double x, double y, double width, double height)
        {
            double xMin = x + 5;
            double xMax = x + width - 5;

            double yMin = y + 5;
            double yMax = y + height - 5;

            if (xMax <= xMin) xMax = xMin + 10;
            if (yMax <= yMin) yMax = yMin + 10;

            return new Point(
                _random.Next((int)xMin, (int)xMax),
                _random.Next((int)yMin, (int)yMax)
            );
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

        private void ShowTextReport()
        {
            ClanReportGenerator reportGenerator =
                new FullClanReport(new TextDisplayFormatter());

            StringBuilder sb = new StringBuilder();
            foreach (var clan in _clanModels)
            {
                sb.AppendLine(reportGenerator.GenerateReport(clan));
                sb.AppendLine("\n");
            }
            GeneratedReport = sb.ToString();
        }
        private void ShowPseudoGraphicsReport()
        {
            ClanReportGenerator reportGenerator =
                new FullClanReport(new PseudoGraphicsFormatter());

            StringBuilder sb = new StringBuilder();
            foreach (var clan in _clanModels)
            {
                sb.AppendLine(reportGenerator.GenerateReport(clan));
                sb.AppendLine("\n");
            }
            GeneratedReport = sb.ToString();
        }

        public void RegisterPerson(Person person, Clan clan)
        {
            _personClanMap[person] = clan;
        }

        public void UnregisterPerson(Person person)
        {
            if (_personClanMap.ContainsKey(person))
                _personClanMap.Remove(person);
        }

        public void SendCommand(Person leader, Squad targetSquad, SquadCommand command)
        {
            if (leader == null || targetSquad == null) return;

            if (!_personClanMap.TryGetValue(leader, out var clan))
            {
                return;
            }

            if (clan.Leader != leader)
            {
                return;
            }

            if (!clan.Squads.Contains(targetSquad))
            {
                return;
            }

            targetSquad.HandleCommand(command);
        }

        public void PersonDied(Person diedPerson)
        {
            var baseDiedPerson = diedPerson.GetBasePerson();

            Clan clan = null;
            Person registeredKey = null;

            foreach (var kvp in _personClanMap)
            {
                if (kvp.Key.GetBasePerson() == baseDiedPerson)
                {
                    clan = kvp.Value;
                    registeredKey = kvp.Key;
                    break;
                }
            }

            if (clan != null && registeredKey != null)
            {
                clan.RemovePerson(registeredKey);

                var clanVM = Clans.FirstOrDefault(c => c.Model == clan);
                if (clanVM != null)
                {
                    var pvm = clanVM.Persons.FirstOrDefault(p => p.Model.GetBasePerson() == baseDiedPerson);
                    if (pvm != null)
                    {
                        clanVM.Persons.Remove(pvm);
                    }

                    foreach (var svm in clanVM.SquadVMs)
                    {
                        svm.UpdateName();
                    }

                    if (clan.Leader == null || clan.Leader.GetBasePerson() == baseDiedPerson)
                    {
                        clanVM.NotifyLeaderChanged();
                    }
                }

                _personClanMap.Remove(registeredKey);
            }

            var allPvm = AllPersons.FirstOrDefault(p => p.Model.GetBasePerson() == baseDiedPerson);
            if (allPvm != null)
            {
                AllPersons.Remove(allPvm);
            }
        }

        public Person FindNearestEnemy(Person attacker)
        {
            var attackerBase = attacker.GetBasePerson();
            Clan attackerClan = null;

            foreach (var kvp in _personClanMap)
            {
                if (kvp.Key.GetBasePerson() == attackerBase)
                {
                    attackerClan = kvp.Value;
                    break;
                }
            }

            if (attackerClan == null) return null;

            Person nearestEnemy = null;
            double minDistance = double.MaxValue;

            foreach (var kvp in _personClanMap)
            {
                Person potentialEnemy = kvp.Key;
                Clan enemyClan = kvp.Value;

                if (enemyClan != attackerClan)
                {
                    double distance = (attacker.Position - potentialEnemy.Position).Length;

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestEnemy = potentialEnemy;
                    }
                }
            }

            return nearestEnemy;
        }

        public Point GetLeaderPosition(Person person)
        {
            var basePerson = person.GetBasePerson();

            Clan personClan = null;
            foreach (var kvp in _personClanMap)
            {
                if (kvp.Key.GetBasePerson() == basePerson)
                {
                    personClan = kvp.Value;
                    break;
                }
            }

            if (personClan != null && personClan.Leader != null)
            {
                return personClan.Leader.Position;
            }

            return person.Position;
        }

        private void SaveCheckpoint()
        {
            var memento = new GameMemento(_clanModels);

            _history.Push(memento);

            MessageBox.Show("Checkpoint Saved!");
        }

        private void LoadCheckpoint()
        {
            if (_history.Count == 0) return;

            var memento = _history.Pop();

            RestoreState(memento);

            MessageBox.Show("Checkpoint Loaded!");
        }

        private void RestoreState(GameMemento memento)
        {
            ResetBattle();

            _clanModels.Clear();
            foreach (var savedClan in memento.ClansState)
            {
                _clanModels.Add(savedClan.DeepCopy());
            }

            RefreshBattlefield();
        }

        private void RefreshBattlefield()
        {
            Clans.Clear();
            AllPersons.Clear();
            _personClanMap.Clear();

            int totalClans = _clanModels.Count;
            if (totalClans == 0) return;

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
                double y_start = segmentHeight * rowIndex;
                double width = x_start + segmentWidth;

                foreach (var squad in currentClan.Squads)
                {
                    foreach (var person in squad.Persons)
                    {
                        person.SetMediator(this);
                        RegisterPerson(person, currentClan);
                    }
                }

                var clanVM = new ClanViewModel(currentClan, x_start, y_start, segmentWidth, segmentHeight, this);
                Clans.Add(clanVM);

                foreach (var p in clanVM.Persons)
                {
                    AllPersons.Add(p);
                }
            }
        }
    }
}
