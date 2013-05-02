using System.IO;
using ProjectCleaner.Parsing;

namespace ProjectCleaner.Modules
{
    public class RemoveUnknownTargetProcessor : ProjectProcessorBase
    {
        public RemoveUnknownTargetProcessor() 
            : base(2, "Remove missing target includes")
        {
        }

        public override bool Handle(ProjectParser project)
        {
            var changed = false;

            var root = project.RootElement;
            foreach (var import in project.Imports)
            {
                var importName = import.Project.ToLower();
                if (!IsRemoveableTarget(importName, import.Project)) continue;
                root.RemoveChild(import);
                changed = true;
            }

            return changed;
        }

        bool IsRemoveableTarget(string importName, string projectName)
        {
            if (importName.EndsWith("stylecop.targets")) return true;
            if (importName.Contains("msbuild")) return false;
            return File.Exists(projectName);
        }
    }
}