using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contact = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contact.Add(new ContactData(GenerateRandomString(30), GenerateRandomString(30))
                {
                    MiddleName = GenerateRandomString(30),
                    NickName = GenerateRandomString(30),

                    Company = GenerateRandomString(100),
                    Title = GenerateRandomString(100),
                    Address = GenerateRandomString(100),

                    HomePhone = GenerateRandomString(20),
                    WorkPhone = GenerateRandomString(20),
                    MobilePhone = GenerateRandomString(20),
                    Fax = GenerateRandomString(20),

                    Email = GenerateRandomString(50),
                    Email2 = GenerateRandomString(50),
                    Email3 = GenerateRandomString(50),
                    HomePage = GenerateRandomString(50),

                    SecondAddress = GenerateRandomString(100),
                    SecondPhone = GenerateRandomString(20),
                    Notes = GenerateRandomString(100)
                });
            }

            return contact;
        }

        [Test, TestCaseSource("RandomContactDataProvider")]
        public void ContactCreationTest(ContactData contact)
        {

            List<ContactData> oldСontacts = app.Contact.GetContactList();

            app.Contact.Create(contact);

            Assert.AreEqual(oldСontacts.Count + 1, app.Contact.GetContactCount());

            List<ContactData> newContacts = app.Contact.GetContactList();
            oldСontacts.Add(contact);
            oldСontacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldСontacts, newContacts);
        }
    }
}