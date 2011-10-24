namespace HelloSpring
{
    public class FormatMultiplesOf3And5 : INumberFormatter
    {
        public bool CanHandle(int number)
        {
            return number%3 == 0 && number%5 == 0;
        }

        public string Handle(int number)
        {
            return "FizzBuzz";
        }
    }
}