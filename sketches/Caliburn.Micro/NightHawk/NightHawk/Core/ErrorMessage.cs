using Caliburn.Micro;

namespace NightHawk.Core
{
    public class ErrorMessage
    {
        public ResultCompletionEventArgs EventArgs { get; set; }

        public ErrorMessage(ResultCompletionEventArgs eventArgs)
        {
            EventArgs = eventArgs;
        }
    }
}