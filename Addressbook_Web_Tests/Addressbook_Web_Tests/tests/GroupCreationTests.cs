using System;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;
using System.Linq;


namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : GroupTestBase
    {
        public static IEnumerable<GroupData> RandomGroupDataProvider()
        {
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < 5; i++)
            {
                groups.Add(new GroupData(GenerateRandomString(30))
                {
                    Header = GenerateRandomString(100),
                    Footer = GenerateRandomString(100)
            });
            }
            return groups;
        }

        public static IEnumerable<GroupData> GroupDataFromExcelFile()
        {
            List<GroupData> groups = new List<GroupData>();
            Excel.Application appExcel = new Excel.Application();
            appExcel.Visible = true;
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), @"GroupsData.xlsx");
            Excel.Workbook wb =  appExcel.Workbooks.Open(fullPath);
            Excel.Worksheet sheet = wb.Sheets[1];
            Excel.Range range = sheet.UsedRange;
            for (int row = 1; row <= range.Rows.Count; row++)
            {
                groups.Add(new GroupData()
                {
                    Name = range.Cells[row,1].Value,
                    Header = range.Cells[row, 2].Value,
                    Footer = range.Cells[row, 3].Value
                });
            }
            wb.Close();
            appExcel.Quit(); 
            return groups;
        }

        public static IEnumerable<GroupData> GroupDataFromXmlFile()
        {
            return (List<GroupData>) 
                new XmlSerializer(typeof(List<GroupData>))
                .Deserialize(new StreamReader(@"GroupsData.xml"));
        }

        public static IEnumerable<GroupData> GroupDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<GroupData>>(
                File.ReadAllText(@"GroupsData.json"));
        }

        public static IEnumerable<GroupData> GroupDataFromCsvFile()
        {
            List<GroupData> groups = new List<GroupData>();

            string[] lines = File.ReadAllLines("GroupsData.csv");
            foreach(string l in lines)
            {
                string[] parts = l.Split(',');
                groups.Add(new GroupData(parts[0])
                {
                    Header = parts[1],
                    Footer = parts[2]
                });
            }
            return groups;
        }

        [Test, TestCaseSource("GroupDataFromJsonFile")]
        public void GroupCreationTestFromFile(GroupData newGroup)
        {
            appManager.Navigator.GoToGroupsPage();
            List<GroupData> oldGroups = GroupData.GetAllFromDb();

            appManager.Groups.Create(newGroup);

            Assert.AreEqual(oldGroups.Count + 1, appManager.Groups.GetGroupsCount());

            List<GroupData> newGroups = GroupData.GetAllFromDb();
            oldGroups.Add(newGroup);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void GroupCreationTestRandomData()
        {
            GroupData newGroup = new GroupData(GenerateRandomString(30))
            {
                Header = GenerateRandomString(100),
                Footer = GenerateRandomString(100)
            };

            appManager.Navigator.GoToGroupsPage();
            List<GroupData> oldGroups = GroupData.GetAllFromDb();

            appManager.Groups.Create(newGroup);

            Assert.AreEqual(oldGroups.Count + 1, appManager.Groups.GetGroupsCount());

            List<GroupData> newGroups = GroupData.GetAllFromDb(); 
            oldGroups.Add(newGroup);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void GroupCreationTest()
        {
            GroupData newGroup = new GroupData("Group #" + GenerateRandomNumber(1000))
            {
                Header = "header",
                Footer = "footer"
            };

            appManager.Navigator.GoToGroupsPage();
            List<GroupData> oldGroups = appManager.Groups.GetGroupsList();

            appManager.Groups.Create(newGroup);

            Assert.AreEqual(oldGroups.Count + 1, appManager.Groups.GetGroupsCount());

            List<GroupData> newGroups = appManager.Groups.GetGroupsList();
            oldGroups.Add(newGroup);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void TestGroupDBConnection()
        {
            DateTime start = DateTime.Now;
            List<GroupData> fromUi = appManager.Groups.GetGroupsList();
            DateTime end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start));

            start = DateTime.Now;
            List<GroupData> fromDb = GroupData.GetAllFromDb();
            end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start));
        }
    }
}
