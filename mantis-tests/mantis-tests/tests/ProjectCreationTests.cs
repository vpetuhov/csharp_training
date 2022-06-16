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
    public class ProjectCreationTests : AuthTestBase
    {
        [SetUp]
        public void Init()
        {
            app.ProjectManagement.DeleteExistingProject(new ProjectData("New project"));
        }

        [Test]
        public void ProjectCreationTest()
        {
            List<ProjectData> oldProjectsList = app.ProjectManagement.GetProjectsList();

            ProjectData project = new ProjectData("Test Project 5")
            {
                Status = "в разработке",
                Visibility = "публичный",
                Enabled = "true",
                Description = "Test Description",
            };

            app.ProjectManagement.CreateNewProject(project);

            Assert.AreEqual(oldProjectsList.Count + 1, app.ProjectManagement.GetCountProjects());

            List<ProjectData> newProjectsList = app.ProjectManagement.GetProjectsList();

            project.Visibility = "публичный";
            oldProjectsList.Add(project);
            oldProjectsList.Sort();
            newProjectsList.Sort();

            Assert.AreEqual(oldProjectsList, newProjectsList);
        }
    }
}