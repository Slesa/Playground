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
    [Export(typeof(MusicAlbumHomeViewModel))]
    [Export(typeof(IChildScreen<MusicViewModel>))]
    public class MusicAlbumHomeViewModel : Screen, IChildScreen<MusicViewModel>
    {
        #region Fields

        private readonly ILastFmService service;
        private readonly LastFmRepository repository;

        #endregion

        #region Constructor
        [ImportingConstructor]
        public MusicAlbumHomeViewModel(ILastFmService service, LastFmRepository repository)
        {
            DisplayName = AppStrings.MusicAlbumHomeTitle;
            this.service = service;
            this.repository = repository;
        }
        #endregion

        #region Properties & Backingfields

        public BindableCollection<Album> Albums
        {
            get { return repository.Albums; }
        }

        public Search CurrentSearch
        {
            get { return service.CurrentAlbumSearch; }
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

        private string searchAlbumTerm;
        public string SearchAlbumTerm
        {
            get { return searchAlbumTerm; }
            set
            {
                searchAlbumTerm = value;
                NotifyOfPropertyChange(() => SearchAlbumTerm);
                NotifyOfPropertyChange(() => CanSearchAlbum);
            }
        }

        public bool CanSearchAlbum
        {
            get
            {
                if (string.IsNullOrEmpty(SearchAlbumTerm))
                {
                    return false;
                }
                return true;
            }
        }

        public bool CanNextAlbum
        {
            get
            {
                return CurrentSearch != null
                    && CurrentSearch.TotalResults > CurrentSearch.StartIndex + CurrentSearch.ItemsPerPage;
            }
        }

        public bool CanPreviousAlbum
        {
            get
            {
                return CurrentSearch != null
                    && CurrentSearch.StartPage != 1;
            }
        }
        #endregion

        #region Methods

        public IEnumerator<IResult> SearchAlbum()
        {
            return Search();
        }

        public IEnumerator<IResult> SearchAlbumShortCut()
        {
            return IsActive ? Search() : null;
        }

        public IEnumerator<IResult> NextAlbum()
        {
            return Search(CurrentSearch.StartPage + 1);
        }

        public IEnumerator<IResult> PreviousAlbum()
        {
            return Search(CurrentSearch.StartPage - 1);
        }

        private IEnumerator<IResult> Search(int page = 0)
        {
            yield return Show.Busy(Parent);
            yield return service.AlbumSearch(SearchAlbumTerm, page);

            NotifyOfPropertyChange(() => CanNextAlbum);
            NotifyOfPropertyChange(() => CanPreviousAlbum);
            NotifyOfPropertyChange(() => SearchResultText);

            yield return Show.NotBusy(Parent);
        }

        public IEnumerator<IResult> OpenAlbum(object selectedItem)
        {
            var album = selectedItem as Album;
            if (album != null)
                yield return Show.Child<MusicAlbumSingleViewModel>()
                    .In(Parent)
                    .Configured(a => a.WithAlbum(album));
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