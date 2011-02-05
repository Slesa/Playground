using System.ComponentModel;

namespace Godot.IcsEditor.Ui.Events
{
    public class BackgroundEventSubscription<TPayload> : EventSubscription<TPayload>
    {
        public BackgroundEventSubscription(IDelegateReference actionReference, IDelegateReference filterReference) 
            : base(actionReference, filterReference)
        {
        }

        public override void InvokeAction(System.Action<TPayload> action, TPayload argument)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += (sender, e) => action((TPayload) e.Argument);

            worker.RunWorkerAsync(argument);
        }
    }
}