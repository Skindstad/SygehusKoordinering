using SygehusKoordinering.DataAccess;
using SygehusKoordinering.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SygehusKoordinering.Object
{
    // Created By Jakob Skindstad Frederiksen
    public class LoginData
    {
        private Personale data = null;
        private List<Display> Displays = new List<Display>();
        public void Add(Personale p)
        {
            data = p;
        }

        public Personale Getpersonal()
        {
            return data;
        }
        public List<Display> GetDisplays()
        {
            return Displays;
        }

        public void AddDisplay(Station location)
        {
            Displays.Add(new Display(location));
        }


        public void AddLocation(string location)
        {
            data.Lokations.Add(location);
        }
        public void ClearLocation()
        {
            Displays.Clear();
            data.Lokations.Clear();
            BundleRepository.removeLocation(data.CPR);
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
