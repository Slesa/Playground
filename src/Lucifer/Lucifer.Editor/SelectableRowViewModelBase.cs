using Caliburn.Micro;

namespace Lucifer.Editor
{
    public class SelectableRowViewModelBase : PropertyChangedBase
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
                NotifyOfPropertyChange(()=>IsSelected);
            }
        }
    }
}