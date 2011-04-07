using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Lucifer.Caliburn;
using Lucifer.Ums.Editor.Resources;

namespace Lucifer.Ums.Editor.ViewModel
{
    public class UmsModuleViewModel : Screen, IModule
    {
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
