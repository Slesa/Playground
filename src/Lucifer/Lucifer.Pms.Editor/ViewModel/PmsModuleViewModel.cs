using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Castle.Windsor;
using Lucifer.Caliburn;
using Lucifer.Editor;
using Lucifer.Pms.Editor.Resources;

namespace Lucifer.Pms.Editor.ViewModel
{
    public class PmsModuleViewModel : Conductor<IScreen>.Collection.OneActive, IModule
    {
        readonly IWindsorContainer _container;

        IEnumerable<IPmsModule> _pmsModules;
        public IEnumerable<IPmsModule> PmsModules { get { return _pmsModules ?? (_pmsModules = _container.ResolveAll<IPmsModule>()); } }
        
        public PmsModuleViewModel(IWindsorContainer container/*, IEventAggregator eventAggregator*/)
        {
            _container = container;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            DisplayName = Strings.PmsModuleTitle;

            PmsModules.Each(x => x.ScreenManager = this);
            Items.AddRange(PmsModules);
            ActivateItem(Items.FirstOrDefault());
        }

        public string ModuleName
        {
            get { return Strings.PmsModuleTitle; }
        }

        public string IconFileName
        {
            get { return @"/Lucifer.Pms.Editor;component/Resources/Lucifer.Pms.png"; }
        }

        public string ToolTip
        {
            get { return Strings.PmsModuleTooltip; }
        }

    }
}
