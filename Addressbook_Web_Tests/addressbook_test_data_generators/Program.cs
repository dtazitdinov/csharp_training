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
            string dataType = args[0];
            int count = Convert.ToInt32(args[1]);
            string filename = args[2];
            string format = args[3];

            if (dataType == "group")
            {
                List<GroupData> groups = new List<GroupData>();
                for (int i = 0; i < count; i++)
                {
                    groups.Add(new GroupData()
                    {
                        Name = TestBase.GenerateRandomString(10),
                        Header = TestBase.GenerateRandomString(50),
                        Footer = TestBase.GenerateRandomString(50)
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
            }
            else if (dataType == "contact")
            {
                List<ContactData> contacts = new List<ContactData>();
                for (int i = 0; i < count; i++)
                {
                    int bYear = TestBase.GenerateRandomNumber(9999);
                    int bMonth = TestBase.GenerateRandomNumber(11) + 1;
                    int bDay = TestBase.GenerateRandomNumber(DateTime.DaysInMonth(bYear, bMonth) - 1) + 1;
                    int yYear = TestBase.GenerateRandomNumber(9999);
                    int yMonth = TestBase.GenerateRandomNumber(11) + 1;
                    int yDay = TestBase.GenerateRandomNumber(DateTime.DaysInMonth(yYear, yMonth) - 1) + 1;

                    contacts.Add(new ContactData()
                    {
                        FirstName = TestBase.GenerateRandomString(10),
                        Middlename = TestBase.GenerateRandomString(10),
                        Lastname = TestBase.GenerateRandomString(10),
                        Nickname = TestBase.GenerateRandomString(10),
                        Title = TestBase.GenerateRandomString(50),
                        Company = TestBase.GenerateRandomString(20),
                        Address = TestBase.GenerateRandomString(50),
                        HomePhone = TestBase.GenerateRandomString(15),
                        MobilePhone = TestBase.GenerateRandomString(15),
                        WorkPhone = TestBase.GenerateRandomString(15),
                        FaxPhone = TestBase.GenerateRandomString(15),
                        Email = TestBase.GenerateRandomString(50).Replace(" ", ""),
                        Email2 = TestBase.GenerateRandomString(50).Replace(" ", ""),
                        Email3 = TestBase.GenerateRandomString(50).Replace(" ", ""),
                        Homepage = TestBase.GenerateRandomString(50),
                        Birthday = new DateTime(bYear, bMonth, bDay),
                        Anniversary = new DateTime(yYear, yMonth, yDay),
                        SecondaryAddress = TestBase.GenerateRandomString(50),
                        SecondaryPhone = TestBase.GenerateRandomString(15),
                        Notes = TestBase.GenerateRandomString(50)
                    });
                }

                StreamWriter writer = new StreamWriter(filename);
                if (format == "xml")
                {
                    writeContactsToXmlFile(contacts, writer);
                }
                else if (format == "json")
                {
                    writeContactsToJsonFile(contacts, writer);
                }
                else
                {
                    System.Console.WriteLine("Unrecognized format = " + format);
                }
                writer.Close();

            }
            else
            {
                System.Console.WriteLine("Unrecognized type = " + dataType);
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
            foreach (GroupData group in groups)
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

        static void writeContactsToXmlFile(List<ContactData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }

        static void writeContactsToJsonFile(List<ContactData> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        }
    }
}
