using Caliburn.Micro;

namespace MediaOwl.Core
{
    /// <summary>
    /// This class is used to show how the <see cref="EventAggregator"/> works.
    /// An ErrorMessage is published in all <see cref="IResult"/>-Instances in <see cref="MediaOwl.Services"/>
    /// and subscribed by <see cref="MediaOwl.ViewModels.ShellViewModel"/>.
    /// </summary>
    public class ErrorMessage
    {
        public ErrorMessage(ResultCompletionEventArgs completionEventArgs)
        {
            CompletionEventArgs = completionEventArgs;
        }
        public ResultCompletionEventArgs CompletionEventArgs { get; private set; }
    }
}