using Microsoft.Data.SqlClient;
using SygehusKoordinering.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SygehusKoordinering.DataAccess
{
    // Created By Jakob Skindstad Frederiksen
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
        // find Lokation
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
        // Få lokation fra navnet
        public static string GetLokation(string Navn)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
                SqlCommand sqlCommand = new("SELECT Id FROM Lokation" +
                    " WHERE Navn = @Navn", connection);
                SqlCommand command = sqlCommand;
                SqlParameter param = new("@Navn", SqlDbType.NVarChar);
                param.Value = Navn;
                command.Parameters.Add(param);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) return reader[0].ToString();
            }
            catch
            {
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
            return "";
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
        // find afdeling
        public List<string> GetAfdelinger()
        {
            List<string> list = new List<string>();
            try
            {
                SqlCommand sqlCommand = new("Select Navn From Afdeling", connection);
                SqlCommand command = sqlCommand;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                list.Clear();
                while (reader.Read())
                {
                    list.Add(reader[0].ToString());
                }
                return list;
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
        // få afdeling fra navn
        public static string GetAfdeling(string Navn)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
                SqlCommand sqlCommand = new("SELECT Id FROM Afdeling" +
                    " WHERE Navn = @Navn", connection);
                SqlCommand command = sqlCommand;
                SqlParameter param = new("@Navn", SqlDbType.NVarChar);
                param.Value = Navn;
                command.Parameters.Add(param);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) return reader[0].ToString();
            }
            catch
            {
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
            return "";
        }
        // få lokations navn  fra afdelingsnavn
        public static string GetLocationFromAfdeling(string AfdelingsNavn)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
                SqlCommand sqlCommand = new("Select Lokation.Navn from Afdeling" +
                    " Join Lokation on Lokation.Id = Afdeling.Lokation" +
                    " WHERE Afdeling.Navn = @Navn", connection);
                SqlCommand command = sqlCommand;
                SqlParameter param = new("@Navn", SqlDbType.NVarChar);
                param.Value = AfdelingsNavn;
                command.Parameters.Add(param);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) return reader[0].ToString();
            }
            catch
            {
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
            return "";
        }
    }
}
