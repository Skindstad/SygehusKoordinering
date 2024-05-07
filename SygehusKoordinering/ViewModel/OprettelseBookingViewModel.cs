﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SygehusKoordinering.DataAccess;
using SygehusKoordinering.Models;
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
            //ProeveList = new ObservableCollection<Proeve>(proeveRepository);
            IsSelected = new ObservableCollection<Proeve>();
            bestiltTime = DateTime.Now.TimeOfDay;
            bestiltDato = DateTime.Now.Date;
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
        string isolationspatient;
        /*
        [ObservableProperty]
        List<string> proeve;

        [ObservableProperty]
        string selectedProeve;
        */

        [ObservableProperty]
        ObservableCollection<Proeve> proeveList;

        [ObservableProperty]
        ObservableCollection<Proeve> isSelected;

        [ObservableProperty]
        string navn;

        [ObservableProperty]
        List<string> saerligeForhold;

        [ObservableProperty]
        string selectedSaerligeForhold;

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
        string createdAf;

        [ObservableProperty]
        string kommentar;
        /*
        [RelayCommand]
        void Create()
        {
            Booking booking = new Booking(id, cpr, selectedAfdeling, afdelingDecription, stueEllerSengeplads, isolationspatient, 
                                          selectedProeve, selectedSaerligeForhold, inaktiv, selectedPrioritet, bestiltTime,
                                          bestiltDato, selectedBestilt, kommentar, createdAf, takedAf, done);
            bookingRepository.Add(booking);
        }
        */
        private void LoadProeve()
        {
            proeveRepository.Search("");
            ProeveList = new ObservableCollection<Proeve>(proeveRepository);
        }
        
        private void LoadSaerligeForhold()
        {
            SaerligeForhold = saerligeForholdRepository.GetSaerligeForholds();
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
    }
}