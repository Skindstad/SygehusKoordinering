using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SygehusKoordinering.DataAccess;
using SygehusKoordinering.Models;
using SygehusKoordinering.View;
using System.Collections.ObjectModel;

namespace SygehusKoordinering.ViewModel
{
    // Created by Benjamin Anan Forsberg
    public partial class PersonaleListeViewModel : ObservableObject
    {
        public static PersonaleRepository personaleRepository = [];

        public PersonaleListeViewModel()
        {
            PersonaleListe = new ObservableCollection<Personale>(personaleRepository);
            Search();
        }

        [ObservableProperty]
        ObservableCollection<Personale> personaleListe;

        [ObservableProperty]
        string cPR;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string mail;

        [ObservableProperty]
        string arbejdTLF;

        [ObservableProperty]
        string status;
        // Used to display all the personale
        [RelayCommand]
        void Search()
        {
            personaleRepository.Search(cPR, name, mail, arbejdTLF, "", "", status);
            PersonaleListe = new ObservableCollection<Personale>(personaleRepository);
        }

    }
}
