namespace HelloSpring
{
    public class FormatMultiplesOf3 : INumberFormatter
    {
        public bool CanHandle(int number)
        {
            return number%3 == 0;
        }

        public string Handle(int number)
        {
            return "Fizz";
        }
    }
}