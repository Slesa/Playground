using System.ComponentModel.Composition;
using BugTracker.Model;
using BugTracker.Results;
using Caliburn.Micro;

namespace BugTracker.ViewModel
{
    [Export]
    public class ListViewModel : Screen
    {
        [Import]
        private IBugRepository _bugRepository;
        public IObservableCollection<Bug> Bugs { get; private set; }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            _bugRepository.BugSaved += (s, e) => { if (!Bugs.Contains(e.Bug)) Bugs.Add(e.Bug); };
            _bugRepository.BugDeleted += (s, e) => Bugs.Remove(e.Bug);

            Bugs = new BindableCollection<Bug>(_bugRepository);
        }

        public IResult Open(Bug bug)
        {
            return new OpenResult<BugViewModel>()
                .In<DetailViewModel>()
                .BeforeActivation(x => x.Bug = bug);
        }

        public IResult CreateNew()
        {
            return new OpenResult<BugViewModel>()
                .In<DetailViewModel>()
                .BeforeActivation(x => x.Bug = new Bug());
        }
    }
}