using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SygehusKoordinering.Models
{
    public class Personale : IDataErrorInfo, IComparable<Personale>
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
        private static readonly string[] validatedProperties = { "Navn", "Mail", "ArbejdTlfNr", "Adgangskode", "CPRNr", "Adresse", "PrivatTlfNr" };
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
                case "Navn": return ValidateName();
                case "Mail": return ValidateMail();
                case "ArbejdTlfNr": return ValidateWorkPhone();
                case "Adgangskode": return ValidatePassword();
                case "CPRNr": return ValidateCPR();
                case "Adresse": return ValidateAddress();
                case "PrivatTlfNr": return ValidatePrivatPhone();
            }
            return null;
        }

        private string? ValidateWorkPhone()
        {
            if (ArbejdTlf.Length != 10) return "Phone must be a number of 10 digits";
            foreach (char c in ArbejdTlf) if (c < '0' || c > '9') return "Phone must be a number";
            return null;
        }
        private string? ValidatePrivatPhone()
        {
            if (PrivatTlf.Length != 10) return "Phone must be a number of 10 digits";
            foreach (char c in PrivatTlf) if (c < '0' || c > '9') return "Phone must be a number";
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
        private string? ValidateMail()
        {
            if (Mail == null || Mail.Length == 0) return "Email can not be empty";
            return null;
        }
        private string? ValidatePassword()
        {
            if (Adgangskode == null || Adgangskode.Length == 0) return "Password can not be empty";
            return null;
        }

        private string? ValidateAddress()
        {
            if (Adresse == null || Adresse.Length == 0) return "Address can not be empty";
            return null;
        }


    }
}
