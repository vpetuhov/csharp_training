using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [SetUp]
        public void Init()
        {
            app.Contact.CreateIfNotExist();
        }

        [Test]
        public void ContactRemovalTest()
        {
            List<ContactData> oldСontacts = app.Contact.GetContactList();

            app.Contact.Remove(0);

            List<ContactData> newContacts = app.Contact.GetContactList();

            oldСontacts.RemoveAt(0);
            Assert.AreEqual(oldСontacts, newContacts);
        }
    }
}