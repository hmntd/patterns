using patterns_lab2_2.Models.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace patterns_lab2_2.ViewModels
{
    public class SquadViewModel: BaseViewModel
    {
        public Squad Model { get; }
        public string Name { get; private set; }
        public int Index { get; private set; }

        public SquadViewModel(Squad model, int index)
        {
            Model = model;
            Index = index;

            UpdateName();
        }

        public void UpdateName()
        {
            Name = $"Squad {Index + 1} ({Model.Persons.Count} persons)";
            OnPropertyChanged(nameof(Name));
        }
    }
}
