using mvvmFirstProject.Model;
using mvvmFirstProject.ViewModel.Commands;
using mvvmFirstProject.ViewModel.Helpers;
using System.Collections.ObjectModel;
using System.ComponentModel;

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

                if(selectedCity != null)
                {
                    OnPropertyChanged("SelectedCity");
                    GetCurrentConditions();
                }
            }
        }

        public ObservableCollection<City> Cities { get; set; }

        public SearchCommand SearchCommand { get; set; }

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
                            Value = "21"
                        }
                    }
                };
            }

            SearchCommand = new SearchCommand(this);
            Cities = new ObservableCollection<City>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public async void MakeQuery()
        {
            var retrievedCities = await AccuWeatherHelper.GetCities(Query);
            Cities.Clear();
            retrievedCities.ForEach(city =>
            {
                Cities.Add(city);
            });
            
        }

        private async void GetCurrentConditions()
        {
            Query = string.Empty;
            CurrentCondition = await AccuWeatherHelper.GetCurrentCondition(SelectedCity.Key);
            Cities.Clear();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
