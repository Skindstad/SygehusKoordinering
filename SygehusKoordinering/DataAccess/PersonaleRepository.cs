using Java.Time;
using Microsoft.Data.SqlClient;
using SygehusKoordinering.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Java.Util.Jar.Attributes;

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

        public void Search(string CPR, string Navn, string Mail, string ArbejdsPhone, string Phone, string Adresse)
        {
            try
            {
                SqlCommand sqlCommand = new("Select CPR, Navn, Mail, status, ArbejdsTlfNr, Adresse, PrivatTlfNr, From Personale WHERE CPR = @CPR Or Navn LIKE @Name Or Mail LIKE @Mail OR ArbejdsTlfNr LIKE @WorkPhone OR PrivatTlfNr LIKE @Phone Or Adresse LIKE @Adresse", connection);
                SqlCommand command = sqlCommand;
                //command.Parameters.Add(CreateParam("@KomNr", komNr + "%", SqlDbType.NVarChar));
                command.Parameters.Add(CreateParam("@CPR", CPR + "%", SqlDbType.Int));
                command.Parameters.Add(CreateParam("@Name", Navn + "%", SqlDbType.NVarChar));
                command.Parameters.Add(CreateParam("@Mail", Mail + "%", SqlDbType.NVarChar));
                command.Parameters.Add(CreateParam("@WorkPhone", ArbejdsPhone + "%", SqlDbType.NVarChar));
                command.Parameters.Add(CreateParam("@Phone", Phone + "%", SqlDbType.NVarChar));
                command.Parameters.Add(CreateParam("@Adresse", Adresse + "%", SqlDbType.NVarChar));
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                list.Clear();
                while (reader.Read())
                {
                    list.Add(new Personale(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[4].ToString(), reader[3].ToString(), reader[5].ToString(), reader[6].ToString()));
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

        public void Login( string Mail, string Adgangskode, string ArbejdsPhone)
        {
            try
            {
                SqlCommand sqlCommand = new("Select CPR, Navn, Mail, status, ArbejdsTlfNr, Adresse, PrivatTlfNr, From Personale WHERE Mail = @Mail AND ArbejdsTlfNr = @WorkPhone AND Adgangskode = @Adgangskode", connection);
                SqlCommand command = sqlCommand;
                command.Parameters.Add(CreateParam("@Mail", Mail + "%", SqlDbType.NVarChar));
                command.Parameters.Add(CreateParam("@Adgangskode", Adgangskode + "%", SqlDbType.NVarChar));
                command.Parameters.Add(CreateParam("@WorkPhone", ArbejdsPhone + "%", SqlDbType.NVarChar));
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                list.Clear();
                while (reader.Read())
                {
                    list.Add(new Personale(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[4].ToString(), reader[3].ToString(), reader[5].ToString(), reader[6].ToString()));
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


        public void Add(Personale data)
        {
            string error = "";
            if (data.CPRNr.Length == 10 && data.Navn.Length > 0 && data.Mail.Length > 0 && data.Adgangskode.Length > 0 && data.ArbejdTlfNr.Length > 0 && data.PrivatTlfNr.Length > 0 && data.Adresse.Length > 0)
            {

                if (CheckIfTheyAlreadyExist(data.CPRNr, data.Mail, data.ArbejdTlfNr, data.PrivatTlfNr) == null)
                {
                    try
                    {
                        SqlCommand sqlCommand = new("INSERT INTO Personale (Navn, Mail, Adgangskode, ArbejdsTlfNr, CPR, Adresse, PrivatTlfNr) VALUES (@Name, @Mail, @Adgangskode, @ArbejdsPhone,@CPR,@Adresse, @Phone)", connection);
                        SqlCommand command = sqlCommand;
                        command.Parameters.Add(CreateParam("@Name", data.Navn, SqlDbType.NVarChar));
                        command.Parameters.Add(CreateParam("@Mail", data.Mail, SqlDbType.NVarChar));
                        command.Parameters.Add(CreateParam("@Adgangskode", data.Adgangskode, SqlDbType.NVarChar));
                        command.Parameters.Add(CreateParam("@ArbejdsPhone", data.ArbejdTlfNr, SqlDbType.NVarChar));
                        command.Parameters.Add(CreateParam("@CPR", data.CPRNr, SqlDbType.NVarChar));
                        command.Parameters.Add(CreateParam("@Adresse", data.Adresse, SqlDbType.NVarChar));
                        command.Parameters.Add(CreateParam("@Phone", data.PrivatTlfNr, SqlDbType.NVarChar));
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

        /*
        public void Update(string id, string komNr, string city, string gruppe, string year, string num)
        {
            Update(new Data(id, komNr, city, gruppe, year, num));
        }

        public void Update(Data data)
        {
            string error = "";
            if (data.IsValid)
            {
                try
                {
                    // string dataId = GetId(data.KomNr, data.Gruppe, data.Year);
                    string gruppeId = KeynummerRepository.GetId(data.Gruppe);
                    SqlCommand sqlCommand = new("UPDATE Data SET Kom_nr = @KomNr, GruppeId = @Gruppe, Aarstal = @Year, Tal = @Num WHERE Id = @DataId", connection);
                    SqlCommand command = sqlCommand;
                    command.Parameters.Add(CreateParam("@DataId", data.DataId, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@KomNr", data.KomNr, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@Gruppe", gruppeId, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@Year", data.Year, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@Num", data.Num, SqlDbType.NVarChar));
                    connection.Open();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        UpdateList(data);
                        OnChanged(DbOperation.UPDATE, DbModeltype.Data);
                        return;
                    }
                    error = string.Format("Data could not be updated");
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
            else error = "Illegal value for Data";
            // throw new DbException("Error in Data repositiory: " + error);
        }

        private void UpdateList(Data data)
        {
            for (int i = 0; i < list.Count; ++i)
                if (list[i].DataId.Equals(data.DataId))
                {
                    list[i].KomNr = data.KomNr;
                    list[i].City = KommuneRepository.GetCity(data.KomNr);
                    list[i].Gruppe = data.Gruppe;
                    list[i].Year = data.Year;
                    list[i].Num = data.Num;
                    break;
                }
        }

        public void Remove(Data data)
        {
            string error = "";
            try
            {
                string dataId = GetId(data.KomNr, data.Gruppe, data.Year);
                using (SqlCommand command = new("DELETE FROM Data WHERE Id = @DataId", connection))
                {
                    command.Parameters.Add(CreateParam("@DataId", dataId, SqlDbType.NVarChar));
                    connection.Open();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        list.Remove(new Data(dataId, "", "", "", "", ""));
                        OnChanged(DbOperation.DELETE, DbModeltype.Data);
                        return;
                    }
                }

                error = string.Format("Data could not be deleted");
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
            throw new DbException("Error in Data repositiory: " + error);
        }

        */


    }
}
