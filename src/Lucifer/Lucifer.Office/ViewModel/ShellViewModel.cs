using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Castle.Windsor;
using Lucifer.Caliburn;
using Lucifer.Office.Resources;

namespace Lucifer.Office.ViewModel
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IShell
    {
        readonly IWindsorContainer _container;

        IEnumerable<IModule> _modules;
        public IEnumerable<IModule> Modules { get { return _modules ?? (_modules = _container.ResolveAll<IModule>()); } }

        public ShellViewModel(IWindsorContainer container/*, IEventAggregator eventAggregator*/)
        {
            _container = container;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            DisplayName = Strings.AppTitle;

            Modules.ToArray().Apply(x => Items.Add(x));
            ActivateItem(Items.FirstOrDefault());
        }
    }
}