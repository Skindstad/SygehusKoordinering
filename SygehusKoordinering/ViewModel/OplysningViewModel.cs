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

        public async void ItemTapped(Booking tappedBooking)
        {
           data.Clear();
           data.Add(tappedBooking);
           await Item();
        }


        [ObservableProperty]
        ObservableCollection<Booking>? localList;

        [ObservableProperty]
        string? afdeling;

        [ObservableProperty]
        string? stueEllerSengeplads;

        [ObservableProperty]
        string? proeve;

        [ObservableProperty]
        string? navn;
        [ObservableProperty]
        string? bestiltTime;

        [ObservableProperty]
        string? rowColor;

        [ObservableProperty]
        string? click;

        [ObservableProperty]
        ImageSource? image;


        public void Find()
        {
                LocalList = new ObservableCollection<Booking>();


                var bookings = bookingRepository.Search(LoginViewModel.Data.Getpersonal().Lokations, LoginViewModel.Data.Getpersonal().CPR);

           foreach (var booking in bookings)
           {
                    //Image 
                    booking.Image = Objects.ReturnImage(booking.TakedAf, booking.Begynd);
                    // Prioritet
                    booking.RowColor = Objects.ReturnPriorityColor(booking.Prioritet);

                    // Time and Date
                    booking.BestiltTime = Objects.ReturnTime(booking.Bestilt, booking.BestiltTime);

                    //Print
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
        public async Task ChangeLokation()
        {
            await Shell.Current.GoToAsync($"../");
        }

        public async Task PersonaleListe()
        {
            await Shell.Current.GoToAsync(nameof(PersonaleListeView));
        }

        // Cant find a way to "Restart" the application
        public async Task LogUd()
        {
            Personale p = LoginViewModel.Data.Getpersonal();
            personalesRepository.Update(p.CPR, p.Navn, p.Mail, p.ArbejdTlf, "0", p.Adresse, p.PrivatTlf);
            LoginViewModel.Data.Clear();
            await Shell.Current.GoToAsync($"../../");
        }
    }

}
