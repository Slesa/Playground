using System.Collections.Generic;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using MediaOwl.Core;
using MediaOwl.Model.LastFm;
using MediaOwl.Services;

namespace MediaOwl.ViewModels
{
    [Export(typeof(MusicTrackSingleViewModel)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class MusicTrackSingleViewModel : Screen, IChildScreen<MusicViewModel>
    {
        #region Fields
        private readonly ILastFmService service;
        #endregion

        #region Constructor

        [ImportingConstructor]
        public MusicTrackSingleViewModel(ILastFmService service)
        {
            this.service = service;
        }

        #endregion

        #region Properties & Backingfields

        private Track currentTrack;
        public Track CurrentTrack
        {
            get { return currentTrack; }
            set
            {
                currentTrack = value;
                NotifyOfPropertyChange(() => CurrentTrack);
            }
        }

        public IList<Tag> Tags
        {
            get { return CurrentTrack == null ? null : CurrentTrack.Tags; }
        }
        public IList<Track> SimilarTracks
        {
            get { return CurrentTrack == null ? null : CurrentTrack.SimilarTracks; }
        }

        public bool CanShowAlbum
        {
            get
            {
                if (CurrentTrack == null)
                    return false;

                if (string.IsNullOrEmpty(CurrentTrack.AlbumName)
                    && string.IsNullOrEmpty(CurrentTrack.ArtistName))
                    return false;

                if (string.IsNullOrEmpty(CurrentTrack.AlbumMusicBrainzId))
                    return false;

                return true;
            }
        }
        public bool CanShowArtist
        {
            get
            {
                if (CurrentTrack == null)
                    return false;
                return !string.IsNullOrEmpty(CurrentTrack.ArtistName);
            }
        }
        #endregion

        #region Methods

        public void WithTrack(Track track)
        {
            DisplayName = track.Name;
            ScreenId = string.IsNullOrEmpty(track.MusicBrainzId)
                           ? track.Name
                           : track.MusicBrainzId;
            Coroutine.BeginExecute(FetchInfo(track));
        }

        public IEnumerator<IResult> FetchInfo(Track track)
        {
            yield return Show.Busy(IoC.Get<MusicViewModel>());

            var singleTrackResult = service.SingleTrack(track);
            yield return singleTrackResult;
            CurrentTrack = singleTrackResult.Entity;

            var similarTracksResult = service.SimilarTracks(CurrentTrack);
            yield return similarTracksResult;
            CurrentTrack.SimilarTracks = similarTracksResult.EntityList;

            NotifyOfPropertyChange(() => Tags);
            NotifyOfPropertyChange(() => SimilarTracks);
            NotifyOfPropertyChange(() => CanShowAlbum);
            NotifyOfPropertyChange(() => CanShowArtist);

            yield return Show.NotBusy(IoC.Get<MusicViewModel>());
        }

        public IEnumerator<IResult> ShowArtist()
        {
            yield return Show.Busy(Parent);
            var artistResult = string.IsNullOrEmpty(CurrentTrack.AlbumMusicBrainzId)
                ? service.SingleArtist(CurrentTrack.ArtistName, false)
                : service.SingleArtist(CurrentTrack.ArtistMusicBrainzId, true);
            yield return artistResult;
            yield return Show.NotBusy(Parent);
            if (artistResult.Entity != null)
            {
                yield return Show.Child<MusicArtistSingleViewModel>()
                .In(Parent)
                .Configured(x => x.WithArtist(artistResult.Entity));
            }
        }

        public IEnumerator<IResult> ShowAlbum()
        {
            yield return Show.Busy(Parent);
            var albumResult = service.SingleAlbum(CurrentTrack.AlbumName, CurrentTrack.ArtistName,
                                                  CurrentTrack.AlbumMusicBrainzId); 
            yield return albumResult;
            yield return Show.NotBusy(Parent);
            if (albumResult.Entity != null)
            {
                yield return Show.Child<MusicAlbumSingleViewModel>()
                .In(Parent)
                .Configured(x => x.WithAlbum(albumResult.Entity));
            }
        }

        public IEnumerator<IResult> OpenPicture()
        {
            yield return Show.Busy(Parent);
            yield return Show.Child<ShowPictureSingleViewModel>()
                .In(Parent)
                .Configured(x => x.WithPicture(CurrentTrack.PictureLarge, CurrentTrack.Name));
            yield return Show.NotBusy(Parent);
        }

        public IEnumerator<IResult> OpenSimilar(object selectedItem)
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