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
    public class BestiltRepository : Repository, IEnumerable<Bestilt>
    {
        private List<Bestilt> list = new List<Bestilt>();

        public IEnumerator<Bestilt> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        //public void Search(string Navn)
        //{
        //    try
        //    {
        //        SqlCommand sqlCommand = new("Select Id, Navn From Bestilt WHERE Navn LIKE @Name", connection);
        //        SqlCommand command = sqlCommand;
        //        command.Parameters.Add(CreateParam("@Name", Navn + "%", SqlDbType.NVarChar));
        //        connection.Open();
        //        SqlDataReader reader = command.ExecuteReader();
        //        list.Clear();
        //        while (reader.Read())
        //        {
        //            list.Add(new Bestilt(reader[0].ToString(), reader[1].ToString()));
        //        }

        //        OnChanged(DbOperation.SELECT, DbModeltype.Bestilt);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new DbException("Error in Location repositiory: " + ex.Message);
        //    }
        //    finally
        //    {
        //        if (connection != null && connection.State == ConnectionState.Open) connection.Close();
        //    }
        //}

        public List<string> GetBestilts()
        {
            List<string> list = new List<string>();
            try
            {
                SqlCommand sqlCommand = new("Select Navn From Bestilt", connection);
                SqlCommand command = sqlCommand;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                list.Clear();
                while (reader.Read())
                {
                    list.Add(reader[0].ToString());
                }
                OnChanged(DbOperation.SELECT, DbModeltype.Bestilt);
                return list;
            }
            catch (Exception ex)
            {
                throw new DbException("Error in Bestilt repositiory: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
        }


        public static string GetBestiltWithName(string Navn)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
                SqlCommand sqlCommand = new("SELECT Id FROM Bestilt" +
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

    public class PrioritetRepository : Repository, IEnumerable<Prioritet>
    {
        private List<Prioritet> list = new List<Prioritet>();

        public IEnumerator<Prioritet> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        //public void Search(string Navn)
        //{
        //    try
        //    {
        //        SqlCommand sqlCommand = new("Select Id, Navn From Prioritet WHERE Navn LIKE @Name", connection);
        //        SqlCommand command = sqlCommand;
        //        command.Parameters.Add(CreateParam("@Name", Navn + "%", SqlDbType.NVarChar));
        //        connection.Open();
        //        SqlDataReader reader = command.ExecuteReader();
        //        list.Clear();
        //        while (reader.Read())
        //        {
        //            list.Add(new Prioritet(reader[0].ToString(), reader[1].ToString()));
        //        }

        //        OnChanged(DbOperation.SELECT, DbModeltype.Locations);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new DbException("Error in Prioritet repositiory: " + ex.Message);
        //    }
        //    finally
        //    {
        //        if (connection != null && connection.State == ConnectionState.Open) connection.Close();
        //    }
        //}

        public List<string> GetPrioritets()
        {
            List<string> list = new List<string>();
            try
            {
                SqlCommand sqlCommand = new("Select Navn From Prioritet", connection);
                SqlCommand command = sqlCommand;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                list.Clear();
                while (reader.Read())
                {
                    list.Add(reader[0].ToString());
                }
                OnChanged(DbOperation.SELECT, DbModeltype.Prioritet);
                return list;
            }
            catch (Exception ex)
            {
                throw new DbException("Error in Prioritet repositiory: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
        }


        public static string GetPrioritetWithName(string Navn)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
                SqlCommand sqlCommand = new("SELECT Id FROM Prioritet" +
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
    public class ProeveRepository : Repository, IEnumerable<Proeve>
    {
        private List<Proeve> list = new List<Proeve>();

        public IEnumerator<Proeve> GetEnumerator()
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
                SqlCommand sqlCommand = new("Select Id, Navn From Proeve WHERE Navn LIKE @Name", connection);
                SqlCommand command = sqlCommand;
                command.Parameters.Add(CreateParam("@Name", Navn + "%", SqlDbType.NVarChar));
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                list.Clear();
                while (reader.Read())
                {
                    list.Add(new Proeve(reader[0].ToString(), reader[1].ToString()));
                }

                OnChanged(DbOperation.SELECT, DbModeltype.Locations);
            }
            catch (Exception ex)
            {
                throw new DbException("Error in Proeve repositiory: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
        }

        //public List<string> GetPoever()
        //{
        //    List<string> list = new List<string>();
        //    try
        //    {
        //        SqlCommand sqlCommand = new("Select Navn From Proeve", connection);
        //        SqlCommand command = sqlCommand;
        //        connection.Open();
        //        SqlDataReader reader = command.ExecuteReader();
        //        list.Clear();
        //        while (reader.Read())
        //        {
        //            list.Add(reader[0].ToString());
        //        }
        //        return list;
        //        OnChanged(DbOperation.SELECT, DbModeltype.Bestilt);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new DbException("Error in Location repositiory: " + ex.Message);
        //    }
        //    finally
        //    {
        //        if (connection != null && connection.State == ConnectionState.Open) connection.Close();
        //    }
        //}


        public static string GetProeveWithName(string Navn)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
                SqlCommand sqlCommand = new("SELECT Id FROM Proeve" +
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
    public class SaerligeForholdRepository : Repository, IEnumerable<SaerligeForhold>
    {
        private List<SaerligeForhold> list = new List<SaerligeForhold>();

        public IEnumerator<SaerligeForhold> GetEnumerator()
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
                SqlCommand sqlCommand = new("Select Id, Navn From SaerligeForhold WHERE Navn LIKE @Name", connection);
                SqlCommand command = sqlCommand;
                command.Parameters.Add(CreateParam("@Name", Navn + "%", SqlDbType.NVarChar));
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                list.Clear();
                while (reader.Read())
                {
                    list.Add(new SaerligeForhold(reader[0].ToString(), reader[1].ToString()));
                }

                OnChanged(DbOperation.SELECT, DbModeltype.SaerligeForhold);
            }
            catch (Exception ex)
            {
                throw new DbException("Error in SaerligeForhold repositiory: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
        }

        //public List<string> GetSaerligeForholds()
        //{
        //    List<string> list = new List<string>();
        //    try
        //    {
        //        SqlCommand sqlCommand = new("Select Navn From SaerligeForhold", connection);
        //        SqlCommand command = sqlCommand;
        //        connection.Open();
        //        SqlDataReader reader = command.ExecuteReader();
        //        list.Clear();
        //        while (reader.Read())
        //        {
        //            list.Add(reader[0].ToString());
        //        }
        //        return list;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new DbException("Error in Location repositiory: " + ex.Message);
        //    }
        //    finally
        //    {
        //        if (connection != null && connection.State == ConnectionState.Open) connection.Close();
        //    }
        //}

        public static string GetSaerligeForholdWithName(string Navn)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
                SqlCommand sqlCommand = new("SELECT Id FROM SaerligeForhold" +
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

}
