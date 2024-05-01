using Microsoft.Data.SqlClient;
using SygehusKoordinering.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SygehusKoordinering.DataAccess
{
    public class LocationRepository : Repository, IEnumerable<Locations>
    {
        private List<Locations> list = new List<Locations>();

        public IEnumerator<Locations> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Search(string Navn)
        {
            try
            {
                SqlCommand sqlCommand = new("Select Id, Navn From Lokation WHERE Navn LIKE @Name", connection);
                SqlCommand command = sqlCommand;
                command.Parameters.Add(CreateParam("@Name", Navn + "%", SqlDbType.NVarChar));
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                list.Clear();
                while (reader.Read())
                {
                    list.Add(new Locations(reader[0].ToString(), reader[1].ToString()));
                }

                OnChanged(DbOperation.SELECT, DbModeltype.Locations);
            }
            catch (Exception ex)
            {
                throw new DbException("Error in Location repositiory: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
        }


    }

    public class AfdelingRepository : Repository, IEnumerable<Afdeling>
    {
        private List<Afdeling> list = new List<Afdeling>();

        public IEnumerator<Afdeling> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Search(string AfdelingNavn, string LocationNavn)
        {
            try
            {
                SqlCommand sqlCommand = new("Select Afdeling.Id, Afdeling.Navn, Omkring, Lokation.Navn From Afdeling Join Lokation on Lokation.Id = Afdeling.Lokation WHERE Afdeling.Navn LIKE @AName OR Lokation.Navn LIKE @LName", connection);
                SqlCommand command = sqlCommand;
                command.Parameters.Add(CreateParam("@AName", AfdelingNavn + "%", SqlDbType.NVarChar));
                command.Parameters.Add(CreateParam("@LName", LocationNavn + "%", SqlDbType.NVarChar));
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                list.Clear();
                while (reader.Read())
                {
                    list.Add(new Afdeling(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString()));
                }

                OnChanged(DbOperation.SELECT, DbModeltype.Afdeling);
            }
            catch (Exception ex)
            {
                throw new DbException("Error in Location repositiory: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
        }

    }
}
