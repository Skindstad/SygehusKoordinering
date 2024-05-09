using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SygehusKoordinering.Models
{
    public class Booking : IDataErrorInfo, IComparable<Booking>
    {
        public string Id { get; set; }
        public string CPR { get; set; }
        public string Navn { get; set; }
        public string Afdeling { get; set; }
        public string AfdelingDecription { get; set; }
        public string StueEllerSengeplads { get; set; }
        public string Isolationspatient { get; set; }
        public List<string> Proeve {  get; set; }
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
        public Color StateColor { get; set; }

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

        public Booking(string id, string cPR, string navn, string afdeling, string afdelingDecription, string stueEllerSengeplads, string isolationspatient, List<string> proeve, List<string> saerligeForhold, string inaktiv,string prioritet, string bestiltTime, string bestiltDato, string bestilt, string kommentar, string createdAf, string takedAf,string begynd, string done)
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

        public int CompareTo(Booking data)
        {
            return CPR.CompareTo(data.CPR);
        }
        private static readonly string[] validatedProperties = {"CPRNr", "Navn", "Afdeling", "StueEllerSengeplads", "Prioritet", "Bestilt", "Kommentar", "CreatedAf" };
        public bool IsValid
        {
            get
            {
                foreach (string property in validatedProperties) if (GetError(property) != null) return false;
                return true;
            }
        }

        string IDataErrorInfo.Error
        {
            get { return IsValid ? null : "Illegal model object"; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get { return Validate(propertyName); }
        }

        private string GetError(string name)
        {
            foreach (string property in validatedProperties) if (property.Equals(name)) return Validate(name);
            return null;
        }

        private string Validate(string name)
        {
            switch (name)
            {
                case "CPRNr": return ValidateCPR();
                case "Navn": return ValidateName();
                case "Afdeling": return ValidateDepartement();
                case "StueEllerSengeplads": return ValidateLivingroom();
                case "Kommentar": return ValidateDecription();
                case "Prioritet": return ValidatePrioritet();
                case "CreatedAf": return ValidateCreated();
                case "Bestilt": return ValidateOrder();
            }
            return null;
        }
        private string? ValidateCPR()
        {
            if (CPR.Length != 10) return "CPR must be a number of 10 digits";
            foreach (char c in CPR) if (c < '0' || c > '9') return "CPR must be a number";
            return null;
        }


        private string? ValidateName()
        {
            if (Navn == null || Navn.Length == 0) return "Name can not be empty";
            return null;
        }
        private string? ValidateDepartement()
        {
            if (Afdeling == null || Afdeling.Length == 0) return "Departement can not be empty";
            return null;
        }
        private string? ValidateLivingroom()
        {
            if (StueEllerSengeplads == null || StueEllerSengeplads.Length == 0) return "Livingroom or Bed space can not be empty";
            return null;
        }
        private string? ValidateDecription()
        {
            if (Kommentar == null || Kommentar.Length == 0) return "Decription can not be empty";
            return null;
        }
        private string? ValidatePrioritet()
        {
            if (Prioritet == null || Prioritet.Length == 0) return "Prioritet can not be empty";
            return null;
        }
        private string? ValidateOrder()
        {
            if (Bestilt == null || Bestilt.Length == 0) return "Order can not be empty";
            return null;
        }

        private string? ValidateCreated()
        {
            if (CreatedAf == null || CreatedAf.Length == 0) return "Created can not be empty";
            return null;
        }

    }
}
