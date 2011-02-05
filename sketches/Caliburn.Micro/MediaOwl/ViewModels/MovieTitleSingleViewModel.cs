using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Services.Client;
using System.Linq;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using MediaOwl.Core;
using MediaOwl.NetflixServiceReference;
using MediaOwl.Services.NetflixResults;
using MediaOwl.Services;

namespace MediaOwl.ViewModels
{
    [Export(typeof(MovieTitleSingleViewModel)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class MovieTitleSingleViewModel : Screen, IChildScreen<MovieViewModel>
    {
        #region Fields
        private readonly NetflixCatalog context;
        #endregion

        #region Constructor
        [ImportingConstructor]
        public MovieTitleSingleViewModel(NetflixCatalog context)
        {
            this.context = context;
        }

        #endregion

        #region Properties & Backingfields

        private Title currentTitle;
        public Title CurrentTitle
        {
            get { return currentTitle; }
            set
            {
                currentTitle = value;
                NotifyOfPropertyChange(() => CurrentTitle);
                NotifyOfPropertyChange(() => Awards);
                NotifyOfPropertyChange(() => Genres);
                NotifyOfPropertyChange(() => Directors);
                NotifyOfPropertyChange(() => Languages);
                NotifyOfPropertyChange(() => AverageRating);
                NotifyOfPropertyChange(() => Summary);
                NotifyOfPropertyChange(() => Cast);
            }
        }

        public BindableCollection<TitleAward> Awards
        {
            get
            {
                if (CurrentTitle == null)
                    return null;
                return new BindableCollection<TitleAward>(CurrentTitle.Awards.OrderBy(a => a.Type));
            }
        }
        public BindableCollection<Genre> Genres
        {
            get
            {
                if (CurrentTitle == null)
                    return null;
                return new BindableCollection<Genre>(CurrentTitle.Genres.OrderBy(g => g.Name));
            }
        }
        public BindableCollection<Person> Directors
        {
            get
            {
                if (CurrentTitle == null)
                    return null;
                return new BindableCollection<Person>(CurrentTitle.Directors);
            }
        }
        public BindableCollection<Language> Languages
        {
            get
            {
                if (CurrentTitle == null)
                    return null;
                return new BindableCollection<Language>(CurrentTitle.Languages);
            }
        }
        public BindableCollection<Person> Cast
        {
            get
            {
                if (CurrentTitle == null)
                    return null;
                return new BindableCollection<Person>(CurrentTitle.Cast);
            }
        }
        public double? AverageRating
        {
            get
            {
                if (CurrentTitle == null || CurrentTitle.AverageRating == null)
                    return null;
                return Math.Round((double)CurrentTitle.AverageRating / 5, 1);
            }
        }
        public string Summary
        {
            get
            {
                if (CurrentTitle == null)
                    return null;
                return ServiceHelper.FormatHtmlString(CurrentTitle.Synopsis);
            }
        }

        #endregion

        #region Methods

        public void WithTitle(Title title)
        {
            DisplayName = title.ShortName;
            ScreenId = title.Id;
            Run.Coroutine(FetchData(title));
        }

        private IEnumerable<IResult> FetchData(Title withtitle)
        {
            yield return Show.Busy(IoC.Get<MovieViewModel>());

            var query = from title in context.Titles
                             .Expand(t => t.Awards)
                             .Expand(t => t.Genres)
                             .Expand(t => t.Directors)
                             .Expand(t => t.Languages)
                             .Expand(t => t.Cast)
                         where title.Id == withtitle.Id
                         select title;
            var item = new DataServiceCollection<Title>(context);
            yield return new LoadDataResult<Title>(item, query);
            CurrentTitle = item.FirstOrDefault();
            yield return Show.NotBusy(IoC.Get<MovieViewModel>());
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

        public IEnumerable<IResult> OpenGenre(object selectedItem)
        {
            if (selectedItem is Genre)
            {
                yield return Show.Child<MovieGenreSingleViewModel>()
                    .In(Parent)
                    .Configured(x => x.WithGenre((Genre)selectedItem));
            }
        }

        public IEnumerator<IResult> OpenPicture()
        {
            yield return Show.Busy(Parent);
            yield return Show.Child<ShowPictureSingleViewModel>()
                .In(Parent)
                .Configured(x => x.WithPicture(new BitmapImage(new Uri(CurrentTitle.BoxArt.LargeUrl,UriKind.Absolute)), CurrentTitle.ShortName));
            yield return Show.NotBusy(Parent);
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