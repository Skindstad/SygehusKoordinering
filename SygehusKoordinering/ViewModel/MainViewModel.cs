using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SygehusKoordinering.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SygehusKoordinering.Models;
using SygehusKoordinering.View;
using System.Windows.Input;

namespace SygehusKoordinering.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        public static LocationRepository locationRepository = [];
        public static BundleRepository bundleRepository = [];


        public static LoginData data = new LoginData();

        public MainViewModel(/*string cpr*/)
        {
            //this.cpr = cpr;
            Oprettelse();
            //Login();
            LocalList = new ObservableCollection<Locations>(locationRepository);
            IsSelected = new ObservableCollection<Locations>();
            Search();
        }

        [ObservableProperty]
        ObservableCollection<Locations> localList;

        [ObservableProperty]
        ObservableCollection<Locations> isSelected;

        [ObservableProperty]
        string navn;


        [RelayCommand]
        void Search()
        {
            locationRepository.Search("");
            LocalList = new ObservableCollection<Locations>(locationRepository);

        }
        [RelayCommand]
        private void ExecuteOk()
        {
            foreach (var location in LocalList)
            {
                if (location.IsSelected)
                {
                    // Call AddLocationsToPersonale method for selected location
                    data.AddLocation(location.Navn);
                    bundleRepository.AddLocationsToPersonale(data.Getpersonal().CPR, location.Navn);
                }
            }
            Oplysning();
        }

        async Task Login()
        {
            await Shell.Current.GoToAsync(nameof(LoginView));
        }

        async Task Oplysning()
        {
            await Shell.Current.GoToAsync(nameof(OplysningView));
        }

        async Task Oprettelse()
        {
            await Shell.Current.GoToAsync(nameof(OprettelseBookingView));
        }

    }
}
