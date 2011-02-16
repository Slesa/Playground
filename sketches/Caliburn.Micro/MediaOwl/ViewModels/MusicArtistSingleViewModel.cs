using System.Collections.Generic;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using MediaOwl.Core;
using MediaOwl.Model.LastFm;
using MediaOwl.Services;
using System.Linq;

namespace MediaOwl.ViewModels
{
    [Export(typeof(MusicArtistSingleViewModel)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class MusicArtistSingleViewModel : Screen, IChildScreen<MusicViewModel>
    {
        #region Fields
        private readonly ILastFmService service;
        #endregion

        #region Constructor

        [ImportingConstructor]
        public MusicArtistSingleViewModel(ILastFmService service)
        {
            this.service = service;
        }

        #endregion

        #region Properties & Backingfields

        private Artist currentArtist;
        public Artist CurrentArtist
        {
            get { return currentArtist; }
            set
            {
                currentArtist = value;
                NotifyOfPropertyChange(() => CurrentArtist);
            }
        }

        public IList<ArtistBase> SimilarArtists
        {
            get { return CurrentArtist == null ? null : CurrentArtist.SimilarArtists; }
        }
        public IList<Album> Albums
        {
            get { return CurrentArtist == null ? null : CurrentArtist.Albums; }
        }
        public IList<Track> Tracks
        {
            get { return CurrentArtist == null ? null : CurrentArtist.Tracks; }
        }
        public IList<Event> NextEvents
        {
            get { return CurrentArtist == null ? null : CurrentArtist.NextEvents; }
        }
        public IList<Tag> Tags
        {
            get { return CurrentArtist == null ? null : CurrentArtist.Tags; }
        }

        #endregion

        #region Methods

        public void WithArtist(ArtistBase artistBase)
        {
            DisplayName = artistBase.Name;

            ScreenId = string.IsNullOrEmpty(artistBase.MusicBrainzId)
                ? artistBase.Name
                : artistBase.MusicBrainzId;

            Coroutine.BeginExecute(FetchInfo(artistBase));
        }

        public IEnumerator<IResult> FetchInfo(ArtistBase artistBase)
        {
            yield return Show.Busy(IoC.Get<MusicViewModel>());

            var singleArtistResult = service.SingleArtist(artistBase);
            yield return singleArtistResult;
            CurrentArtist = singleArtistResult.Entity; 

            var similarArtistsResult = service.SimilarArtists(CurrentArtist);
            yield return similarArtistsResult;
            CurrentArtist.SimilarArtists = similarArtistsResult.EntityList;

            var bioArtistResult = service.MoreInfo(CurrentArtist);
            yield return bioArtistResult;
            CurrentArtist.Bio = bioArtistResult.Entity;

            var albumsResult = service.TopAlbums(CurrentArtist);
            yield return albumsResult;
            CurrentArtist.Albums = albumsResult.EntityList.OrderByDescending(x => x.PlayCount).ToList();

            var tracksResult = service.TopTracks(CurrentArtist);
            yield return tracksResult;
            CurrentArtist.Tracks = tracksResult.EntityList.OrderByDescending(x => x.PlayCount).ToList();

            var tagsResult = service.TopTags(CurrentArtist);
            yield return tagsResult;
            CurrentArtist.Tags = tagsResult.EntityList;

            var eventsResult = service.NextEvents(CurrentArtist);
            yield return eventsResult;
            CurrentArtist.NextEvents = eventsResult.EntityList;

            NotifyOfPropertyChange(() => CurrentArtist);
            NotifyOfPropertyChange(() => SimilarArtists);
            NotifyOfPropertyChange(() => Albums);
            NotifyOfPropertyChange(() => Tracks);
            NotifyOfPropertyChange(() => Tags);
            NotifyOfPropertyChange(() => NextEvents);

            yield return Show.NotBusy(IoC.Get<MusicViewModel>());
        }

        public IEnumerator<IResult> OpenSimilar(object similarArtist)
        {
            if (similarArtist is ArtistBase)
            {
                yield return Show.Child<MusicArtistSingleViewModel>()
                    .In(Parent)
                    .Configured(x => x.WithArtist((ArtistBase) similarArtist));
            }
        }

        public IEnumerator<IResult> OpenPicture()
        {
            yield return Show.Busy(Parent);
            yield return Show.Child<ShowPictureSingleViewModel>()
                .In(Parent)
                .Configured(x => x.WithPicture(CurrentArtist.PictureLarge, CurrentArtist.Name));
            yield return Show.NotBusy(Parent);
        }

        public IEnumerator<IResult> OpenAlbum(object selectedItem)
        {
            if (selectedItem is Album)
            {
                yield return Show.Child<MusicAlbumSingleViewModel>()
                    .In(Parent)
                    .Configured(x => x.WithAlbum((Album)selectedItem));
            }
        }

        public IEnumerator<IResult> OpenTrack(object selectedItem)
        {
            if (selectedItem is Track)
            {
                yield return Show.Child<MusicTrackSingleViewModel>()
                    .In(Parent)
                    .Configured(x => x.WithTrack((Track)selectedItem));
            }
        }

        public IEnumerator<IResult> OpenTag(object selectedItem)
        {
            if (selectedItem is Tag)
            {
                yield return Show.Child<MusicTagSingleViewModel>()
                    .In(Parent)
                    .Configured(x => x.WithTag((Tag)selectedItem));
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

        public int? Order { get { return null; } }

        #endregion
    }
}