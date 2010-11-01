namespace Caliburn.Micro.Recipes.Filters.Results {
    using System;
    using System.Windows;

    public class MessageBoxResult : IResult {
        readonly string message;

        public MessageBoxResult(string message) {
            this.message = message;
        }

        public void Execute(ActionExecutionContext context) {
            MessageBox.Show(message);
            Completed(this, new ResultCompletionEventArgs());
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }
}