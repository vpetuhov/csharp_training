using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectRemovingTests : AuthTestBase
    {
        [SetUp]
        public void Init()
        {
            app.ProjectManagement.CreateIfNotExist();
        }

        [Test]
        public void RemoveProject()
        {
            List<ProjectData> oldProjectsList = app.ProjectManagement.GetProjectsList();

            app.ProjectManagement.RemoveProject(0);

            List<ProjectData> newProjectsList = app.ProjectManagement.GetProjectsList();

            Assert.AreEqual(oldProjectsList.Count - 1, app.ProjectManagement.GetCountProjects());

            oldProjectsList.RemoveAt(0);
            oldProjectsList.Sort();
            newProjectsList.Sort();
            Assert.AreEqual(oldProjectsList, newProjectsList);
        }
    }
}
