using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : ContactTestBase
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

            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData oldData = oldContacts[0];

            app.Contact.Modify(oldData, newData);

            Assert.AreEqual(oldContacts.Count, app.Contact.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts[0].FirstName = "Sarah";
            oldContacts[0].LastName = "Kerrigan";
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                if (contact.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.FirstName, contact.FirstName);
                    Assert.AreEqual(newData.LastName, contact.LastName);
                }
            }
        }
    }
}