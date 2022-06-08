using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;
using WebAddressbookTests;

namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            string type = args[0];
            int count = Convert.ToInt32(args[1]);
            string filename = args[2];
            string format = args[3];

            List<GroupData> groups = new List<GroupData>();
            List<ContactData> contacts = new List<ContactData>();

            if (type == "group")
            {
                for (int i = 0; i < count; i++)
                {
                    groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                    {
                        Header = TestBase.GenerateRandomString(10),
                        Footer = TestBase.GenerateRandomString(10)
                    });
                }
            }

            else if (type == "contact")
            {
                for (int i = 0; i < count; i++)
                {
                    contacts.Add(new ContactData(TestBase.GenerateRandomString(30), TestBase.GenerateRandomString(30))
                    {
                        MiddleName = TestBase.GenerateRandomString(30),
                        NickName = TestBase.GenerateRandomString(30),

                        Title = TestBase.GenerateRandomString(100),
                        Company = TestBase.GenerateRandomString(100),
                        Address = TestBase.GenerateRandomString(100),

                        HomePhone = TestBase.GenerateRandomString(20),
                        MobilePhone = TestBase.GenerateRandomString(20),
                        WorkPhone = TestBase.GenerateRandomString(20),
                        Fax = TestBase.GenerateRandomString(20),

                        Email = TestBase.GenerateRandomString(50),
                        Email2 = TestBase.GenerateRandomString(50),
                        Email3 = TestBase.GenerateRandomString(50),

                        HomePage = TestBase.GenerateRandomString(20),
                        SecondAddress = TestBase.GenerateRandomString(100),
                        SecondPhone = TestBase.GenerateRandomString(20),
                        Notes = TestBase.GenerateRandomString(50)
                    });
                }
            }

            else
            {
                System.Console.Out.Write("Unrecognized type " + type);
            }

            if (format == "excel")
            {
                if (type == "group")
                {
                    WriteGroupsToExcelFile(groups, filename);
                }
                else if (type == "contact")
                {
                    writeContactsToExcelFile(contacts, filename);
                }
            }

            else
            {
                StreamWriter writer = new StreamWriter(filename);
                if (format == "csv")
                {
                    if (type == "group")
                    {
                        writeGroupsToCsvFile(groups, writer);
                    }
                    else if (type == "contact")
                    {
                        writeContactsToCsvFile(contacts, writer);
                    }
                }

                else if (format == "xml")
                {
                    if (type == "group")
                    {
                        writeGroupsToXmlFile(groups, writer);
                    }
                    else if (type == "contact")
                    {
                        writeContactsToXmlFile(contacts, writer);
                    }
                }

                else if (format == "json")
                {
                    if (type == "group")
                    {
                        writeGroupsToJsonFile(groups, writer);
                    }
                    else if (type == "contact")
                    {
                        writeContactsToJsonFile(contacts, writer);
                    }
                }

                else
                {
                    System.Console.Out.Write("Unrecognized format " + format);
                }
                writer.Close();
            }
        }

        static void WriteGroupsToExcelFile(List<GroupData> groups, string filename)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = wb.ActiveSheet;

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
            app.Visible = false;
            app.Quit();
        }

        static void writeContactsToExcelFile(List<ContactData> contacts, string filename)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = app.ActiveSheet;

            int row = 1;
            foreach (ContactData contact in contacts)
            {
                sheet.Cells[row, 1] = contact.FirstName;
                sheet.Cells[row, 2] = contact.LastName;
                sheet.Cells[row, 3] = contact.MiddleName;
                sheet.Cells[row, 4] = contact.NickName;

                sheet.Cells[row, 5] = contact.Company;
                sheet.Cells[row, 6] = contact.Title;
                sheet.Cells[row, 7] = contact.Address;

                sheet.Cells[row, 8] = contact.HomePhone;
                sheet.Cells[row, 9] = contact.WorkPhone;
                sheet.Cells[row, 10] = contact.MobilePhone;
                sheet.Cells[row, 11] = contact.Fax;

                sheet.Cells[row, 12] = contact.Email;
                sheet.Cells[row, 13] = contact.Email2;
                sheet.Cells[row, 14] = contact.Email3;
                sheet.Cells[row, 15] = contact.HomePage;

                sheet.Cells[row, 22] = contact.SecondAddress;
                sheet.Cells[row, 23] = contact.SecondPhone;
                sheet.Cells[row, 24] = contact.Notes;
                row++;
                }

            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            File.Delete(fullPath);
            wb.SaveAs(fullPath);

            wb.Close();
            app.Visible = false;
            app.Quit();
        }

        static void writeGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine(String.Format("{0}, {1}, {2}",
                group.Name, group.Header, group.Footer));
            }
        }

        static void writeContactsToCsvFile(List<ContactData> contacts, StreamWriter writer)
        {
            foreach (ContactData contact in contacts)
            {
                writer.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}," +
                                 "{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23}",
                                 contact.FirstName, contact.LastName, contact.MiddleName, contact.NickName,
                                 contact.Company, contact.Title, contact.Address,
                                 contact.HomePhone, contact.WorkPhone, contact.MobilePhone, contact.Fax,
                                 contact.Email, contact.Email2, contact.Email3, contact.HomePage,
                                 contact.SecondAddress, contact.SecondPhone, contact.Notes);
            }
        }

        static void writeGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }

        static void writeContactsToXmlFile(List<ContactData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }

        static void writeGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
        }

        static void writeContactsToJsonFile(List<ContactData> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        }
    }
}