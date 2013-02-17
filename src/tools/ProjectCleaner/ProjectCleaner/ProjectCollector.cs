using System.Collections.Generic;
using System.IO;

namespace ProjectCleaner
{
    public class ProjectCollector : ICollectProjects
    {
        public IEnumerable<string> CollectFiles(string path)
        {
            var directoryInfo = new DirectoryInfo(path);
            foreach (var directory in directoryInfo.EnumerateDirectories())
            {
                var subFiles = CollectFiles(directory.FullName);
                foreach (var file in subFiles) yield return file;
            }

            var projectFiles = directoryInfo.EnumerateFiles("*.csproj");
            foreach (var file in projectFiles) yield return file.FullName;
        }
    }
}