using Caliburn.Micro;

namespace Lucifer.Editor
{
    public interface ISelectableRowViewModelBase
    {
        bool IsSelected { get; set; }
    }

    public class SelectableRowViewModelBase : PropertyChangedBase, ISelectableRowViewModelBase
    {
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