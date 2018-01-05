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
            GroupData groupForRemove = new GroupData("Group for remove");

            appManager.Groups.Create(groupForRemove);
            appManager.Groups.Remove(groupForRemove);
        }
    }
}
