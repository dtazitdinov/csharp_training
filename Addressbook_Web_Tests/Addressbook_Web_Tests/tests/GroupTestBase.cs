using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class GroupTestBase : AuthTestBase
    {
        [TearDown]
        public void CompareGroupsUI_DB()
        {
            if (PROTECTED_LONG_UI_CHECKS)
            {
                List<GroupData> fromUI = appManager.Groups.GetGroupsList();
                List<GroupData> fromDB = GroupData.GetAllFromDb();
                /*fromUI = (from g in fromUI
                            orderby g.Id
                            select g).ToList();*/
                fromUI.Sort();
                fromDB.Sort();

                Assert.AreEqual(fromUI, fromDB);
            }
        }
    }
}
