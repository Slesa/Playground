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
    [Export(typeof(MoviePersonHomeViewModel))]
    [Export(typeof(IChildScreen<MovieViewModel>))]
    public class MoviePersonHomeViewModel : Screen, IChildScreen<MovieViewModel>
    {
        #region Fields
        private readonly NetflixCatalog context;
        #endregion

        #region Constructor
        [ImportingConstructor]
        public MoviePersonHomeViewModel(NetflixCatalog context)
        {
            this.context = context;
            this.context.MergeOption = MergeOption.OverwriteChanges;
            DisplayName = AppStrings.MoviePersonHomeTitle;
            People = new BindableCollection<Person>();
            CurrentSearch = new Search
                                {
                                    StartPage = 1,
                                    StartIndex = 0,
                                    ItemsPerPage = Convert.ToInt32(NetflixDataAccess.DefaultPageSize)
                                };
        }

        #endregion

        #region Properties & Backingfields

        private BindableCollection<Person> people;
        public BindableCollection<Person> People
        {
            get { return people; }
            set
            {
                people = value;
                NotifyOfPropertyChange(() => People);
            }
        }

        private string searchPersonTerm;
        public string SearchPersonTerm
        {
            get { return searchPersonTerm; }
            set
            {
                searchPersonTerm = value;
                NotifyOfPropertyChange(() => SearchPersonTerm);
                NotifyOfPropertyChange(() => CanSearchPerson);
            }
        }

        public bool CanSearchPerson
        {
            get
            {
                if (string.IsNullOrEmpty(SearchPersonTerm))
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

        public bool CanNextPerson
        {
            get
            {
                return CurrentSearch != null
                    && CurrentSearch.TotalResults > CurrentSearch.StartIndex + CurrentSearch.ItemsPerPage;
            }
        }

        public bool CanPreviousPerson
        {
            get
            {
                return CurrentSearch != null
                    && CurrentSearch.StartPage > 1;
            }
        }

        #endregion

        #region Methods

        public IEnumerator<IResult> SearchPerson()
        {
            return Search();
        }

        public IEnumerator<IResult> SearchPersonShortCut()
        {
            return IsActive ? Search() : null;
        }

        private IEnumerator<IResult> Search(int page = 1)
        {
            yield return Show.Busy(Parent);

            CurrentSearch.SearchTerms = SearchPersonTerm;
            CurrentSearch.StartPage = page;
            CurrentSearch.StartIndex = page*CurrentSearch.ItemsPerPage - CurrentSearch.ItemsPerPage;
            var dataServiceCollection = new DataServiceCollection<Person>(context);
            var query = (from person in context.People.IncludeTotalCount()
                         where person.Name.Contains(SearchPersonTerm)
                         orderby person.Name
                         select person).Skip(page * CurrentSearch.ItemsPerPage - CurrentSearch.ItemsPerPage).Take(CurrentSearch.ItemsPerPage);
            var result = new LoadDataResult<Person>(dataServiceCollection, query, (sender, e) =>
                                                           {
                                                               CurrentSearch.TotalResults =
                                                                   e.QueryOperationResponse.TotalCount;
                                                           });
            yield return result;

            People.Clear();
            People.AddRange(dataServiceCollection);

            NotifyOfPropertyChange(() => CurrentSearch);
            NotifyOfPropertyChange(() => CanNextPerson);
            NotifyOfPropertyChange(() => CanPreviousPerson);
            NotifyOfPropertyChange(() => SearchResultText);

            yield return Show.NotBusy(Parent);
        }

        public IEnumerator<IResult> NextPerson()
        {
            return Search(CurrentSearch.StartPage + 1);
        }

        public IEnumerator<IResult> PreviousPerson()
        {
            return Search(CurrentSearch.StartPage - 1);
        }

        public IEnumerator<IResult> OpenPerson(object selectedItem)
        {
            var person = selectedItem as Person;
            if (person != null)
                yield return Show.Child<MoviePersonSingleViewModel>()
                    .In(Parent)
                    .Configured(a => a.WithPerson(person));
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