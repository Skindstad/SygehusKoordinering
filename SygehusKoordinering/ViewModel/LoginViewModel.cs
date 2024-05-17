using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SygehusKoordinering.DataAccess;
using SygehusKoordinering.Models;
using SygehusKoordinering.Object;

namespace SygehusKoordinering.ViewModel
{
    //Create by Jakob Skindstad Frederiksen
    public partial class LoginViewModel : ObservableObject
    {
        public static PersonaleRepository personales = [];

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

   


}
