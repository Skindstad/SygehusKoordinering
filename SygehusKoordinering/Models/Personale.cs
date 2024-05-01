﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SygehusKoordinering.Models
{
    public class Personale : IDataErrorInfo, IComparable<Personale>
    {
        public string CPRNr { get; set; }
        public string Navn { get; set; }
        public string Mail { get; set; }
        public string ArbejdTlfNr { get; set; }
        public string Adgangskode { get; set; }
        public string Adresse { get; set; }
        public string PrivatTlfNr { get; set; }
        public string Status { get; set; }
        public List<string> Lokations { get; set; }

        public Personale()
        {
            Navn = "";
            Mail = "";
            ArbejdTlfNr = "";
            Status = "";
            CPRNr = "";
            Adresse = "";
            PrivatTlfNr = "";
            Lokations = new List<string>();
        }


        public Personale(string cPRNr, string navn, string mail, string arbejdTlfNr, string status, string adresse, string privatTlfNr, List<string> lokations)
        {
            Navn = navn;
            Mail = mail;
            ArbejdTlfNr = arbejdTlfNr;
            Status = status;
            CPRNr = cPRNr;
            Adresse = adresse;
            PrivatTlfNr = privatTlfNr;
            Lokations = lokations;
        }

        public override bool Equals(object obj)
        {
            try
            {
                Personale data = (Personale)obj;
                return CPRNr.Equals(data.CPRNr);
            }
            catch
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return CPRNr.GetHashCode();
        }

        /*  public override string ToString()
          {
              return string.Format("{0} ", DataID);
          }*/

        // Implementerer ordning af objekter, så der alene sammenlignes på postnummer.
        public int CompareTo(Personale data)
        {
            return CPRNr.CompareTo(data.CPRNr);
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
            if (ArbejdTlfNr.Length != 10) return "Phone must be a number of 10 digits";
            foreach (char c in ArbejdTlfNr) if (c < '0' || c > '9') return "Phone must be a number";
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
            if (CPRNr.Length != 10) return "CPR must be a number of 10 digits";
            foreach (char c in CPRNr) if (c < '0' || c > '9') return "CPR must be a number";
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
