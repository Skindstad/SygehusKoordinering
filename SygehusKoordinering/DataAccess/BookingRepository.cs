﻿using Microsoft.Data.SqlClient;
using SygehusKoordinering.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SygehusKoordinering.DataAccess
{
    // Created By Jakob Skindstad Frederiksen
    public class BookingRepository : Repository, IEnumerable<Booking>
    {
        private List<Booking> list = new List<Booking>();

        public IEnumerator<Booking> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        // Finde Booking
        public List<Booking> Search(List<string> Lokation, string PersonaleCPRTaken)
        {
            List<string> locationId = new List<string>();
            try
            {
                foreach (var item in Lokation)
                {
                    string LokationId = LocationRepository.GetLokation(item);
                    locationId.Add(LokationId);
                }
                StringBuilder dataLocation = new StringBuilder();
                foreach (var item in locationId)
                {
                    dataLocation.Append($"'{item}', ");
                }
                dataLocation.Remove(dataLocation.Length - 2, 2);

                SqlCommand sqlCommand = new("Select Booking.Id, Booking.CPR, Booking.Navn, Afdeling.Navn, Afdeling.Omkring, StueEllerSengeplads, Isolationspatient, Inaktiv, Prioritet.Navn, BestiltTime, BestiltDato, Bestilt.Navn, Kommentar, c.Navn as CreatedAf, t.Navn as TakenAf, Begynd,  Done " +
                    "From Booking JOIN Afdeling ON Booking.Afdeling = Afdeling.Id " +
                    "JOIN Prioritet ON Booking.Prioritet = Prioritet.Id " +
                    "JOIN Bestilt ON Booking.Bestilt = Bestilt.Id " +
                    "JOIN Personale c ON c.CPR = Booking.CreatedAf " +
                    "LEFT JOIN Personale t ON t.CPR = Booking.TakedAf " +
                    $"WHERE Afdeling.Lokation IN ({dataLocation}) AND (t.CPR = @PersonaleCPRTaken OR t.CPR IS NULL) AND Done = 'False'" +
                    "ORDER BY Prioritet.Id DESC, BestiltTime ASC", connection);
                SqlCommand command = sqlCommand;
                command.Parameters.Add(CreateParam("@PersonaleCPRTaken", PersonaleCPRTaken , SqlDbType.NVarChar));
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                list.Clear();
                while (reader.Read())
                {
                    list.Add(new Booking(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), GetProeve(reader[0].ToString()), GetSaerligeForhold(reader[0].ToString()), reader[7].ToString(),
                        reader[8].ToString(), reader[9].ToString(), reader[10].ToString(), reader[11].ToString(), reader[12].ToString(), reader[13].ToString(), reader[14].ToString(), reader[15].ToString(), reader[16].ToString()));
                }
                OnChanged(DbOperation.SELECT, DbModeltype.Booking);
            }
            catch (Exception ex)
            {
                throw new DbException("Error in Booking repositiory: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
            return list;
        }
        // Find Booking persons prøver
        public static List<string> GetProeve(string Id)
        {
            SqlConnection connection = null;
            List<string> proeve = new List<string>();
            try
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
                SqlCommand sqlCommand = new("SELECT Proeve.Navn FROM Booking " +
                    "JOIN BookedForProeve ON BookedForProeve.Booked = Booking.Id " +
                    "JOIN Proeve ON BookedForProeve.Proeve = Proeve.Id " +
                    "WHERE Booked = @Id", connection);
                SqlCommand command = sqlCommand;
                SqlParameter param = new("@Id", SqlDbType.NVarChar);
                param.Value = Id;
                command.Parameters.Add(param);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    proeve.Add(reader[0].ToString());
                }
                return proeve;
            }
            catch
            {
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
            return null;
        }
        // Find Booking persons Særlige Forhold
        public static List<string> GetSaerligeForhold(string Id)
        {
            SqlConnection connection = null;
            List<string> saerligeForhold = new List<string>();
            try
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
                SqlCommand sqlCommand = new("SELECT SaerligeForhold.Navn FROM Booking " +
                    "JOIN BookedForSaerligeForhold ON Booking.Id = BookedForSaerligeForhold.Booked " +
                    "JOIN SaerligeForhold ON BookedForSaerligeForhold.SaerligeForhold = SaerligeForhold.Id " +
                    "WHERE Booked = @Id", connection);
                SqlCommand command = sqlCommand;
                SqlParameter param = new("@Id", SqlDbType.NVarChar);
                param.Value = Id;
                command.Parameters.Add(param);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    saerligeForhold.Add(reader[0].ToString());
                }
                return saerligeForhold;
            }
            catch
            {
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
            return null;
        }
        // Skabe Booking
        public void Add(Booking data)
        {
            string error = "";
            BundleRepository bundleRepository = new BundleRepository();
            if (data.CPR != string.Empty && data.Navn != string.Empty && data.Afdeling != string.Empty && data.StueEllerSengeplads != string.Empty  && data.Prioritet.Length > 0 && data.BestiltTime.Length > 0 && data.BestiltDato.Length > 0 
                && data.Bestilt.Length > 0)
            {
                string AfdelingId = AfdelingRepository.GetAfdeling(data.Afdeling);
                string PrioritetId = PrioritetRepository.GetPrioritetWithName(data.Prioritet);
                string BestiltId = BestiltRepository.GetBestiltWithName(data.Bestilt);
                    try
                    {
                        SqlCommand sqlCommand = new("INSERT INTO Booking (CPR, Navn, Afdeling, StueEllerSengeplads,Isolationspatient, Inaktiv,  Prioritet, BestiltTime, BestiltDato, Bestilt, Kommentar, CreatedAf) VALUES (@CPR, @Navn, @Afdeling, @StueEllerSengeplads,@Isolationspatient, @Inaktiv, @Prioritet,@BestiltTime, @BestiltDato, @Bestilt, @Kommentar, @CreatedAf)", connection);
                        SqlCommand command = sqlCommand;
                        command.Parameters.Add(CreateParam("@CPR", data.CPR, SqlDbType.NVarChar));
                        command.Parameters.Add(CreateParam("@Navn", data.Navn, SqlDbType.NVarChar));
                        command.Parameters.Add(CreateParam("@Afdeling", AfdelingId, SqlDbType.NVarChar));
                        command.Parameters.Add(CreateParam("@StueEllerSengeplads", data.StueEllerSengeplads, SqlDbType.NVarChar));
                        command.Parameters.Add(CreateParam("@Isolationspatient", data.Isolationspatient, SqlDbType.NVarChar));
                        command.Parameters.Add(CreateParam("@Inaktiv", data.Inaktiv, SqlDbType.NVarChar));
                        command.Parameters.Add(CreateParam("@Prioritet", PrioritetId, SqlDbType.NVarChar));
                        command.Parameters.Add(CreateParam("@BestiltTime", data.BestiltTime, SqlDbType.NVarChar));
                        command.Parameters.Add(CreateParam("@BestiltDato", data.BestiltDato, SqlDbType.NVarChar));
                        command.Parameters.Add(CreateParam("@Bestilt", BestiltId, SqlDbType.NVarChar));
                        command.Parameters.Add(CreateParam("@Kommentar", data.Kommentar, SqlDbType.NVarChar));
                        command.Parameters.Add(CreateParam("@CreatedAf", data.CreatedAf, SqlDbType.NVarChar));
                        connection.Open();
                        if (command.ExecuteNonQuery() == 1)
                        {
                        string id = GetBooking(data.CPR, data.BestiltTime, data.BestiltDato);
                            foreach (var item in data.Proeve)
                            {
                                bundleRepository.AddBookedToProeve(id, item);
                            }
                            foreach (var item in data.SaerligeForhold)
                            {
                                bundleRepository.AddBookedToSaerligeForhold(id, item);
                            }
                        list.Add(data);
                            list.Sort();
                            OnChanged(DbOperation.INSERT, DbModeltype.Booking);
                            return;
                        }
                        error = string.Format("Booking could not be inserted in the database");
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
            else error = "Illegal value for Booking";
            Console.WriteLine(error);
            // throw new DbException("Error in Data repositiory: " + error);
        }
        // Finde Bookingen skaber
        public Personale FindCreatedAf(string Id)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
                SqlCommand sqlCommand = new("SELECT CreatedAf FROM Booking " +
                    "WHERE Id = @Id", connection);
                SqlCommand command = sqlCommand;
                SqlParameter param = new("@Id", SqlDbType.NVarChar);
                param.Value = Id;
                command.Parameters.Add(param);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) return PersonaleRepository.GetPerson(reader[0].ToString());
            }
            catch
            {
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
            return null;
        }

        // Finde den rette Bookingen
        public static string GetBooking(string CPR, string Time, string date)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
                SqlCommand sqlCommand = new("SELECT ID FROM Booking " +
                    "WHERE CPR = @CPR AND BestiltTime = @Time AND BestiltDato = @Date", connection);
                SqlCommand command = sqlCommand;
                SqlParameter param = new("@CPR", SqlDbType.NVarChar);
                SqlParameter param2 = new("@Time", SqlDbType.NVarChar);
                SqlParameter param3 = new("@Date", SqlDbType.NVarChar);
                param.Value = CPR;
                param2.Value = Time;
                param3.Value = date;
                command.Parameters.Add(param);
                command.Parameters.Add(param2);
                command.Parameters.Add(param3);
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
            return null;
        }
        // Få den nyeste update på booking
        public static string GetUpdatedTime(string Id)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
                SqlCommand sqlCommand = new("SELECT Updated FROM Booking " +
                    "WHERE Id = @Id", connection);
                SqlCommand command = sqlCommand;
                SqlParameter param = new("@Id", SqlDbType.NVarChar);
                param.Value = Id;
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
            return null;
        }


        // Opdater Bookingen
        public void Update(Booking booking, string takenAf, string Begynd, string Kommentar, string Done)
        {
            string error = "";
            if (booking.Id != string.Empty)
            {
                try
                {
                    DateTime updated = DateTime.Now;
                    string updatedString = updated.ToString("yyyy-MM-dd HH:mm");
                    SqlCommand sqlCommand = new("UPDATE Booking SET TakedAf = @TakedAf, Begynd = @Begynd, Done = @Done, Kommentar = @Kommentar, Updated = @Updated WHERE Id = @Id", connection);
                    SqlCommand command = sqlCommand;
                    command.Parameters.Add(CreateParam("@Id", booking.Id, SqlDbType.NVarChar));
                    if(takenAf != null)
                    {
                        command.Parameters.Add(CreateParam("@TakedAf", takenAf, SqlDbType.NVarChar));
                    } else
                    {
                        SqlParameter takedAfParam = new SqlParameter("@TakedAf", (object)takenAf ?? DBNull.Value);
                        command.Parameters.Add(takedAfParam);
                    }
                    
                    command.Parameters.Add(CreateParam("@Begynd", Begynd, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@Done", Done, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@Kommentar", Kommentar, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@Updated", updated, SqlDbType.DateTime));
                    connection.Open();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        UpdateList(booking, takenAf, Begynd, Kommentar, Done);
                        OnChanged(DbOperation.UPDATE, DbModeltype.Booking);
                        return;
                    }
                    error = string.Format("Booking could not be updated");
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
            else error = "Illegal value for Booking";
        }

        private void UpdateList(Booking booking, string takenAf, string Begynd, string Kommentar, string Done)
        {
            for (int i = 0; i < list.Count; ++i)
                if (list[i].Id.Equals(booking.Id))
                {
                    list[i].TakedAf = takenAf;
                    list[i].Begynd = Begynd;
                    list[i].Kommentar = Kommentar;
                    list[i].Done = Done;
                    break;
                }
        }

    }
}

