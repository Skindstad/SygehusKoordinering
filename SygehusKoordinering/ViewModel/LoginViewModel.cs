using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SygehusKoordinering.DataAccess;
using SygehusKoordinering.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SygehusKoordinering.ViewModel
{
   public partial class LoginViewModel : ObservableObject
    {
        public static PersonaleRepository personales = [];


        public LoginViewModel() {}

        [ObservableProperty]
        string mail;

        [ObservableProperty]
        string arbejdeTlf;

        [ObservableProperty]
        string adgangskode;


        [RelayCommand]
        void Login()
        {
           Personale me = personales.Login(Mail, Adgangskode, ArbejdeTlf);

            if(me != null)
            {
                MainViewModel.data.Add(me);
                Main();
            }

        }



        
        async Task Main()
        {
            await Shell.Current.GoToAsync("..");
        }
    }

    public class LoginData
    {
        private Personale data;

        public void Add(Personale p) 
        {
        data = p;
        }

        public Personale Getpersonal()
        {
            return data;
        }

        public void AddLocation(string location)
        {
            data.Lokations.Add(location);
        }
        public void ClearLocation()
        {
            data.Lokations.Clear();
            BundleRepository.removeLocation(data.CPR);
        }

    }

}
