using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Identity.Client;
using SygehusKoordinering.DataAccess;
using SygehusKoordinering.Models;
using SygehusKoordinering.Object;

namespace SygehusKoordinering.ViewModel
{
    //Create by Jakob Skindstad Frederiksen
    public partial class LoginViewModel : ObservableObject
    {
        public static PersonaleRepository personales = [];
        public static LoginData Data = new LoginData();


        [ObservableProperty]
        string mail;

        [ObservableProperty]
        string arbejdeTlf;

        [ObservableProperty]
        string adgangskode;

        [RelayCommand]
        async void Login()
        {
           Personale me = personales.Login(Mail, Adgangskode, ArbejdeTlf);

            if(me != null)
            {
                Data.Add(me);
                Clear();
               await LokationPage();
            }
        }

        private void Clear()
        {
            Mail = string.Empty;
            ArbejdeTlf = string.Empty;
            Adgangskode = string.Empty;
        }

        public async Task LokationPage()
        {
            await Shell.Current.GoToAsync(nameof(MainPage));
        }
    }

   


}
