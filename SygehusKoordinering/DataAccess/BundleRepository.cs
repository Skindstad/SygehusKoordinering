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
    public class BundleRepository : Repository, IEnumerable<Personale>
    {
        private List<Personale> list = new List<Personale>();

        public IEnumerator<Personale> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void AddLocationsToPersonale(string CPR, string LokationNavn)
        {
            string error = "";
            if (CPR.Length == 9 && LokationNavn.Length > 0)
            {
                string LokationId = LocationRepository.GetLokation(LokationNavn);
                if (LokationId != null)
                {
                    try
                    {
                        SqlCommand sqlCommand = new("INSERT INTO PersonalePaaLokation (Personal, Lokation) VALUES (@Personale, @Lokation)", connection);
                        SqlCommand command = sqlCommand;
                        command.Parameters.Add(CreateParam("@Personale", CPR, SqlDbType.NVarChar));
                        command.Parameters.Add(CreateParam("@Lokation", LokationId, SqlDbType.NVarChar));
                        connection.Open();
                        if (command.ExecuteNonQuery() == 1)
                        {
                            OnChanged(DbOperation.INSERT, DbModeltype.Personale);
                            return;
                        }
                        error = string.Format("PersonalePaaLokation could not be inserted in the database");
                    }
                    catch (Exception ex)
                    {
                        error = ex.Message;
                    }
                    finally
                    {
                        if (connection != null && connection.State == ConnectionState.Open) connection.Close();
                    }
                }
            }
            else error = "Illegal value for PersonalePaaLokation";
            Console.WriteLine(error);
            // throw new DbException("Error in Data repositiory: " + error);
        }


        public static void removeLocation(string CPR)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
                SqlCommand sqlCommand = new("DELETE FROM PersonalePaaLokation" +
                    " WHERE Personal = @CPR", connection);
                SqlCommand command = sqlCommand;
                SqlParameter param = new("@CPR", SqlDbType.NVarChar);
                param.Value = CPR;
                command.Parameters.Add(param);
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch
            {
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
        }

        public void AddBookedToProeve(string BookingId, string ProeveNavn)
        {
            string error = "";
            if (BookingId != string.Empty && ProeveNavn.Length > 0)
            {
                string ProveId = ProeveRepository.GetProeveWithName(ProeveNavn);
                if (ProveId != null)
                {
                    try
                    {
                        SqlCommand sqlCommand = new("INSERT INTO BookedForProeve (Booked, Proeve) VALUES (@BookingId, @ProveId)", connection);
                        SqlCommand command = sqlCommand;
                        command.Parameters.Add(CreateParam("@BookingId", BookingId, SqlDbType.NVarChar));
                        command.Parameters.Add(CreateParam("@ProveId", ProveId, SqlDbType.NVarChar));
                        connection.Open();
                        if (command.ExecuteNonQuery() == 1)
                        {
                            OnChanged(DbOperation.INSERT, DbModeltype.Booking);
                            return;
                        }
                        error = string.Format("BookedForProeve could not be inserted in the database");
                    }
                    catch (Exception ex)
                    {
                        error = ex.Message;
                    }
                    finally
                    {
                        if (connection != null && connection.State == ConnectionState.Open) connection.Close();
                    }
                }
            }
            else error = "Illegal value for BookedForProeve";
            Console.WriteLine(error);
            // throw new DbException("Error in Data repositiory: " + error);
        }

        //public static void removeProeve(string Id)
        //{
        //    SqlConnection connection = null;
        //    try
        //    {
        //        connection = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
        //        SqlCommand sqlCommand = new("DELETE FROM BookedForProeve" +
        //            " WHERE Booked = @ID", connection);
        //        SqlCommand command = sqlCommand;
        //        SqlParameter param = new("@ID", SqlDbType.NVarChar);
        //        param.Value = Id;
        //        command.Parameters.Add(param);
        //        connection.Open();
        //        command.ExecuteNonQuery();
        //    }
        //    catch
        //    {
        //    }
        //    finally
        //    {
        //        if (connection != null && connection.State == ConnectionState.Open) connection.Close();
        //    }
        //}

        public void AddBookedToSaerligeForhold(string BookingId, string SaerligeForholdNavn)
        {
            string error = "";
            if (BookingId != string.Empty && SaerligeForholdNavn.Length > 0)
            {
                string SaerligeForholdId = SaerligeForholdRepository.GetSaerligeForholdWithName(SaerligeForholdNavn);
                if (SaerligeForholdId != null)
                {
                    try
                    {
                        SqlCommand sqlCommand = new("INSERT INTO BookedForSaerligeForhold (Booked, SaerligeForhold) VALUES (@BookingId, @ProveId)", connection);
                        SqlCommand command = sqlCommand;
                        command.Parameters.Add(CreateParam("@BookingId", BookingId, SqlDbType.NVarChar));
                        command.Parameters.Add(CreateParam("@SaerligeForholdId", SaerligeForholdId, SqlDbType.NVarChar));
                        connection.Open();
                        if (command.ExecuteNonQuery() == 1)
                        {
                            OnChanged(DbOperation.INSERT, DbModeltype.Booking);
                            return;
                        }
                        error = string.Format("BookedForSaerligeForhold could not be inserted in the database");
                    }
                    catch (Exception ex)
                    {
                        error = ex.Message;
                    }
                    finally
                    {
                        if (connection != null && connection.State == ConnectionState.Open) connection.Close();
                    }
                }
            }
            else error = "Illegal value for BookedForProeve";
            Console.WriteLine(error);
            // throw new DbException("Error in Data repositiory: " + error);
        }

        //public static void removeSaerligeForhold(string Id)
        //{
        //    SqlConnection connection = null;
        //    try
        //    {
        //        connection = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
        //        SqlCommand sqlCommand = new("DELETE FROM BookedForSaerligeForhold" +
        //            " WHERE Booked = @Id", connection);
        //        SqlCommand command = sqlCommand;
        //        SqlParameter param = new("@Id", SqlDbType.NVarChar);
        //        param.Value = Id;
        //        command.Parameters.Add(param);
        //        connection.Open();
        //        command.ExecuteNonQuery();
        //    }
        //    catch
        //    {
        //    }
        //    finally
        //    {
        //        if (connection != null && connection.State == ConnectionState.Open) connection.Close();
        //    }
        //}

    }
}
