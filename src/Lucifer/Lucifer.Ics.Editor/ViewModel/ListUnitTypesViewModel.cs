using Caliburn.Micro;
using Lucifer.Ics.Editor.Resources;

namespace Lucifer.Ics.Editor.ViewModel
{
    public class ListUnitTypesViewModel: Screen, IIcsModule
    {
        public string ModuleName
        {
            get { return Strings.UnitTypesModule; }
        }

        public string IconFileName
        {
            get { return @"/Lucifer.Ics.Editor;component/Resources/UnitType.png"; }
            }

        public string ToolTip
        {
            get { return Strings.UnitTypesTooltip; }
        }
    }
}
