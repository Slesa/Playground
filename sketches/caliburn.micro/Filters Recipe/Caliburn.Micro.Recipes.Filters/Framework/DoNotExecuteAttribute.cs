namespace Caliburn.Micro.Recipes.Filters.Framework {
    /// <summary>
    /// Simply skip the action execution
    /// </summary>
    public class DoNotExecuteAttribute : ExecutionWrapperBase {
        protected override bool CanExecute(ActionExecutionContext context) {
            return false;
        }
    }
}