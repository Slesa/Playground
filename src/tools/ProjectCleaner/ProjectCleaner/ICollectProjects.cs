using System.Collections.Generic;

namespace ProjectCleaner
{
    public interface ICollectProjects
    {
        IEnumerable<string> CollectFiles(string path);
    }
}