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
    public class ManagementMenuHelper : HelperBase
    {
        private string baseURL;

        public ManagementMenuHelper(ApplicationManager manager, string baseURL) : base(manager)
        {
            this.baseURL = baseURL;
        }

        public void GoToManageProjectPage()
        {
            if (driver.Url == baseURL + "mantisbt-2.25.4/manage_proj_page.php")
            {
                return;
            }
            driver.FindElement(By.LinkText("Управление проектами")).Click();
        }

        public void GoToManageOverviewPage()
        {
            if (driver.Url == baseURL + "mantisbt-2.25.4/manage_overview_page.php")
            {
                return;
            }
            driver.FindElement(By.XPath("//div[@id='sidebar']/ul/li[7]/a/span")).Click();
        }

        public bool CheckOpenManageProjectPage()
        {
            if (driver.Url == baseURL + "mantisbt-2.25.4/manage_proj_page.php")
            {
                return true;
            }

            return false;
        }
    }
}