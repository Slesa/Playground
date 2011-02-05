using System;
using System.ComponentModel.Composition;
using System.Data.Services.Client;
using System.Linq;
using Caliburn.Micro;
using MediaOwl.Core;

namespace MediaOwl.Services.NetflixResults
{
    /// <summary>
    /// This <see cref="IResult"/> loads data defined in an <see cref="IQueryable"/> into the <see cref="collection"/>
    /// </summary>
    public class LoadDataResult<T> : IResult
    {
        private readonly DataServiceCollection<T> collection;
        private readonly IQueryable<T> qry;

        private readonly Action<object, LoadCompletedEventArgs> completedAction;

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="collection">The collection that loads the data</param>
        /// <param name="qry">The query</param>
        /// <param name="completedAction">An action, that is executed on successful completion</param>
        public LoadDataResult(DataServiceCollection<T> collection, IQueryable<T> qry, Action<object, LoadCompletedEventArgs> completedAction = null)
        {
            this.collection = collection;
            this.qry = qry;
            this.completedAction = completedAction;
        }

        [Import]
        public IEventAggregator EventAgg { get; set; }

        public void Execute(ActionExecutionContext context = null)
        {
            var stopWatch = new StopWatch(10000);

            collection.LoadCompleted += (sender, args) =>
                                            {
                                                if (args.Error != null)
                                                {
                                                    stopWatch.Stop();
                                                    var e = new ResultCompletionEventArgs { Error = args.Error, WasCancelled = true };
                                                    EventAgg.Publish(new ErrorMessage(e));
                                                    Completed(this, e);
                                                }

                                                if (args.Cancelled)
                                                {
                                                    stopWatch.Stop();
                                                    Completed(this,
                                                              new ResultCompletionEventArgs { Error = new Exception("LoadData was cancelled;"), WasCancelled = false });
                                                    return;
                                                }

                                                if (completedAction != null)
                                                    Caliburn.Micro.Execute.OnUIThread(() => completedAction(sender, args));

                                                stopWatch.Stop();
                                                Completed(this, new ResultCompletionEventArgs());
                                            };
            stopWatch.StopWatchEnded += (sender, e) =>
            {
                var args = new ResultCompletionEventArgs
                {
                    Error = new TimeoutException("Timeout of " + GetType().Name),
                    WasCancelled = true
                };
                EventAgg.Publish(new ErrorMessage(args));
                stopWatch.Stop();
                Completed(this, args);
            };

            stopWatch.Start();
            collection.LoadAsync(qry);
        }

        public event EventHandler<ResultCompletionEventArgs> Completed;
    }
}