using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

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

        public ContactHelper GoToNewContactPage()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper FillNewContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.FirstName);
            Type(By.Name("lastname"), contact.LastName);
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/input[21]")).Click();
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            return this;
        }

        public ContactHelper InitContactModification(int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + (index + 1) + "]")).Click();
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public ContactHelper RemoveContact(int v)
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            driver.SwitchTo().Alert().Accept(); 
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
                ContactData data = new ContactData("Jim", "Raynor");

                Create(data);
            }
            return this;
        }

        public List<ContactData> GetContactList()
        {
            manager.Navigator.GoToHomePage();

            List<ContactData> contacts = new List<ContactData>();
            ICollection<IWebElement> elements = driver.FindElements(By.Name("entry"));
            foreach (IWebElement element in elements)
            {
                IList<IWebElement> entry = element.FindElements(By.TagName("td"));
                string lastName = entry[1].Text;
                string firstName = entry[2].Text;

                contacts.Add(new ContactData(firstName, lastName));
            }

            return contacts;
        }
    }
}