using System;
using System.Data;
using System.Data.SqlClient;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace BankDataAccessLayer
{
    public class clsUserDataAccess
    {
        // Creating CRUD METHODS : ADD / CREATE , UPDATE , DELETE , GetUser / READ , GetAllUserss
        static public bool GetUserByUserName(string UserName, ref string Password, ref int Permissions,
           ref string FirstName, ref string LastName, ref string Email, ref string Phone)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT UserAccounts.UserName, Persons.FirstName, Persons.LastName,
                Persons.Email, Persons.Phone, UserAccounts.Password, UserAccounts.Permissions
                FROM UserAccounts INNER JOIN Persons ON UserAccounts.PersonID = Persons.PersonID 
                WHERE UserAccounts.UserName = @UserName";

            using (SqlCommand cmd = new SqlCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@UserName", UserName);

                try
                {
                    Connection.Open();
                    SqlDataReader DataReader = cmd.ExecuteReader();

                    if (DataReader.Read()) // if no records / rows returned >> exit
                    {
                        // Convert.ToString >> handles NULL exception error
                        FirstName = Convert.ToString(DataReader["FirstName"]);
                        LastName = Convert.ToString(DataReader["LastName"]);
                        Email = Convert.ToString(DataReader["Email"]);
                        Phone = Convert.ToString(DataReader["Phone"]);
                        Password = Convert.ToString(DataReader["Password"]);
                        Permissions = Convert.ToInt32(DataReader["Permissions"]);
                    }

                    DataReader.Close();
                    return true;
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

         /* static public bool GetUserByPassword(ref string UserName, string Password, ref int Permissions,
           ref string FirstName, ref string LastName, ref string Email, ref string Phone)
        {
            bool IsFound = false;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT UserAccounts.UserName, Persons.FirstName, Persons.LastName," +
                " Persons.Email, Persons.Phone, UserAccounts.Password, UserAccounts.Permissions\r\n" +
                "FROM UserAccounts INNER JOIN\r\nPersons ON UserAccounts.PersonID = Persons.PersonID\r\nWHERE Password = @Password;";

            using (SqlCommand cmd = new SqlCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@Password", Password);

                try
                {
                    SqlDataReader DataReader = cmd.ExecuteReader();
                    Connection.Open();

                    if (DataReader.Read()) // if no records / rows returned >> exit
                    {
                        // Convert.ToString >> handles NULL exception error
                        IsFound = true;
                        FirstName = Convert.ToString(DataReader["FirstName"]);
                        LastName = Convert.ToString(DataReader["LastName"]);
                        Email = Convert.ToString(DataReader["Email"]);
                        Phone = Convert.ToString(DataReader["Phone"]);
                        UserName = Convert.ToString(DataReader["UserName"]);
                        Permissions = Convert.ToInt32(DataReader["Permissions"]);
                    }

                    DataReader.Close();
                }
                catch (Exception ex)
                {
                    // exception error messagebox                
                    IsFound = false;
                }
                finally // always will be executed
                {
                    Connection.Close();
                }

            }

            return IsFound;
        }
         */

        // return int >> counting rows affected
        static public bool UpdateUser(string UserName, string Password, int Permissions,
            string FirstName, string LastName, string Email, string Phone)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE UserAccounts
                             SET UserAccounts.Password = @Password,
                                 UserAccounts.Permissions = @Permissions
                             FROM UserAccounts
                             INNER JOIN Persons ON UserAccounts.PersonID = Persons.PersonID
                             WHERE UserAccounts.UserName = @UserName

                            UPDATE Persons
                            SET Persons.FirstName = @FirstName,
                                Persons.LastName = @LastName,
                                Persons.Email = @Email,
                                Persons.Phone = @Phone
                            FROM Persons
                            INNER JOIN UserAccounts ON Persons.PersonID = UserAccounts.PersonID
                            WHERE UserAccounts.UserName = @UserName";

            using (SqlCommand cmd = new SqlCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@Password", Password);
                cmd.Parameters.AddWithValue("@Permissions", Permissions);
                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@LastName", LastName);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Phone", Phone);

                try
                {
                    Connection.Open();
                    return cmd.ExecuteNonQuery() > 0;

                    // Maybe Message Box
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

        //static public bool DeleteUser(string UserName)
        //{
        //    int rowsaffected = 0;
        //    SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

        //    string query = "DELETE FROM UserAccounts \r\nWHERE UserName = @UserName;";

        //    using (SqlCommand cmd = new SqlCommand(query, Connection))
        //    {
        //        cmd.Parameters.AddWithValue("@UserName", UserName);

        //        try
        //        {
        //            Connection.Open();
        //            rowsaffected = cmd.ExecuteNonQuery();

        //            // Maybe Message Box
        //        }
        //        catch 
        //        {
        //            // exception error messagebox  
        //            return false;
        //        }
        //        finally // always will be executed
        //        {
        //            Connection.Close();
        //        }

        //    }

        //    return rowsaffected > 0;
        //}

        // returns the new auto generated ID

        static public int AddNewUser(string UserName, string Password, int Permissions,
            string FirstName, string LastName, string Email, string Phone)
        {
            int PersonID = -1;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "INSERT INTO Persons(FirstName,LastName,Email,Phone)\r\n" +
                           "VALUES(@FirstName , @LastName, @Email, @Phone); " +
                           "SELECT SCOPE_IDENTITY()\r\n\r\n" +
                           "INSERT INTO UserAccounts(UserName,Password,Permissions,PersonID)" +
                           "VALUES(@UserName, @Password, @Permissions,  SCOPE_IDENTITY())";

            using (SqlCommand cmd = new SqlCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@Password", Password);
                cmd.Parameters.AddWithValue("@Permissions", Permissions);
                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@LastName", LastName);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Phone", Phone);

                try
                {
                    Connection.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        PersonID = insertedID;
                    }

                    // Maybe Message Box
                }
                catch 
                {
                    PersonID = -1;
                    // exception error messagebox  
                }
                finally // always will be executed
                {
                    Connection.Close();
                }

            }

            return PersonID;
        }

        static public bool IsUserExist(string UserName, string Password)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select 1 from UserAccounts\nWHERE UserName = @UserName AND Password = @Password;";

            using (SqlCommand cmd = new SqlCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@Password", Password);

                try
                {
                    Connection.Open();
                    return (cmd.ExecuteScalar() != null);
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

        static public bool IsUserExist(string UserName)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Select 1 from UserAccounts
                            WHERE UserName = @UserName";

            using (SqlCommand cmd = new SqlCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@UserName", UserName);

                try
                {
                    Connection.Open();
                    return (cmd.ExecuteScalar() != null);
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

        static public DataTable GetAllUsers()
        {
            // offline Tabular of data (disconnected from the database)
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT UserAccounts.UserName, Persons.FirstName, Persons.LastName, Persons.Email, Persons.Phone, UserAccounts.Password, UserAccounts.Permissions
                            FROM Persons INNER JOIN
                            UserAccounts ON Persons.PersonID = UserAccounts.PersonID";

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
                catch { return null; }

                finally { connection.Close(); }
            }

            return dt;

        }

        static public DataRow GetUserByUserName(string UserName)
        {
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT UserAccounts.UserName, Persons.FirstName, Persons.LastName, Persons.Email, Persons.Phone, UserAccounts.Password, UserAccounts.Permissions
                            FROM Persons INNER JOIN
                            UserAccounts ON Persons.PersonID = UserAccounts.PersonID
                            Where UserName = @UserName;";
        
            using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
            {
                cmd.Parameters.AddWithValue("@UserName", UserName);

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
    }

}
