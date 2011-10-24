namespace HelloSpring
{
    public class FormatMultiplesOf5 : INumberFormatter
    {
        public bool CanHandle(int number)
        {
            return number%5 == 0;
        }

        public string Handle(int number)
        {
            return "Buzz";
        }
    }
}