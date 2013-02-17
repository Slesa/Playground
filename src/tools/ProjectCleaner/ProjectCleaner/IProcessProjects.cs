using ProjectCleaner.Parsing;

namespace ProjectCleaner
{
    public interface IProcessProjects
    {
        bool Handle(ProjectParser project);
    }
}