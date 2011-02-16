using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using MediaOwl.Core;
using MediaOwl.Model.LastFm;
using MediaOwl.Services;

namespace MediaOwl.ViewModels
{
    [Export(typeof(MusicTagSingleViewModel)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class MusicTagSingleViewModel : Screen, IChildScreen<MusicViewModel>
    {
        #region Fields
        private readonly ILastFmService service;
        #endregion

        #region Constructor
        [ImportingConstructor]
        public MusicTagSingleViewModel(ILastFmService service)
        {
            this.service = service;
        }

        #endregion

        #region Properties & Backingfields

        private Tag currentTag;
        public Tag CurrentTag
        {
            get { return currentTag; }
            set
            {
                currentTag = value;
                NotifyOfPropertyChange(() => CurrentTag);
                NotifyOfPropertyChange(() => TopArtists);
                NotifyOfPropertyChange(() => TopAlbums);
                NotifyOfPropertyChange(() => TopTracks);
                NotifyOfPropertyChange(() => WeeklyArtistChart);
            }
        }

        public IList<Artist> TopArtists
        {
            get { return CurrentTag == null ? null : CurrentTag.Artists; }
        }
        public IList<Album> TopAlbums
        {
            get { return CurrentTag == null ? null : CurrentTag.Albums; }
        }
        public IList<Track> TopTracks
        {
            get { return CurrentTag == null ? null : CurrentTag.Tracks; }
        }
        public IList<Artist> WeeklyArtistChart
        {
            get { return CurrentTag.WeeklyArtistChart; }
        }

        #endregion

        #region Methods

        public void WithTag(Tag tag)
        {
            CurrentTag = tag;
            DisplayName = "Tag: \'" + CurrentTag.Name + "\'";
            ScreenId = CurrentTag.Name;
            Coroutine.BeginExecute(FetchInfo());
        }

        private IEnumerator<IResult> FetchInfo()
        {
            yield return Show.Busy(IoC.Get<MusicViewModel>());

            var topArtistsResult = service.TopArtists(CurrentTag);
            yield return topArtistsResult;
            CurrentTag.Artists = topArtistsResult.EntityList;

            var topAlbumsResult = service.TopAlbums(CurrentTag);
            yield return topAlbumsResult;
            CurrentTag.Albums = topAlbumsResult.EntityList;

            var topTracksResult = service.TopTracks(CurrentTag);
            yield return topTracksResult;
            CurrentTag.Tracks = topTracksResult.EntityList;

            var weeklyArtistChartResult = service.WeeklyArtistChart(CurrentTag);
            yield return weeklyArtistChartResult;

            double hightestWeight = 0d;
            CurrentTag.WeeklyArtistChart = new List<Artist>();
            foreach (var a in weeklyArtistChartResult.EntityList)
            {
                if (CurrentTag.WeeklyArtistChart.Count == 0)
                    hightestWeight = a.Weight;

                if (hightestWeight > 0)
                    a.Points = Math.Round(a.Weight / hightestWeight * 300d, 1);

                CurrentTag.WeeklyArtistChart.Add(a);
            }

            NotifyOfPropertyChange(() => CurrentTag);
            NotifyOfPropertyChange(() => TopArtists);
            NotifyOfPropertyChange(() => TopAlbums);
            NotifyOfPropertyChange(() => TopTracks);
            NotifyOfPropertyChange(() => WeeklyArtistChart);

            yield return Show.NotBusy(IoC.Get<MusicViewModel>());
        }

        public IEnumerator<IResult> OpenArtist(object selectedItem)
        {
            if (selectedItem is Artist)
            {
                yield return Show.Child<MusicArtistSingleViewModel>()
                    .In(Parent)
                    .Configured(x => x.WithArtist((Artist)selectedItem));
            }
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