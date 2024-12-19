using System;
using System.Data;
using BankDataAccessLayer;

namespace BankBusinessLogicLayer
{
    public class clsUser : clsPerson
    {
        public enum enMode { AddNew, Update }
        public enum enSaveResults { Success, FailedUserNameNotExist, FailedUserNameExists, failed }

        public string UserName { get; set; }
        public string Password { get; set; }
        public int Permissions { get; set; }
        enMode Mode = enMode.AddNew;

        public clsUser() :
            base()
        {
            UserName = "";
            Password = "";
            Permissions = 0;

            Mode = enMode.AddNew;
        }

        private clsUser(string userName, string password, int permissions,
            string firstName, string lastName, string email, string phone) :
            base(firstName, lastName, email, phone)
        {
            UserName = userName;
            Password = password;
            Permissions = permissions;

            Mode = enMode.Update;
        }

        static public clsUser FindUser(string UserName)
        {
            string Password = "", FirstName = "", LastName = "", Email = "", Phone = "";
            int Permissions = 0;

            if (clsUserDataAccess.GetUserByUserName(UserName, ref Password, ref Permissions,
           ref FirstName, ref LastName, ref Email, ref Phone))

                return new clsUser(UserName, Password, Permissions,
            FirstName, LastName, Email, Phone);

            else
                return null;

        }

        /* static public clsUser FindUser(string UserName, string Password)
        {
            string FirstName = "", LastName = "", Email = "", Phone = "";
            int Permissions = 0;

            if (clsUserDataAccess.GetUserByPassword(UserName, Password, ref Permissions,
           ref FirstName, ref LastName, ref Email, ref Phone)) 

                return new clsUser(UserName, Password, Permissions,
            FirstName, LastName, Email, Phone);

            else

                return null;

        } */

        private bool _AddNew()
        {
            this.Id = clsUserDataAccess.AddNewUser(this.UserName, this.Password, this.Permissions,
            this.FirstName, this.LastName, this.Email, this.Phone);

            return (this.Id != -1);
        }

        private bool _Update()
        {
            return clsUserDataAccess.UpdateUser(this.UserName, this.Password, this.Permissions,
            this.FirstName, this.LastName, this.Email, this.Phone);
        }

        //static public bool DeleteUser(string UserName)
        //{
        //    return clsUserDataAccess.DeleteUser(UserName);
        //}

        static public bool IsUserExist(string userName , string password)
        {
            return clsUserDataAccess.IsUserExist(userName, password);
        }

        static public bool IsUserExist(string userName)
        {
            return clsUserDataAccess.IsUserExist(userName);
        }

        static public DataTable GetAllUsers()
        {
            return clsUserDataAccess.GetAllUsers();
        }

        static public DataRow GetUserByUserName(string userName)
        {
            return clsUserDataAccess.GetUserByUserName(userName);
        }

        public enSaveResults Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (_AddNew())
                    {
                        Mode = enMode.Update;
                        return enSaveResults.Success;
                    }
                    else
                        return enSaveResults.FailedUserNameExists;

                case enMode.Update:

                    if (_Update())
                        return enSaveResults.Success;
                    else
                        return enSaveResults.FailedUserNameNotExist;

                default:
                    return enSaveResults.failed;
            }
        }

    }
}
