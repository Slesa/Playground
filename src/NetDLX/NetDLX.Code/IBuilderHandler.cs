namespace NetDLX.Code
{
    public interface IBuilderHandler
    {
        bool CanHandle(string line);
        string Handle(Program program, string line);
    }
}