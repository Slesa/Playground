namespace Godot.IcsEditor.Ui.Events
{
    public interface IEventAggregator
    {
        TEventType GetEvent<TEventType>() where TEventType : EventBase;
    }
}