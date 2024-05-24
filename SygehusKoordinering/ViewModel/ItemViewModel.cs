using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SygehusKoordinering.DataAccess;
using SygehusKoordinering.Models;
using SygehusKoordinering.Object;
using SygehusKoordinering.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SygehusKoordinering.ViewModel
{
    // Create by Jakob Skindstad Frederiksen
    public partial class ItemViewModel : ObservableObject
    {
        public BookingRepository bookings = [];
        public OplysningViewModel oplysning;
        public ItemViewModel item;

        public static bool Complete = false;

        public ItemViewModel() 
        {
            Booking Book = OplysningViewModel.data.GetBooking();
            bookings = new BookingRepository();


            // Navn
            Navn = Book.Navn;
            // CPR
            CPR = Book.CPR;
            // Prioritet
            Prioritet = Book.Prioritet;
            textColor = Objects.ReturnPriorityColor(Book.Prioritet);

            //Prøve
            Proeve = Objects.ReturnProeveString(Book.Proeve, "/ ");

            //Time and Date
            if(DateTime.Now.Date.ToString("yyyy-MM-dd") == DateTime.Parse(Book.BestiltDato).ToString("yyyy-MM-dd"))
            {
                Date = "I dag ";
            } else
            {
               DateTime time = DateTime.Parse(Book.BestiltDato);
                Date = time.ToString("yyyy-MM-dd");
            }

            Time = Book.BestiltTime;

            // Location
            Afdeling = Book.Afdeling;
            StueEllerSengeplads = Book.StueEllerSengeplads;
            
            //Kommentar
            Kommentar = Book.Kommentar;

            // Created Af
            Personale createdAf =  bookings.FindCreatedAf(Book.Id);

            kontaktPerson = createdAf.Navn;
            Phone = createdAf.ArbejdTlf;


            // Taked Af
            if(Book.TakedAf == "")
            {
                isBookVisible = true;
                isFinishVisible = false;
                isNoVisible = false;
                isVidereGivVisible = false;
                isBeginVisible = false;
            } else if(Book.TakedAf != "" && Book.Done == "True" && Book.Done == "True")
            {
                isBookVisible = false;
                isFinishVisible = false;
                isNoVisible = false;
                isVidereGivVisible = false;
                isBeginVisible = false;
            } else if (Book.TakedAf != "" && Book.Done == "False" && Book.Begynd == "True")
            {
                isBookVisible = false;
                isFinishVisible = true;
                isNoVisible = false;
                isVidereGivVisible = true;
                isBeginVisible = false;
            } else if(Book.TakedAf != "" && Book.Done == "False" && Book.Begynd == "False")
            {
                isBookVisible = false;
                isFinishVisible = false;
                isNoVisible = true;
                isVidereGivVisible = false;
                isBeginVisible = true;
            }
            

        }

        [ObservableProperty]
        string cPR;

        [ObservableProperty]
        string navn;

        [ObservableProperty]
        string prioritet;

        [ObservableProperty]
        Color textColor;

        [ObservableProperty]
        string stueEllerSengeplads;


        [ObservableProperty]
        string proeve;

        [ObservableProperty]
        string date;

        [ObservableProperty]
        string time;

        [ObservableProperty]
        string afdeling;

        [ObservableProperty]
        string kommentar;
        [ObservableProperty]
        string kontaktPerson;

        [ObservableProperty]
        string phone;

        [ObservableProperty]
        bool isBookVisible;

        [ObservableProperty]
        bool isNoVisible;

        [ObservableProperty]
        bool isVidereGivVisible;

        [ObservableProperty]
        bool isBeginVisible;
        [ObservableProperty]
        bool isFinishVisible;

        // 
        [RelayCommand]
        void Book()
        {

            bookings.Update(OplysningViewModel.data.GetBooking(), LoginViewModel.Data.Getpersonal().CPR, "0", OplysningViewModel.data.GetBooking().Kommentar, "0");

            Objects.SendNotify(afdeling, "");
            Oplysning();
        }
        [RelayCommand]
        void No()
        {
            bookings.Update(OplysningViewModel.data.GetBooking(), null, "0", OplysningViewModel.data.GetBooking().Kommentar, "0");
            Objects.SendNotify(afdeling, "");
            Oplysning();
        }
        [RelayCommand]
        void Begin()
        {
            bookings.Update(OplysningViewModel.data.GetBooking(), LoginViewModel.Data.Getpersonal().CPR, "1", OplysningViewModel.data.GetBooking().Kommentar, "0");
            Objects.SendNotify(afdeling, "");
            Oplysning();

        }
        [RelayCommand]
        void VidereGiv()
        {
            Complete = false;
            Conformation();
        }

        [RelayCommand]
        void Finish()
        {
            Complete = true;
            Conformation();
        }
        async Task Conformation()
        {
            await Shell.Current.GoToAsync(nameof(ConformationView));
        }

        async Task Oplysning()
        {
            await Shell.Current.GoToAsync($"..", false);
        }

    }
}
