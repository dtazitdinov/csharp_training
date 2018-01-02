using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : TestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            GroupData group = new GroupData("group_" + OtherMetods.NextRndNumToStr(1000));
            group.Header = "header";
            group.Footer = "footer";

            appManager.Group.Create(group);
            appManager.Group.Remove();
        }
    }
}
