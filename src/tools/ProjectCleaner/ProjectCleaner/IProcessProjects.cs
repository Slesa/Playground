using ProjectCleaner.Parsing;

namespace ProjectCleaner
{
    public interface IProcessProjects
    {
        bool Handle(ProjectParser project);
        bool IsEnabled { get; set; }
        string Name { get; }
        int Priority { get; }
    }

    public abstract class ProjectProcessorBase : IProcessProjects
    {
        protected ProjectProcessorBase(int prio, string name)
        {
            IsEnabled = true;
            Name = name;
            Priority = prio;
        }

        public abstract bool Handle(ProjectParser project);
        public string Name { get; private set; }
        public int Priority { get; private set; }
        public bool IsEnabled { get; set; }
    }
}