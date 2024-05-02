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

namespace SygehusKoordinering.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        public static LocationRepository locationRepository = [];
        public static BundleRepository bundleRepository = [];

        public MainViewModel()
        {
            Login();
            LocalList = new ObservableCollection<Locations>(locationRepository);
            Search();
        }

        [ObservableProperty]
        ObservableCollection<Locations> localList;

        [ObservableProperty]
        string navn;


        [RelayCommand]
        void Search()
        {
            locationRepository.Search("");
            LocalList = new ObservableCollection<Locations>(locationRepository);

        }


        async Task Login()
        {
            await Shell.Current.GoToAsync(nameof(LoginView));
        }
    }
}
