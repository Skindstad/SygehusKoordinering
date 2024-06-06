using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SygehusKoordinering.Models
{
    // Created By Jakob Skindstad Frederiksen
    public class Afdeling : IComparable<Afdeling>
    {
        public string Id { get; set; }
        public string Navn { get; set; }
        public string Omkring { get; set; }
        public string Location { get; set; }

        public Afdeling()
        {
            Id = "";
            Navn = "";
            Omkring = "";
            Location = "";
        }

        public Afdeling(string id, string navn, string omkring, string location)
        {
            Id = id;
            Navn = navn;
            Omkring = omkring;
            Location = location;
        }


        public override bool Equals(object obj)
        {
            try
            {
                Afdeling data = (Afdeling)obj;
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


        public int CompareTo(Afdeling data)
        {
            return Id.CompareTo(data.Id);
        }

    }
}
