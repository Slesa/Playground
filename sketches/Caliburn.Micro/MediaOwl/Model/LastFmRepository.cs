using System.ComponentModel.Composition;
using System.Linq;
using System.Xml.Linq;
using Caliburn.Micro;
using MediaOwl.Model.LastFm;
using MediaOwl.Services;

namespace MediaOwl.Model
{
    /// <summary>
    /// The repository of the current Last.fm-Data. Gets renewed on every search.
    /// </summary>
    [Export(typeof(LastFmRepository))]
    public class LastFmRepository
    {
        #region Constructor

        public LastFmRepository()
        {
            Artists = new BindableCollection<Artist>();
            Albums = new BindableCollection<Album>();
            Tracks = new BindableCollection<Track>();
            TopTags = new BindableCollection<Tag>();
        }

        #endregion

        #region Properties & Backingfields

        /// <summary>
        /// The Artists of the <see cref="CurrentArtistSearch"/>
        /// It is filled in<see cref="MediaOwl.Services.LastFmResults.ArtistSearchResult"/>.
        /// </summary>
        public BindableCollection<Artist> Artists { get; set; }

        /// <summary>
        /// The Albums of the <see cref="CurrentAlbumSearch"/>
        /// It is filled in <see cref="MediaOwl.Services.LastFmResults.AlbumSearchResult"/>.
        /// </summary>
        public BindableCollection<Album> Albums { get; set; }

        /// <summary>
        /// The Tracks of the <see cref="CurrentTrackSearch"/>
        /// It is filled in <see cref="MediaOwl.Services.LastFmResults.TrackSearchResult"/>.
        /// </summary>
        public BindableCollection<Track> Tracks { get; set; }

        /// <summary>
        /// The current TopTags of Last.Fm
        /// It is filled in <see cref="MediaOwl.Services.LastFmResults.TopTagsResult"/>.
        /// </summary>
        public BindableCollection<Tag> TopTags { get; set; }
        
        /// <summary>
        /// The current search for artists.
        /// It gets updated in <see cref="MediaOwl.Services.LastFmResults.ArtistSearchResult"/>.
        /// </summary>
        public Search CurrentArtistSearch { get; set; }

        /// <summary>
        /// The current search for artists.
        /// It gets updated filled in <see cref="MediaOwl.Services.LastFmResults.AlbumSearchResult"/>.
        /// </summary>
        public Search CurrentAlbumSearch { get; set; }

        /// <summary>
        /// The current search for artists.
        /// It gets updated filled in <see cref="MediaOwl.Services.LastFmResults.TrackSearchResult"/>.
        /// </summary>
        public Search CurrentTrackSearch { get; set; }

        #endregion

        #region Methods

        public void AddData(XElement xmlData, RepositoryTypes repositoryType)
        {
            switch (repositoryType)
            {
                case RepositoryTypes.Artists:
                    AddArtists(xmlData);
                    break;
                case RepositoryTypes.Albums:
                    AddAlbums(xmlData);
                    break;
                case RepositoryTypes.Tracks:
                    AddTracks(xmlData);
                    break;
                case RepositoryTypes.TopTags:
                    AddTopTags(xmlData);
                    break;
                default: break;
            }
        }

        private Search GetSearch(XElement xmlData)
        {
            var openSearchXml =
                xmlData.Descendants("results").Descendants().Where(
                    x =>
                    x.Name.Namespace == LastFmDataAccess.NameSpaceOpenSearch);

            return new Search(openSearchXml);
        }

        private void AddAlbums(XElement xmlData)
        {
            CurrentAlbumSearch = GetSearch(xmlData);

            if (CurrentAlbumSearch.TotalResults != 0)
            {
                var elements = xmlData.Descendants("album");
                Albums.Clear();
                foreach (var element in elements)
                    Albums.Add(new Album(element));
            }
        }

        private void AddArtists(XElement xmlData)
        {
            CurrentArtistSearch = GetSearch(xmlData);

            if (CurrentArtistSearch.TotalResults != 0)
            {
                var elements = xmlData.Descendants("artist");
                Artists.Clear();
                foreach (var element in elements)
                    Artists.Add(new Artist(element));
            }
        }

        private void AddTracks(XElement xmlData)
        {
            CurrentTrackSearch = GetSearch(xmlData);

            if (CurrentTrackSearch.TotalResults != 0)
            {
                var elements = xmlData.Descendants("track");
                Tracks.Clear();
                foreach (var element in elements)
                    Tracks.Add(new Track(element));
            }
        }

        private void AddTopTags(XElement xmlData)
        {
            var elements = xmlData.Descendants("tag");
            TopTags.Clear();
            foreach (var element in elements)
                TopTags.Add(new Tag(element));
        }

        #endregion

        public enum RepositoryTypes
        {
            Artists, Albums, Tracks, TopTags, None
        }
    }
}