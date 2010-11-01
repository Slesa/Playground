namespace Caliburn.Micro.Recipes.Filters.Framework {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class FilterManager {
        public static Func<ActionExecutionContext, IEnumerable<IFilter>> GetFiltersFor = context => {
            //TODO: apply caching?
            return context.Target.GetType().GetAttributes<IFilter>(true)
                .Union(context.Method.GetAttributes<IFilter>(true))
                .OrderBy(x => x.Priority);
        };

        public static IResult WrapWith(this IResult inner, IEnumerable<IExecutionWrapper> wrappers) {
            return wrappers.Aggregate(inner, (current, wrapper) => wrapper.Wrap(current));
        }
    }
}