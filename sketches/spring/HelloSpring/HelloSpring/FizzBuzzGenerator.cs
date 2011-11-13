using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelloSpring
{
    public class FizzBuzzGenerator
    {
        List<INumberFormatter> _formatters;

        public FizzBuzzGenerator(List<INumberFormatter> formatters)
        {
            _formatters = formatters;
        }

        public string GetOutput(int maximum)
        {
            var result = new StringBuilder();
            for (var i = 1; i <= maximum; i++)
                result.Append(GetOutputFor(i)).Append(" ");
            return result.ToString();
        }

        string GetOutputFor(int number)
        {
            return (from numberFormatter in _formatters
                    where numberFormatter.CanHandle(number)
                    select numberFormatter.Handle(number)).FirstOrDefault();
        }
    }
}