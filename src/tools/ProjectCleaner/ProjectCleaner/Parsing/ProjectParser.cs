using System.Collections.Generic;
using Microsoft.Build.Construction;

namespace ProjectCleaner.Parsing
{
    public class ProjectParser
    {
        public ProjectParser(string projectFile)
        {
            ProjectFile = projectFile;
        }

        public string DirectoryPath { get; set; }
        public string ProjectFile { get; set; }
        public ProjectRootElement RootElement { get; private set; }
        public ICollection<ProjectImportElement> Imports { get; private set; }

        public bool ParseProject()
        {
            RootElement = ProjectRootElement.Open(ProjectFile);
            if (RootElement == null) return false;

            DirectoryPath = RootElement.DirectoryPath;
            Imports = RootElement.Imports;
            
            /*

            ReadItemLists(root);

            var defaultGroup = root.PropertyGroups.FirstOrDefault(x => string.IsNullOrEmpty(x.Condition));
            if (defaultGroup != null) ReadProperties(defaultGroup.Properties.ToList());

            ReadPropertyGroups(root);
            */
            return true;
        }

    }
}