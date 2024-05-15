using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SygehusKoordinering.DataAccess;
using SygehusKoordinering.Models;
using SygehusKoordinering.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SygehusKoordinering.ViewModel
{
    public partial class ConformationViewModel : ObservableObject
    {
        public BookingRepository bookings = [];

        public ConformationViewModel() 
        {
            Booking Book = OplysningViewModel.data.GetBooking();
            bookings = new BookingRepository();
            if (ItemViewModel.Complete == true)
            {
                Faediggoere = true;
                VidereGivHidden = false;

                Tid = ["1-10 min", "10-20 min", "20-30 min", "Forgæves tur"];
                Aarsag = ["Ingen yderligere kommentarer", "Vente på elevator", "Patient ikke klar", "Plejepersonale ikke klar", "Røntgen ikke klar", "OP ikke Klar", "Behandlingsrum ikke klar"];


            } else
            {
                Faediggoere = false;
                VidereGivHidden = true;

                if (DateTime.Now.Date.ToString("yyyy-MM-dd") == DateTime.Parse(Book.BestiltDato).ToString("yyyy-MM-dd"))
                {
                    Date = "I dag ";
                }
                else
                {
                    DateTime time = DateTime.Parse(Book.BestiltDato);
                    Date = time.ToString("yyyy-MM-dd");
                }
                Time = Book.BestiltTime;
                Afdeling = Book.Afdeling;
                StueEllerSengeplads = Book.StueEllerSengeplads;

            }
            
        }
        [ObservableProperty]
        List<string> tid;

        [ObservableProperty]
        string selectedTid;

        [ObservableProperty]
        List<string> aarsag;

        [ObservableProperty]
        string selectedAarsag;

        [ObservableProperty]
        bool faediggoere;

        [ObservableProperty]
        bool videreGivHidden;

        [ObservableProperty]
        string videreGivKommentar;

        [ObservableProperty]
        string date;

        [ObservableProperty]
        string time;

        [ObservableProperty]
        string afdeling;

        [ObservableProperty]
        string stueEllerSengeplads;

        [RelayCommand]
        void VidereGiv()
        {
            string newKommentar = OplysningViewModel.data.GetBooking().Kommentar + "\n" + videreGivKommentar;


            bookings.Update(OplysningViewModel.data.GetBooking(), null, "0", newKommentar, "0");
            foreach (var station in MainViewModel.stations)
            {
                if (station.Name == AfdelingRepository.GetLocationFromAfdeling(afdeling))
                {
                    station.currentPriority = "";
                    station.nodify();
                }
            }
            Oplysning();

        }
        [RelayCommand]
        void Finish()
        {
            string newKommentar = OplysningViewModel.data.GetBooking().Kommentar + "\n" + selectedTid + "\n" + selectedAarsag;
           bookings.Update(OplysningViewModel.data.GetBooking(), MainViewModel.data.Getpersonal().CPR, "1", newKommentar, "1");
            foreach (var station in MainViewModel.stations)
            {
                if (station.Name == AfdelingRepository.GetLocationFromAfdeling(afdeling))
                {
                    station.currentPriority = "";
                    station.nodify();
                }
            }
            Oplysning();
        }

        async Task Oplysning()
        {
            await Shell.Current.GoToAsync(nameof(OplysningView));
        }

    }
}
