using System;
using System.Data;
using BankDataAccessLayer;

namespace BankBusinessLogicLayer
{
    public class clsPerson
    {
        // data-members (auto-implemented)

         public int Id { get; set; }
         public string FirstName { get; set; }
         public string LastName { get; set; }
         public string Email { get; set; }
         public string Phone { get; set; }

        // constructors

        // non-arguments
        public clsPerson()
        {
            Id = -1;
            FirstName = "";
            LastName = "";
            Email = "";
            Phone = "";
        }

        // parametirized
        public clsPerson(string firstName, string lastName, string email, string phone)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
        }

    }
}
