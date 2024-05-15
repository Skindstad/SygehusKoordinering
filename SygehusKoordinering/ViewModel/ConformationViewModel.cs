using CommunityToolkit.Mvvm.ComponentModel;
using SygehusKoordinering.DataAccess;
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
        public OplysningViewModel oplysning;

        public ConformationViewModel() 
        { 
            if(ItemViewModel.Complete == true)
            {
                Faediggoere = true;
                videreGiv = false;
            } else
            {
                Faediggoere = false;
                videreGiv = true;
            }
            
        }



        [ObservableProperty]
        bool faediggoere;

        [ObservableProperty]
        bool videreGiv;




        //void VidereGiv()
        //{
        //    bookings.Update(OplysningViewModel.data.GetBooking(), MainViewModel.data.Getpersonal().CPR, "1", OplysningViewModel.data.GetBooking().Kommentar, "0");


        //}
        //void Finish()
        //{
        //   bookings.Update(OplysningViewModel.data.GetBooking(), MainViewModel.data.Getpersonal().CPR, "1", OplysningViewModel.data.GetBooking().Kommentar, "1");


        //}

        async Task Oplysning()
        {
            await Shell.Current.GoToAsync(nameof(OplysningView));
        }

    }
}
