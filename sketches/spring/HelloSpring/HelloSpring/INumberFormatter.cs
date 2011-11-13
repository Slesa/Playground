namespace HelloSpring
{
    public interface INumberFormatter
    {
        bool CanHandle(int number);
        string Handle(int number);
    }
}