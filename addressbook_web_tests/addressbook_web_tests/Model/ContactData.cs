using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData
    {
        private string firstName;
        private string lastName;

        public ContactData(string firstName, string lastname)
        {
            this.firstName = firstName;
            this.lastName = lastname;
        }

        public string FirstName
        {
            get 
            { 
                return firstName; 
            }
            set
            {
                firstName = value;
            }
        }
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
            }
        }
    }
}