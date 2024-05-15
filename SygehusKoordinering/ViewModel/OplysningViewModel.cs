using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SygehusKoordinering.Assets;
using SygehusKoordinering.DataAccess;
using SygehusKoordinering.Models;
using SygehusKoordinering.View;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SygehusKoordinering.ViewModel
{
    public partial class OplysningViewModel : ObservableObject
    {
        public static BookingRepository bookingRepository = new BookingRepository();
        public static OplysningData data;
        public static PersonaleRepository personalesRepository = [];

        public OplysningViewModel()
        {
            foreach (var item in MainViewModel.data.GetDisplays())
            {
                item.Oplysning(this);
            }
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
                    switch (booking.Prioritet)
                    {
                        case "Normal":
                            booking.RowColor = Colors.Blue;
                            break;
                        case "Haster":
                            booking.RowColor = Colors.Orange;
                            break;
                        case "Livstruende":
                            booking.RowColor = Colors.Red;
                            break;
                        default:
                            booking.RowColor = Colors.Transparent;
                            break;
                    }

                    // Time and Date
                    DateTime time = DateTime.Parse(booking.BestiltTime);
                    string formattedTime = time.ToString("HH:mm");
                    string formatEstra;
                    DateTime estra;
                    if(booking.Bestilt == "Til Bestilt tid")
                    {
                        booking.BestiltTime = formattedTime;
                    }
                    if (booking.Bestilt == "Inden for 1 time")
                    {
                        estra = time.AddHours(-1);
                        formatEstra = estra.ToString("HH:mm");
                        booking.BestiltTime = formatEstra + " - " + formattedTime;
                    }
                    if (booking.Bestilt == "Inden for 2 time")
                    {
                        estra = time.AddHours(-2);
                        formatEstra = estra.ToString("HH:mm");
                        booking.BestiltTime = formatEstra + " - " + formattedTime;
                    }
                    if (booking.Bestilt == "Inden for 3 time")
                    {
                        estra = time.AddHours(-3);
                        formatEstra = estra.ToString("HH:mm");
                        booking.BestiltTime = formatEstra + " - " + formattedTime;
                    }


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
           //await Shell.Current.GoToAsync(nameof(MainPage));
        }
    }
    public class OplysningData
    {
        public Booking data;

        public Booking GetBooking()
        {
            return data;
        }
        public void Add(Booking booking)
        {
            data = booking;
        }
    }

}
