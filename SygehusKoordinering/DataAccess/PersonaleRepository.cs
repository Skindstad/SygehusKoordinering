using Microsoft.Data.SqlClient;
using Microsoft.Maui.Devices.Sensors;
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
    public class PersonaleRepository : Repository, IEnumerable<Personale>
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
        // Find medarbejder
        public void Search(string CPR, string Navn, string Mail, string ArbejdsPhone, string Phone, string Adresse, string Status)
        {
            try
            {
                SqlCommand sqlCommand = new("Select CPR, Navn, Mail, Status, ArbejdsTlfNr, Adresse, PrivatTlfNr From Personale" +
                    " WHERE CPR LIKE @CPR Or Navn LIKE @Name Or Mail LIKE @Mail OR ArbejdsTlfNr LIKE @WorkPhone" +
                    " OR PrivatTlfNr LIKE @Phone Or Adresse LIKE @Adresse OR Status LIKE @Status", connection);
                SqlCommand command = sqlCommand;
                //command.Parameters.Add(CreateParam("@KomNr", komNr + "%", SqlDbType.NVarChar));
                command.Parameters.Add(CreateParam("@CPR", CPR + "%", SqlDbType.NVarChar));
                command.Parameters.Add(CreateParam("@Name", Navn + "%", SqlDbType.NVarChar));
                command.Parameters.Add(CreateParam("@Mail", Mail + "%", SqlDbType.NVarChar));
                command.Parameters.Add(CreateParam("@WorkPhone", ArbejdsPhone + "%", SqlDbType.NVarChar));
                command.Parameters.Add(CreateParam("@Phone", Phone + "%", SqlDbType.NVarChar));
                command.Parameters.Add(CreateParam("@Adresse", Adresse + "%", SqlDbType.NVarChar));
                command.Parameters.Add(CreateParam("@Status", Status + "%", SqlDbType.NVarChar));
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                list.Clear();
                while (reader.Read())
                {
                    list.Add(new Personale(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[4].ToString(), reader[3].ToString(), reader[5].ToString(), reader[6].ToString(), GetLokation(reader[0].ToString())));
                }

                OnChanged(DbOperation.SELECT, DbModeltype.Personale);
            }
            catch (Exception ex)
            {
                throw new DbException("Error in Personale repositiory: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
        }
        // login
        public Personale Login( string Mail, string Adgangskode, string ArbejdsPhone)
        {
            if (Mail != null && Adgangskode != null && ArbejdsPhone != null)
            {
                try
                {
                    SqlCommand sqlCommand = new("Select CPR, Navn, Mail, status, ArbejdsTlfNr, Adresse, PrivatTlfNr From Personale WHERE Mail = @Mail AND ArbejdsTlfNr = @WorkPhone AND Adgangskode = @Adgangskode", connection);
                    SqlCommand command = sqlCommand;
                    command.Parameters.Add(CreateParam("@Mail", Mail, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@Adgangskode", Adgangskode, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@WorkPhone", ArbejdsPhone, SqlDbType.NVarChar));
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {

                        BundleRepository.removeLocation(reader[0].ToString());
                        Update(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[4].ToString(), "1", reader[5].ToString(), reader[6].ToString());
                        return new Personale(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[4].ToString(), "True", reader[5].ToString(), reader[6].ToString(), new List<string>());
                    }
                    OnChanged(DbOperation.SELECT, DbModeltype.Personale);
                }
                catch (Exception ex)
                {
                    throw new DbException("Error in Personale repositiory: " + ex.Message);
                }
                finally
                {
                    if (connection != null && connection.State == ConnectionState.Open) connection.Close();
                }
            }
            return null;
        }


        // få lokation fra medarbejders CPR
        public static List<string> GetLokation(string CPR)
        {
            SqlConnection connection = null;
            List<string> lokation = new List<string>();
            try
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
                SqlCommand sqlCommand = new("SELECT Lokation.Navn FROM Personale Join PersonalePaaLokation on PersonalePaaLokation.Personal = Personale.CPR Join Lokation on PersonalePaaLokation.Lokation = Lokation.Id" +
                    " WHERE CPR = @CPR", connection);
                SqlCommand command = sqlCommand;
                SqlParameter param = new("@CPR", SqlDbType.NVarChar);
                param.Value = CPR;
                command.Parameters.Add(param);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lokation.Add(reader[0].ToString());
                }
                return lokation;
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


        // få Medarbejder fra CPR
        public static Personale GetPerson(string CPR)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
                SqlCommand sqlCommand = new("Select CPR, Navn, Mail, status, ArbejdsTlfNr, Adresse, PrivatTlfNr From Personale WHERE CPR = @CPR", connection);
                SqlCommand command = sqlCommand;
                SqlParameter param = new("@CPR", SqlDbType.NVarChar);
                param.Value = CPR;
                command.Parameters.Add(param);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new Personale(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[4].ToString(), reader[3].ToString(), reader[5].ToString(), reader[6].ToString(), new List<string>());
                }
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

        // Rediger på Medarbejder
        public void Update(string cpr, string navn, string mail, string arbejdTlfNr, string status, string adresse, string privatTlfNr)
        {
            Update(new Personale(cpr,navn, mail, arbejdTlfNr, status, adresse, privatTlfNr, null));
        }

        public void Update(Personale data)
        {
            string error = "";
            if (data.CPR != string.Empty)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString))
                    {

                        SqlCommand sqlCommand = new("UPDATE Personale SET Navn = @Navn, Mail = @Mail, ArbejdsTlfNr = @ArbejdTlf, Status = @Status, Adresse = @Adresse, PrivatTlfNr = @PrivatTlf WHERE CPR = @CPR", connection);
                        SqlCommand command = sqlCommand;
                        command.Parameters.Add(CreateParam("@CPR", data.CPR, SqlDbType.NVarChar));
                        command.Parameters.Add(CreateParam("@Navn", data.Navn, SqlDbType.NVarChar));
                        command.Parameters.Add(CreateParam("@Mail", data.Mail, SqlDbType.NVarChar));
                        command.Parameters.Add(CreateParam("@ArbejdTlf", data.ArbejdTlf, SqlDbType.NVarChar));
                        command.Parameters.Add(CreateParam("@Status", data.Status, SqlDbType.NVarChar));
                        command.Parameters.Add(CreateParam("@Adresse", data.Adresse, SqlDbType.NVarChar));
                        command.Parameters.Add(CreateParam("@PrivatTlf", data.PrivatTlf, SqlDbType.NVarChar));
                        connection.Open();
                        if (command.ExecuteNonQuery() == 1)
                        {
                            UpdateList(data);
                            OnChanged(DbOperation.UPDATE, DbModeltype.Personale);
                            return;
                        }
                        error = string.Format("Personale could not be updated");
                    }
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
            }
            else error = "Illegal value for Personale";
            // throw new DbException("Error in Data repositiory: " + error);
        }

        private void UpdateList(Personale data)
        {
            for (int i = 0; i < list.Count; ++i)
                if (list[i].CPR.Equals(data.CPR))
                {
                    list[i].Navn = data.Navn;
                    list[i].Mail = data.Mail;
                    list[i].ArbejdTlf = data.ArbejdTlf;
                    list[i].Status = data.Status;
                    list[i].Adresse = data.Adresse;
                    list[i].PrivatTlf = data.PrivatTlf;
                    break;
                }
        }

    }
}
