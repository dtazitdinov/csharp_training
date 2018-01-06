using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            GroupData newGroup = new GroupData("group #" + OtherMetods.RandomNumber(1000));
            newGroup.Header = "header";
            newGroup.Footer = "footer";

            appManager.Groups.Create(newGroup);
        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            GroupData group = new GroupData("");
            group.Header = "";
            group.Footer = "";

            appManager.Groups.Create(group);
        }
    }
}
