﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SygehusKoordinering.DataAccess;
using SygehusKoordinering.Models;
using SygehusKoordinering.Object;
using SygehusKoordinering.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SygehusKoordinering.ViewModel
{
    // Create Af Jakob Skindstad Frederiksen
    public partial class ConformationViewModel : ObservableObject
    {
        public BookingRepository bookings = [];

        public ConformationViewModel() 
        {
            // gets the inforamtion it got form oplysningView
            Booking Book = OplysningViewModel.data.GetBooking();
            bookings = new BookingRepository();


            //Check to see what need to be show
            if (ItemViewModel.Complete == true)
            {
                Faediggoere = true;
                VidereGivHidden = false;

                // Data alway the same.
                Tid = Objects.tids;
                Aarsag = Objects.ListAarsag;
                selectedAarsag = aarsag[0];


            } else
            {
                Faediggoere = false;
                VidereGivHidden = true;

                // make sure the date and time is right
                DateTime time = DateTime.Parse(Book.BestiltDato);
                Date = time.ToString("yyyy-MM-dd");

                DateTime UpdatedTime = DateTime.Parse(BookingRepository.GetUpdatedTime(Book.Id));
                Time = UpdatedTime.ToString("HH:mm");

                // Afdeling and Stue og sengeplads
                Afdeling = Book.Afdeling;
                StueEllerSengeplads = Book.StueEllerSengeplads;


                // Proeve
                Proeve = Objects.ReturnProeveString(Book.Proeve, " / ");

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

        [ObservableProperty]
        string proeve;

        // Make sure the Kommentar with all the right information before it get send to databasen
        [RelayCommand]
        void VidereGiv()
        {
            string newKommentar = OplysningViewModel.data.GetBooking().Kommentar + "\n" + videreGivKommentar;
            bookings.Update(OplysningViewModel.data.GetBooking(), null, "0", newKommentar, "0");
            Objects.SendNotify(afdeling, "");
            Oplysning();

        }

        // Make sure the Kommentar with all the right information before it get send to databasen
        [RelayCommand]
        void Finish()
        {
            string newKommentar = OplysningViewModel.data.GetBooking().Kommentar + "\n" + selectedTid + "\n" + selectedAarsag;
            bookings.Update(OplysningViewModel.data.GetBooking(), LoginViewModel.Data.Getpersonal().CPR, "1", newKommentar, "1");
            Objects.SendNotify(afdeling, "");
            Oplysning();
        }

        //Send you to OplysningsView
        async Task Oplysning()
        {
            await Shell.Current.GoToAsync($"../../");
        }

    }
}
