using System.Linq;
using Microsoft.Build.Construction;
using ProjectCleaner.Parsing;

namespace ProjectCleaner.Modules
{
    public class RemoveSourceControlProcessor : IProcessProjects
    {
        public bool Handle(ProjectParser project)
        {
            var changed = false;

            var root = project.RootElement;
            var propertyGroups = root.PropertyGroups;
            foreach (var propertyGroup in propertyGroups)
            {
                System.Diagnostics.Debug.WriteLine("PropertyGroup(" + propertyGroup.Condition + ")");

                var sccProperties = propertyGroup.AllChildren.Where(x => x is ProjectPropertyElement);
                foreach (ProjectPropertyElement sccProperty in sccProperties)
                {
                    if (!sccProperty.Name.StartsWith("Scc")) continue;

                    System.Diagnostics.Debug.WriteLine(sccProperty);
                    propertyGroup.RemoveChild(sccProperty);
                    changed = true;
                }
            }

            return changed;
        }
    }
}