using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SygehusKoordinering.Models
{
    // Created By Jakob Skindstad Frederiksen
    public class Proeve : IComparable<Proeve>
    {
        public string Id { get; set; }
        public string Navn { get; set; }

        private bool isSelectedProeve;
        public bool IsSelectedProeve
        {
            get { return isSelectedProeve; }
            set { isSelectedProeve = value; }
        }
        public Proeve()
        {
            Id = "";
            Navn = "";
            IsSelectedProeve = false;
        }

        public Proeve(string id, string navn)
        {
            Id = id;
            Navn = navn;
            IsSelectedProeve = false;
        }

        public override bool Equals(object obj)
        {
            try
            {
                Proeve data = (Proeve)obj;
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

        public int CompareTo(Proeve data)
        {
            return Id.CompareTo(data.Id);
        }

    }
}
