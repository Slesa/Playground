using System.Collections.Generic;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using NightHawkSL.Core;
using NightHawkSL.Resources;

namespace NightHawkSL.ViewModels
{
    [Export(typeof(TestMainOneHomeViewModel))]
    [Export(typeof(IChildScreen<TestViewModel>))]
    public class TestMainOneHomeViewModel : Screen, IChildScreen<TestViewModel>
    {
        public TestMainOneHomeViewModel()
        {
            DisplayName = AppStrings.TestMainOneHomeTitle;
        }

        private string _childScreenId;
        public string ChildScreenId
        {
            get { return _childScreenId; }
            set
            {
                _childScreenId = value;
                NotifyOfPropertyChange(() => ChildScreenId);
                NotifyOfPropertyChange(() => CanOpenFromOne);
            }
        }

        public bool CanOpenFromOne
        {
            get{return !string.IsNullOrEmpty(ChildScreenId);}
        }

        public IEnumerator<IResult> OpenFromOne()
        {
            yield return Show.Child<TestMainSingleViewModel>()
                .In(Parent)
                .Configured(a => a.With(ChildScreenId, this));
        }

        public string ScreenId
        {
            get { return GetType().Name; }
        }

        public int? Order { get { return 0; } }
    }
}