using CommunityToolkit.Mvvm.ComponentModel;
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
        public static LoginData data = new LoginData();
        public OprettelseBookingViewModel()
        {
            LoadAfdeling();
            LoadProeve();
            LoadSaerligeForhold();
            LoadPrioritet();
            LoadBestiltTime();
            //ProeveList = new ObservableCollection<Proeve>(proeveRepository);
            IsSelectedProeve = new ObservableCollection<Proeve>();
            IsSelectedSaerlig = new ObservableCollection<SaerligeForhold>();
            bestiltTime = DateTime.Now.TimeOfDay;
            bestiltDato = DateTime.Now.Date;
            SelectedBestilt = bestilt[0];
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
        
        private List<string> proever;
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
        
        private List<string> saerligeForhold;
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

        string createdAf = data.Getpersonal().CPR;

        [RelayCommand]
        void Create()
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
            
            Booking booking = new Booking("", cpr, name, selectedAfdeling, "", stueEllerSengeplads, isolationspatient, 
                                          proever, saerligeForhold, inaktiv, selectedPrioritet, bestiltTime.ToString(),
                                          bestiltDato.ToString(), selectedBestilt, kommentar, createdAf, "", "");
            bookingRepository.Add(booking);
            
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
    }
}
