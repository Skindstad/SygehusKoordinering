using Microsoft.Data.SqlClient;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SygehusKoordinering.DataAccess
{
    // Definerer typen af hhv. den databaseoperation, der er udført og af hvilket repository.
    public enum DbOperation { SELECT, INSERT, UPDATE, DELETE };
    public enum DbModeltype { Personal, Afdeling, Location, Booking }

    // EventArgs for en databaseoperation.
    public class DbEventArgs(DbOperation operation, DbModeltype modeltype) : EventArgs
    {
        public DbOperation Operation { get; private set; } = operation;
        public DbModeltype Modeltype { get; private set; } = modeltype;
    }
    // Exception type programmets repositories.
    public class DbException(string message) : Exception(message)
    {
    }

    public delegate void DbEventHandler(object sender, DbEventArgs e);

    // Basisklasse til et Repository.
    // Klassens konstruktør er defineret protected, da det ikke giver nogen mening at instantiere klassen.
    public class Repository
    {
        public event DbEventHandler RepositoryChanged;
        protected SqlConnection connection = null;

        protected Repository()
        {
            try
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
            }
            catch (Exception ex)
            {
                throw new DbException("Error in repositiory: " + ex.Message);
            }
        }

        public void OnChanged(DbOperation opr, DbModeltype mt)
        {
            RepositoryChanged?.Invoke(this, new DbEventArgs(opr, mt));
        }

        protected static SqlParameter CreateParam(string name, object value, SqlDbType type)
        {
            SqlParameter param = new(name, type)
            {
                Value = value
            };
            return param;
        }
    }
}
