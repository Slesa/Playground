using System;

namespace HelloSpring
{
    public class FormatNumber : INumberFormatter
    {
        public bool CanHandle(int number)
        {
            return true;
        }

        public string Handle(int number)
        {
            return String.Format("{0}", number);
        }
    }
}