﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SygehusKoordinering.Models
{
    // Created By Jakob Skindstad Frederiksen
    public class SaerligeForhold : IDataErrorInfo, IComparable<SaerligeForhold>
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
        private static readonly string[] validatedProperties = { "Navn" };
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
            }
            return null;
        }
        private string? ValidateName()
        {
            if (Navn == null || Navn.Length == 0) return "Name can not be empty";
            return null;
        }


    }
}
