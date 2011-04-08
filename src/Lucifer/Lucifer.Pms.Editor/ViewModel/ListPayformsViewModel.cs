using System.Collections.ObjectModel;
using Caliburn.Micro;
using Lucifer.Pms.Editor.Resources;
using Lucifer.Pms.Model.Entities;

namespace Lucifer.Pms.Editor.ViewModel
{
    public class ListPayformsViewModel : Screen, IPmsModule
    {
        public ListPayformsViewModel()
        {
            DisplayName = Strings.PayformsModule;
            CreateAllPayforms();
        }

        public ObservableCollection<PayformRowViewModel> AllPayforms { get; private set; }

        void CreateAllPayforms()
        {
            AllPayforms = new ObservableCollection<PayformRowViewModel>
                {
                    new PayformRowViewModel(new Payform {Name = "Payform 1"}),
                };
        }

        public string ModuleName
        {
            get { return Strings.PayformsModule; }
        }

        public string IconFileName
        {
            get { return @"/Lucifer.Pms.Editor;component/Resources/Payform.png"; }
        }

        public string ToolTip
        {
            get { return Strings.PayformsTooltip; }
        }
       
    }
}