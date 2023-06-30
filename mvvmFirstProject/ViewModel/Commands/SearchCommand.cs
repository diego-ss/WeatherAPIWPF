using System;
using System.Windows.Input;

namespace mvvmFirstProject.ViewModel.Commands
{
    public class SearchCommand : ICommand
    {
        private WeatherViewModel WeatherViewModel { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove {  CommandManager.RequerySuggested -= value; }
        }

        public SearchCommand(WeatherViewModel weatherViewModel)
        {
            WeatherViewModel = weatherViewModel;
        }
        public bool CanExecute(object parameter)
        {
            string query = parameter as string;

            if (string.IsNullOrEmpty(query))
                return false;

            return true;
        }

        public void Execute(object parameter)
        {
            WeatherViewModel.MakeQuery();
        }
    }
}
