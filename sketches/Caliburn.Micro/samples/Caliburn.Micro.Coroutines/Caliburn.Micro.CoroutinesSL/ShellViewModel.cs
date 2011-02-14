using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Caliburn.Micro.CoroutinesSL
{
    [Export(typeof(IShell))]
    public class ShellViewModel : Conductor<object>, IShell
    {
        readonly ScreenOneViewModel _initialScreen;
        readonly Stack<object> _previous = new Stack<object>();
        bool _goingBack;

        [ImportingConstructor]
        public ShellViewModel(ScreenOneViewModel initialScreen)
        {
            _initialScreen = initialScreen;
        }

        public bool CanGoBack
        {
            get { return _previous.Count > 0; }
        }
        
        public void GoBack()
        {
            _goingBack = true;
            ActivateItem(_previous.Pop());
            _goingBack = false;
        }

        protected override void OnInitialize()
        {
            ActivateItem(_initialScreen);
            base.OnInitialize();
        }

        protected override void ChangeActiveItem(object newItem, bool closePrevious)
        {
            if( ActiveItem!=null && !_goingBack)
                _previous.Push(ActiveItem);

            NotifyOfPropertyChange(() => CanGoBack);

            base.ChangeActiveItem(newItem, closePrevious);
        }
    }
}