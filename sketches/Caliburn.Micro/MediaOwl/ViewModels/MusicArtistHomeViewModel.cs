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
    [Export(typeof(MusicArtistHomeViewModel))]
    [Export(typeof(IChildScreen<MusicViewModel>))]
    public class MusicArtistHomeViewModel : Screen, IChildScreen<MusicViewModel>
    {
        #region Fields

        private readonly ILastFmService service;
        private readonly LastFmRepository repository;

        #endregion

        #region Constructor
        [ImportingConstructor]
        public MusicArtistHomeViewModel(ILastFmService service, LastFmRepository repository)
        {
            DisplayName = AppStrings.MusicArtistHomeTitle;
            this.service = service;
            this.repository = repository;
        }

        #endregion

        #region Properties & Backingfields

        public BindableCollection<Artist> Artists
        {
            get { return repository.Artists; }
        }

        public Search CurrentSearch
        {
            get { return service.CurrentArtistSearch; }
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

        private string searchArtistTerm;
        public string SearchArtistTerm
        {
            get { return searchArtistTerm; }
            set
            {
                searchArtistTerm = value;
                NotifyOfPropertyChange(() => SearchArtistTerm);
                NotifyOfPropertyChange(() => CanSearchArtist);
            }
        }

        public bool CanSearchArtist
        {
            get
            {
                if (string.IsNullOrEmpty(SearchArtistTerm))
                {
                    return false;
                }
                return true;
            }
        }

        public bool CanNextArtist
        {
            get
            {
                return CurrentSearch != null 
                    && CurrentSearch.TotalResults > CurrentSearch.StartIndex + CurrentSearch.ItemsPerPage;
            }
        }

        public bool CanPreviousArtist
        {
            get
            {
                return CurrentSearch != null 
                    && CurrentSearch.StartPage != 1;
            }
        }

        #endregion

        #region Methods

        public IEnumerator<IResult> SearchArtist()
        {
            return Search();
        }

        public IEnumerator<IResult> SearchArtistShortCut()
        {
            return IsActive ? Search() : null;
        }

        public IEnumerator<IResult> NextArtist()
        {
            return Search(CurrentSearch.StartPage + 1);
        }

        public IEnumerator<IResult> PreviousArtist()
        {
            return Search(CurrentSearch.StartPage - 1);
        }

        private IEnumerator<IResult> Search(int page = 0)
        {
            yield return Show.Busy(Parent);
            yield return service.ArtistSearch(SearchArtistTerm, page);

            NotifyOfPropertyChange(() => CanNextArtist);
            NotifyOfPropertyChange(() => CanPreviousArtist);
            NotifyOfPropertyChange(() => SearchResultText);

            yield return Show.NotBusy(Parent);
        }

        public IEnumerator<IResult> OpenArtist(object selectedItem)
        {
            var artist = selectedItem as ArtistBase;
            if (artist != null)
                yield return Show.Child<MusicArtistSingleViewModel>()
                    .In(Parent)
                    .Configured(a => a.WithArtist(artist));
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