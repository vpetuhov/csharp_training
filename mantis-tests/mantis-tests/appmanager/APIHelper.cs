using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class APIHelper : HelperBase
    {
        public APIHelper(ApplicationManager manager) : base(manager) { }

        Mantiss.MantisConnectPortTypeClient client = new Mantiss.MantisConnectPortTypeClient();

        public void CreateNewProject(AccountData account, ProjectData projectData)
        {
            Mantiss.ProjectData project = new Mantiss.ProjectData();
            project.name = projectData.ProjectName;
            project.description = projectData.Description;
            project.status = new Mantiss.ObjectRef();
            project.status.name = projectData.Status;
            project.enabled = projectData.Enabled == "True" ? true : false;
            project.view_state = new Mantiss.ObjectRef();
            project.view_state.name = projectData.Visibility;

            client.mc_project_add(account.Username, account.Password, project);
        }

        public void RemoveProject(AccountData account, int index)
        {
            List<ProjectData> projectsList = GetProjectsList(account);

            var id = projectsList[0].Id;
            client.mc_project_delete(account.Username, account.Password, id);
        }

        public void DeleteExistingProject(AccountData account, ProjectData projectData)
        {
            List<ProjectData> projectsList = GetProjectsList(account);

            if (projectsList.Count == 0)
                return;

            var indexOfExistProject = projectsList.FindIndex(x => x.ProjectName == projectData.ProjectName);

            if (indexOfExistProject != -1)
            {
                var id = projectsList.Find(x => x.ProjectName == projectData.ProjectName).Id;
                client.mc_project_delete(account.Username, account.Password, id);
            }
        }
        public void CreateIfNotExist(AccountData account)
        {
            if (GetProjectsList(account).Count == 0)
            {
                Mantiss.ProjectData project = new Mantiss.ProjectData();
                project.name = new ProjectData("test").ProjectName;

                client.mc_project_add(account.Username, account.Password, project);
            }
        }

        public List<ProjectData> GetProjectsList(AccountData account)
        {
            List<ProjectData> projectsList = new List<ProjectData>();

            var projects = client.mc_projects_get_user_accessible(account.Username, account.Password);

            foreach (var project in projects)
            {
                projectsList.Add(new ProjectData()
                {
                    Id = project.id,
                    ProjectName = project.name,
                    Description = project.description,
                    Status = project.status.name,
                    Enabled = project.enabled.ToString(),
                    Visibility = project.view_state.name
                });
            }

            return projectsList;
        }
        public int GetCountProjects(AccountData account)
        {
            return GetProjectsList(account).Count;
        }
    }
}