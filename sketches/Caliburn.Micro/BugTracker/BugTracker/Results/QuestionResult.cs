using System;
using BugTracker.ViewModel;
using Caliburn.Micro;

namespace BugTracker.Results
{
    public class QuestionResult : IResult
    {
        private readonly QuestionViewModel _question;

        private Answer? _cancelAnswer;

        public QuestionResult(QuestionViewModel question)
        {
            _question = question;
        }

        #region IResult Members

        public void Execute(ActionExecutionContext context)
        {
            var windowManager = IoC.Get<IWindowManager>();
            windowManager.ShowDialog(_question);

            var args = new ResultCompletionEventArgs();
            args.WasCancelled = _cancelAnswer.HasValue && _question.GivenAnswer == _cancelAnswer.Value;

            Completed(this, args);
        }

        public event EventHandler<ResultCompletionEventArgs> Completed;

        #endregion

        public QuestionResult CancelOn(Answer answer)
        {
            _cancelAnswer = answer;

            return this;
        }
    }
}