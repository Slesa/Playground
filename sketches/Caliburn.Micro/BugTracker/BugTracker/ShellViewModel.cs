using System.ComponentModel.Composition;
using BugTracker.ViewModel;
using Caliburn.Micro;

namespace BugTracker
{
    [Export(typeof(IShell))]
    public class ShellViewModel : Screen, IShell
    {
        [ImportingConstructor]
        public ShellViewModel(DetailViewModel bugDetails, ListViewModel bugList)
        {
            BugDetails = bugDetails;
            BugList = bugList;

            BugDetails.ActivateWith(this);
            BugList.ActivateWith(this);
        }

        public DetailViewModel BugDetails { get; set; }
        public ListViewModel BugList { get; set; }
    }
}