using System;
using System.Reflection;

namespace Godot.IcsEditor.Ui.Events
{
    public class DelegateReference : IDelegateReference
    {
        readonly Delegate _delegate;
        readonly WeakReference _weakReference;
        readonly MethodInfo _method;
        readonly Type _delegateType;

        public DelegateReference(Delegate @delegate, bool keepReferenceAlive)
        {
            if( @delegate==null )
                throw new ArgumentNullException("delegate");

            if (keepReferenceAlive)
                _delegate = @delegate;
            else
            {
                _weakReference = new WeakReference(@delegate.Target);
                _method = @delegate.Method;
                _delegateType = @delegate.GetType();
            }
        }

        public Delegate Target
        {
            get
            {
                return _delegate ?? TryGetDelegate();
            }
        }

        Delegate TryGetDelegate()
        {
            if (_method.IsStatic)
                return Delegate.CreateDelegate(_delegateType, null, _method);
            var target = _weakReference.Target;
            return target != null ? Delegate.CreateDelegate(_delegateType, target, _method) : null;
        }
    }
}