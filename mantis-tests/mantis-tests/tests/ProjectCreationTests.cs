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

        [Test]
        public void ProjectCreationTest()
        {
            List<ProjectData> oldProjectsList = app.API.GetProjectsList(account);

            ProjectData project = new ProjectData("Test Project 10")
            {
                Status = "development",
                Visibility = "public",
                Enabled = "True",
                Description = "Test Description",
            };

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