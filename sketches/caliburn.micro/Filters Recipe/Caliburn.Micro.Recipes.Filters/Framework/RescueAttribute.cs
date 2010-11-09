namespace Caliburn.Micro.Recipes.Filters.Framework {
    using System;

    /// <summary>
    /// Allows to specify a "rescue" method to handle exception occurred during execution
    /// </summary>
    /// <example>
    /// <code>
    /// [Rescue]
    /// public void ThrowingAction()
    /// {
    ///    throw new NotImplementedException();
    /// }
    /// public bool Rescue(Exception ex)
    /// {
    ///    MessageBox.Show(ex.ToString());
    ///    return true;
    /// }
    /// </code>
    /// </example>
    public class RescueAttribute : ExecutionWrapperBase {
        public RescueAttribute()
            : this("Rescue") {}

        public RescueAttribute(string methodName) {
            MethodName = methodName;
        }

        public string MethodName { get; private set; }

        protected override bool HandleException(ActionExecutionContext context, Exception ex) {
            var method = context.Target.GetType()
                .GetMethod(MethodName, new[] { typeof(Exception) });

            if(method == null)
                return false;

            try {
                var result = method.Invoke(context.Target, new object[] { ex });
                if(result is bool)
                    return (bool)result;
                return true;
            }
            catch {
                return false;
            }
        }
    }
}