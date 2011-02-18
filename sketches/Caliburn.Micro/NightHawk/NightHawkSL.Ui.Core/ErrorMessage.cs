using Caliburn.Micro;

namespace NightHawkSL.Ui.Core
{
    public class ErrorMessage
    {
        public ErrorMessage(ResultCompletionEventArgs completionEventArgs)
        {
            CompletionEventArgs = completionEventArgs;
        }
        public ResultCompletionEventArgs CompletionEventArgs { get; private set; }
    }
}