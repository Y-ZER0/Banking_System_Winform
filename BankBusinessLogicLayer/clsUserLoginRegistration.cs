using System;
using System.Data;
using BankDataAccessLayer;

namespace BankBusinessLogicLayer
{
    public class clsUserLoginRegistration
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Permissions { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public DateTime LogDate { get; set; }

        private clsUserLoginRegistration(string userName, string password, int permissions,
            string firstName, string lastName, string email, string phone)

        { UserName = userName; Password = password; FirstName = firstName; LastName = lastName; Email = email; Phone = phone; LogDate = DateTime.Now; }

        static public bool AddNewLoginRegistration(string UserName)
        {
            return clsUserLoginRegistrationDataAccess.AddNewLoginRegistration(UserName);
        }

        static public DataTable GetAllLogs()
        {
            return clsUserLoginRegistrationDataAccess.GetAllLogs();
        }
    }
}
