using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SygehusKoordinering.DataAccess;
using SygehusKoordinering.Models;
using SygehusKoordinering.Object;
using SygehusKoordinering.View;
using System.Collections.ObjectModel;

namespace SygehusKoordinering.ViewModel
{
    // Created By Jakob Skindstad Frederiksen
    public partial class OplysningViewModel : ObservableObject
    {
        public static BookingRepository bookingRepository = new BookingRepository();
        public static OplysningData data;
        public static PersonaleRepository personalesRepository = [];

        public OplysningViewModel()
        {
            Objects.SetOplysningsViewModel(this);
            data = new OplysningData();
            Find();
        }

        public void ItemTapped(Booking tappedBooking)
        {
            data.Add(tappedBooking);
            Item();
        }


        [ObservableProperty]
        ObservableCollection<Booking> localList;

        [ObservableProperty]
        string afdeling;

        [ObservableProperty]
        string stueEllerSengeplads;

        [ObservableProperty]
        string proeve;

        [ObservableProperty]
        string navn;
        [ObservableProperty]
        string bestiltTime;

        [ObservableProperty]
        string rowColor;

        [ObservableProperty]
        string click;

        [ObservableProperty]
        ImageSource image;


        public void Find()
        {
                LocalList = new ObservableCollection<Booking>();


                var bookings = bookingRepository.Search(MainViewModel.data.Getpersonal().Lokations, MainViewModel.data.Getpersonal().CPR);

           foreach (var booking in bookings)
           {
                booking.Image = "white_syringe.png";


                if (booking.TakedAf != "" && booking.Begynd == "True")
                    {
                    booking.Image = "black_syringe.png";
                    } 
                    else if (booking.TakedAf != "")
                    {
                    booking.Image = "yellow_syringe.png";
                    }
                    // Prioritet
                    booking.RowColor = Objects.ReturnPriorityColor(booking.Prioritet);

                    // Time and Date
                    booking.BestiltTime = Objects.ReturnTime(booking.Bestilt, booking.BestiltTime);


                    LocalList.Add(booking);
             
            }
        }

        async Task Item()
        {
            await Shell.Current.GoToAsync(nameof(ItemView));
        }

        public async Task Oprettelse()
        {
            await Shell.Current.GoToAsync(nameof(OprettelseBookingView));
        }

        public async Task PersonaleListe()
        {
            await Shell.Current.GoToAsync(nameof(PersonaleListeView));
        }

        [RelayCommand]
        async Task LogUd()
        {
            Personale p = MainViewModel.data.Getpersonal();
            personalesRepository.Update(p.CPR, p.Navn, p.Mail, p.ArbejdTlf, "0", p.Adresse, p.PrivatTlf);
            p = null;
           //await Shell.Current.GoToAsync(nameof(MainPage));
        }
    }

}
