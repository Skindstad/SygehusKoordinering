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

        public ICommand OkCommand { get; }

        private string cpr;

        public MainViewModel(string cpr)
        {
            this.cpr = cpr;
            //Login();
            LocalList = new ObservableCollection<Locations>(locationRepository);
            IsSelected = new ObservableCollection<Locations>();
            OkCommand = new RelayCommand(ExecuteOkCommand);
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

        private void ExecuteOkCommand()
        {
            foreach (var location in LocalList)
            {
                if (location.IsSelected)
                {
                    // Call AddLocationsToPersonale method for selected location
                    bundleRepository.AddLocationsToPersonale("CPR", location.Navn);
                }
            }
        }

        async Task Login()
        {
            await Shell.Current.GoToAsync(nameof(LoginView));
        }
    }
}
