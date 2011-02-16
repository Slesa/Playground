using System.Collections.Generic;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using MediaOwl.Core;
using MediaOwl.Model.LastFm;
using MediaOwl.Services;

namespace MediaOwl.ViewModels
{
    [Export(typeof(MusicAlbumSingleViewModel)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class MusicAlbumSingleViewModel : Screen, IChildScreen<MusicViewModel>
    {
        #region Fields
        private readonly ILastFmService service;
        #endregion

        #region Constructor

        [ImportingConstructor]
        public MusicAlbumSingleViewModel(ILastFmService service)
        {
            this.service = service;
        }

        #endregion

        #region Properties & Backingfields

        private Album currentAlbum;
        public Album CurrentAlbum
        {
            get { return currentAlbum; }
            set
            {
                currentAlbum = value;
                NotifyOfPropertyChange(() => CurrentAlbum);
            }
        }

        public IList<Tag> Tags
        {
            get { return CurrentAlbum == null ? null : CurrentAlbum.Tags; }
        }
        public IList<Track> Tracks
        {
            get { return CurrentAlbum == null ? null : CurrentAlbum.Tracks; }
        }

        public bool CanShowArtist
        {
            get
            {
                if (CurrentAlbum == null)
                    return false;
                return !string.IsNullOrEmpty(CurrentAlbum.ArtistName);
            }
        }

        #endregion

        #region Methods

        public void WithAlbum(Album album)
        {
            DisplayName = album.Name;
            ScreenId = string.IsNullOrEmpty(album.MusicBrainzId)
                           ? album.Name
                           : album.MusicBrainzId;
            Coroutine.BeginExecute(FetchInfo(album));
        }

        public IEnumerator<IResult> FetchInfo(Album album)
        {
            yield return Show.Busy(IoC.Get<MusicViewModel>());

            var singleAlbumResult = service.SingleAlbum(album);
            yield return singleAlbumResult;
            CurrentAlbum = singleAlbumResult.Entity;

            NotifyOfPropertyChange(() => Tags);
            NotifyOfPropertyChange(() => Tracks);
            NotifyOfPropertyChange(() => CanShowArtist);

            yield return Show.NotBusy(IoC.Get<MusicViewModel>());
        }

        public IEnumerator<IResult> ShowArtist()
        {
            yield return Show.Busy(Parent);
            var artistResult = service.SingleArtist(CurrentAlbum.ArtistName, false);
            yield return artistResult;
            yield return Show.NotBusy(Parent);
            if (artistResult.Entity != null)
            {
                yield return Show.Child<MusicArtistSingleViewModel>()
                .In(Parent)
                .Configured(x => x.WithArtist(artistResult.Entity));
            }
        }

        public IEnumerator<IResult> OpenPicture()
        {
            yield return Show.Busy(Parent);
            yield return Show.Child<ShowPictureSingleViewModel>()
                .In(Parent)
                .Configured(x => x.WithPicture(CurrentAlbum.PictureLarge, CurrentAlbum.Name));
            yield return Show.NotBusy(Parent);
        }

        public IEnumerator<IResult> OpenTrack(object track)
        {
            if (track is Track)
            {
                yield return Show.Child<MusicTrackSingleViewModel>()
                    .In(Parent)
                    .Configured(x => x.WithTrack((Track)track));
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