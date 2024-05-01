using Microsoft.Maui.Devices.Sensors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SygehusKoordinering.Models
{
    public class PersonaleOgLocation : IDataErrorInfo, IComparable<PersonaleOgLocation>
    {
        public string CPR {  get; set; }
        public string PersonName { get; set; }
        public string Mail { get; set; }
        public string ArbejdsTlfNr { get; set; }
        public string PrivatTlfNr { get; set; }
        public string Status { get; set; }
        public string LokationNavn { get; set; }
        public string Created { get; set; }

        public PersonaleOgLocation() 
        {
            CPR = "";
            PersonName = "";
            Mail = "";
            ArbejdsTlfNr = "";
            PrivatTlfNr = "";
            Status = "";
            LokationNavn = "";
            Created = "";
        }
            
        public PersonaleOgLocation(string cPR, string personName, string mail, string arbejdsTlfNr, string privatTlfNr, string status, string lokationNavn, string created)
        {
            CPR = cPR;
            PersonName = personName;
            Mail = mail;
            ArbejdsTlfNr = arbejdsTlfNr;
            PrivatTlfNr = privatTlfNr;
            Status = status;
            LokationNavn = lokationNavn;
            Created = created;
        }

        public override bool Equals(object obj)
        {
            try
            {
                PersonaleOgLocation data = (PersonaleOgLocation)obj;
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

        /*  public override string ToString()
          {
              return string.Format("{0} ", DataID);
          }*/

        // Implementerer ordning af objekter, så der alene sammenlignes på postnummer.
        public int CompareTo(PersonaleOgLocation data)
        {
            return CPR.CompareTo(data.CPR);
        }
        private static readonly string[] validatedProperties = { "CPR", "PersonNavn", "Mail", "ArbejdTlfNr", "PrivatTlfNr", "LocationNavn" };
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
                case "CPR": return ValidateCPR();
                case "PersonNavn": return ValidateName();
                case "Mail": return ValidateMail();
                case "ArbejdTlfNr": return ValidateWorkPhone();
                case "PrivatTlfNr": return ValidatePrivatPhone();
                case "LocationNavn": return ValidateLocationNavn();
            }
            return null;
        }

        private string? ValidateWorkPhone()
        {
            if (ArbejdsTlfNr.Length != 10) return "Phone must be a number of 10 digits";
            foreach (char c in ArbejdsTlfNr) if (c < '0' || c > '9') return "Phone must be a number";
            return null;
        }
        private string? ValidatePrivatPhone()
        {
            if (PrivatTlfNr.Length != 10) return "Phone must be a number of 10 digits";
            foreach (char c in PrivatTlfNr) if (c < '0' || c > '9') return "Phone must be a number";
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
            if (PersonName == null || PersonName.Length == 0) return "Name can not be empty";
            return null;
        }
        private string? ValidateMail()
        {
            if (Mail == null || Mail.Length == 0) return "Email can not be empty";
            return null;
        }
        private string? ValidateLocationNavn()
        {
            if (LokationNavn == null || LokationNavn.Length == 0) return "LocationNavn can not be empty";
            return null;
        }





    }
}
