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

        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("Jim", "Raynor")
            {
                MiddleName = "James",
                NickName = "NickName",

                Company = "Eng",
                Title = "Title",
                Address = "г.Москва, ул.Баррикадная, д.30",

                HomePhone = "+7(965) 125 85 63",
                WorkPhone = "8(43678)8-25-36",
                MobilePhone = "89088881134",
                Fax = "225-25-25",

                Email = "email@mail.ru",
                Email2 = "email2@yandex.ru",
                Email3 = "email3@gmail.com",
                HomePage = "http://HomePage.ru",

                SecondAddress = "г.Москва, ул.Неизвестная, д.38",
                SecondPhone = "185-96-525",
                Notes = "Notes"
            };

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