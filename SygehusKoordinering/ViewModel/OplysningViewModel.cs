using CommunityToolkit.Mvvm.ComponentModel;
using SygehusKoordinering.DataAccess;
using SygehusKoordinering.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SygehusKoordinering.ViewModel
{
    public partial class OplysningViewModel : ObservableObject
    {
        public static BookingRepository bookingRepository = [];


        public OplysningViewModel()
        {
            Find();
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

        void Find()
        {
            
            bookingRepository.Search("", "", "");

            foreach (var booking in bookingRepository)
            {
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
                DateTime time = DateTime.Parse(booking.BestiltTime);
                string formattedTime = time.ToString("HH:mm");
                booking.BestiltTime = formattedTime;
                booking.Afdeling = booking.Afdeling + ", " + booking.StueEllerSengeplads;
            }
            LocalList = new ObservableCollection<Booking>(bookingRepository);


            
        }




    }

}
