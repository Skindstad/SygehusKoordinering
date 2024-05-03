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


        public LoginViewModel()
        {
        
        }

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
                //MainViewModel mainViewModel = new MainViewModel(me.CPR);
                Main(/*mainViewModel*/);
            }

        }



        
        async Task Main(/*MainViewModel mainViewModel*/)
        {
            await Shell.Current.GoToAsync(".."/*, mainViewModel*/);
        }
    }
}
