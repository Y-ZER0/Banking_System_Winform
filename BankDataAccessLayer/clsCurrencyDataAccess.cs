using System;
using System.Data;
using System.Data.SqlClient;

namespace BankDataAccessLayer
{
    public class clsCurrencyDataAccess
    {
        static public DataTable GetAllCurrencies()
        {
            // offline Tabular of data (disconnected from the database)
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Currency;";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                        dt.Load(reader);

                    reader.Close();
                }
                catch 
                {
                    return null;
                }
                finally { connection.Close(); }
            }

            return dt;
        }

        static public DataRow GetCurrencyByCode(string CurrencyCode)
        {
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM Currency
                            WHERE CurrencyCode = @CurrencyCode;";

            using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
            {
                cmd.Parameters.AddWithValue("@CurrencyCode", CurrencyCode);

                try
                {
                    sqlConnection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                        dt.Load(reader);

                    reader.Close();

                    // returns the first row
                    if (dt.Rows.Count > 0)
                    {
                        // Return the first row
                        return dt.Rows[0];
                    }
                    else
                    {
                        // No rows found, return null or handle accordingly
                        return null;
                    }
                }
                catch { return null; }

                finally { sqlConnection.Close(); }
            }

        }

        static public bool IsCurrencyExist(string CurrencyCode)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT 1 FROM Currency\r\nWHERE CurrencyCode = @CurrencyCode;";

            using (SqlCommand cmd = new SqlCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@CurrencyCode", CurrencyCode);

                try
                {
                    Connection.Open();
                    return cmd.ExecuteScalar() != null;
                }
                catch
                {
                    // exception error messagebox  
                    return false;
                }
                finally // always will be executed
                {
                    Connection.Close();
                }

            }
        }

        static public bool UpdateRate(string CurrencyCode, Decimal NewRate)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "UPDATE Currency\r\n" +
                "SET Rate = @Rate\r\n" +
                "WHERE CurrencyCode = @CurrencyCode;";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@CurrencyCode", CurrencyCode);
                cmd.Parameters.AddWithValue("@Rate", NewRate);

                try
                {
                    connection.Open();

                    // returns number of affected rows
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        static public bool FindCurrencyByCode(string CurrencyCode, ref string Country, ref string CurrencyName
            , ref decimal Rate)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Currency\r\nWHERE CurrencyCode = @CurrencyCode;";

            using (SqlCommand cmd = new SqlCommand(query, connection)) 
            {
                cmd.Parameters.AddWithValue("@CurrencyCode", CurrencyCode);

                try
                {
                    connection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Country = reader["Country"].ToString();
                        CurrencyName = reader["CurrencyName"].ToString();
                        Rate = Convert.ToDecimal(reader["Rate"]);
                    }

                    reader.Close();

                    return true;
                }

                catch { return false; }

                finally { connection.Close();  }
            }
        }
    }
}
