using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SygehusKoordinering.DataAccess;
using System.Collections.ObjectModel;
using SygehusKoordinering.Models;
using SygehusKoordinering.View;
using SygehusKoordinering.Object;

namespace SygehusKoordinering.ViewModel
{
    // Created by Benjamin Anan Forsberg
    public partial class MainViewModel : ObservableObject
    {
        public static LocationRepository locationRepository = [];
        public static BundleRepository bundleRepository = [];
        public static List<Station> stations = new List<Station>();

        LoginData login = LoginViewModel.Data;

        public MainViewModel()
        {
            LocalList = new ObservableCollection<Locations>(locationRepository);
            IsSelected = new ObservableCollection<Locations>();
            Search();
        }

        [ObservableProperty]
        ObservableCollection<Locations> localList;

        [ObservableProperty]
        ObservableCollection<Locations> isSelected;

        [ObservableProperty]
        string? navn;

        [ObservableProperty]
        string errorMessage;


        [RelayCommand]
        void Search()
        {
            locationRepository.Search("");
            LocalList = new ObservableCollection<Locations>(locationRepository);

        }
        [RelayCommand]
        private async void ExecuteOk()
        {
                bool SomeIsSelected = false;
            try
            {
                stations.Clear();
                login.ClearLocation();
                foreach (var location in LocalList)
                {
                    stations.Add(new Station(location.Navn));
                    if (location.IsSelected)
                    {
                        // Call AddLocationsToPersonale method for selected location
                        login.AddLocation(location.Navn);
                        login.AddDisplay(stations.Last());
                        stations.Last().Add(login.GetDisplays().Last());

                        bundleRepository.AddLocationsToPersonale(login.Getpersonal().CPR, location.Navn);
                        SomeIsSelected = true;
                    }
                }
                if (SomeIsSelected == true)
                {
                    Clear();
                    await Oplysning();
                } else
                {
                    ErrorMessage = "Invalid. Please try again.";
                }
            } catch (Exception ex)
            {
                ErrorMessage = $"An error occurred: {ex.Message}";
            }
        }

        private void Clear()
        {
            ErrorMessage = string.Empty;
        }



        async Task Oplysning()
        {
            await Shell.Current.GoToAsync(nameof(OplysningView));
        }

    }


}
