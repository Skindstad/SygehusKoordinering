using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SygehusKoordinering.Models
{
    // Created By Jakob Skindstad Frederiksen
    public class Locations : IComparable<Locations>
    {
        public string Id { get; set; }
        public string Navn { get; set; }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; }
        }

        public Locations()
        {
            Id = "";
            Navn = "";
            IsSelected = false;
        }

        public Locations(string id, string navn)
        {
            Id = id;
            Navn = navn;
            IsSelected = false;
        }

        public override bool Equals(object obj)
        {
            try
            {
                Locations data = (Locations)obj;
                return Id.Equals(data.Id);
            }
            catch
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public int CompareTo(Locations data)
        {
            return Id.CompareTo(data.Id);
        }


    }
}
