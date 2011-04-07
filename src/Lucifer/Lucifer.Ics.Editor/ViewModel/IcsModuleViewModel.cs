using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Castle.Windsor;
using Lucifer.Caliburn;
using Lucifer.Ics.Editor.Resources;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class IcsModuleViewModel : Screen, IModule
    {
        readonly IWindsorContainer _container;

        IEnumerable<IIcsModule> _icsModules;
        public IEnumerable<IIcsModule> Items { get { return _icsModules ?? (_icsModules = _container.ResolveAll<IIcsModule>()); } }
        
        public IcsModuleViewModel(IWindsorContainer container/*, IEventAggregator eventAggregator*/)
        {
            _container = container;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            DisplayName = Strings.IcsModuleTitle;

            //Items.AddRange(IcsModules);
            ////ActivateItem(Items.FirstOrDefault());
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