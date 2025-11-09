using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace patterns_lab2_2.ViewModels
{
    public class BaseViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public class RelayCommand : ICommand
        {
            private Action _execute;
            public RelayCommand(Action execute) { _execute = execute; }
            public bool CanExecute(object parameter) => true;
            public void Execute(object parameter) => _execute();
            public event EventHandler CanExecuteChanged { add { } remove { } }
        }
    }
}
