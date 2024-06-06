using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SygehusKoordinering.Models
{
    // Created By Jakob Skindstad Frederiksen
    public class SaerligeForhold : IComparable<SaerligeForhold>
    {
        public string Id { get; set; }
        public string Navn { get; set; }

        private bool isSelectedSaerlig;
        public bool IsSelectedSaerlig
        {
            get { return isSelectedSaerlig; }
            set { isSelectedSaerlig = value; }
        }
        public SaerligeForhold()
        {
            Id = "";
            Navn = "";
            IsSelectedSaerlig = false;
        }

        public SaerligeForhold(string id, string navn)
        {
            Id = id;
            Navn = navn;
            IsSelectedSaerlig = false;
        }

        public override bool Equals(object obj)
        {
            try
            {
                SaerligeForhold data = (SaerligeForhold)obj;
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

        public int CompareTo(SaerligeForhold data)
        {
            return Id.CompareTo(data.Id);
        }

    }
}
