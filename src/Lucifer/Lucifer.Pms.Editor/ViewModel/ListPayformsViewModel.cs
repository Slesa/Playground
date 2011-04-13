using System.Collections.ObjectModel;
using System.Linq;
using Caliburn.Micro;
using Lucifer.DataAccess;
using Lucifer.Pms.Editor.Resources;
using Lucifer.Pms.Model.Queries;

namespace Lucifer.Pms.Editor.ViewModel
{
    public class ListPayformsViewModel : Screen, IPmsModule
    {
        readonly IDbConversation _dbConversation;

        public ListPayformsViewModel(IDbConversation dbConversation)
        {
            _dbConversation = dbConversation;
            DisplayName = Strings.PayformsModule;
            CreateAllPayforms();
        }

        public ObservableCollection<PayformRowViewModel> AllPayforms { get; private set; }

        void CreateAllPayforms()
        {
            AllPayforms = new ObservableCollection<PayformRowViewModel>(_dbConversation
                .Query(new AllPayformsQuery())
                .Select(x=>new PayformRowViewModel(x)));
            /*AllPayforms = new ObservableCollection<PayformRowViewModel>
                {
                    new PayformRowViewModel(new Payform {Name = "Payform 1"}),
                };*/
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