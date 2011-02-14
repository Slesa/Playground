using System.Collections.Generic;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using MediaOwl.Core;
using MediaOwl.Resources;

namespace MediaOwl.ViewModels
{
    [Export(typeof(TestMainTwoHomeViewModel))]
    [Export(typeof(IChildScreen<TestViewModel>))]
    public class TestMainTwoHomeViewModel : Screen, IChildScreen<TestViewModel>
    {
        #region Constructor

        public TestMainTwoHomeViewModel()
        {
            DisplayName = AppStrings.TestMainTwoHomeTitle;
        }

        #endregion

        #region Methods

        public IEnumerator<IResult> OpenFromTwo(object id)
        {
            if (id != null)
                yield return Show.Child<TestMainSingleViewModel>()
                    .In(Parent)
                    .Configured(a => a.With(id.ToString(), this));
        }

        #endregion

        #region Implementation of IChildScreen

        public string ScreenId
        {
            get { return GetType().Name; }
        }

        public int? Order { get { return 1; } }

        #endregion
    }
}