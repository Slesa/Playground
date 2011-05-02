using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using BugTracker.Model;
using BugTracker.Results;
using Caliburn.Micro;

namespace BugTracker.ViewModel
{
    [Export, PartCreationPolicy(CreationPolicy.NonShared)]
    public class BugViewModel : Screen, IChild<IConductor>
    {
        private Bug _bug;
        [Import]
        private IBugRepository _bugs;

        public Bug Bug
        {
            get { return _bug; }
            set
            {
                _bug = value;
                DisplayName = string.Format("Bug {0}", Bug.Id);
            }
        }

        public bool CanSave { get; private set; }

        #region IChild<IConductor> Members

        public IConductor Parent { get; set; }

        #endregion

        protected override void OnInitialize()
        {
            base.OnInitialize();
            Bug.PropertyChanged += BugOnPropertyChanged;
        }

        private void BugOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanSave = Bug["Description"] == null;
            if (e.PropertyName == "Id")
                DisplayName = string.Format("Bug {0}", Bug.Id);
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            if (close)
            {
                Bug.PropertyChanged -= BugOnPropertyChanged;
            }
        }

        public IEnumerable<IResult> Delete()
        {
            var question = new QuestionViewModel("Really?", "Do you really want to delete this bug?", Answer.Yes,
                                                 Answer.No);

            yield return new QuestionResult(question)
                .CancelOn(Answer.No);

            _bugs.Delete(Bug);
            TryClose();
        }

        public IEnumerable<IResult> Save()
        {
            yield return new BusyResult()
                .Show()
                .WithMessage("Saving bug...");

            _bugs.BugSaved += BugsOnBugSaved;

            _bugs.Save(Bug);
        }

        private void BugsOnBugSaved(object sender, BugEvent bugEvent)
        {
            _bugs.BugSaved -= BugsOnBugSaved;
            CanSave = false;
            Coroutine.BeginExecute(new List<IResult> { new BusyResult().Hide() }.GetEnumerator());
        }
    }
}