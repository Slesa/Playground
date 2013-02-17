using System.IO;
using ProjectCleaner.Parsing;

namespace ProjectCleaner.Modules
{
    public class BackupProjectFileProcessor : IProcessProjects
    {
        public bool Handle(ProjectParser project)
        {
            var fileInfo = new FileInfo(project.ProjectFile);
            var newFileName = fileInfo.FullName + ".bak";
            
            if (!File.Exists(newFileName)) fileInfo.CopyTo(newFileName);

            return false;
        }
    }
}