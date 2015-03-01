using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Input;

namespace Database
{
    public class BadViewModel
    {
        public BadViewModel()
        {
            CreateDatasets();
        }

        public ObservableCollection<UserViewModel> Users { get; private set; }

        void CreateDatasets()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            using (var set = new DataSet("users"))
            {
                // Put code that adds stuff to DataSet here.
                // ... The DataSet will be cleaned up outside the block.
            }
            Mouse.OverrideCursor = null;
        }
    }

}
