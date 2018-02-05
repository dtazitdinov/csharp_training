using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAddressbookTests;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;

namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = Convert.ToInt32(args[0]);
            string filename = args[1];
            string format = args[2];

            
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < count; i++)
            {
                groups.Add(new GroupData(TestBase.GenerateRandomString(30))
                {
                    Header = TestBase.GenerateRandomString(100),
                    Footer = TestBase.GenerateRandomString(100)
                });                                 
            }

            if (format == "excel")
            {
                WriteGroupsToExcelFile(groups, filename);
            }
            else
            {
                StreamWriter writer = new StreamWriter(filename);
                if (format == "csv")
                {
                    writeGroupsToCsvFile(groups, writer);
                }
                else if (format == "xml")
                {
                    writeGroupsToXmlFile(groups, writer);
                }
                else if (format == "json")
                {
                    writeGroupsToJsonFile(groups, writer);
                }
                else
                {
                    System.Console.WriteLine("Unrecognized format = " + format);
                }
                writer.Close();
            }

            
            System.Console.WriteLine("Successful");
        }

        static void writeGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {           
                writer.WriteLine($"{group.Name}," +
                                 $"{group.Header}," +
                                 $"{group.Footer}");
            }
        }

        static void writeGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }

        static void writeGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));

        }

        static void WriteGroupsToExcelFile(List<GroupData> groups, string filename)
        {
            Excel.Application appExcel = new Excel.Application();
            appExcel.Visible = true;
            Excel.Workbook wb = appExcel.Workbooks.Add();
            Excel.Worksheet sheet = appExcel.ActiveSheet;

            int row = 1;
            foreach(GroupData group in groups)
            {
                sheet.Cells[row, 1] = group.Name;
                sheet.Cells[row, 2] = group.Header;
                sheet.Cells[row, 3] = group.Footer;
                row++;
            }
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            File.Delete(fullPath);
            wb.SaveAs(fullPath);
            wb.Close();
            appExcel.Quit();
        }

        /*            string[] lines = File.ReadAllLines("ContactData.csv");
            foreach (string l in lines)
            {
                int bYear = GenerateRandomNumber(9999);
                int bMonth = GenerateRandomNumber(11) + 1;
                int bDay = GenerateRandomNumber(DateTime.DaysInMonth(bYear, bMonth) - 1) + 1;
                int yYear = GenerateRandomNumber(9999);
                int yMonth = GenerateRandomNumber(11) + 1;
                int yDay = GenerateRandomNumber(DateTime.DaysInMonth(yYear, yMonth) - 1) + 1;


                string[] parts = l.Split(',');
                groups.Add(new ContactData(parts[0])
                {
                    Middlename = GenerateRandomString(30),
                    Lastname = GenerateRandomString(30),
                    Nickname = GenerateRandomString(30),
                    Title = GenerateRandomString(100),
                    Company = GenerateRandomString(30),
                    Address = GenerateRandomString(100),
                    HomePhone = GenerateRandomString(15),
                    MobilePhone = GenerateRandomString(15),
                    WorkPhone = GenerateRandomString(15),
                    FaxPhone = GenerateRandomString(15),
                    Email = GenerateRandomString(100).Replace(" ", ""),
                    Email2 = GenerateRandomString(100).Replace(" ", ""),
                    Email3 = GenerateRandomString(100).Replace(" ", ""),
                    Homepage = GenerateRandomString(100),
                    Birthday = new DateTime(bYear, bMonth, bDay),
                    Anniversary = new DateTime(yYear, yMonth, yDay),
                    SecondaryAddress = GenerateRandomString(100),
                    SecondaryPhone = GenerateRandomString(15),
                    Notes = GenerateRandomString(150)
                });
            }*/
    }
}
