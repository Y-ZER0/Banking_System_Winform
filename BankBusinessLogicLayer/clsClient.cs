using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Policy;
using BankDataAccessLayer;

namespace BankBusinessLogicLayer
{
    public class clsClient : clsPerson
    {
        public enum enMode { AddNew , Update }
        public enum enSaveResults { Success , FailedAccNumberNotExist , FailedAccNumExists , failed }

        public int AccNumber { get; set; }
        public string PinCode { get; set; }
        public Decimal Balance { get; set; }
        enMode Mode = enMode.AddNew;

        public clsClient() :
            base()
        {
            AccNumber = 0;
            PinCode = "";
            Balance = new Decimal(0);
            Mode = enMode.AddNew;
        }

        // subclass constructor class superclass constructor
        private clsClient(int accNumber, string pinCode, decimal balance,
            string firstName, string lastName, string email, string phone) :
            base(firstName, lastName, email, phone)
        {
            AccNumber = accNumber;
            PinCode = pinCode;
            Balance = balance;
            Mode = enMode.Update;
        }

        // static and object methods (calling DAL methods from BLL)

        private bool _Update()
        {
            return clsClientDataAccess.UpdateClient(AccNumber, PinCode, Balance,
             FirstName, LastName, Email, Phone);
        }

        private bool _Add()
        {
            this.Id = clsClientDataAccess.AddNewClient(this.AccNumber, this.PinCode, this.Balance,
             this.FirstName, this.LastName, this.Email, this.Phone);

            return (this.Id != -1);
        }

        static public clsClient FindClientByAccNumber(int AccNumber)
        {
            string PinCode = "", FirstName = "", LastName = "", Email = "", Phone = ""; decimal Balance = new Decimal(0);

            if (clsClientDataAccess.GetClientByAccNumber(AccNumber, ref PinCode, ref Balance,
           ref FirstName, ref LastName, ref Email, ref Phone))

                return new clsClient(AccNumber, PinCode, Balance,
            FirstName, LastName, Email, Phone);

            else
                return null;
        }

        static public bool DeleteClient(int AccNumber)
        {
            return clsClientDataAccess.DeleteClient(AccNumber);
        }

        static public bool IsClientExist(int AccNumber)
        {
            return clsClientDataAccess.IsClientExist(AccNumber);
        }

        static public DataTable GetAllClients()
        {
            return clsClientDataAccess.GetAllClients();
        }

        static public DataTable GetAllClientsBalances()
        {
            return clsClientDataAccess.GetAllClientsBalances();
        }

        static public DataRow GetClientByAccNum(int AccNum)
        {
            return clsClientDataAccess.GetClientByAccNum(AccNum);
        }

        static public bool DeleteAllClients()
        {
            return clsClientDataAccess.DeleteAllClients();
        }

        public enSaveResults Save()
        {
            switch (Mode)
            {
                case enMode.Update:
                {
                        if (_Update())
                            return enSaveResults.Success;
                        else
                            return enSaveResults.FailedAccNumberNotExist;
                }

                case enMode.AddNew:
                {
                        if (_Add())
                        {
                            Mode = enMode.Update;
                            return enSaveResults.Success;
                        }
                        else
                            return enSaveResults.FailedAccNumExists;
                }

                default:
                    return enSaveResults.failed;
            }

        }

        static public decimal GetTotalBalances()
        {
            return clsClientDataAccess.GetTotalBalances();
        }

    }
}
