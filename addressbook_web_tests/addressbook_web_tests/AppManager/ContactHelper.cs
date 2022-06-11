using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;


namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        public ContactHelper Create(ContactData contact)
        {
            GoToNewContactPage();
            FillNewContactForm(contact);
            SubmitContactCreation();
            manager.Navigator.ReturnToHomePage();
            return this;
        }

        public ContactHelper Modify(int v, ContactData newData)
        {
            if(IsContactCreated() == false)
            {
                GoToNewContactPage();
                SubmitContactCreation();
                manager.Navigator.ReturnToHomePage();
            }
            SelectContact(v);
            InitContactModification(v);
            FillNewContactForm(newData);
            SubmitContactModification();
            manager.Navigator.ReturnToHomePage();
            return this;
        }

        public ContactHelper Modify(ContactData oldData, ContactData newData)
        {
            if (IsContactCreated() == false)
            {
                GoToNewContactPage();
                SubmitContactCreation();
                manager.Navigator.ReturnToHomePage();
            }
            InitContactModification(oldData.Id);
            FillNewContactForm(newData);
            SubmitContactModification();
            manager.Navigator.ReturnToHomePage();
            return this;
        }

        public ContactHelper Remove(int v)
        {
            if (IsContactCreated() == false)
            {
                GoToNewContactPage();
                SubmitContactCreation();
                manager.Navigator.ReturnToHomePage();
            }
            SelectContact(v);
            RemoveContact(v);
            return this;
        }

        public ContactHelper Remove(ContactData contact)
        {
            if (IsContactCreated() == false)
            {
                GoToNewContactPage();
                SubmitContactCreation();
                manager.Navigator.ReturnToHomePage();
            }
            SelectContact(contact.Id);
            RemoveContact(contact.Id);
            return this;
        }

        public ContactHelper GoToNewContactPage()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper FillNewContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.FirstName);
            Type(By.Name("middlename"), contact.MiddleName);
            Type(By.Name("lastname"), contact.LastName);
            Type(By.Name("nickname"), contact.NickName);
            Type(By.Name("title"), contact.Title);
            Type(By.Name("company"), contact.Company);
            Type(By.Name("address"), contact.Address);
            Type(By.Name("home"), contact.HomePhone);
            Type(By.Name("mobile"), contact.MobilePhone);
            Type(By.Name("work"), contact.WorkPhone);
            Type(By.Name("fax"), contact.Fax);
            Type(By.Name("email"), contact.Email);
            Type(By.Name("email2"), contact.Email2);
            Type(By.Name("email3"), contact.Email3);
            Type(By.Name("homepage"), contact.HomePage);
            Type(By.Name("address2"), contact.SecondAddress);
            Type(By.Name("phone2"), contact.SecondPhone);
            Type(By.Name("notes"), contact.Notes);
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/input[21]")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            return this;
        }

        public ContactHelper SelectContact(string id)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]' and @value = '" + id + "'])")).Click();
            return this;
        }

        public ContactHelper OpenDetailsContactPage(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[6]
                .FindElement(By.TagName("a")).Click(); 
            return this;
        }

        public ContactHelper InitContactModification(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();
            return this;
        }

        public ContactHelper InitContactModification(string id)
        {
            driver.FindElement(By.XPath("//input[@id='" + id + "']/../.."))
                 .FindElements(By.TagName("td"))[7]
                 .FindElement(By.TagName("a")).Click(); 
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper RemoveContact(int v)
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            driver.SwitchTo().Alert().Accept();
            contactCache = null;
            return this;
        }

        public ContactHelper RemoveContact(string id)
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            driver.SwitchTo().Alert().Accept();
            contactCache = null;
            return this;
        }

        public bool IsContactCreated()
        {
            return IsElementPresent(By.Name("selected[]"));
        }

        public ContactHelper CreateIfNotExist()
        {
            manager.Navigator.GoToHomePage();

            if (IsContactCreated() == false)
            {
                ContactData data = new ContactData("Jim", "Raynor")
                {
                    MiddleName = "James",
                    NickName = "NickName",

                    Company = "Eng",
                    Title = "Title",
                    Address = "г.Москва, ул.Баррикадная, д.30",

                    HomePhone = "+7(965) 125 85 63",
                    WorkPhone = "8(43678)8-25-36",
                    MobilePhone = "89088881134",
                    Fax = "225-25-25",

                    Email = "email@mail.ru",
                    Email2 = "email2@yandex.ru",
                    Email3 = "email3@gmail.com",
                    HomePage = "http://HomePage.ru",

                    SecondAddress = "г.Москва, ул.Неизвестная, д.38",
                    SecondPhone = "185-96-525",
                    Notes = "Notes"
                };

                Create(data);
            }
            return this;
        }

        private List<ContactData> contactCache = null;


        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.GoToHomePage();

                List<ContactData> contacts = new List<ContactData>();
                ICollection<IWebElement> elements = driver.FindElements(By.Name("entry"));
                foreach (IWebElement element in elements)
                {
                    IList<IWebElement> entry = element.FindElements(By.TagName("td"));
                    string id = entry[0].FindElement(By.Name("selected[]")).GetAttribute("value");
                    string lastName = entry[1].Text;
                    string firstName = entry[2].Text;

                    contactCache.Add(new ContactData(firstName, lastName)
                    {
                        Id = id
                    });
                }
            }
            return new List<ContactData> (contactCache);
        }

        public int GetContactCount()
        {
            manager.Navigator.GoToHomePage();
            return driver.FindElements(By.Name("entry")).Count;
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmail = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhones,
                AllEmails = allEmail,
            };
        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(index);

            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string middleName = driver.FindElement(By.Name("middlename")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string nickName = driver.FindElement(By.Name("nickname")).GetAttribute("value");

            string company = driver.FindElement(By.Name("company")).GetAttribute("value");
            string title = driver.FindElement(By.Name("title")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string fax = driver.FindElement(By.Name("fax")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");
            string homePage = driver.FindElement(By.Name("homepage")).GetAttribute("value");

            string secondAddress = driver.FindElement(By.Name("address2")).GetAttribute("value");
            string secondPhone = driver.FindElement(By.Name("phone2")).GetAttribute("value");
            string notes = driver.FindElement(By.Name("notes")).GetAttribute("value");


            return new ContactData(firstName, lastName)
            {
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                NickName = nickName,
                Company = company,
                Title = title,
                Address = address, 
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Fax = fax,
                Email = email,
                Email2 = email2,
                Email3 = email3,
                HomePage = homePage,
                SecondAddress = secondAddress,
                SecondPhone = secondPhone,
                Notes = notes
            };
        }

        public ContactData GetContactInformationFromDetailsForm(int index)
        {
            manager.Navigator.GoToHomePage();
            OpenDetailsContactPage(index);

            var contactDetailInfo = driver.FindElement(By.CssSelector("div#content")).Text;

            string[] lines = contactDetailInfo.Split(new[] { "\r\n\r\n\r\n", "\r\n\r\n", "\r\n" }, StringSplitOptions.None);
            List<string> details = new List<string>();
            foreach (string line in lines)
            {
                details.Add(line);
            }

            return new ContactData(null, null)
            {
                ContactDetails = details
            };
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.GoToHomePage();

            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);

            return Int32.Parse(m.Value);
        }

        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();
            ClearGroupFilter();
            SelectContactToAdd(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactTogroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        public ContactHelper RemoveContactFromGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();
            SelectGroupFromFilter(group.Name);
            SelectContactToAdd(contact.Id);
            CommitRemovingContactFromGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);


            return this;
        }

        public void CommitRemovingContactFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }

        public void SelectGroupFromFilter(string groupName)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(groupName);
        }

        public void SelectContactToAdd(string contactId)
        {
            driver.FindElement(By.Id(contactId)).Click();
        }

        public void CommitAddingContactTogroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        public void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        public void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }
    }
}