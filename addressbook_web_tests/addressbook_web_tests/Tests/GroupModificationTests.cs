using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : TestBase
{
        [Test]
        public void GroupModificationTest()
        {
            GroupData newData = new GroupData("111");
            newData.Header = "222";
            newData.Footer = "333";

            app.Groups.Modify(1, newData);
        }
    }
}