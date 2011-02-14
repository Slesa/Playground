using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Services.Client;
using System.Linq;
using Caliburn.Micro;
using MediaOwl.Core;
using MediaOwl.Model.Netflix;
using MediaOwl.NetflixServiceReference;
using MediaOwl.Services.NetflixResults;
using MediaOwl.Services;

namespace MediaOwl.ViewModels
{
    [Export(typeof(MoviePersonSingleViewModel)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class MoviePersonSingleViewModel : Screen, IChildScreen<MovieViewModel>
    {
        #region Fields
        private readonly NetflixCatalog context;
        #endregion

        #region Constructor
        [ImportingConstructor]
        public MoviePersonSingleViewModel(NetflixCatalog context)
        {
            this.context = context;
            directorGenre = new BindableCollection<GenreExtended>();
            actorGenre = new BindableCollection<GenreExtended>();
        }

        #endregion

        #region Properties & Backingfields

        private Person currentPerson;
        public Person CurrentPerson
        {
            get { return currentPerson; }
            set
            {
                currentPerson = value;
                NotifyOfPropertyChange(() => CurrentPerson);
                NotifyOfPropertyChange(() => Awards);
                NotifyOfPropertyChange(() => MoviesDirected);
                NotifyOfPropertyChange(() => MoviesActed);
            }
        }

        private string personType;
        public string PersonType
        {
            get
            {
                return personType;
            }
            set
            {
                personType = value;
                NotifyOfPropertyChange(() => PersonType);
            }
        }

        private bool isActor;
        public bool IsActor
        {
            get { return isActor; }
            set
            {
                isActor = value;
                NotifyOfPropertyChange(() => IsActor);
            }
        }

        private bool isDirector;
        public bool IsDirector
        {
            get { return isDirector; }
            set
            {
                isDirector = value;
                NotifyOfPropertyChange(() => IsDirector);
            }
        }

        private double directorRating;
        public double DirectorRating
        {
            get
            {
                return directorRating;
            }
            set
            {
                directorRating = value;
                NotifyOfPropertyChange(() => DirectorRating);
            }
        }

        private double actorRating;
        public double ActorRating
        {
            get
            {
                return actorRating;
            }
            set
            {
                actorRating = value;
                NotifyOfPropertyChange(() => ActorRating);
            }
        }

        public BindableCollection<Title> MoviesDirected
        {
            get
            {
                if (CurrentPerson == null)
                    return null;
                return new BindableCollection<Title>(CurrentPerson.TitlesDirected.OrderByDescending(t => t.ReleaseYear));
            }
        }

        public BindableCollection<Title> MoviesActed
        {
            get
            {
                if (CurrentPerson == null)
                    return null;
                return new BindableCollection<Title>(CurrentPerson.TitlesActedIn.OrderByDescending(t => t.ReleaseYear));
            }
        }

        public BindableCollection<TitleAward> Awards
        {
            get
            {
                if (CurrentPerson == null)
                    return null;
                return new BindableCollection<TitleAward>(CurrentPerson.Awards.OrderBy(a => a.Type));
            }
        }

        private BindableCollection<GenreExtended> actorGenre;
        public BindableCollection<GenreExtended> ActorGenre
        {
            get { return actorGenre; }
            set
            {
                actorGenre = value;
                NotifyOfPropertyChange(() => ActorGenre);
            }
        }

        private BindableCollection<GenreExtended> directorGenre;
        public BindableCollection<GenreExtended> DirectorGenre
        {
            get { return directorGenre; }
            set
            {
                directorGenre = value;
                NotifyOfPropertyChange(() => DirectorGenre);
            }
        }

        #endregion

        #region Methods

        public void WithPerson(Person person)
        {
            DisplayName = person.Name;
            ScreenId = person.Id.ToString();
            Run.Coroutine(FetchData(person));
        }

        private IEnumerable<IResult> FetchData(Person withperson)
        {
            yield return Show.Busy(IoC.Get<MovieViewModel>());

            var query = from person in context.People
                            .Expand(p => p.TitlesActedIn.SubExpand(x => x.Genres))
                            .Expand(p => p.TitlesDirected.SubExpand(x => x.Genres))
                            .Expand(p => p.Awards)
                         where person.Id == withperson.Id
                         select person;
            var item = new DataServiceCollection<Person>(context);
            yield return new LoadDataResult<Person>(item, query);
            var current = item.FirstOrDefault();

            isActor = false;
            isDirector = false;
            PersonType = string.Empty;

            if (current != null)
            {
                if (current.TitlesActedIn.Count > 0)
                {
                    PersonType = "Actor";
                    IsActor = true;

                    double sum = current.TitlesActedIn.Where(title => title.AverageRating != null).Sum(title => (double)title.AverageRating);
                    ActorRating = sum / current.TitlesActedIn.Count(x => x.AverageRating != null) / 5;

                    foreach (var title in current.TitlesActedIn)
                    {
                        foreach (var genre in title.Genres)
                        {
                            if (genre == null) continue;
                            // ReSharper disable AccessToModifiedClosure
                            if (ActorGenre.Any(g => g.Name == genre.Name))

                                ActorGenre.FirstOrDefault(g => g.Name == genre.Name).Amount++;
                            else
                                ActorGenre.Add(new GenreExtended {Amount = 1, GenreBase = genre});
                            // ReSharper restore AccessToModifiedClosure
                        }
                    }

                    var topGenres = ActorGenre.OrderByDescending(g => g.Amount).Take(10);
                    ActorGenre = new BindableCollection<GenreExtended>(topGenres);
                }


                if (current.TitlesDirected.Count > 0)
                {
                    if (PersonType == "Actor")
                        PersonType += " and ";
                    PersonType += "Director";
                    IsDirector = true;
                    double sum = current.TitlesDirected.Where(title => title.AverageRating != null).Sum(title => (double)title.AverageRating);
                    DirectorRating = sum / current.TitlesDirected.Count(x => x.AverageRating != null) / 5;

                    foreach (var title in current.TitlesDirected)
                    {
                        foreach (var genre in title.Genres)
                        {
                            if (genre == null) continue;
                            // ReSharper disable AccessToModifiedClosure
                            if (DirectorGenre.Any(g => g.Name == genre.Name))

                                DirectorGenre.FirstOrDefault(g => g.Name == genre.Name).Amount++;
                            else
                                DirectorGenre.Add(new GenreExtended { Amount = 1, GenreBase = genre });
                            // ReSharper restore AccessToModifiedClosure
                        }
                    }
                    var topGenres = DirectorGenre.OrderByDescending(g => g.Amount).Take(10);
                    DirectorGenre = new BindableCollection<GenreExtended>(topGenres);
                }

                CurrentPerson = current;
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

        public IEnumerable<IResult> OpenGenre(object selectedItem)
        {
            if (selectedItem is GenreExtended)
            {
                yield return Show.Child<MovieGenreSingleViewModel>()
                    .In(Parent)
                    .Configured(x => x.WithGenre(((GenreExtended)selectedItem).GenreBase));
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