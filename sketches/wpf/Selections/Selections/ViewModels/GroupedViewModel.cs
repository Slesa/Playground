namespace Selections.ViewModels
{
    public class GroupedViewModel : ItemViewModel
    {
        readonly int _id;

        public GroupedViewModel(int id, string name)
            :base(name)
        {
            _id = id;
        }

        public int Group { get { return _id/10; } }
    }
}