using System;
using System.ComponentModel.Composition;

namespace Caliburn.Micro.CoroutinesSL
{
    public class ShowScreen : IResult
    {
        readonly Type _screenType;
        readonly string _name;

        [Import]
        public IShell Shell { get; set; }

        public ShowScreen(string name)
        {
            _name = name;
        }

        public ShowScreen(Type screenType)
        {
            _screenType = screenType;
        }

        public void Execute(ActionExecutionContext context)
        {
            var screen = !string.IsNullOrEmpty(_name)
                             ? IoC.Get<object>(_name)
                             : IoC.GetInstance(_screenType, null);

            Shell.ActivateItem(screen);
            Completed(this, new ResultCompletionEventArgs());
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };

        public static ShowScreen Of<T>()
        {
            return new ShowScreen(typeof(T));
        }
    }
}