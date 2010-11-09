namespace Caliburn.Micro.Recipes.Filters.Framework {
    using System;

    /// <summary>
    /// Sets "IsBusy" property to true (on models implementing ICanBeBusy) during the execution
    /// </summary>
    /// <example>
    /// <code>
    /// [SetBusy]
    /// [Async] //prevents UI freezing, thus allowing busy state representation
    /// public void VeryLongAction() { ... }
    /// </code>
    /// </example>
    public class SetBusyAttribute : ExecutionWrapperBase {
        protected override void BeforeExecute(ActionExecutionContext context) {
            SetBusy(context.Target as ICanBeBusy, true);
        }

        protected override void AfterExecute(ActionExecutionContext context) {
            SetBusy(context.Target as ICanBeBusy, false);
        }

        protected override bool HandleException(ActionExecutionContext context, Exception ex) {
            SetBusy(context.Target as ICanBeBusy, false);
            return false;
        }

        void SetBusy(ICanBeBusy model, bool isBusy) {
            if(model != null)
                model.IsBusy = isBusy;
        }
    }
}