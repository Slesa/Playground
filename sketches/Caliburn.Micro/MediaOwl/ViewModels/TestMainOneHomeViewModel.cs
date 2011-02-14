using System.Collections.Generic;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using MediaOwl.Core;
using MediaOwl.Resources;

namespace MediaOwl.ViewModels
{
    [Export(typeof(TestMainOneHomeViewModel))]
    [Export(typeof(IChildScreen<TestViewModel>))]
    public class TestMainOneHomeViewModel : Screen, IChildScreen<TestViewModel>
    {
        #region Constructor

        public TestMainOneHomeViewModel()
        {
            DisplayName = AppStrings.TestMainOneHomeTitle;
        }

        #endregion

        #region Properties & Backingfields

        private string childScreenId;
        public string ChildScreenId
        {
            get { return childScreenId; }
            set
            {
                childScreenId = value;
                NotifyOfPropertyChange(() => ChildScreenId);
                NotifyOfPropertyChange(() => CanOpen);
            }
        }

        public bool CanOpen
        {
            get{return !string.IsNullOrEmpty(ChildScreenId);}
        }

        #endregion

        #region Methods

        public IEnumerator<IResult> OpenFromOne()
        {
            yield return Show.Child<TestMainSingleViewModel>()
                .In(Parent)
                .Configured(a => a.With(ChildScreenId, this));
        }

        #endregion

        #region Implementation of IChildScreen

        public string ScreenId
        {
            get { return GetType().Name; }
        }

        public int? Order { get { return 0; } }

        #endregion
    }
}