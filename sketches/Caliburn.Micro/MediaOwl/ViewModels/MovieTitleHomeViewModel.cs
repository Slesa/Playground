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
    public class MovieTitleHomeViewModel : Screen, IChildScreen<MovieViewModel>
    {
        #region Fields
        private readonly NetflixCatalog context;
        #endregion

        #region Constructor
        [ImportingConstructor]
        public MovieTitleHomeViewModel(NetflixCatalog context)
        {
            this.context = context;
            this.context.MergeOption = MergeOption.OverwriteChanges;
            DisplayName = AppStrings.MovieTitleHomeTitle;
            Titles = new BindableCollection<Title>();
            CurrentSearch = new Search
                                {
                                    StartPage = 1,
                                    StartIndex = 0,
                                    ItemsPerPage = Convert.ToInt32(NetflixDataAccess.DefaultPageSize)
                                };
        }

        #endregion

        #region Properties & Backingfields

        private BindableCollection<Title> titles;
        public BindableCollection<Title> Titles
        {
            get { return titles; }
            set
            {
                titles = value;
                NotifyOfPropertyChange(() => Titles);
            }
        }

        private string searchTitleTerm;
        public string SearchTitleTerm
        {
            get { return searchTitleTerm; }
            set
            {
                searchTitleTerm = value;
                NotifyOfPropertyChange(() => SearchTitleTerm);
                NotifyOfPropertyChange(() => CanSearchTitle);
            }
        }

        public bool CanSearchTitle
        {
            get
            {
                if (string.IsNullOrEmpty(SearchTitleTerm))
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

        public bool CanNextTitle
        {
            get
            {
                return CurrentSearch != null
                    && CurrentSearch.TotalResults > CurrentSearch.StartIndex + CurrentSearch.ItemsPerPage;
            }
        }

        public bool CanPreviousTitle
        {
            get
            {
                return CurrentSearch != null
                    && CurrentSearch.StartPage > 1;
            }
        }

        #endregion

        #region Methods

        public IEnumerator<IResult> SearchTitle()
        {
            return Search();
        }

        public IEnumerator<IResult> SearchTitleShortCut()
        {
            return IsActive ? Search() : null;
        }

        private IEnumerator<IResult> Search(int page = 1)
        {
            yield return Show.Busy(Parent);

            CurrentSearch.SearchTerms = SearchTitleTerm;
            CurrentSearch.StartPage = page;
            CurrentSearch.StartIndex = page*CurrentSearch.ItemsPerPage - CurrentSearch.ItemsPerPage;
            var t = new DataServiceCollection<Title>(context);
            var query = (from title in context.Titles.IncludeTotalCount()
                         where title.Name.Contains(SearchTitleTerm) ||
                                title.ShortName.Contains(SearchTitleTerm)
                         orderby title.ReleaseYear descending
                         select title).Skip(page * CurrentSearch.ItemsPerPage - CurrentSearch.ItemsPerPage).Take(CurrentSearch.ItemsPerPage);
            var result = new LoadDataResult<Title>(t, query, (sender, e) =>
                                                           {
                                                               CurrentSearch.TotalResults =
                                                                   e.QueryOperationResponse.TotalCount;
                                                           });
            yield return result;

            Titles.Clear();
            Titles.AddRange(t);

            NotifyOfPropertyChange(() => CurrentSearch);
            NotifyOfPropertyChange(() => CanNextTitle);
            NotifyOfPropertyChange(() => CanPreviousTitle);
            NotifyOfPropertyChange(() => SearchResultText);

            yield return Show.NotBusy(Parent);
        }

        public IEnumerator<IResult> NextTitle()
        {
            return Search(CurrentSearch.StartPage + 1);
        }

        public IEnumerator<IResult> PreviousTitle()
        {
            return Search(CurrentSearch.StartPage - 1);
        }

        public IEnumerator<IResult> OpenTitle(object selectedItem)
        {
            var title = selectedItem as Title;
            if (title != null)
                yield return Show.Child<MovieTitleSingleViewModel>()
                    .In(Parent)
                    .Configured(a => a.WithTitle(title));
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