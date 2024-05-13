using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SygehusKoordinering.DataAccess;
using SygehusKoordinering.Models;
using SygehusKoordinering.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SygehusKoordinering.ViewModel
{
    public partial class ItemViewModel : ObservableObject
    {
        public BookingRepository bookings = [];
        public OplysningViewModel oplysning;
        public ItemViewModel item;
        public ItemViewModel() 
        {
            Booking Book = OplysningViewModel.data.GetBooking();
            bookings = new BookingRepository();

            Navn = Book.Navn;
            CPR = Book.CPR;
            Prioritet = Book.Prioritet;
            //Proever = Book.Proeve;
            switch (Book.Prioritet)
            {
                case "Normal":
                    textColor = Colors.Blue;
                    break;
                case "Haster":
                    textColor = Colors.Orange;
                    break;
                case "Livstruende":
                    textColor = Colors.Red;
                    break;
                default:
                    textColor = Colors.Transparent;
                    break;
            }
            


            if(DateTime.Now.Date.ToString("yyyy-MM-dd") == DateTime.Parse(Book.BestiltDato).ToString("yyyy-MM-dd"))
            {
                Date = "I dag ";
            } else
            {
               DateTime time = DateTime.Parse(Book.BestiltDato);
                Date = time.ToString("yyyy-MM-dd");
            }
            Time = Book.BestiltTime;
            Afdeling = Book.Afdeling;
            Kommentar = Book.Kommentar;

            Personale createdAf =  bookings.FindCreatedAf(Book.Id);

            kontaktPerosn = createdAf.Navn;
            Phone = createdAf.ArbejdTlf;

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
        string proever;

        [ObservableProperty]
        string date;

        [ObservableProperty]
        string time;

        [ObservableProperty]
        string afdeling;

        [ObservableProperty]
        string kommentar;
        [ObservableProperty]
        string kontaktPerosn;

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


        [RelayCommand]
        void Book()
        {

            bookings.Update(OplysningViewModel.data.GetBooking(), MainViewModel.data.Getpersonal().CPR, "0", OplysningViewModel.data.GetBooking().Kommentar, "0");
            Oplysning();
        }
        [RelayCommand]
        void No()
        {
            bookings.Update(OplysningViewModel.data.GetBooking(), null, "0", OplysningViewModel.data.GetBooking().Kommentar, "0");
            Oplysning();
        }
        [RelayCommand]
        void Begin()
        {
            bookings.Update(OplysningViewModel.data.GetBooking(), MainViewModel.data.Getpersonal().CPR, "1", OplysningViewModel.data.GetBooking().Kommentar, "0");
            Oplysning();

        }
        [RelayCommand]
        void VidereGiv()
        {
            //bookings.Update(OplysningViewModel.data.GetBooking().Id, MainViewModel.data.Getpersonal().CPR, "1", OplysningViewModel.data.GetBooking().Kommentar, "0");
            Oplysning();
        }
        [RelayCommand]
        void Finish()
        {
            bookings.Update(OplysningViewModel.data.GetBooking(), MainViewModel.data.Getpersonal().CPR, "1", OplysningViewModel.data.GetBooking().Kommentar, "1");
            Oplysning();
        }

        async Task Oplysning()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}
