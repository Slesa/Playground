using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Lucifer.Caliburn;
using Lucifer.Pms.Editor.Resources;

namespace Lucifer.Pms.Editor.ViewModel
{
    public class PmsModuleViewModel : Screen, IModule
    {
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
