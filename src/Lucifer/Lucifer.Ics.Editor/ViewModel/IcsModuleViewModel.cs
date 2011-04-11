using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Castle.Windsor;
using Lucifer.Caliburn;
using Lucifer.Editor;
using Lucifer.Ics.Editor.Resources;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class IcsModuleViewModel : Conductor<IScreen>.Collection.OneActive, IModule
    {
        readonly IWindsorContainer _container;

        IEnumerable<IIcsModule> _icsModules;
        public IEnumerable<IIcsModule> IcsModules { get { return _icsModules ?? (_icsModules = _container.ResolveAll<IIcsModule>()); } }
        
        public IcsModuleViewModel(IWindsorContainer container/*, IEventAggregator eventAggregator*/)
        {
            _container = container;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            DisplayName = Strings.IcsModuleTitle;

            IcsModules.Each(x => x.ScreenManager = this);
            Items.AddRange(IcsModules);
            ActivateItem(Items.FirstOrDefault());
        }

        public string ModuleName
        {
            get { return Strings.IcsModuleTitle; }
        }

        public string IconFileName
        {
            get { return @"/Lucifer.Ics.Editor;component/Resources/Lucifer.Ics.png"; }
        }

        public string ToolTip
        {
            get { return Strings.IcsModuleTooltip; }
        }

    }
}