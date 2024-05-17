﻿using CommunityToolkit.Mvvm.ComponentModel;
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
        public static List<Station> stations = new List<Station>();

        public static LoginData data = new LoginData();

        public MainViewModel()
        {
            //Oprettelse();
            Login();
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
            bool SomeIsSelected = false;
            if(data.Getpersonal() == null)
            {
                Login();
            }
            else
            {
                stations.Clear();
                data.ClearLocation();
                foreach (var location in LocalList)
                {
                    stations.Add(new Station(location.Navn));
                    if (location.IsSelected)
                    {
                        // Call AddLocationsToPersonale method for selected location
                        data.AddLocation(location.Navn);
                        data.AddDisplay(stations.Last());
                        stations.Last().Add(data.GetDisplays().Last());

                        bundleRepository.AddLocationsToPersonale(data.Getpersonal().CPR, location.Navn);
                        SomeIsSelected = true;
                    }
                }
                if (SomeIsSelected == true)
                {
                    Oplysning();
                }
            } 
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
