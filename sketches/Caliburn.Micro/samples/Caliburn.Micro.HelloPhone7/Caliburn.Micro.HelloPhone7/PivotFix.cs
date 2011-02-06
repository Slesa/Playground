using System;

namespace Caliburn.Micro.HelloPhone7
{
    public class PivotFix<T>
    {
        Conductor<T>.Collection.OneActive _conductor;
        bool _readyToActivate;
        T _toActivate;
        bool _doneReactivating;

        public PivotFix(Conductor<T>.Collection.OneActive conductor)
        {
            _conductor = conductor;
            conductor.CloseStrategy = new DefaultCloseStrategy<T>(false);
        }

        public void OnViewLoaded(object view, Action<object> onViewLoaded)
        {
            onViewLoaded(view);

            _readyToActivate = true;
            if (_toActivate != null && !_doneReactivating)
            {
                _conductor.ActivateItem(_toActivate);
                _doneReactivating = true;
            }
        }

        public void ChangeActiveItem(T newItem, bool closePrevious, Action<T, bool> changeActiveItemBase)
        {
            if (newItem == null)
                return;

            if (!_readyToActivate && !_doneReactivating)
            {
                if (_conductor.Items.IndexOf(newItem) > 0)
                {
                    _toActivate = newItem;
                    return;
                }
            }

            changeActiveItemBase(newItem, closePrevious);
        }
    }
}