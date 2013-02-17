using System.IO;
using ProjectCleaner.Parsing;

namespace ProjectCleaner.Modules
{
    public class RemoveUnknownTargetProcessor : IProcessProjects
    {
        public bool Handle(ProjectParser project)
        {
            var changed = false;

            var root = project.RootElement;
            foreach (var import in project.Imports)
            {
                var importName = import.Project.ToLower();
                if (importName.Contains("msbuild")) continue;
                if (File.Exists(import.Project)) continue;
                root.RemoveChild(import);
                changed = true;
            }

            return changed;
        }
    }
}