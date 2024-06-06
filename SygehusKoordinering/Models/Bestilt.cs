using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SygehusKoordinering.Models
{
    // Created By Jakob Skindstad Frederiksen
    public class Bestilt : IComparable<Bestilt>
    {
        public string Id { get; set; }
        public string Navn { get; set; }

        public Bestilt()
        {
            Id = "";
            Navn = "";
        }

        public Bestilt(string id, string navn)
        {
            Id = id;
            Navn = navn;
        }

        public override bool Equals(object obj)
        {
            try
            {
                Bestilt data = (Bestilt)obj;
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

        public int CompareTo(Bestilt data)
        {
            return Id.CompareTo(data.Id);
        }

    }
}
