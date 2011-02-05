using System;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using MediaOwl.Core;

namespace MediaOwl.ViewModels
{
    [Export(typeof(TestMainSingleViewModel)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class TestMainSingleViewModel : Screen, IChildScreen<TestViewModel>
    {

        #region Constructor
        
        public TestMainSingleViewModel()
        {
            CanTryClose = true;
        }

        #endregion

        #region Properties & Backingfields

        private string parentName;
        public string ParentName
        {
            get { return parentName; }
            set
            {
                parentName = value;
                NotifyOfPropertyChange(() => ParentName);
            }
        }

        private string openedFromName;
        public string OpenedFromName
        {
            get { return openedFromName; }
            set
            {
                openedFromName = value;
                NotifyOfPropertyChange(() => OpenedFromName);
            }
        }

        private DateTime? activatedTime;
        public DateTime? ActivatedTime
        {
            get { return activatedTime; }
            set
            {
                activatedTime = value;
                NotifyOfPropertyChange(() => ActivatedTime);
            }
        }

        private DateTime? deactivatedTime;
        public DateTime? DeactivatedTime
        {
            get { return deactivatedTime; }
            set
            {
                deactivatedTime = value;
                NotifyOfPropertyChange(() => DeactivatedTime);
            }
        }

        private bool canTryClose;
        public bool CanTryClose
        {
            get { return canTryClose; }
            set
            {
                canTryClose = value;
                NotifyOfPropertyChange(() => CanTryClose);
            }
        }

        #endregion

        #region Methods

        public void With(string id, IScreen openedFrom)
        {
            ScreenId = id;
            DisplayName = "Child: \'" + ScreenId + "\'";
            OpenedFromName = string.Format("{0}, {1}", openedFrom.GetType().Name, openedFrom.DisplayName);
            
            PropertyChanged += (s, e) =>
                                        {
                                            if (e.PropertyName == "Parent")
                                            {
                                                ParentName = string.Format("{0}, {1}", Parent.GetType().Name,
                                                                           ((IScreen)Parent).DisplayName);
                                            }
                                        };

            Activated += (s, e) =>
                                  {
                                      ActivatedTime = DateTime.Now;
                                  };

            Deactivated += (s, e) =>
                               {
                                   DeactivatedTime = DateTime.Now;
                               };
        }

        

        #endregion

        #region Implementation of IChildScreen

        private string screenId;
        public string ScreenId
        {
            get { return screenId; }
            set
            {
                screenId = value;
                NotifyOfPropertyChange(() => ScreenId);
            }
        }

        public int? Order { get { return null; } }

        #endregion
    }
}