using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {
        [SetUp]
        public void Init()
        {
            app.Groups.CreateIfNotExist();
        }

        [Test]
        public void GroupModificationTest()
        {
            GroupData newData = new GroupData("Group");
            newData.Header = "Header";
            newData.Footer = "Footer";

            app.Groups.Modify(0, newData);
        }
    }
}