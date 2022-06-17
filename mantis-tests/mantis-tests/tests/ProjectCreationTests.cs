using System;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectCreationTests : TestBase
    {
        [SetUp]
        public void Init()
        {
            app.API.DeleteExistingProject(account, new ProjectData("New project"));
        }
        public static IEnumerable<ProjectData> RandomProjectDataProvider()
        {
            List<ProjectData> project = new List<ProjectData>();
            for (int i = 0; i < 1; i++)
            {
                project.Add(new ProjectData(GenerateRandomString(20))
                {
                    Description = GenerateRandomString(100),
                    Status = "development",
                    Visibility = "public",
                    Enabled = "True",
                });
            }
            return project;
        }

        [Test, TestCaseSource("RandomProjectDataProvider")]
        public void ProjectCreationTest(ProjectData project)
        {
            List<ProjectData> oldProjectsList = app.API.GetProjectsList(account);

            app.API.CreateNewProject(account, project);

            Assert.AreEqual(oldProjectsList.Count + 1, app.API.GetCountProjects(account));

            List<ProjectData> newProjectsList = app.API.GetProjectsList(account);

            oldProjectsList.Add(project);
            oldProjectsList.Sort();
            newProjectsList.Sort();

            Assert.AreEqual(oldProjectsList, newProjectsList);
        }
    }
}