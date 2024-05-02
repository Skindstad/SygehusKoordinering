using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SygehusKoordinering.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SygehusKoordinering.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        public static LocationRepository locationRepository = [];

        public MainViewModel()
        {
            LocalList = new ObservableCollection<Location>(locationRepository);
        }

        [ObservableProperty]
        ObservableCollection<Location> localList;

        [ObservableProperty]
        string navn;





        [RelayCommand]
        void Search()
        {
            locationRepository.Search(navn);
            LocalList = new ObservableCollection<Location>(locationRepository);

        }
    }
}
