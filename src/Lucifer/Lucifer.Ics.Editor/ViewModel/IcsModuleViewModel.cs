using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Lucifer.Caliburn;
using Lucifer.Ics.Editor.Resources;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class IcsModuleViewModel : Screen, IModule
    {
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

        public IEnumerable<IModule> SubModules
        {
            get { throw new NotImplementedException(); }
        }
    }
}