using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SygehusKoordinering.Models
{
    // Created By Jakob Skindstad Frederiksen
    public class Prioritet : IComparable<Prioritet>
    {
        public string Id { get; set; }
        public string Navn { get; set; }

        public Prioritet()
        {
            Navn = "";
        }

        public Prioritet(string id, string navn)
        {
            Id = id;
            Navn = navn;
        }

        public override bool Equals(object obj)
        {
            try
            {
                Prioritet data = (Prioritet)obj;
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

        public int CompareTo(Prioritet data)
        {
            return Id.CompareTo(data.Id);
        }

    }
}
