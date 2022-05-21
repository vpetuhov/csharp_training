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
            ContactData contact = new ContactData("Jim", "Raynor");

            List<ContactData> oldСontacts = app.Contact.GetContactList();

            app.Contact.Create(contact);
   
            List<ContactData> newContacts = app.Contact.GetContactList();
            oldСontacts.Add(contact);
            oldСontacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldСontacts, newContacts);
        }
    }
}