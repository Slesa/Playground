using System.Collections.Generic;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using MediaOwl.Core;
using MediaOwl.Model;
using MediaOwl.Model.LastFm;
using MediaOwl.Resources;
using MediaOwl.Services;

namespace MediaOwl.ViewModels
{
    [Export(typeof(MusicTrackHomeViewModel))]
    [Export(typeof(IChildScreen<MusicViewModel>))]
    public class MusicTrackHomeViewModel : Screen, IChildScreen<MusicViewModel>
    {
        #region Fields

        private readonly ILastFmService service;
        private readonly LastFmRepository repository;

        #endregion

        #region Constructor

        [ImportingConstructor]
        public MusicTrackHomeViewModel(ILastFmService service, LastFmRepository repository)
        {
            DisplayName = AppStrings.MusicTrackHomeTitle;
            this.service = service;
            this.repository = repository;
        }

        #endregion

        #region Properties & Backingfields

        public BindableCollection<Track> Tracks
        {
            get { return repository.Tracks; }
        }

        public Search CurrentSearch
        {
            get { return repository.CurrentTrackSearch; }
        }

        public string SearchResultText
        {
            get
            {
                return CurrentSearch == null
                    ? string.Empty
                    : CurrentSearch.SearchResultText;
            }
        }

        private string searchTrackTerm;
        public string SearchTrackTerm
        {
            get { return searchTrackTerm; }
            set
            {
                searchTrackTerm = value;
                NotifyOfPropertyChange(() => SearchTrackTerm);
                NotifyOfPropertyChange(() => CanSearchTrack);
            }
        }

        private string searchTrackTerm2;
        public string SearchTrackTerm2
        {
            get { return searchTrackTerm2; }
            set
            {
                searchTrackTerm2 = value;
                NotifyOfPropertyChange(() => SearchTrackTerm2);
            }
        }

        public bool CanSearchTrack
        {
            get
            {
                if (string.IsNullOrEmpty(searchTrackTerm))
                {
                    return false;
                }
                return true;
            }
        }

        public bool CanNextTrack
        {
            get
            {
                return CurrentSearch != null
                    && CurrentSearch.TotalResults > CurrentSearch.StartIndex + CurrentSearch.ItemsPerPage;
            }
        }

        public bool CanPreviousTrack
        {
            get
            {
                return CurrentSearch != null
                    && CurrentSearch.StartPage != 1;
            }
        }

        #endregion

        #region Methods

        public IEnumerator<IResult> SearchTrack()
        {
            return Search();
        }

        public IEnumerator<IResult> SearchTrackShortCut()
        {
            return IsActive ? Search() : null;
        }

        public IEnumerator<IResult> NextTrack()
        {
            return Search(CurrentSearch.StartPage + 1);
        }

        public IEnumerator<IResult> PreviousTrack()
        {
            return Search(CurrentSearch.StartPage - 1);
        }

        private IEnumerator<IResult> Search(int page = 0)
        {
            yield return Show.Busy(Parent);
            yield return service.TrackSearch(SearchTrackTerm, SearchTrackTerm2, page);

            NotifyOfPropertyChange(() => CanNextTrack);
            NotifyOfPropertyChange(() => CanPreviousTrack);
            NotifyOfPropertyChange(() => SearchResultText);

            yield return Show.NotBusy(Parent);
        }

        public IEnumerator<IResult> OpenTrack(object selectedItem)
        {
            var track = selectedItem as Track;
            if (track != null)
                yield return Show.Child<MusicTrackSingleViewModel>()
                    .In(Parent)
                    .Configured(a => a.WithTrack(track));
        }

        #endregion

        #region Implementation of IChildScreen

        public string ScreenId
        {
            get { return GetType().Name; }
        }

        public int? Order { get { return 2; } }

        #endregion
    }
}