using System.Linq;
using Microsoft.Build.Construction;
using ProjectCleaner.Parsing;

namespace ProjectCleaner.Modules
{
    public class ResetTargetFrameworkProcessor : ProjectProcessorBase
    {
        public ResetTargetFrameworkProcessor()
            : base(3, "Reset target framework version to 4.0")
        {
        }

        public override bool Handle(ProjectParser project)
        {
            var changed = false;

            var root = project.RootElement;
            var propertyGroups = root.PropertyGroups;
            foreach (var propertyGroup in propertyGroups)
            {
                var projectElements = propertyGroup.AllChildren.Where(x => x is ProjectPropertyElement);
                foreach (ProjectPropertyElement propertyElement in projectElements)
                {
                    if (!propertyElement.Name.ToLower().Equals("targetframeworkversion")) continue;
                    propertyElement.Value = "v4.0";
                    changed = true;
                }
            }
            return changed;
        }
    }
}