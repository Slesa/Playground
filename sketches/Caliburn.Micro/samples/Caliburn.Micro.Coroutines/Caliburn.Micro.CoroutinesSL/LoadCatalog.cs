using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.ReflectionModel;
using System.Linq;

namespace Caliburn.Micro.CoroutinesSL
{
    public class LoadCatalog : IResult
    {
        readonly string _uri;
        readonly Dictionary<string, DeploymentCatalog> _catalogs = new Dictionary<string, DeploymentCatalog>();

        [Import]
        public AggregateCatalog Catalog { get; set; }

        public LoadCatalog(string relativeUri)
        {
            _uri = relativeUri;
        }

        public void Execute(ActionExecutionContext context)
        {
            DeploymentCatalog catalog;

            if( _catalogs.TryGetValue(_uri, out catalog))
                Completed(this, new ResultCompletionEventArgs());
            else
            {
                catalog = new DeploymentCatalog(new Uri("/ClientBin/"+_uri, UriKind.RelativeOrAbsolute));
                catalog.DownloadCompleted += (s, e) =>
                    {
                        if (e.Error == null)
                        {
                            _catalogs[_uri] = catalog;
                            Catalog.Catalogs.Add(catalog);
                            catalog.Parts
                                .Select(part => ReflectionModelServices.GetPartType(part).Value.Assembly)
                                .Where(assembly => !AssemblySource.Instance.Contains(assembly))
                                .Apply(x => AssemblySource.Instance.Add(x));
                        }
                        else
                        {
                            Loader.Hide().Execute(context);
                        }

                        Completed(this, new ResultCompletionEventArgs {Error = e.Error, WasCancelled = false});
                    };
                catalog.DownloadAsync();
            }
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }
}