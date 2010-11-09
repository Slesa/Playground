namespace Caliburn.Micro.Recipes.Filters.Framework {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class FilterFramework {
        public static void Configure() {
            var oldPrepareContext = ActionMessage.PrepareContext;
            ActionMessage.PrepareContext = context => {
                oldPrepareContext(context);
                PrepareContext(context);
            };

            ActionMessage.InvokeAction = InvokeAction;
        }

        public static void PrepareContext(ActionExecutionContext context) {
            var contextAwareFilters = FilterManager.GetFiltersFor(context)
                .OfType<IContextAware>()
                .ToArray();

            contextAwareFilters.Apply(x => x.MakeAwareOf(context));
            context.Message.Detaching += (o, e) => { contextAwareFilters.Apply(x => x.Dispose()); };
        }

        public static void InvokeAction(ActionExecutionContext context) {
            var values = MessageBinder.DetermineParameters(context, context.Method.GetParameters());
            var result = Coroutine.CreateParentEnumerator(ExecuteActionWithParameters(values).GetEnumerator());

            var wrappers = FilterManager.GetFiltersFor(context).OfType<IExecutionWrapper>();
            var pipeline = result.WrapWith(wrappers);

            //if pipeline has error, action execution should throw! 
            pipeline.Completed += (o, e) => {
                Execute.OnUIThread(() => {
                    if(e.Error != null) {
                        throw new Exception(
                            string.Format("An error occurred while executing {0}.", context.Message),
                            e.Error
                            );
                    }
                });
            };

            pipeline.Execute(context);
        }

        static IEnumerable<IResult> ExecuteActionWithParameters(object[] values) {
            var actionExecution = new ExecuteActionResult(values);
            yield return actionExecution;

            var outcomeEnumerator = actionExecution.GetOutcomeEnumerator();
            if(outcomeEnumerator == null)
                yield break;

            try {
                while(outcomeEnumerator.MoveNext()) {
                    yield return outcomeEnumerator.Current;
                }
            }
            finally {
                outcomeEnumerator.Dispose();
            }
        }
    }
}