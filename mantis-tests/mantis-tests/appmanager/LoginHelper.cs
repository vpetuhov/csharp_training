using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class LoginHelper : HelperBase
    {
        private string baseURL;
        public LoginHelper(ApplicationManager manager, string baseURL) : base(manager)
        {
            this.baseURL = baseURL;
        }

        public void OpenLoginPage()
        {
            if (driver.Url == baseURL + "mantisbt-2.25.4/login_page.php")
            {
                return;
            }

            driver.Navigate().GoToUrl(baseURL + "mantisbt-2.25.4/login_page.php");
        }

        public void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                if (IsLoggedIn(account))
                {
                    return;
                }

                Logout();
            }
            Type(By.Name("username"), account.Username);
            driver.FindElement(By.XPath("//input[@value='Вход']")).Click();
            Type(By.Name("password"), account.Password);
            driver.FindElement(By.XPath("//input[@value='Вход']")).Click();
        }

        public void Logout()
        {
            if (IsLoggedIn())
            {
                driver.FindElement(By.LinkText("Выход")).Click();
            }
        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(By.CssSelector("span.user-info"));
        }

        public bool IsLoggedIn(AccountData account)
        {
            return IsLoggedIn()
                && GetLoggedUserName() == account.Username;
        }

        public string GetLoggedUserName()
        {
             return driver.FindElement(By.CssSelector("span.user-info")).Text;
        }
    }
}