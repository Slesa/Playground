namespace Caliburn.Micro.Recipes.Filters.Framework {
    using System;

    /// <summary>
    /// Allows to specify a guard method or property with an arbitrary name
    /// </summary>
    /// <example>
    /// <code>
    /// [Preview("IsMyActionAvailable")]
    /// public void MyAction(int value) { ... }
    /// public bool IsMyActionAvailable(int value) { ... }
    /// </code>
    /// </example>
    public class PreviewAttribute : Attribute, IContextAware {
        public PreviewAttribute(string methodName) {
            MethodName = methodName;
        }

        public string MethodName { get; private set; }
        public int Priority { get; set; }

        public void MakeAwareOf(ActionExecutionContext context) {
            var targetType = context.Target.GetType();
            var guard = targetType.GetMethod(MethodName);
            if(guard == null)
                guard = targetType.GetMethod("get_" + MethodName);

            if(guard == null)
                return;

            var oldCanExecute = context.CanExecute;
            context.CanExecute = () => {
                if(oldCanExecute!=null && !oldCanExecute())
                    return false;

                return (bool)guard.Invoke(
                    context.Target,
                    MessageBinder.DetermineParameters(context, guard.GetParameters())
                    );
            };
        }

        public void Dispose() {}
    }
}