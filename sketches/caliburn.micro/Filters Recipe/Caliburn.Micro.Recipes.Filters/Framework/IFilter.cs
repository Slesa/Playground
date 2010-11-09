namespace Caliburn.Micro.Recipes.Filters.Framework {
    using System;

    public interface IFilter {
        int Priority { get; }
    }

    public interface IExecutionWrapper : IFilter {
        IResult Wrap(IResult inner);
    }

    public interface IContextAware : IFilter, IDisposable {
        void MakeAwareOf(ActionExecutionContext context);
    }
}