using patterns_lab2_2.Models.Persons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace patterns_lab2_2.ViewModels
{
    public class AddClanViewModel: BaseViewModel
    {
        private Clan _clan;
        public ObservableCollection<Brush> ColorOptions { get; } = new ObservableCollection<Brush>();
        public AddClanViewModel(Clan clan)
        {
            _clan = clan;

            ColorOptions.Add(_clan.Color);
            ColorOptions.Add(Brushes.Red);
            ColorOptions.Add(Brushes.Blue);
            ColorOptions.Add(Brushes.Yellow);
            ColorOptions.Add(Brushes.Purple);
            ColorOptions.Add(Brushes.Orange);
            ColorOptions.Add(Brushes.White);
        }
        public string ClanName
        {
            get => _clan.Name;
            set { _clan.Name = value; OnPropertyChanged(); }
        }

        public Brush SelectedColor
        {
            get => _clan.Color;
            set { _clan.Color = value; OnPropertyChanged(); }
        }
    }
}
