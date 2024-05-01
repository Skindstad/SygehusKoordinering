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

        public void Search(string CPR, string PNavn, string PersonaleNavn)
        {
            try
            {
                SqlCommand sqlCommand = new("Select Booking.Id, Booking.CPR, Booking.Navn, Afdeling.Navn, StueEllerSengeplads, Isolationspatient, Inaktiv, Prioritet.Navn, BestiltTime, BestiltDato, Bestilt.Navn, Kommentar, c.Navn as CreatedAf, t.Navn as TakenAf, Done " +
                    "From Booking JOIN Afdeling ON Booking.Afdeling = Afdeling.Id " +
                    "JOIN Prioritet ON Booking.Prioritet = Prioritet.Id " +
                    "JOIN Bestilt ON Booking.Bestilt = Bestilt.Id " +
                    "JOIN Personale c ON c.CPR = Booking.CreatedAf " +
                    "LEFT JOIN Personale t ON t.CPR = Booking.TakedAf " +
                    "WHERE Booking.CPR = @PCPR OR Booking.Navn LIKE @PNavn OR t.Navn LIKE @PersonaleNavn ", connection);
                SqlCommand command = sqlCommand;
                //command.Parameters.Add(CreateParam("@KomNr", komNr + "%", SqlDbType.NVarChar));
                command.Parameters.Add(CreateParam("@CPR", CPR, SqlDbType.Int));
                command.Parameters.Add(CreateParam("@PNavn", PNavn + "%", SqlDbType.NVarChar));
                command.Parameters.Add(CreateParam("@PersonaleNavn", PersonaleNavn + "%", SqlDbType.NVarChar));
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                list.Clear();
                while (reader.Read())
                {
                    list.Add(new Booking(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), GetProeve(reader[0].ToString()), GetSaerligeForhold(reader[0].ToString()), reader[7].ToString(),
                        reader[8].ToString(), reader[9].ToString(), reader[10].ToString(), reader[11].ToString(), reader[12].ToString(), reader[13].ToString(), reader[14].ToString(), reader[15].ToString()));
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
        }

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

        public void Remove(string Id)
        {
            string error = "";
            try
            {
                BundleRepository.removeProeve(Id);
                BundleRepository.removeSaerligeForhold(Id);
                using (SqlCommand command = new("DELETE FROM Booking WHERE Id = @Id", connection))
                {
                    command.Parameters.Add(CreateParam("@Id", Id, SqlDbType.NVarChar));
                    connection.Open();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        list.Remove(new Booking(Id, "", "", "", "", "", "", new List<string>(), new List<string>(), "", "", "", "", "", "", "", "", ""));
                        OnChanged(DbOperation.DELETE, DbModeltype.Personale);
                        return;
                    }
                }

                error = string.Format("Booking could not be deleted");
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
            throw new DbException("Error in Booking repositiory: " + error);
        }



    }
}

