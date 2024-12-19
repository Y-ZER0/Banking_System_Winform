using System;
using System.Data;
using System.Data.SqlClient;
using BankDataAccessLayer;

namespace BankBusinessLogicLayer
{
    public class clsCurrency
    {
        public string CurrencyCode { get; set; }
        public string Country { get; set; }
        public string CurrencyName { get; set; }
        public Decimal Rate { get; set; }

        public clsCurrency()
        {
            CurrencyCode = "";
            Country = "";
            CurrencyName = "";
            Rate = new Decimal(0);
        }

        private clsCurrency(string currencyCode, string country, string currencyName, decimal rate)
        {
            CurrencyCode = currencyCode;
            Country = country;
            CurrencyName = currencyName;
            Rate = rate;
        }

        static public clsCurrency FindCurrency(string CurrencyCode)
        {
            string country = "", currencyName = "";  decimal rate = 0;

            if (clsCurrencyDataAccess.FindCurrencyByCode(CurrencyCode, ref country, ref currencyName
                , ref rate)) 

                return new clsCurrency(CurrencyCode, country, currencyName, rate);

            else

                return null;
        }

        static public DataTable GetAllCurrencies()
        {
            return clsCurrencyDataAccess.GetAllCurrencies();
        }

        static public DataRow GetCurrencyByCode(string CurrencyCode)
        {
            return clsCurrencyDataAccess.GetCurrencyByCode(CurrencyCode);
        }

        static public bool IsCurrencyExist(string CurrencyCode)
        {
            return clsCurrencyDataAccess.IsCurrencyExist(CurrencyCode);
        }

        static public bool UpdateRate(string CurrencyCode, Decimal NewRate)
        {
            return clsCurrencyDataAccess.UpdateRate(CurrencyCode, NewRate);
        }


    }
}
