using System;
using Caliburn.Micro;
using Lucifer.Editor.ViewModel;

namespace Lucifer.Editor.Results
{
    public class QuestionResult : IResult
    {
        readonly QuestionViewModel _questionViewModel;
        Answer? _cancelAnswer;

        public QuestionResult(QuestionViewModel viewModel)
        {
            _questionViewModel = viewModel;
        }

        public void Execute(ActionExecutionContext context)
        {
            var windowManager = IoC.Get<IWindowManager>();
            windowManager.ShowDialog(_questionViewModel);

            var args = new ResultCompletionEventArgs();
            args.WasCancelled = _cancelAnswer.HasValue && _questionViewModel.GivenAnswer == _cancelAnswer.Value;

            Completed(this, args);
        }

        public event EventHandler<ResultCompletionEventArgs> Completed;

        public QuestionResult CancelOn(Answer answer)
        {
            _cancelAnswer = answer;
            return this;
        }
    }
}