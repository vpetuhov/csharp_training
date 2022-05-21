using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [SetUp]
        public void Init()
        {
            app.Contact.CreateIfNotExist();
        }

        [Test]
        public void ContactModificationTest()
        {
            ContactData newData = new ContactData("Sarah", "Kerrigan");

            List<ContactData> oldContacts = app.Contact.GetContactList();

            app.Contact.Modify(0, newData);

            List<ContactData> newContacts = app.Contact.GetContactList();
            oldContacts[0].FirstName = "Sarah";
            oldContacts[0].LastName = "Kerrigan";
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}