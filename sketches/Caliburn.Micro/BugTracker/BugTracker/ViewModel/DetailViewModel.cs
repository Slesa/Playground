using System.ComponentModel.Composition;
using System.Linq;
using Caliburn.Micro;

namespace BugTracker.ViewModel
{
    [Export]
    public class DetailViewModel : Conductor<BugViewModel>.Collection.OneActive
    {
        public bool CanSaveAll { get; private set; }

        public void SaveAll()
        {
            foreach (BugViewModel bugVm in Items)
            {
                if (bugVm.CanSave)
                    bugVm.Save();
            }
            CanSaveAll = false;
        }

        public override void ActivateItem(BugViewModel item)
        {
            BugViewModel vm = Items.SingleOrDefault(x => x.Bug == item.Bug);
            if (vm != null)
            {
                base.ActivateItem(vm);
            }
            else
            {
                item.Parent = this;
                item.PropertyChanged += (s, e) =>
                                            {
                                                if (e.PropertyName == "CanSave")
                                                    CanSaveAll = Items.Any(x => x.CanSave);
                                            };

                base.ActivateItem(item);
            }
        }
    }
}