using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : ContactTestBase
    {
        [SetUp]
        public void Init()
        {
            app.Contact.CreateIfNotExist();
        }

        [Test]
        public void ContactRemovalTest()
        {
            List<ContactData> oldСontacts = ContactData.GetAll();
            ContactData toBeRemoved = oldСontacts[0];

            app.Contact.Remove(toBeRemoved);

            Assert.AreEqual(oldСontacts.Count - 1, app.Contact.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();

            oldСontacts.RemoveAt(0);
            oldСontacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldСontacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }
        }
    }
}