using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using Caliburn.Micro;
using MediaOwl.Core;
using MediaOwl.Model;

namespace MediaOwl.Services.LastFmResults
{
    /// <summary>
    /// This <see cref="IResult"/> asyncronously loads XmlData. It has a built in <see cref="StopWatch"/>,
    /// which acts like a timeout-timer.
    /// </summary>
    public class LoadLastFmXmlDataResult : IResult
    {
        private readonly string searchString;
        private readonly LastFmRepository repository;
        private readonly LastFmRepository.RepositoryTypes addToRepository;

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="searchString">The searchstring built by the <see cref="ServiceHelper" />.</param>
        /// <param name="repository">The repository to pass the result to.</param>
        /// <param name="addToRepository">The <see cref="LastFmRepository.RepositoryTypes"/> type of result.</param>
        public LoadLastFmXmlDataResult(string searchString, LastFmRepository repository, LastFmRepository.RepositoryTypes addToRepository)
        {
            this.searchString = searchString;
            this.repository = repository;
            this.addToRepository = addToRepository;
        }

        [Import]
        public IEventAggregator EventAgg { get; set; }

        /// <summary>
        /// The root element of the returned <see cref="XDocument"/>
        /// </summary>
        public XElement XmlResult { get; set; }

        #region Implementation of IResult

        /// <summary>
        /// Executes the result using the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Execute(ActionExecutionContext context)
        {
            if (string.IsNullOrEmpty(LastFmDataAccess.ApiKey))
            {
                var args = new ResultCompletionEventArgs
                {
                    Error = new Exception("Can't execute query. No API-Key found."),
                    WasCancelled = true
                };
                Completed(this, args);
                EventAgg.Publish(new ErrorMessage(args));
            }

            var stopWatch = new StopWatch(10000);
            var webClient = new WebClient();

            webClient.OpenReadCompleted += (sender, e) =>
            {
                stopWatch.Stop();
                if (e.Error == null)
                {
                    XDocument doc = XDocument.Load(e.Result);
                    XElement ee = doc.Descendants("error").FirstOrDefault();

                    if (ee != null)
                    {
                        stopWatch.Stop();
                        Completed(this, new ResultCompletionEventArgs
                        {
                            Error = new Exception(ee.Value),
                            WasCancelled = false
                        });
                        return;
                    }
                    XmlResult = doc.Root;

                    if (repository != null && addToRepository != LastFmRepository.RepositoryTypes.None)
                        repository.AddData(XmlResult, addToRepository);


                    Completed(this, new ResultCompletionEventArgs());
                    return;
                }
                var args = new ResultCompletionEventArgs { Error = e.Error, WasCancelled = false };
                Completed(this, args);
                EventAgg.Publish(new ErrorMessage(args));
            };
            stopWatch.StopWatchEnded += (sender, e) =>
            {
                var args = new ResultCompletionEventArgs
                {
                    Error = new TimeoutException("Timeout of " + GetType().Name),
                    WasCancelled = false
                };
                EventAgg.Publish(new ErrorMessage(args));
                Completed(this, args);
            };
            stopWatch.Start();
            webClient.OpenReadAsync(new Uri(searchString));
        }

        public event EventHandler<ResultCompletionEventArgs> Completed;

        #endregion
    }
}