using Caliburn.Micro;

namespace Nubis.Core
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