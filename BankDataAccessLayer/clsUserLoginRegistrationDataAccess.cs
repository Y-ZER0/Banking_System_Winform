using System;
using System.Data;
using System.Data.SqlClient;

namespace BankDataAccessLayer
{
    public class clsUserLoginRegistrationDataAccess
    {
        // only add process
        static public bool AddNewLoginRegistration(string UserName)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO LoginRegister
                       ( UserName, FirstName, LastName, Email, Phone, Password, LogDate )
     SELECT UserAccounts.UserName, Persons.FirstName, Persons.LastName, Persons.Email, Persons.Phone, UserAccounts.Password, CURRENT_TIMESTAMP as LogDate
                             FROM Persons INNER JOIN
                             UserAccounts ON Persons.PersonID = UserAccounts.PersonID
                             WHERE UserAccounts.UserName = @UserName";

            using (SqlCommand cmd = new SqlCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@UserName", UserName);

                try
                {
                    Connection.Open();
                    return cmd.ExecuteNonQuery() > 0;

                    // Maybe Message Box
                }
                catch { return false; }

                finally { Connection.Close(); }
            }

        }

        static public DataTable GetAllLogs()
        {
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM LoginRegister";

            using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
            {
                try
                {
                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = cmd.ExecuteReader();

                    if (sqlDataReader.HasRows)
                        dt.Load(sqlDataReader);

                    sqlDataReader.Close();

                    return dt;
                }

                catch { return null; }

                finally { sqlConnection.Close(); } }
        }
    }
}

