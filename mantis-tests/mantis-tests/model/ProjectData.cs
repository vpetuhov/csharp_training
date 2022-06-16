using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class ProjectData : IEquatable<ProjectData>, IComparable<ProjectData>

    {
        public ProjectData(string projectName)
        {
            ProjectName = projectName;
        }

        public string ProjectName { get; set; }
        public string Status { get; set; }
        public string Enabled { get; set; }
        public string Visibility { get; set; }
        public string Description { get; set; }


        public bool Equals(ProjectData other)
        {
            if (object.ReferenceEquals(other, null))
                return false;
            if (object.ReferenceEquals(this, other))
                return true;
            return ProjectName == other.ProjectName && Description == other.Description 
                   && Status == other.Status && Enabled == other.Enabled && Visibility == other.Visibility;
        
        }

        public int CompareTo(ProjectData other)
        {
            if (object.ReferenceEquals(other, null))
                return 1;

            if (ProjectName != null && ProjectName.CompareTo(other.ProjectName) != 0)
                return ProjectName.CompareTo(other.ProjectName);
            else if (Description != null && Description.CompareTo(other.Description) != 0)
                return Description.CompareTo(other.Description);
            else if (Status.CompareTo(other.Status) != 0)
                return Status.CompareTo(other.Status);
            else if (Enabled.CompareTo(other.Enabled) != 0)
                return Enabled.CompareTo(other.Enabled);
            else if (Visibility.CompareTo(other.Visibility) != 0)
                return Visibility.CompareTo(other.Visibility);

            return 0;
        }
    }   
}