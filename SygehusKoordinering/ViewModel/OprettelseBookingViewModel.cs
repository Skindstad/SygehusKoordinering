﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    public partial class OprettelseBookingViewModel : ObservableObject
    {
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

        [ObservableProperty]
        string id;

        [ObservableProperty]
        string cpr;

        [ObservableProperty]
        string name;
        
        [ObservableProperty]
        List<string> afdeling;
        
        [ObservableProperty]
        string selectedAfdeling;

        [ObservableProperty]
        string stueEllerSengeplads;

        [ObservableProperty]
        bool isolationspatient;
        
        private List<string> proever  = new List<string>();
        /*
        [ObservableProperty]
        string selectedProeve;
        */

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
        string inaktiv;

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

        string createdAf = MainViewModel.data.Getpersonal().CPR;

        [RelayCommand]
        async Task Create()
        {
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

            if(inaktiv != "1")
            {
                inaktiv = "0";
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


            string formateDate = bestiltDato.ToString("yyyy-MM-dd");

            if(kommentar == null)
            {
                kommentar = "";
            }


            Booking booking = new Booking("", cpr, name, selectedAfdeling, "", stueEllerSengeplads, selectedIsolation, 
                                          proever, saerligeForhold, inaktiv, selectedPrioritet, formateTime,
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
                await Oplysning();
            }

        }
        
        private void LoadProeve()
        {
            proeveRepository.Search("");
            ProeveList = new ObservableCollection<Proeve>(proeveRepository);
        }
        
        private void LoadSaerligeForhold()
        {
            saerligeForholdRepository.Search("");
            SaerligeForholdList = new ObservableCollection<SaerligeForhold>(saerligeForholdRepository);
        }
        
        private void LoadAfdeling()
        {
            Afdeling = afdelingsRepository.GetAfdelinger();
        }

        private void LoadPrioritet()
        {
            Prioritet = prioritetRepository.GetPrioritets();
        }

        private void LoadBestiltTime()
        {
            Bestilt = bestiltRepository.GetBestilts();
        }

        [RelayCommand]
        void Clear()
        {
            Cpr = string.Empty;
            Name = string.Empty;
            SelectedAfdeling = null;
            StueEllerSengeplads = string.Empty;
            Isolationspatient = false;
            IsSelectedProeve = null;
            IsSelectedSaerlig = null;
            Inaktiv = null;
            SelectedPrioritet = Prioritet.FirstOrDefault();
            BestiltTime = DateTime.Now.TimeOfDay;
            BestiltDato = DateTime.Now.Date;
            SelectedBestilt = Bestilt.FirstOrDefault();
            Kommentar = string.Empty;
        }
        // Some error when choosing pressing no
        private Task<bool> ShowConfirmationDialogAsync()
        {
            // Show a dialog to confirm if the user wants to create more tasks
            return Application.Current.MainPage.DisplayAlert("Confirmation", "Do you want to create more tasks?", "Yes", "No");
        }

        public async Task Oplysning()
        {
            await Shell.Current.GoToAsync($"..");
        }
    }
}
