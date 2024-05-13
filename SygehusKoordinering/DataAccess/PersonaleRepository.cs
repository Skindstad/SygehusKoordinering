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

        public void Search(string CPR, string Navn, string Mail, string ArbejdsPhone, string Phone, string Adresse, string Status)
        {
            try
            {
                SqlCommand sqlCommand = new("Select CPR, Navn, Mail, status, ArbejdsTlfNr, Adresse, PrivatTlfNr From Personale WHERE CPR = @CPR Or Navn LIKE @Name Or Mail LIKE @Mail OR ArbejdsTlfNr LIKE @WorkPhone OR PrivatTlfNr LIKE @Phone Or Adresse LIKE @Adresse OR Status = @Status", connection);
                SqlCommand command = sqlCommand;
                //command.Parameters.Add(CreateParam("@KomNr", komNr + "%", SqlDbType.NVarChar));
                command.Parameters.Add(CreateParam("@CPR", CPR, SqlDbType.Int));
                command.Parameters.Add(CreateParam("@Name", Navn + "%", SqlDbType.NVarChar));
                command.Parameters.Add(CreateParam("@Mail", Mail + "%", SqlDbType.NVarChar));
                command.Parameters.Add(CreateParam("@WorkPhone", ArbejdsPhone, SqlDbType.NVarChar));
                command.Parameters.Add(CreateParam("@Phone", Phone, SqlDbType.NVarChar));
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


        public void Add(Personale data)
        {
            string error = "";
            BundleRepository bundleRepository = new BundleRepository();
            if (data.CPR.Length == 9 && data.Navn.Length > 0 && data.Mail.Length > 0 && data.Adgangskode.Length > 0 && data.ArbejdTlf.Length > 0 && data.PrivatTlf.Length > 0 && data.Adresse.Length > 0)
            {

                if (CheckIfTheyAlreadyExist(data.CPR, data.Mail, data.ArbejdTlf, data.PrivatTlf) == null)
                {
                    try
                    {
                        SqlCommand sqlCommand = new("INSERT INTO Personale (Navn, Mail, Adgangskode, ArbejdsTlfNr, CPR, Adresse, PrivatTlfNr) VALUES (@Name, @Mail, @Adgangskode, @ArbejdsPhone,@CPR,@Adresse, @Phone)", connection);
                        SqlCommand command = sqlCommand;
                        command.Parameters.Add(CreateParam("@Name", data.Navn, SqlDbType.NVarChar));
                        command.Parameters.Add(CreateParam("@Mail", data.Mail, SqlDbType.NVarChar));
                        command.Parameters.Add(CreateParam("@Adgangskode", data.Adgangskode, SqlDbType.NVarChar));
                        command.Parameters.Add(CreateParam("@ArbejdsPhone", data.ArbejdTlf, SqlDbType.NVarChar));
                        command.Parameters.Add(CreateParam("@CPR", data.CPR, SqlDbType.NVarChar));
                        command.Parameters.Add(CreateParam("@Adresse", data.Adresse, SqlDbType.NVarChar));
                        command.Parameters.Add(CreateParam("@Phone", data.PrivatTlf, SqlDbType.NVarChar));
                        connection.Open();
                        if (command.ExecuteNonQuery() == 1)
                        {
                            list.Add(data);
                            list.Sort();
                            OnChanged(DbOperation.INSERT, DbModeltype.Personale);
                            return;
                        }
                        error = string.Format("Personale could not be inserted in the database");
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
            else error = "Illegal value for Personale";
            Console.WriteLine(error);
            // throw new DbException("Error in Data repositiory: " + error);
        }

        public static string CheckIfTheyAlreadyExist(string CPR, string Mail, string ArbejdsPhone, string Phone)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
                SqlCommand sqlCommand = new("SELECT Id FROM Personale WHERE CPR = @CPR Or Mail = @Mail OR ArbejdsTlfNr = @ArbejdsPhone OR PrivatTlfNr = @Phone", connection);
                SqlCommand command = sqlCommand;
                SqlParameter param = new("@CPR", SqlDbType.NVarChar);
                SqlParameter param2 = new("@Mail", SqlDbType.NVarChar);
                SqlParameter param3 = new("@ArbejdsPhone", SqlDbType.NVarChar);
                SqlParameter param4 = new("@Phone", SqlDbType.NVarChar);
                param.Value = CPR;
                param2.Value = Mail;
                param3.Value = ArbejdsPhone;
                param4.Value = Phone;
                command.Parameters.Add(param);
                command.Parameters.Add(param2);
                command.Parameters.Add(param3);
                command.Parameters.Add(param4);
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
         
        public void Remove(string CPR)
        {
            string error = "";
            try
            {
                BundleRepository.removeLocation(CPR);
                using (SqlCommand command = new("DELETE FROM Personale WHERE CPR = @CPR", connection))
                {
                    command.Parameters.Add(CreateParam("@CPR", CPR, SqlDbType.NVarChar));
                    connection.Open();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        command.ExecuteNonQuery();
                        list.Remove(new Personale(CPR, "", "", "", "", "", "", new List<string>()));
                        OnChanged(DbOperation.DELETE, DbModeltype.Personale);
                        return;
                    }
                }

                error = string.Format("Personale could not be deleted");
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
            throw new DbException("Error in Personale repositiory: " + error);
        }

       


    }
}
