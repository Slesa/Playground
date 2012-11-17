using System;

namespace Selections.Helpers
{
    /// <summary>
    /// Weak event handler implementation.
    /// </summary>
    /// <typeparam name="TInstance">The type of the object with the actual handler.</typeparam>
    /// <typeparam name="TSender">Type of the event sender.</typeparam>
    /// <typeparam name="TEventArgs">Type of the event arguments.</typeparam>
    public class WeakEventHandler<TInstance, TSender, TEventArgs>
        where TInstance : class
    {
        private readonly WeakReference instanceReference;
        private Action<TInstance, TSender, TEventArgs> handlerAction;
        private Action<WeakEventHandler<TInstance, TSender, TEventArgs>> detachAction;

        /// <summary>
        /// Initializes a new instance of the WeakEventHandler{TInstance, TSender, TEventArgs} class.
        /// </summary>
        /// <param name="instance">The object with the actual handler, to which a weak reference will be held.</param>
        /// <param name="handlerAction">An action to invoke the actual handler.</param>
        /// <param name="detachAction">An action to detach the weak handler from the event.</param>
        public WeakEventHandler(
            TInstance instance,
            Action<TInstance, TSender, TEventArgs> handlerAction,
            Action<WeakEventHandler<TInstance, TSender, TEventArgs>> detachAction)
        {
            this.instanceReference = new WeakReference(instance);
            this.handlerAction = handlerAction;
            this.detachAction = detachAction;
        }

        /// <summary>
        /// Removes the weak event handler from the event.
        /// </summary>
        public void Detach()
        {
            if (this.detachAction != null)
            {
                this.detachAction(this);
                this.detachAction = null;
            }
        }

        /// <summary>
        /// Invokes the handler action.
        /// </summary>
        /// <remarks>
        /// This method must be added as the handler to the event.
        /// </remarks>
        /// <param name="sender">The event source object.</param>
        /// <param name="e">The event arguments.</param>
        public void OnEvent(TSender sender, TEventArgs e)
        {
            var instance = this.instanceReference.Target as TInstance;
            if (instance != null)
            {
                if (this.handlerAction != null)
                {
                    this.handlerAction((TInstance)this.instanceReference.Target, sender, e);
                }
            }
            else
            {
                this.Detach();
            }
        }
    }
}