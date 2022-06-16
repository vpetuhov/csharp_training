using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class ApplicationManager
    {
        public IWebDriver driver;
        public string baseURL;
        public static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();
        public LoginHelper loginHelper;
        public ManagementMenuHelper managementMenuHelper;
        public ProjectManagementHelper projectManagementHelper;
        public APIHelper apiHelper;

        public ApplicationManager()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.BrowserExecutableLocation = @"c:\Program Files\Mozilla Firefox\firefox.exe";
            options.UseLegacyImplementation = true;
            driver = new FirefoxDriver(options);
            baseURL = "http://localhost:8080/";

            loginHelper = new LoginHelper(this, baseURL);
            managementMenuHelper = new ManagementMenuHelper(this, baseURL);
            projectManagementHelper = new ProjectManagementHelper(this);
            apiHelper = new APIHelper(this);
        }

        ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        public static ApplicationManager GetInstance()
        {
            if (! app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                app.Value = newInstance;
                newInstance.Auth.OpenLoginPage();
            }
            return app.Value;
        }

        public IWebDriver Driver 
        {
            get
            {
                return driver;
            }
        }
        public LoginHelper Auth
        {
            get
            {
                return loginHelper;
            }
        }

        public ManagementMenuHelper ManagementMenu
        {
            get
            {
                return managementMenuHelper;
            }
        }

        public ProjectManagementHelper ProjectManagement
        {
            get
            {
                return projectManagementHelper;
            }
        }

        public APIHelper API
        {
            get
            {
                return apiHelper;
            }
        }
    }
}