using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Castle.Windsor;
using Lucifer.Caliburn;
using Lucifer.Editor;
using Lucifer.Ums.Editor.Resources;

namespace Lucifer.Ums.Editor.ViewModel
{
    public class UmsModuleViewModel : Conductor<IScreen>.Collection.OneActive, IModule
    {
        readonly IWindsorContainer _container;

        IEnumerable<IUmsModule> _umsModules;
        public IEnumerable<IUmsModule> UmsModules { get { return _umsModules ?? (_umsModules = _container.ResolveAll<IUmsModule>().OrderBy(x=>x.ModuleName)); } }
        
        public UmsModuleViewModel(IWindsorContainer container)
        {
            _container = container;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            DisplayName = Strings.UmsModuleTitle;

            UmsModules.Each(x => x.ScreenManager = this);
            Items.AddRange(UmsModules);
            ActivateItem(Items.FirstOrDefault());
        }

        public string ModuleName
        {
            get { return Strings.UmsModuleTitle; }
        }

        public string IconFileName
        {
            get { return @"/Lucifer.Ums.Editor;component/Resources/Lucifer.Ums.png"; }
        }

        public string ToolTip
        {
            get { return Strings.UmsModuleTooltip; }
        }

    }
}
