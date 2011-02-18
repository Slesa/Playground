using System;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using NightHawkSL.Ui.Core;

namespace NightHawkSL.Module.Test.ViewModels
{
    [Export(typeof(TestMainSingleViewModel)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class TestMainSingleViewModel : Screen, IChildScreen<TestViewModel>
    {
        public TestMainSingleViewModel()
        {
            CanTryClose = true;
        }

        private string _parentName;
        public string ParentName
        {
            get { return _parentName; }
            set
            {
                _parentName = value;
                NotifyOfPropertyChange(() => ParentName);
            }
        }

        private string _openedFromName;
        public string OpenedFromName
        {
            get { return _openedFromName; }
            set
            {
                _openedFromName = value;
                NotifyOfPropertyChange(() => OpenedFromName);
            }
        }

        private DateTime? _activatedTime;
        public DateTime? ActivatedTime
        {
            get { return _activatedTime; }
            set
            {
                _activatedTime = value;
                NotifyOfPropertyChange(() => ActivatedTime);
            }
        }

        private DateTime? _deactivatedTime;
        public DateTime? DeactivatedTime
        {
            get { return _deactivatedTime; }
            set
            {
                _deactivatedTime = value;
                NotifyOfPropertyChange(() => DeactivatedTime);
            }
        }

        private bool _canTryClose;
        public bool CanTryClose
        {
            get { return _canTryClose; }
            set
            {
                _canTryClose = value;
                NotifyOfPropertyChange(() => CanTryClose);
            }
        }

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

        private string _screenId;
        public string ScreenId
        {
            get { return _screenId; }
            set
            {
                _screenId = value;
                NotifyOfPropertyChange(() => ScreenId);
            }
        }

        public int? Order { get { return null; } }
    }
}