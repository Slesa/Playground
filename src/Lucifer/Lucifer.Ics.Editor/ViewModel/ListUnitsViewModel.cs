using Caliburn.Micro;
using Lucifer.Ics.Editor.Resources;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class ListUnitsViewModel: Screen, IIcsModule
    {
        public string ModuleName
        {
            get { return Strings.UnitsModule; }
        }

        public string IconFileName
        {
            get { return @"/Lucifer.Ics.Editor;component/Resources/Unit.png"; }
        }

        public string ToolTip
        {
            get { return Strings.UnitsTooltip; }
        }
    }
}