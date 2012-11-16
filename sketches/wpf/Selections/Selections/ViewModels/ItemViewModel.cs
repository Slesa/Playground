namespace Selections.ViewModels
{
    public class ItemViewModel
    {
        public ItemViewModel(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}