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
    public class ProjectManagementHelper : HelperBase
    {
        public ProjectManagementHelper(ApplicationManager manager) : base(manager) { }
        
        public ProjectManagementHelper CreateNewProject(ProjectData project)
        {
            if (manager.ManagementMenu.CheckOpenManageProjectPage() == false)
            {
                manager.ManagementMenu.GoToManageOverviewPage();
                manager.ManagementMenu.GoToManageProjectPage();
            }

            InitProjectCreation();
            FillProjectCreationForm(project);
            SubmitProjectCreation();

            return this;
        }
        private ProjectManagementHelper InitProjectCreation()
        {
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            return this;
        }

        private ProjectManagementHelper FillProjectCreationForm(ProjectData project)
        {
            Type(By.Name("name"), project.ProjectName);
            Type(By.Name("description"), project.Description);
            return this;
        }

        private ProjectManagementHelper SubmitProjectCreation()
        {
            driver.FindElement(By.XPath("//input[@value='Добавить проект']")).Click();
            driver.FindElement(By.LinkText("Продолжить")).Click();
            return this;
        }

        public ProjectManagementHelper RemoveProject(int index)
        {
            if (manager.ManagementMenu.CheckOpenManageProjectPage() == false)
            {
                manager.ManagementMenu.GoToManageOverviewPage();
                manager.ManagementMenu.GoToManageProjectPage();
            }

            InitRemovingProject(index);
            SubmitRemovingProject();

            return this;
        }

        public ProjectManagementHelper InitRemovingProject(int index)
        {
            driver.FindElement(By.ClassName("widget-body"))
                  .FindElement(By.TagName("table"))
                  .FindElement(By.TagName("tbody"))
                  .FindElements(By.TagName("tr"))[index]
                  .FindElement(By.TagName("a")).Click();

            return this;
        }

        public ProjectManagementHelper SubmitRemovingProject()
        {
            driver.FindElement(By.XPath("//input[@value='Удалить проект']")).Click();
            driver.FindElement(By.XPath("//input[@value='Удалить проект']")).Click();

            return this;
        }

        public int GetCountProjects()
        {
            if (manager.ManagementMenu.CheckOpenManageProjectPage() == false)
            {
                manager.ManagementMenu.GoToManageOverviewPage();
                manager.ManagementMenu.GoToManageProjectPage();
            }

            var countProjects = driver.FindElement(By.ClassName("widget-body"))
                              .FindElement(By.TagName("table"))
                              .FindElement(By.TagName("tbody"))
                              .FindElements(By.TagName("tr"));

            return countProjects.Count;
        }

        public List<ProjectData> GetProjectsList()
        {
            List<ProjectData> list = new List<ProjectData>();

            if (manager.ManagementMenu.CheckOpenManageProjectPage() == false)
            {
                manager.ManagementMenu.GoToManageOverviewPage();
                manager.ManagementMenu.GoToManageProjectPage();
            }

            ICollection<IWebElement> countProjects = driver.FindElement(By.ClassName("widget-body"))
                                                  .FindElement(By.TagName("table"))
                                                  .FindElement(By.TagName("tbody"))
                                                  .FindElements(By.TagName("tr"));

            if (countProjects.Count == 0)
            {
                return list;
            }

            foreach (IWebElement count in countProjects)
            {
                IList<IWebElement> cells = count.FindElements(By.TagName("td"));
                string projectName = cells[0].Text;
                string status = cells[1].Text;
                string enabled = "";
                if (cells[2].Text == " ")
                    enabled = "false";
                else
                    enabled = "true";
                string visibility = cells[3].Text;
                string description = cells[4].Text;

                list.Add(new ProjectData(projectName)
                {
                    Status = status,
                    Enabled = enabled,
                    Visibility = visibility,
                    Description = description
                });
            }

            return list;
        }

        public void DeleteExistingProject(ProjectData project)
        {
            List<ProjectData> list = GetProjectsList();

            if (list.Count == 0)
                return;

            var indexExistingProject = list.FindIndex(x => x.ProjectName == project.ProjectName);

            if (indexExistingProject != -1)
            {
                RemoveProject(indexExistingProject);
            }
        }

        public void CreateIfNotExist()
        {
            if (GetCountProjects() == 0)
            {
                CreateNewProject(new ProjectData("New Project"));
            }
        }
    }
}