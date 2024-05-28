using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Identity.Client;
using SygehusKoordinering.DataAccess;
using SygehusKoordinering.Models;
using SygehusKoordinering.Object;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SygehusKoordinering.ViewModel
{
    // Created by Benjamin Anan Forsberg
    public partial class OprettelseBookingViewModel : ObservableObject
    {
        // Gives acces to thier respective repository
        public static BestiltRepository bestiltRepository = [];
        public static PrioritetRepository prioritetRepository = [];
        public static ProeveRepository proeveRepository = [];
        public static SaerligeForholdRepository saerligeForholdRepository = [];
        public static AfdelingRepository afdelingsRepository = [];
        public static BookingRepository bookingRepository = [];
        public OprettelseBookingViewModel()
        {
            LoadAfdeling();
            LoadProeve();
            LoadSaerligeForhold();
            LoadPrioritet();
            LoadBestiltTime();
            IsSelectedProeve = new ObservableCollection<Proeve>();
            IsSelectedSaerlig = new ObservableCollection<SaerligeForhold>();
            bestiltTime = DateTime.Now.TimeOfDay;
            bestiltDato = DateTime.Now.Date;
            SelectedBestilt = bestilt[0];
            selectedPrioritet = Prioritet[0];
        }
        // A bunch of variables
        [ObservableProperty]
        string cpr;

        [ObservableProperty]
        string cprError;

        [ObservableProperty]
        string name;
        [ObservableProperty]
        string nameError;

        [ObservableProperty]
        List<string> afdeling;
        
        [ObservableProperty]
        string selectedAfdeling;

        [ObservableProperty]
        string selectedAfdelingError;

        [ObservableProperty]
        string stueEllerSengeplads;

        [ObservableProperty]
        string stueEllerSengepladsError;

        [ObservableProperty]
        bool isolationspatient;
        
        private List<string> proever  = new List<string>();
        
        [ObservableProperty]
        string selectedProeveError;

        [ObservableProperty]
        ObservableCollection<Proeve> proeveList;

        [ObservableProperty]
        ObservableCollection<Proeve> isSelectedProeve;

        [ObservableProperty]
        string navn;
        
        private List<string> saerligeForhold = new List<string>();
        /*
        [ObservableProperty]
        string selectedSaerligeForhold;
        */
        [ObservableProperty]
        ObservableCollection<SaerligeForhold> saerligeForholdList;

        [ObservableProperty]
        ObservableCollection<SaerligeForhold> isSelectedSaerlig;

        [ObservableProperty]
        bool inaktiv;

        [ObservableProperty]
        List<string> prioritet;

        [ObservableProperty]
        string selectedPrioritet;

        [ObservableProperty]
        TimeSpan bestiltTime;

        [ObservableProperty]
        DateTime bestiltDato;

        [ObservableProperty]
        List<string> bestilt;

        [ObservableProperty]
        string selectedBestilt;

        [ObservableProperty]
        string kommentar;



        // Gets CPR from the personal that logined
        string createdAf = LoginViewModel.Data.Getpersonal().CPR;

        [RelayCommand]
        async Task Create()
        {
            bool IsValid = ValidateInputs();

            if (IsValid)
            {

                // Adds the selected proever and saerligeforhold to thier respective lists
                foreach (var proeve in ProeveList)
                {
                    if (proeve.IsSelectedProeve)
                    {
                        proever.Add(proeve.Navn);
                    }
                }
                foreach (var Saerlig in SaerligeForholdList)
                {
                    if (Saerlig.IsSelectedSaerlig)
                    {
                        saerligeForhold.Add(Saerlig.Navn);
                    }
                }
                // Converter for inaktiv
                string selectedInaktiv;
                if (inaktiv == true)
                {
                    selectedInaktiv = "1";
                }
                else
                {
                    selectedInaktiv = "0";
                }

                //Converter for isolationspatient
                string selectedIsolation;
                if (isolationspatient == true)
                {
                    selectedIsolation = "1";
                }
                else
                {
                    selectedIsolation = "0";
                }

                // Formats the time based on the selectedBestilt
                string formateTime = null;
                TimeSpan estra;
                TimeSpan time;
                switch (selectedBestilt)
                {
                    case "Til Bestilt tid":
                        formateTime = bestiltTime.ToString(@"hh\:mm");
                        break;
                    case "Inden for 1 time":
                        estra = TimeSpan.FromHours(1);
                        time = bestiltTime.Add(estra);
                        formateTime = time.ToString(@"hh\:mm");
                        break;
                    case "Inden for 2 time":
                        estra = TimeSpan.FromHours(2);
                        time = bestiltTime.Add(estra);
                        formateTime = time.ToString(@"hh\:mm");
                        break;
                    case "Inden for 3 time":
                        estra = TimeSpan.FromHours(3);
                        time = bestiltTime.Add(estra);
                        formateTime = time.ToString(@"hh\:mm");
                        break;
                }

                // Formats the date
                string formateDate = bestiltDato.ToString("yyyy-MM-dd");

                if (kommentar == null)
                {
                    kommentar = "";
                }
                // Takes all the inputs and creates a new booking
                Booking booking = new Booking("", cpr, name, selectedAfdeling, "", stueEllerSengeplads, selectedIsolation,
                                              proever, saerligeForhold, selectedInaktiv, selectedPrioritet, formateTime,
                                              formateDate, selectedBestilt, kommentar, createdAf, "", "", "");
                bookingRepository.Add(booking);

                // Sends a Notify
                Objects.SendNotify(selectedAfdeling, selectedPrioritet);

                // After creating the booking, show the confirmation dialog
                bool createMore = await ShowConfirmationDialogAsync();

                if (createMore)
                {
                    Clear();
                }
                else
                {
                    Clear();
                    await Oplysning();
                }
            }
        }


        private bool ValidateInputs()
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(Cpr))
            {
                CprError = "CPR is required.";
                isValid = false;
            }
            else
            {
                CprError = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(Name))
            {
                NameError = "Navn is required.";
                isValid = false;
            }
            else
            {
                NameError = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(SelectedAfdeling))
            {
                SelectedAfdelingError = "Afdeling is required.";
                isValid = false;
            }
            else
            {
                SelectedAfdelingError = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(StueEllerSengeplads))
            {
                StueEllerSengepladsError = "Stue Eller SengePlads is required.";
                isValid = false;
            }
            else
            {
                StueEllerSengepladsError = string.Empty;
            }
            int count = 0;
            foreach (var proeve in ProeveList)
            {
                if (!proeve.IsSelectedProeve)
                {
                    count++;
                }
            }

            if (ProeveList.Count == count)
            {
                SelectedProeveError = "Prøve is required.";
                isValid = false;
            }
            else
            {
                SelectedProeveError = string.Empty;
            }


            return isValid;
        }


        // Method to get list of proever
        private void LoadProeve()
        {
            proeveRepository.Search("");
            ProeveList = new ObservableCollection<Proeve>(proeveRepository);
        }
        // Method to get list of searligeforhold
        private void LoadSaerligeForhold()
        {
            saerligeForholdRepository.Search("");
            SaerligeForholdList = new ObservableCollection<SaerligeForhold>(saerligeForholdRepository);
        }
        // Method to get list of Afdelinger
        private void LoadAfdeling()
        {
            Afdeling = afdelingsRepository.GetAfdelinger();
        }
        // Method to get list of Prioriteter
        private void LoadPrioritet()
        {
            Prioritet = prioritetRepository.GetPrioritets();
        }
        // Method to get list of Bestilt
        private void LoadBestiltTime()
        {
            Bestilt = bestiltRepository.GetBestilts();
        }

        // Method to clear the page, so that the user can easily make a new task
        void Clear()
        {
            Cpr = string.Empty;
            Name = string.Empty;
            SelectedAfdeling = null;
            StueEllerSengeplads = string.Empty;
            Isolationspatient = false;
            proever.Clear();
            saerligeForhold.Clear();
            LoadProeve();
            LoadSaerligeForhold();
            Inaktiv = false;
            SelectedPrioritet = Prioritet.FirstOrDefault();
            BestiltTime = DateTime.Now.TimeOfDay;
            BestiltDato = DateTime.Now.Date;
            SelectedBestilt = Bestilt.FirstOrDefault();
            Kommentar = string.Empty;
        }
        // Makes a Confirm Window
        private Task<bool> ShowConfirmationDialogAsync()
        {
            // Show a dialog to confirm if the user wants to create more tasks
            return Application.Current.MainPage.DisplayAlert("Confirmation", "Do you want to create more tasks?", "Yes", "No");
        }
        // Method that goes to Oplysning
        public async Task Oplysning()
        {
            await Shell.Current.GoToAsync($"..");
        }
    }
}
