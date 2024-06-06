using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SygehusKoordinering.Models
{
    // Created By Jakob Skindstad Frederiksen
    public class Booking : IComparable<Booking>
    {
        public string Id { get; set; }
        public string CPR { get; set; }
        public string Navn { get; set; }
        public string Afdeling { get; set; }
        public string AfdelingDecription { get; set; }
        public string StueEllerSengeplads { get; set; }
        public string Isolationspatient { get; set; }
        public List<string> Proeve { get; set; }
        public List<string> SaerligeForhold { get; set; }
        public string Inaktiv { get; set; }
        public string Prioritet { get; set; }
        public string BestiltTime { get; set; }
        public string BestiltDato { get; set; }
        public string Bestilt { get; set; }
        public string Kommentar { get; set; }
        public string CreatedAf { get; set; }
        public string TakedAf { get; set; }
        public string Begynd { get; set; }
        public string Done { get; set; }
        public Color RowColor { get; set; }
        public ImageSource Image { get; set; }

        public Booking()
        {
            Id = "";
            CPR = "";
            Navn = "";
            Afdeling = "";
            AfdelingDecription = "";
            StueEllerSengeplads = "";
            Isolationspatient = "";
            Proeve = new List<string>();
            SaerligeForhold = new List<string>();
            Inaktiv = "";
            Prioritet = "";
            BestiltTime = "";
            BestiltDato = "";
            Bestilt = "";
            Kommentar = "";
            CreatedAf = "";
            TakedAf = "";
            Begynd = "";
            Done = "";
        }

        public Booking(string id, string cPR, string navn, string afdeling, string afdelingDecription, string stueEllerSengeplads, string isolationspatient, List<string> proeve, List<string> saerligeForhold, string inaktiv, string prioritet, string bestiltTime, string bestiltDato, string bestilt, string kommentar, string createdAf, string takedAf, string begynd, string done)
        {
            Id = id;
            CPR = cPR;
            Navn = navn;
            Afdeling = afdeling;
            AfdelingDecription = afdelingDecription;
            StueEllerSengeplads = stueEllerSengeplads;
            Isolationspatient = isolationspatient;
            Proeve = proeve;
            SaerligeForhold = saerligeForhold;
            Inaktiv = inaktiv;
            Prioritet = prioritet;
            BestiltTime = bestiltTime;
            BestiltDato = bestiltDato;
            Bestilt = bestilt;
            Kommentar = kommentar;
            CreatedAf = createdAf;
            TakedAf = takedAf;
            Begynd = begynd;
            Done = done;
        }


        public override bool Equals(object obj)
        {
            try
            {
                Booking data = (Booking)obj;
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

        public int CompareTo(Booking data)
        {
            return Id.CompareTo(data.Id);
        }
    }
}
