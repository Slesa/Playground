using Caliburn.Micro;

namespace Lucifer.Editor
{
    public interface ISelectableRowViewModelBase
    {
        bool IsSelected { get; set; }
    }

    public class SelectableRowViewModelBase<T> : PropertyChangedBase, ISelectableRowViewModelBase
    {
        public T ElementData { get; set; }

        bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value == _isSelected)
                    return;
                _isSelected = value;
                NotifyOfPropertyChange(() => IsSelected);
            }
        }
    }
}