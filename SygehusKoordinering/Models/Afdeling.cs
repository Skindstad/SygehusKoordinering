using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SygehusKoordinering.Models
{
    // Created By Jakob Skindstad Frederiksen
    public class Afdeling : IDataErrorInfo, IComparable<Afdeling>
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
        private static readonly string[] validatedProperties = { "Navn", "Omkring" };
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
                case "Omkring": return ValidateDecription();
                case "Location": return ValidateLocation();
            }
            return null;
        }
        private string? ValidateName()
        {
            if (Navn == null || Navn.Length == 0) return "Name can not be empty";
            return null;
        }

        private string? ValidateDecription()
        {
            if (Navn == null || Navn.Length == 0) return "Decription can not be empty";
            return null;
        }

        private string? ValidateLocation()
        {
            if (Location == null || Location.Length == 0) return "Location can not be empty";
            return null;
        }

    }
}
