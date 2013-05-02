using System.IO;
using ProjectCleaner.Parsing;

namespace ProjectCleaner.Modules
{
    public class BackupProjectFileProcessor : ProjectProcessorBase
    {
        public BackupProjectFileProcessor() 
            : base(0, "Make a backup of the original project file")
        {
        }

        public override bool Handle(ProjectParser project)
        {
            var fileInfo = new FileInfo(project.ProjectFile);
            if (fileInfo.IsReadOnly) fileInfo.IsReadOnly = false;

            var newFileName = fileInfo.FullName + ".bak";
            
            if (!File.Exists(newFileName)) fileInfo.CopyTo(newFileName);

            return false;
        }
    }
}