using mvvmFirstProject.Model;
using mvvmFirstProject.ViewModel.Helpers;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace mvvmFirstProject.ViewModel
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private string query;

        public string Query
        {
            get { return query; }
            set { 
                query = value;
                OnPropertyChanged("Query");
            }
        }

        private CurrentCondition currentCondition;

        public CurrentCondition CurrentCondition
        {
            get { return currentCondition; }
            set { 
                currentCondition = value;
                OnPropertyChanged("CurrentCondition");
            }
        }

        private City selectedCity;

        public City SelectedCity
        {
            get { return selectedCity; }
            set { 
                selectedCity = value;
                OnPropertyChanged("SelectedCity");
            }
        }

        public WeatherViewModel()
        {
            // só mostra no design, não na aplicação rodando
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                SelectedCity = new City
                {
                    LocalizedName = "New York"
                };

                CurrentCondition = new CurrentCondition
                {
                    WeatherText = "Partly cloudy",
                    Temperature = new Temperature
                    {
                        Metric = new Units
                        {
                            Value = 21
                        }
                    }
                };
            }     
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public async void MakeQuery()
        {
            var cities = await AccuWeatherHelper.GetCities(Query);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
