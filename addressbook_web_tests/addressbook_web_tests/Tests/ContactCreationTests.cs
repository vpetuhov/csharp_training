using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : ContactTestBase
    {
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contact = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contact.Add(new ContactData(GenerateRandomString(30), GenerateRandomString(30))
                {
                    MiddleName = GenerateRandomString(30),
                    NickName = GenerateRandomString(30),

                    Company = GenerateRandomString(100),
                    Title = GenerateRandomString(100),
                    Address = GenerateRandomString(100),

                    HomePhone = GenerateRandomString(20),
                    WorkPhone = GenerateRandomString(20),
                    MobilePhone = GenerateRandomString(20),
                    Fax = GenerateRandomString(20),

                    Email = GenerateRandomString(50),
                    Email2 = GenerateRandomString(50),
                    Email3 = GenerateRandomString(50),
                    HomePage = GenerateRandomString(50),

                    SecondAddress = GenerateRandomString(100),
                    SecondPhone = GenerateRandomString(20),
                    Notes = GenerateRandomString(100)
                });
            }

            return contact;
        }

        public static IEnumerable<ContactData> ContactDataFromCsvFile()
        {
            List<ContactData> contacts = new List<ContactData>();
            string[] lines = File.ReadAllLines(@"contacts.csv");

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                contacts.Add(new ContactData()
                {
                    FirstName = parts[0],
                    LastName = parts[1],
                    MiddleName = parts[2],
                    NickName = parts[3],

                    Company = parts[4],
                    Title = parts[5],
                    Address = parts[6],

                    HomePhone = parts[7],
                    WorkPhone = parts[8],
                    MobilePhone = parts[9],
                    Fax = parts[10],

                    Email = parts[11],
                    Email2 = parts[12],
                    Email3 = parts[13],
                    HomePage = parts[14],

                    SecondAddress = parts[21],
                    SecondPhone = parts[22],
                    Notes = parts[23]
                });
            }
            return contacts;
        }

        public static IEnumerable<ContactData> ContactDataFromXmlFile()
        {
            return (List<ContactData>)
                new XmlSerializer(typeof(List<ContactData>)).Deserialize(new StreamReader(@"contacts.xml"));
        }

        public static IEnumerable<ContactData> ContactDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<ContactData>>(File.ReadAllText(@"contacts.json"));
        }

        public static IEnumerable<ContactData> ContactDataFromExcelFile()
        {
            List<ContactData> contacts = new List<ContactData>();
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Open(Path.Combine(Directory.GetCurrentDirectory(), @"contacts.xlsx"));
            Excel.Worksheet sheet = wb.Sheets[1];
            Excel.Range range = sheet.UsedRange;

            for (int i = 1; i <= range.Rows.Count; i++)
            {
                contacts.Add(new ContactData()
                {
                    FirstName = range.Cells[i, 1].Value.ToString(),
                    LastName = range.Cells[i, 2].Value.ToString(),
                    MiddleName = range.Cells[i, 3].Value.ToString(),
                    NickName = range.Cells[i, 4].Value.ToString(),

                    Company = range.Cells[i, 5].Value.ToString(),
                    Title = range.Cells[i, 6].Value.ToString(),
                    Address = range.Cells[i, 7].Value.ToString(),

                    HomePhone = range.Cells[i, 8].Value.ToString(),
                    WorkPhone = range.Cells[i, 9].Value.ToString(),
                    MobilePhone = range.Cells[i, 10].Value.ToString(),
                    Fax = range.Cells[i, 11].Value.ToString(),

                    Email = range.Cells[i, 12].Value.ToString(),
                    Email2 = range.Cells[i, 13].Value.ToString(),
                    Email3 = range.Cells[i, 14].Value.ToString(),
                    HomePage = range.Cells[i, 15].Value.ToString(),

                    SecondAddress = range.Cells[i, 16].Value.ToString(),
                    SecondPhone = range.Cells[i, 17].Value.ToString(),
                    Notes = range.Cells[i, 18].Value.ToString(),
                });
            }

            wb.Close();
            app.Visible = false;
            app.Quit();

            return contacts;
        }

        [Test, TestCaseSource("ContactDataFromXmlFile")]
        public void ContactCreationTest(ContactData contact)
        {

            List<ContactData> oldСontacts = ContactData.GetAll();

            app.Contact.Create(contact);

            Assert.AreEqual(oldСontacts.Count + 1, app.Contact.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();
            oldСontacts.Add(contact);
            oldСontacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldСontacts, newContacts);
        }
    }
}