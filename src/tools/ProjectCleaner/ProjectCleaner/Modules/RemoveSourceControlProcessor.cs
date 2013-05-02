using System.IO;
using System.Linq;
using Microsoft.Build.Construction;
using ProjectCleaner.Parsing;

namespace ProjectCleaner.Modules
{
    public class RemoveSourceControlProcessor : ProjectProcessorBase
    {
        public RemoveSourceControlProcessor() 
            : base(1, "Remove source control connections")
        {
        }

        public override bool Handle(ProjectParser project)
        {
            var changed = false;

            var root = project.RootElement;
            var propertyGroups = root.PropertyGroups;
            foreach (var propertyGroup in propertyGroups)
            {
                //System.Diagnostics.Debug.WriteLine("PropertyGroup(" + propertyGroup.Condition + ")");

                var sccProperties = propertyGroup.AllChildren.Where(x => x is ProjectPropertyElement);
                foreach (ProjectPropertyElement sccProperty in sccProperties)
                {
                    if (!sccProperty.Name.StartsWith("Scc")) continue;

                    //System.Diagnostics.Debug.WriteLine(sccProperty);
                    propertyGroup.RemoveChild(sccProperty);
                    changed = true;
                }
            }

            var directoryInfo = new DirectoryInfo(project.DirectoryPath);
            var sccFiles = directoryInfo.EnumerateFiles("*.vspscc").Concat(directoryInfo.EnumerateFiles("*.vssscc"));
            foreach (var sccFile in sccFiles)
            {
                sccFile.MoveTo(sccFile.FullName+".bak");
            }
            return changed;
        }
    }
}