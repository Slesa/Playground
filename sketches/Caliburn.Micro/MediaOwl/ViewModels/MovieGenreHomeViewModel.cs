using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Services.Client;
using System.Linq;
using Caliburn.Micro;
using MediaOwl.Core;
using MediaOwl.Model;
using MediaOwl.NetflixServiceReference;
using MediaOwl.Resources;
using MediaOwl.Services;
using MediaOwl.Services.NetflixResults;

namespace MediaOwl.ViewModels
{
    [Export(typeof(MovieTitleHomeViewModel))]
    [Export(typeof(IChildScreen<MovieViewModel>))]
    public class MovieGenreHomeViewModel : Screen, IChildScreen<MovieViewModel>
    {
        #region Fields
        private readonly NetflixCatalog context;
        #endregion

        #region Constructor
        [ImportingConstructor]
        public MovieGenreHomeViewModel(NetflixCatalog context)
        {
            this.context = context;
            this.context.MergeOption = MergeOption.OverwriteChanges;
            DisplayName = AppStrings.MovieGenreHomeTitle;
            Genres = new BindableCollection<Genre>();
            CurrentSearch = new Search
                                {
                                    StartPage = 1,
                                    StartIndex = 0,
                                    ItemsPerPage = Convert.ToInt32(NetflixDataAccess.DefaultPageSize)
                                };
        }

        #endregion

        #region Properties & Backingfields

        private BindableCollection<Genre> genres;
        public BindableCollection<Genre> Genres
        {
            get { return genres; }
            set
            {
                genres = value;
                NotifyOfPropertyChange(() => Genres);
            }
        }

        private string searchGenreTerm;
        public string SearchGenreTerm
        {
            get { return searchGenreTerm; }
            set
            {
                searchGenreTerm = value;
                NotifyOfPropertyChange(() => SearchGenreTerm);
                NotifyOfPropertyChange(() => CanSearchGenre);
            }
        }

        public bool CanSearchGenre
        {
            get
            {
                if (string.IsNullOrEmpty(SearchGenreTerm))
                {
                    return false;
                }
                return true;
            }
        }

        private Search currentSearch;
        public Search CurrentSearch
        {
            get { return currentSearch; }
            set
            {
                
                currentSearch = value;
                NotifyOfPropertyChange(() => CurrentSearch);
            }
        }

        public string SearchResultText
        {
            get { return CurrentSearch.SearchResultText; }
        }

        public bool CanNextGenre
        {
            get
            {
                return CurrentSearch != null
                    && CurrentSearch.TotalResults > CurrentSearch.StartIndex + CurrentSearch.ItemsPerPage;
            }
        }

        public bool CanPreviousGenre
        {
            get
            {
                return CurrentSearch != null
                    && CurrentSearch.StartPage > 1;
            }
        }

        #endregion

        #region Methods

        public IEnumerator<IResult> SearchGenre()
        {
            return Search();
        }

        public IEnumerator<IResult> SearchGenreShortCut()
        {
            return IsActive ? Search() : null;
        }

        private IEnumerator<IResult> Search(int page = 1)
        {
            yield return Show.Busy(Parent);

            CurrentSearch.SearchTerms = SearchGenreTerm;
            CurrentSearch.StartPage = page;
            CurrentSearch.StartIndex = page*CurrentSearch.ItemsPerPage - CurrentSearch.ItemsPerPage;
            var collection = new DataServiceCollection<Genre>(context);
            var query = (from genre in context.Genres.IncludeTotalCount()
                         where genre.Name.Contains(SearchGenreTerm)
                         orderby genre.Name
                         select genre).Skip(page * CurrentSearch.ItemsPerPage - CurrentSearch.ItemsPerPage).Take(CurrentSearch.ItemsPerPage);
            var result = new LoadDataResult<Genre>(collection, query, (sender, e) =>
                                                           {
                                                               CurrentSearch.TotalResults =
                                                                   e.QueryOperationResponse.TotalCount;
                                                           });
            yield return result;

            Genres.Clear();
            Genres.AddRange(collection);

            NotifyOfPropertyChange(() => CurrentSearch);
            NotifyOfPropertyChange(() => CanNextGenre);
            NotifyOfPropertyChange(() => CanPreviousGenre);
            NotifyOfPropertyChange(() => SearchResultText);

            yield return Show.NotBusy(Parent);
        }

        public IEnumerator<IResult> NextGenre()
        {
            return Search(CurrentSearch.StartPage + 1);
        }

        public IEnumerator<IResult> PreviousGenre()
        {
            return Search(CurrentSearch.StartPage - 1);
        }

        public IEnumerator<IResult> OpenGenre(object selectedItem)
        {
            var genre = selectedItem as Genre;
            if (genre != null)
                yield return Show.Child<MovieGenreSingleViewModel>()
                    .In(Parent)
                    .Configured(a => a.WithGenre(genre));
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