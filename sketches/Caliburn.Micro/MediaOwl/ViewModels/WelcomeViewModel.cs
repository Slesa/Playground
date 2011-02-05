using System;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using MediaOwl.Core;
using MediaOwl.Resources;

namespace MediaOwl.ViewModels
{
    [Export(typeof(WelcomeViewModel))]
    public class WelcomeViewModel : Screen
    {
        #region Fields

        #endregion

        #region Constructor
        public WelcomeViewModel()
        {
            DisplayName = AppStrings.WelcomeTitle;
        }
        #endregion

        #region Properties & Backingfields

        #endregion

        #region Methods

        #endregion

        #region Events

        #endregion
      
      
    }
}