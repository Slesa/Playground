using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Caliburn.Micro;
using MediaOwl.Model;
using MediaOwl.Model.LastFm;

namespace MediaOwl.Services.LastFmResults
{
    /// <summary>
    /// This <see cref="IResult"/> loads an entity of the given type. It uses <see cref="LoadLastFmXmlDataResult"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity, must inherit from <see cref="EntityBase"/></typeparam>
    public class LoadLastFmEntityDataResult<TEntity> : IResult
        where TEntity : EntityBase
    {
        private readonly string searchString;
        private readonly string xmlElementName;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="searchString">The searchstring built by the <see cref="ServiceHelper" />.</param>
        /// <param name="xmlElementName">The element-name of an entity in the xml-file</param>
        public LoadLastFmEntityDataResult(string searchString, string xmlElementName)
        {
            this.searchString = searchString;
            this.xmlElementName = xmlElementName;
        }

        /// <summary>
        /// The Result is stored here
        /// </summary>
        public TEntity Entity { get; private set; }

        #region Implementation of IResult

        public void Execute(ActionExecutionContext context)
        {
            Coroutine.Execute(EntityResult());
        }

        private IEnumerator<IResult> EntityResult()
        {
            var result = new LoadLastFmXmlDataResult(searchString, null, LastFmRepository.RepositoryTypes.None);
            yield return result;

            var entityXml = result.XmlResult.Descendants(xmlElementName).FirstOrDefault();
            var entity = Activator.CreateInstance(typeof (TEntity)) as EntityBase;
            if (entity != null) entity.FromXml(entityXml);
            Entity = entity as TEntity;

            Completed(this, new ResultCompletionEventArgs());
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate {};

        #endregion
    }
}