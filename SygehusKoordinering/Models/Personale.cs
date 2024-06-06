using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SygehusKoordinering.Models
{
    // Created By Jakob Skindstad Frederiksen
    public class Personale : IComparable<Personale>
    {
        public string CPR { get; set; }
        public string Navn { get; set; }
        public string Mail { get; set; }
        public string ArbejdTlf { get; set; }
        public string Adgangskode { get; set; }
        public string Adresse { get; set; }
        public string PrivatTlf { get; set; }
        public string Status { get; set; }
        public List<string> Lokations { get; set; }

        public Personale()
        {
            Navn = "";
            Mail = "";
            ArbejdTlf = "";
            Status = "";
            CPR = "";
            Adresse = "";
            PrivatTlf = "";
            Lokations = new List<string>();
        }


        public Personale(string cPR, string navn, string mail, string arbejdTlfNr, string status, string adresse, string privatTlfNr, List<string> lokations)
        {
            Navn = navn;
            Mail = mail;
            ArbejdTlf = arbejdTlfNr;
            Status = status;
            CPR = cPR;
            Adresse = adresse;
            PrivatTlf = privatTlfNr;
            Lokations = lokations;
        }

        public override bool Equals(object obj)
        {
            try
            {
                Personale data = (Personale)obj;
                return CPR.Equals(data.CPR);
            }
            catch
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return CPR.GetHashCode();
        }

        public int CompareTo(Personale data)
        {
            return CPR.CompareTo(data.CPR);
        }

    }
}
