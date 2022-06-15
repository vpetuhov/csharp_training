using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class RemovingContactFromGroupTests : AuthTestBase
    {
        [SetUp]
        public void Setup()
        {
            app.Contact.CreateIfNotExist();
            app.Groups.CreateIfNotExist();
        }

        [Test]
        public void TestRemovingContactFromGroup()
        {
            GroupData group = GroupData.GetAll()[0];
            app.Contact.CheckContactNotExist(group);
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = group.GetContacts().First();

            app.Contact.RemoveContactFromGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Remove(contact);
            oldList.Sort();
            newList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
