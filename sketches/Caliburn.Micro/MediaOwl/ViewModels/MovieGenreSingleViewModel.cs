using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Services.Client;
using System.Linq;
using Caliburn.Micro;
using MediaOwl.Core;
using MediaOwl.NetflixServiceReference;
using MediaOwl.Services.NetflixResults;

namespace MediaOwl.ViewModels
{
    [Export(typeof(MovieGenreSingleViewModel)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class MovieGenreSingleViewModel : Screen, IChildScreen<MovieViewModel>
    {
        #region Fields
        private readonly NetflixCatalog context;
        #endregion

        #region Constructor
        [ImportingConstructor]
        public MovieGenreSingleViewModel(NetflixCatalog context)
        {
            this.context = context;
        }

        #endregion

        #region Properties & Backingfields

        private Genre currentGenre;
        public Genre CurrentGenre
        {
            get { return currentGenre; }
            set
            {
                currentGenre = value;
                NotifyOfPropertyChange(() => CurrentGenre);
                NotifyOfPropertyChange(() => LatestTitles);
                NotifyOfPropertyChange(() => BestTitles);
            }
        }

        private BindableCollection<Title> latestTitles;
        public BindableCollection<Title> LatestTitles
        {
            get { return latestTitles; }
            set
            {
                latestTitles = value;
                NotifyOfPropertyChange(() => LatestTitles);
            }
        }

        private BindableCollection<Title> bestTitles;
        public BindableCollection<Title> BestTitles
        {
            get { return bestTitles; }
            set
            {
                bestTitles = value;
                NotifyOfPropertyChange(() => BestTitles);
            }
        }

        #endregion

        #region Methods

        public void WithGenre(Genre genre)
        {
            DisplayName = genre.Name;
            ScreenId = genre.Name;
            Run.Coroutine(FetchData(genre));
        }

        private IEnumerable<IResult> FetchData(Genre withgenre)
        {
            yield return Show.Busy(IoC.Get<MovieViewModel>());

            var query = from genre in context.Genres
                        //.Expand(g => g.Titles)
                         where genre.Name == withgenre.Name
                         select genre;

            var item = new DataServiceCollection<Genre>(context);
            yield return new LoadDataResult<Genre>(item, query);
            CurrentGenre = item.FirstOrDefault();

            if (CurrentGenre != null)
            {
                var queryBest = (from g in context.Genres
                          from title in g.Titles
                          where g.Name == CurrentGenre.Name
                          orderby title.AverageRating descending 
                          select title).Take(50);

                var collBest = new DataServiceCollection<Title>(context);
                yield return new LoadDataResult<Title>(collBest, queryBest);
                BestTitles = new BindableCollection<Title>(collBest);


                var queryLatest = (from g in context.Genres
                                   from title in g.Titles
                                 where g.Name == CurrentGenre.Name
                                 orderby title.ReleaseYear descending 
                                 select title).Take(50);

                var collLatest = new DataServiceCollection<Title>(context);
                yield return new LoadDataResult<Title>(collLatest, queryLatest);
                LatestTitles = new BindableCollection<Title>(collLatest);
            }

            yield return Show.NotBusy(IoC.Get<MovieViewModel>());
        }

        public IEnumerable<IResult> OpenTitle(object selectedItem)
        {
            if (selectedItem is Title)
            {
                yield return Show.Child<MovieTitleSingleViewModel>()
                    .In(Parent)
                    .Configured(x => x.WithTitle((Title) selectedItem));
            }
        }

        public IEnumerable<IResult> OpenPerson(object selectedItem)
        {
            if (selectedItem is Person)
            {
                yield return Show.Child<MoviePersonSingleViewModel>()
                    .In(Parent)
                    .Configured(x => x.WithPerson((Person)selectedItem));
            }
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

        public int? Order
        {
            get { return null; }
        }

        #endregion
    }
}