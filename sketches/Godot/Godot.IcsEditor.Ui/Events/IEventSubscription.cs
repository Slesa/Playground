using System;

namespace Godot.IcsEditor.Ui.Events
{
    public interface IEventSubscription
    {
        SubscriptionToken SubscriptionToken { get; set; }

        Action<object[]> GetExecutionStrategy();
    }
}