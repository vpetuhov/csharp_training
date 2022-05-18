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

            app.Contact.Modify(1, newData);
        }
    }
}