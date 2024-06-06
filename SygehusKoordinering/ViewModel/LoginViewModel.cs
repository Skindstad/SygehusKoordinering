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

        // Bindings
        [ObservableProperty]
        string mail;

        [ObservableProperty]
        string arbejdeTlf;

        [ObservableProperty]
        string adgangskode;

        [ObservableProperty]
        string mailError;

        [ObservableProperty]
        string arbejdeTlfError;

        [ObservableProperty]
        string adgangskodeError;

        [ObservableProperty]
        string errorMessage;


        // Login
        [RelayCommand]
        async void Login()
        {
            bool isValid = ValidateInputs();

            if (isValid)
            {
                try
                {
                    Personale me = personales.Login(Mail, Adgangskode, ArbejdeTlf);

                    if (me != null)
                    {
                        Data.Add(me);
                        Clear();
                        await LokationPage();
                    }
                    else
                    {
                        ErrorMessage = "Invalid login credentials. Please try again.";
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage = $"An error occurred: {ex.Message}";
                }
            }
        }
        // Check login
        private bool ValidateInputs()
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(Mail))
            {
                MailError = "Email is required.";
                isValid = false;
            }
            else
            {
                MailError = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(ArbejdeTlf))
            {
                ArbejdeTlfError = "Work phone number is required.";
                isValid = false;
            }
            else
            {
                ArbejdeTlfError = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(Adgangskode))
            {
                AdgangskodeError = "Password is required.";
                isValid = false;
            }
            else
            {
                AdgangskodeError = string.Empty;
            }

            return isValid;
        }

        // Fjern det skrevet login fra input felterne
        private void Clear()
        {
            Mail = string.Empty;
            ArbejdeTlf = string.Empty;
            Adgangskode = string.Empty;
            ErrorMessage = string.Empty;
        }
        // send til ny side
        public async Task LokationPage()
        {
            await Shell.Current.GoToAsync(nameof(MainPage));
        }
    }

   


}
