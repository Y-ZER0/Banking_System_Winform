using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Policy;

namespace BankDataAccessLayer
{
    public class clsClientDataAccess
    {
        // Creating CRUD METHODS : ADD / CREATE , UPDATE , DELETE , GetClient / READ , GetAllClients
        static public bool GetClientByAccNumber(int AccNumber , ref string PinCode , ref decimal Balance,
           ref string FirstName , ref string LastName , ref string Email , ref string Phone)
        {
            bool IsFound = false;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
                        SELECT Clients.AccNumber, 
                               Clients.PinCode, 
                               Persons.FirstName, 
                               Persons.LastName, 
                               Persons.Email, 
                               Persons.Phone, 
                               Clients.Balance
                        FROM Persons
                        INNER JOIN Clients ON Persons.PersonID = Clients.PersonID
                        WHERE Clients.AccNumber = @AccNumber";


            using (SqlCommand cmd = new SqlCommand(query, Connection)) 
            {
                cmd.Parameters.AddWithValue("@AccNumber", AccNumber);

                try
                {
                    Connection.Open();
                    SqlDataReader DataReader = cmd.ExecuteReader();

                    if (DataReader.Read()) // if no records / rows returned >> exit
                    {
                        // Convert.ToString >> handles NULL exception error
                        IsFound = true;
                        FirstName = Convert.ToString(DataReader["FirstName"]);
                        LastName = Convert.ToString(DataReader["LastName"]);
                        Email = Convert.ToString(DataReader["Email"]);
                        Phone = Convert.ToString(DataReader["Phone"]);
                        PinCode = Convert.ToString(DataReader["PinCode"]);
                        Balance = Convert.ToDecimal(DataReader["Balance"]);
                    }

                    DataReader.Close();
                    return IsFound;
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

        // return int >> counting rows affected
        static public bool UpdateClient(int AccNumber, string PinCode, decimal Balance,
            string FirstName, string LastName, string Email, string Phone)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            // Best Practice to Update mutiple tables at once >> is that to update Each Table Seperatley using JOIN
            string query = @"
                    UPDATE Clients 
                    SET Clients.PinCode = @PinCode,
                        Clients.Balance = @Balance
                    FROM Clients
                    INNER JOIN Persons ON Clients.PersonID = Persons.PersonID
                    WHERE Clients.AccNumber = @AccNumber;
                
                    UPDATE Persons
                    SET Persons.FirstName = @FirstName,
                        Persons.LastName = @LastName,
                        Persons.Email = @Email,
                        Persons.Phone = @Phone
                    FROM Persons
                    INNER JOIN Clients ON Persons.PersonID = Clients.PersonID
                    WHERE Clients.AccNumber = @AccNumber;
                ";

            using (SqlCommand cmd = new SqlCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@AccNumber", AccNumber);
                cmd.Parameters.AddWithValue("@PinCode", PinCode);
                cmd.Parameters.AddWithValue("@Balance", Balance);
                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@LastName", LastName);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Phone", Phone);

                try
                {
                    Connection.Open();

                    // rows affected
                    return cmd.ExecuteNonQuery() > 0;
                    // Maybe Message Box
                }
                catch 
                {
                    return false;
                }
                finally // always will be executed
                {
                    Connection.Close();
                }

            }

        }

        static public bool DeleteClient(int AccNumber)
        {
            int rowsaffected = 0;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            // T-SQL technique to declare and assign / set value to a variable
            string query = $@"

                DECLARE @PersonID INT;
                SET @PersonID = (SELECT PersonID FROM Clients WHERE AccNumber = @AccNumber);
            
                DELETE FROM Clients WHERE AccNumber = @AccNumber;
            
                DELETE FROM Persons WHERE PersonID = @PersonID;
            ";

            using (SqlCommand cmd = new SqlCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@AccNumber", AccNumber);

                try
                {
                    Connection.Open();
                    rowsaffected = cmd.ExecuteNonQuery();

                }
                catch 
                {
                    return false;
                }
                finally // always will be executed
                {
                    Connection.Close();
                }

            }

            return rowsaffected > 0;
        }

        // returns the new auto generated ID
        static public int AddNewClient(int AccNumber, string PinCode, decimal Balance,
            string FirstName, string LastName, string Email, string Phone)
        {
            int PersonID = -1; // Client is failed to get added = -1
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "INSERT INTO Persons(FirstName, LastName, Email, Phone)\r\n" +
                "VALUES(@FirstName, @LastName, @Email, @Phone)" +
                "\r\nSELECT SCOPE_IDENTITY();" +
                "\r\nINSERT INTO Clients(AccNumber, PinCode, Balance , PersonID)\r\n" +
                "VALUES(@AccNumber, @PinCode, @Balance , SCOPE_IDENTITY());";

            using (SqlCommand cmd = new SqlCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@AccNumber", AccNumber);
                cmd.Parameters.AddWithValue("@PinCode", PinCode);
                cmd.Parameters.AddWithValue("@Balance", Balance);
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


                }
                catch 
                {
                    PersonID = -1;
                }
                finally // always will be executed
                {
                    Connection.Close();
                }
            }

            return PersonID;
        }

        static public bool IsClientExist(int AccNumber)
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT 1 FROM Clients\r\nWHERE AccNumber = @AccNumber;";

            using (SqlCommand cmd = new SqlCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@AccNumber", AccNumber);

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

        static public DataTable GetAllClients()
        {
            // offline Tabular of data (disconnected from the database)
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Clients.AccNumber, Clients.PinCode, Persons.FirstName, Persons.LastName, Persons.Email, Persons.Phone, Clients.Balance" +
                "\r\nFROM Persons INNER JOIN Clients ON Persons.PersonID = Clients.PersonID";

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
                    return null ;
                }
                finally { connection.Close(); }
            }

            return dt;

        }

        static public DataTable GetAllClientsBalances()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT  Clients.AccNumber, Persons.FirstName, Clients.Balance" +
                "\r\nFROM Persons INNER JOIN Clients ON Persons.PersonID = Clients.PersonID";

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

        static public DataRow GetClientByAccNum(int AccNumber)
        {
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Clients.AccNumber, Clients.PinCode, Persons.FirstName, Persons.LastName, Persons.Email, Persons.Phone, Clients.Balance
                            FROM Persons INNER JOIN Clients ON Persons.PersonID = Clients.PersonID 
                            WHERE Clients.AccNumber = @AccNumber;";

            using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
            {
                cmd.Parameters.AddWithValue("@AccNumber", AccNumber);

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

        static public bool DeleteAllClients()
        {
            SqlConnection sqlConnection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "DELETE Clients;";

            using (SqlCommand cmd = new SqlCommand(query , sqlConnection))
            {
                try
                {
                    sqlConnection.Open();

                    // returns the number of affected rows
                    return (cmd.ExecuteNonQuery() > 0);
                }
                catch
                { return false; }

                finally { sqlConnection.Close(); }
            }

        }

        static public decimal GetTotalBalances()
        {
            decimal TotalBalances = 0;
            SqlConnection sqlConnection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Balance FROM Clients;";

            using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
            {
                try
                {
                    sqlConnection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    // Accessing Data by Column Index >> faster in performance wise
                    while (reader.Read())
                        TotalBalances += reader.GetDecimal(0);

                    reader.Close();

                }

                catch { return 0; }

                finally { sqlConnection.Close(); }

                return TotalBalances;
            }
        }


    }
}
