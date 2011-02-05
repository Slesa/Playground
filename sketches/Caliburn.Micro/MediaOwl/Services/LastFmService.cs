using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Net;
using System.Net.Browser;
using MediaOwl.Model;
using MediaOwl.Model.LastFm;
using MediaOwl.Services.LastFmResults;

namespace MediaOwl.Services
{
    /// <summary>
    /// The interface providing all Last.fm-Services of the application
    /// </summary>
    public interface ILastFmService
    {
        #region Artist-Services

        /// <summary>
        /// Searches for artists on Last.fm. The result is written to the <see cref="LastFmRepository"/>.
        /// </summary>
        /// <param name="searchString">The string to search for</param>
        /// <param name="page">The page to show</param>
        /// <returns>An <see cref="Caliburn.Micro.IResult"/> of type <see cref="LoadLastFmXmlDataResult"/></returns>
        LoadLastFmXmlDataResult ArtistSearch(string searchString, int page = 0);

        /// <summary>
        /// Searches for artists on Last.fm. The missing parameters are taken from <see cref="LastFmRepository.CurrentArtistSearch"/>
        /// The result is written to the <see cref="LastFmRepository"/>.
        /// </summary>
        /// <param name="page">The page to show</param>
        /// <returns>An <see cref="Caliburn.Micro.IResult"/> of type <see cref="LoadLastFmXmlDataResult"/></returns>
        LoadLastFmXmlDataResult ArtistSearch(int page);

        /// <summary>
        /// Searches for a single artist.
        /// </summary>
        /// <param name="artistId">The name of the artist or his MusicBrainzId.</param>
        /// <param name="isMusicBrainzId">Indicates whether the <see cref="artistId"/> is the MusicBrainzId or not.</param>
        /// <returns>An <see cref="Caliburn.Micro.IResult"/> of type <see cref="SingleArtistResult"/></returns>
        LoadLastFmEntityDataResult<Artist> SingleArtist(string artistId, bool isMusicBrainzId);

        /// <summary>
        /// Searches for a single artist.
        /// </summary>
        /// <param name="artistBase">The <see cref="ArtistBase"/> to be extended to an <see cref="Artist"/></param>
        /// <returns>An <see cref="Caliburn.Micro.IResult"/> of type <see cref="SingleArtistResult"/></returns>
        LoadLastFmEntityDataResult<Artist> SingleArtist(ArtistBase artistBase);

        /// <summary>
        /// Gets additional information on an artist.
        /// </summary>
        /// <param name="artist">The artist</param>
        /// <returns>An <see cref="Caliburn.Micro.IResult"/> of type <see cref="MoreInfoResult"/></returns>
        LoadLastFmEntityDataResult<Biography> MoreInfo(Artist artist);

        /// <summary>
        /// Gets the top-albums of an <see cref="Artist"/>
        /// </summary>
        /// <param name="artist">The artist</param>
        /// <returns>An <see cref="Caliburn.Micro.IResult"/> of type <see cref="ArtistTopAlbumsResult"/></returns>
        LoadLastFmListDataResult<Album> TopAlbums(Artist artist);

        /// <summary>
        /// Gets the top-tracks of an <see cref="Artist"/>
        /// </summary>
        /// <param name="artist">The artist</param>
        /// <returns>An <see cref="Caliburn.Micro.IResult"/> of type <see cref="ArtistTopTracksResult"/></returns>
        LoadLastFmListDataResult<Track> TopTracks(Artist artist);

        /// <summary>
        /// Gets the top-tags of an <see cref="Artist"/>
        /// </summary>
        /// <param name="artist">The artist</param>
        /// <returns>An <see cref="Caliburn.Micro.IResult"/> of type <see cref="ArtistTopTagsResult"/></returns>
        LoadLastFmListDataResult<Tag> TopTags(Artist artist);

        /// <summary>
        /// Gets the upcoming events of an <see cref="Artist"/>
        /// </summary>
        /// <param name="artist">The artist</param>
        /// <returns>An <see cref="Caliburn.Micro.IResult"/> of type <see cref="NextEventsResult"/></returns>
        LoadLastFmListDataResult<Event> NextEvents(Artist artist);

        /// <summary>
        /// Gets similar artists of an <see cref="Artist"/>
        /// </summary>
        /// <param name="artist">The artist</param>
        /// <returns>An <see cref="Caliburn.Micro.IResult"/> of type <see cref="SimilarArtistsResult"/></returns>
        LoadLastFmListDataResult<ArtistBase> SimilarArtists(Artist artist);

        /// <summary>
        /// The actual artist search
        /// </summary>
        Search CurrentArtistSearch { get; }


        #endregion

        #region Album-Services

        /// <summary>
        /// Searches for albums on Last.fm. The result is written to the <see cref="LastFmRepository"/>.
        /// </summary>
        /// <param name="searchString">The string to search for</param>
        /// <param name="page">The page to show</param>
        /// <returns>An <see cref="Caliburn.Micro.IResult"/> of type <see cref="LoadLastFmXmlDataResult"/></returns>
        LoadLastFmXmlDataResult AlbumSearch(string searchString, int page = 0);

        /// <summary>
        /// Searches for albums on Last.fm.
        /// The missing parameters are taken from <see cref="LastFmRepository.CurrentAlbumSearch"/>
        /// The result is written to the <see cref="LastFmRepository"/>.
        /// </summary>
        /// <param name="page">The page to show</param>
        /// <returns>An <see cref="Caliburn.Micro.IResult"/> of type <see cref="LoadLastFmXmlDataResult"/></returns>
        LoadLastFmXmlDataResult AlbumSearch(int page);

        /// <summary>
        /// Gets a single <see cref="Album"/>.
        /// </summary>
        /// <param name="albumName">The name of the album</param>
        /// <param name="artistName">The name of the artist</param>
        /// <returns>An <see cref="Caliburn.Micro.IResult"/> of type <see cref="SingleAlbumResult"/></returns>
        LoadLastFmEntityDataResult<Album> SingleAlbum(string albumName, string artistName, string musicBrainzId = null);

        /// <summary>
        /// Gets a single <see cref="Album"/>.
        /// </summary>
        /// <param name="album">The <see cref="Album"/> that needs to be renewed.</param>
        /// <returns>An <see cref="Caliburn.Micro.IResult"/> of type <see cref="SingleAlbumResult"/></returns>
        LoadLastFmEntityDataResult<Album> SingleAlbum(Album album);

        /// <summary>
        /// The actual album search
        /// </summary>
        Search CurrentAlbumSearch { get; }
        #endregion

        #region Track-Services

        /// <summary>
        /// The actual track search
        /// </summary>
        Search CurrentTrackSearch { get; }

        /// <summary>
        /// Searches for tracks on Last.fm.
        /// The result is written to the <see cref="LastFmRepository"/>.
        /// </summary>
        /// <param name="searchString">The string to search for</param>
        /// <param name="artistName">The artist name to limit the search</param>
        /// <param name="page">The page to show</param>
        /// <returns>An <see cref="Caliburn.Micro.IResult"/> of type <see cref="TrackSearchResult"/></returns>
        LoadLastFmXmlDataResult TrackSearch(string searchString, string artistName = null, int page = 0);

        /// <summary>
        /// Searches for tracks on Last.fm.
        /// The missing parameters are taken from <see cref="LastFmRepository.CurrentTrackSearch"/>
        /// The result is written to the <see cref="LastFmRepository"/>.
        /// </summary>
        /// <param name="page">The page to show</param>
        /// <returns>An <see cref="Caliburn.Micro.IResult"/> of type <see cref="TrackSearchResult"/></returns>
        LoadLastFmXmlDataResult TrackSearch(int page);

        /// <summary>
        /// Gets a single <see cref="Track"/>.
        /// </summary>
        /// <param name="trackName">The name of the track.</param>
        /// <param name="artistName">The name of the artist.</param>
        /// <returns>An <see cref="Caliburn.Micro.IResult"/> of type <see cref="SingleTrackResult"/></returns>
        LoadLastFmEntityDataResult<Track> SingleTrack(string trackName, string artistName, string musicBrainzId = null);
        
        /// <summary>
        /// Gets a single <see cref="Track"/>.
        /// </summary>
        /// <param name="track">The <see cref="Track"/> that needs to be renewed.</param>
        /// <returns>An <see cref="Caliburn.Micro.IResult"/> of type <see cref="SingleTrackResult"/></returns>
        LoadLastFmEntityDataResult<Track> SingleTrack(Track track);

        /// <summary>
        /// Gets similar <see cref="Track"/>s of a <see cref="Track"/>.
        /// </summary>
        /// <param name="track">The track.</param>
        /// <returns>An <see cref="Caliburn.Micro.IResult"/> of type <see cref="SimilarTracksResult"/></returns>
        LoadLastFmListDataResult<Track> SimilarTracks(Track track);

        #endregion

        #region Tag-Services

        /// <summary>
        /// Searches for the top-tags on Last.fm.
        /// The result is written to the <see cref="LastFmRepository"/>.
        /// </summary>
        /// <returns>An <see cref="Caliburn.Micro.IResult"/> of type <see cref="TopTagsResult"/></returns>
        LoadLastFmXmlDataResult TopTags();

        /// <summary>
        /// Gets the top-albums of a <see cref="Tag"/>.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns>An <see cref="Caliburn.Micro.IResult"/> of type <see cref="TagTopAlbumsResult"/></returns>
        LoadLastFmListDataResult<Album> TopAlbums(Tag tag);

        /// <summary>
        /// Gets the top-artists of a <see cref="Tag"/>.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns>An <see cref="Caliburn.Micro.IResult"/> of type <see cref="TagTopArtistsResult"/></returns>
        LoadLastFmListDataResult<Artist> TopArtists(Tag tag);

        /// <summary>
        /// Gets the top-tracks of a <see cref="Tag"/>.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns>An <see cref="Caliburn.Micro.IResult"/> of type <see cref="TagTopTracksResult"/></returns>
        LoadLastFmListDataResult<Track> TopTracks(Tag tag);

        /// <summary>
        /// Gets the latest week's chart of artists corresponding to a <see cref="Tag"/>
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns>An <see cref="Caliburn.Micro.IResult"/> of type <see cref="TagWeeklyArtistChartResult"/></returns>
        LoadLastFmListDataResult<Artist> WeeklyArtistChart(Tag tag);

        #endregion
    }

    /// <summary>
    /// This class is a collection of all <see cref="MediaOwl.Services.LastFmResults"/>.
    /// It implements <see cref="ILastFmService"/>
    /// </summary>
    [Export(typeof(ILastFmService))]
    public class LastFmService : ILastFmService
    {
        private readonly LastFmRepository repository;

        [ImportingConstructor]
        public LastFmService(LastFmRepository repository)
        {
            WebRequest.RegisterPrefix(LastFmDataAccess.BaseUri, WebRequestCreator.ClientHttp);
            this.repository = repository;
        }

        #region Searches

        public Search CurrentArtistSearch { get { return repository.CurrentArtistSearch; } }
        public Search CurrentAlbumSearch { get { return repository.CurrentAlbumSearch; } }
        public Search CurrentTrackSearch { get { return repository.CurrentTrackSearch; } }

        #endregion

        #region Artist-Results

        public LoadLastFmXmlDataResult ArtistSearch(string searchString, int page = 0)
        {
            var p1 = new Parameter
            {
                Name = LastFmDataAccess.ParamMethod,
                Value = LastFmDataAccess.MethodArtistSearch
            };
            var p2 = new Parameter
            {
                Name = LastFmDataAccess.ParamArtist,
                Value = searchString
            };
            var p3 = new Parameter
            {
                Name = LastFmDataAccess.ParamPage,
                Value = page.ToString()
            };
            return new LoadLastFmXmlDataResult(
                SearchStringBuilder(new[] { p1, p2, p3 }),
                repository,
                LastFmRepository.RepositoryTypes.Artists
                );
        }
        public LoadLastFmXmlDataResult ArtistSearch(int page)
        {
            return ArtistSearch(CurrentArtistSearch.SearchTerms, page);
        }

        public LoadLastFmEntityDataResult<Artist> SingleArtist(string artistId, bool isMusicBrainzId)
        {
            var p1 = new Parameter
            {
                Name = LastFmDataAccess.ParamMethod,
                Value = LastFmDataAccess.MethodArtistGetInfo
            };
            Parameter p2;
            if (isMusicBrainzId)
            {
                p2 = new Parameter
                {
                    Name = LastFmDataAccess.ParamMBID,
                    Value = artistId
                };
            }
            else
            {
                p2 = new Parameter
                {
                    Name = LastFmDataAccess.ParamArtist,
                    Value = artistId
                };
            }
            var p3 = new Parameter
            {
                Name = LastFmDataAccess.ParamAutocorrect,
                Value = true.ToString()
            };

            return new LoadLastFmEntityDataResult<Artist>(
                SearchStringBuilder(new[]{p1, p2, p3}),
                "artist");
        }
        public LoadLastFmEntityDataResult<Artist> SingleArtist(ArtistBase artistBase)
        {
            return string.IsNullOrEmpty(artistBase.MusicBrainzId) 
                ? SingleArtist(artistBase.Name, false) 
                : SingleArtist(artistBase.MusicBrainzId, true);
        }

        public LoadLastFmEntityDataResult<Biography> MoreInfo(Artist artist)
        {
            var p1 = new Parameter
            {
                Name = LastFmDataAccess.ParamMethod,
                Value = LastFmDataAccess.MethodArtistGetInfo
            };
            Parameter p2;
            if (string.IsNullOrEmpty(artist.MusicBrainzId))
            {
                p2 = new Parameter
                {
                    Name = LastFmDataAccess.ParamArtist,
                    Value = artist.Name
                };
            }
            else
            {
                p2 = new Parameter
                {
                    Name = LastFmDataAccess.ParamMBID,
                    Value = artist.MusicBrainzId
                };
            }
            var p3 = new Parameter
            {
                Name = LastFmDataAccess.ParamAutocorrect,
                Value = true.ToString()
            };

            return new LoadLastFmEntityDataResult<Biography>(
                SearchStringBuilder(new[] {p1, p2, p3}),
                "bio");
        }

        public LoadLastFmListDataResult<Album> TopAlbums(Artist artist)
        {
            var p1 = new Parameter
            {
                Name = LastFmDataAccess.ParamMethod,
                Value = LastFmDataAccess.MethodArtistGetTopAlbums
            };
            Parameter p2;
            if (string.IsNullOrEmpty(artist.MusicBrainzId))
            {
                p2 = new Parameter
                {
                    Name = LastFmDataAccess.ParamArtist,
                    Value = artist.Name
                };
            }
            else
            {
                p2 = new Parameter
                {
                    Name = LastFmDataAccess.ParamMBID,
                    Value = artist.MusicBrainzId
                };
            }
            var p3 = new Parameter
            {
                Name = LastFmDataAccess.ParamAutocorrect,
                Value = true.ToString()
            };

            return new LoadLastFmListDataResult<Album>(SearchStringBuilder(new[] { p1, p2, p3 }), "album");
        }

        public LoadLastFmListDataResult<Track> TopTracks(Artist artist)
        {
            var p1 = new Parameter
            {
                Name = LastFmDataAccess.ParamMethod,
                Value = LastFmDataAccess.MethodArtistGetTopTracks
            };
            Parameter p2;
            if (string.IsNullOrEmpty(artist.MusicBrainzId))
            {
                p2 = new Parameter
                {
                    Name = LastFmDataAccess.ParamArtist,
                    Value = artist.Name
                };
            }
            else
            {
                p2 = new Parameter
                {
                    Name = LastFmDataAccess.ParamMBID,
                    Value = artist.MusicBrainzId
                };
            }
            var p3 = new Parameter
            {
                Name = LastFmDataAccess.ParamAutocorrect,
                Value = true.ToString()
            };

            return new LoadLastFmListDataResult<Track>(
                SearchStringBuilder(new[] { p1, p2, p3 }),
                "track");
        }

        public LoadLastFmListDataResult<Tag> TopTags(Artist artist)
        {
            var p1 = new Parameter
            {
                Name = LastFmDataAccess.ParamMethod,
                Value = LastFmDataAccess.MethodArtistGetTopTags
            };
            Parameter p2;
            if (string.IsNullOrEmpty(artist.MusicBrainzId))
            {
                p2 = new Parameter
                {
                    Name = LastFmDataAccess.ParamArtist,
                    Value = artist.Name
                };
            }
            else
            {
                p2 = new Parameter
                {
                    Name = LastFmDataAccess.ParamMBID,
                    Value = artist.MusicBrainzId
                };
            }
            var p3 = new Parameter
            {
                Name = LastFmDataAccess.ParamAutocorrect,
                Value = true.ToString()
            };

            return new LoadLastFmListDataResult<Tag>(
                SearchStringBuilder(new[] { p1, p2, p3 }),
                "tag");
        }

        public LoadLastFmListDataResult<Event> NextEvents(Artist artist)
        {
            var p1 = new Parameter
            {
                Name = LastFmDataAccess.ParamMethod,
                Value = LastFmDataAccess.MethodArtistGetEvents
            };
            Parameter p2;
            if (string.IsNullOrEmpty(artist.MusicBrainzId))
            {
                p2 = new Parameter
                {
                    Name = LastFmDataAccess.ParamArtist,
                    Value = artist.Name
                };
            }
            else
            {
                p2 = new Parameter
                {
                    Name = LastFmDataAccess.ParamMBID,
                    Value = artist.MusicBrainzId
                };
            }
            var p3 = new Parameter
            {
                Name = LastFmDataAccess.ParamAutocorrect,
                Value = true.ToString()
            };

            return new LoadLastFmListDataResult<Event>(
                SearchStringBuilder(new[] { p1, p2, p3 }),
                "event");
        }

        public LoadLastFmListDataResult<ArtistBase> SimilarArtists(Artist artist)
        {
            const int limit = 16;
            var p1 = new Parameter
            {
                Name = LastFmDataAccess.ParamMethod,
                Value = LastFmDataAccess.MethodArtistGetSimilar
            };
            Parameter p2;
            if (string.IsNullOrEmpty(artist.MusicBrainzId))
            {
                p2 = new Parameter
                {
                    Name = LastFmDataAccess.ParamArtist,
                    Value = artist.Name
                };
            }
            else
            {
                p2 = new Parameter
                {
                    Name = LastFmDataAccess.ParamMBID,
                    Value = artist.MusicBrainzId
                };
            }
            var p3 = new Parameter
            {
                Name = LastFmDataAccess.ParamAutocorrect,
                Value = true.ToString()
            };
            var p4 = new Parameter
            {
                Name = LastFmDataAccess.ParamLimit,
                Value = limit.ToString()
            };

            return new LoadLastFmListDataResult<ArtistBase>(
                SearchStringBuilder(new[] { p1, p2, p3, p4 }),
                "artist");
        }

        #endregion

        #region Album-Results

        public LoadLastFmXmlDataResult AlbumSearch(string searchString, int page = 0)
        {
            var p1 = new Parameter
            {
                Name = LastFmDataAccess.ParamMethod,
                Value = LastFmDataAccess.MethodAlbumSearch
            };
            var p2 = new Parameter
            {
                Name = LastFmDataAccess.ParamAlbum,
                Value = searchString
            };
            var p3 = new Parameter
            {
                Name = LastFmDataAccess.ParamPage,
                Value = page.ToString()
            };
            return new LoadLastFmXmlDataResult(
                SearchStringBuilder(new[] { p1, p2, p3 }),
                repository,
                LastFmRepository.RepositoryTypes.Albums
                );
        }
        public LoadLastFmXmlDataResult AlbumSearch(int page)
        {
            return AlbumSearch(CurrentAlbumSearch.SearchTerms, page);
        }

        public LoadLastFmEntityDataResult<Album> SingleAlbum(string albumName, string artistName, string musicBrainzId = null)
        {
            var p1 = new Parameter
            {
                Name = LastFmDataAccess.ParamMethod,
                Value = LastFmDataAccess.MethodAlbumGetInfo
            };

            var p2 = new Parameter
            {
                Name = LastFmDataAccess.ParamAutocorrect,
                Value = true.ToString()
            };
            Parameter p3;
            string s;
            if (string.IsNullOrEmpty(musicBrainzId))
            {
                p3 = new Parameter
                {
                    Name = LastFmDataAccess.ParamAlbum,
                    Value = albumName
                };
                var p4 = new Parameter
                {
                    Name = LastFmDataAccess.ParamArtist,
                    Value = artistName
                };
                s = SearchStringBuilder(new[] { p1, p2, p3, p4 });
            }
            else
            {
                p3 = new Parameter
                {
                    Name = LastFmDataAccess.ParamMBID,
                    Value = musicBrainzId
                };
                s = SearchStringBuilder(new[] { p1, p2, p3 });
            }

            return new LoadLastFmEntityDataResult<Album>(s, "album");
        }
        public LoadLastFmEntityDataResult<Album> SingleAlbum(Album album)
        {
            return SingleAlbum(album.Name, album.ArtistName, album.MusicBrainzId);
        }

        #endregion

        #region Track-Results

        public LoadLastFmXmlDataResult TrackSearch(string searchString, string artistName = null, int page = 0)
        {
            var p1 = new Parameter
            {
                Name = LastFmDataAccess.ParamMethod,
                Value = LastFmDataAccess.MethodTrackSearch
            };
            var p2 = new Parameter
            {
                Name = LastFmDataAccess.ParamTrack,
                Value = searchString
            };
            var p3 = new Parameter
            {
                Name = LastFmDataAccess.ParamPage,
                Value = page.ToString()
            };
            string s;
            if (string.IsNullOrEmpty(artistName))
            {
                s = SearchStringBuilder(new[] { p1, p2, p3 });
            }
            else
            {
                var p4 = new Parameter
                {
                    Name = LastFmDataAccess.ParamArtist,
                    Value = artistName
                };
                s = SearchStringBuilder(new[] { p1, p2, p3, p4 });
            }
            return new LoadLastFmXmlDataResult(s, repository, LastFmRepository.RepositoryTypes.Tracks);
        }
        public LoadLastFmXmlDataResult TrackSearch(int page)
        {
            return TrackSearch(CurrentTrackSearch.SearchTerms, null, page);
        }

        public LoadLastFmEntityDataResult<Track> SingleTrack(string trackName, string artistName, string musicBrainzId = null)
        {
            var p1 = new Parameter
            {
                Name = LastFmDataAccess.ParamMethod,
                Value = LastFmDataAccess.MethodTrackGetInfo
            };

            var p2 = new Parameter
            {
                Name = LastFmDataAccess.ParamAutocorrect,
                Value = true.ToString()
            };
            Parameter p3;
            string s;
            if (string.IsNullOrEmpty(musicBrainzId))
            {
                p3 = new Parameter
                {
                    Name = LastFmDataAccess.ParamTrack,
                    Value = trackName
                };
                var p4 = new Parameter
                {
                    Name = LastFmDataAccess.ParamArtist,
                    Value = artistName
                };
                s = SearchStringBuilder(new[] { p1, p2, p3, p4 });
            }
            else
            {
                p3 = new Parameter
                {
                    Name = LastFmDataAccess.ParamMBID,
                    Value = musicBrainzId
                };
                s = SearchStringBuilder(new[] { p1, p2, p3 });
            }

            return new LoadLastFmEntityDataResult<Track>(s, "track");
        }
        public LoadLastFmEntityDataResult<Track> SingleTrack(Track track)
        {
            return SingleTrack(track.Name, track.ArtistName, track.MusicBrainzId);
        }

        public LoadLastFmListDataResult<Track> SimilarTracks(Track track)
        {
            const int limit = 16;
            string s;
            var p1 = new Parameter
            {
                Name = LastFmDataAccess.ParamMethod,
                Value = LastFmDataAccess.MethodTrackGetSimilar
            };

            var p2 = new Parameter
            {
                Name = LastFmDataAccess.ParamAutocorrect,
                Value = true.ToString()
            };
            var p3 = new Parameter
            {
                Name = LastFmDataAccess.ParamLimit,
                Value = limit.ToString()
            };

            if (string.IsNullOrEmpty(track.MusicBrainzId))
            {
                var p4 = new Parameter
                {
                    Name = LastFmDataAccess.ParamArtist,
                    Value = track.ArtistName
                };

                var p5 = new Parameter
                {
                    Name = LastFmDataAccess.ParamTrack,
                    Value = track.Name
                };
                s = SearchStringBuilder(new[] { p1, p2, p3, p4, p5 });
            }
            else
            {
                var p4 = new Parameter
                {
                    Name = LastFmDataAccess.ParamMBID,
                    Value = track.MusicBrainzId
                };
                s = SearchStringBuilder(new[] { p1, p2, p3, p4 });
            }

            return new LoadLastFmListDataResult<Track>(s, "track");
        }
        #endregion

        #region Tag-Results

        public LoadLastFmXmlDataResult TopTags()
        {
            var p1 = new Parameter
            {
                Name = LastFmDataAccess.ParamMethod,
                Value = LastFmDataAccess.MethodTagGetTopTags
            };
            var p2 = new Parameter
            {
                Name = LastFmDataAccess.ParamLimit,
                Value = 100.ToString()
            };
            return new LoadLastFmXmlDataResult(
                SearchStringBuilder(new[] { p1, p2 }),
                repository,
                LastFmRepository.RepositoryTypes.TopTags);
        }

        public LoadLastFmListDataResult<Album> TopAlbums(Tag tag)
        {
            var p1 = new Parameter
            {
                Name = LastFmDataAccess.ParamMethod,
                Value = LastFmDataAccess.MethodTagGetTopAlbums
            };
            var p2 = new Parameter
            {
                Name = LastFmDataAccess.ParamTag,
                Value = tag.Name
            };

            return new LoadLastFmListDataResult<Album>(
                SearchStringBuilder(new[] { p1, p2 }),
                "album");
        }
        public LoadLastFmListDataResult<Artist> TopArtists(Tag tag)
        {
            var p1 = new Parameter
            {
                Name = LastFmDataAccess.ParamMethod,
                Value = LastFmDataAccess.MethodTagGetTopArtists
            };
            var p2 = new Parameter
            {
                Name = LastFmDataAccess.ParamTag,
                Value = tag.Name
            };

            return new LoadLastFmListDataResult<Artist>(
                SearchStringBuilder(new[] { p1, p2 }),
                "artist");
        }
        public LoadLastFmListDataResult<Track> TopTracks(Tag tag)
        {
            var p1 = new Parameter
            {
                Name = LastFmDataAccess.ParamMethod,
                Value = LastFmDataAccess.MethodTagGetTopTracks
            };
            var p2 = new Parameter
            {
                Name = LastFmDataAccess.ParamTag,
                Value = tag.Name
            };

            return new LoadLastFmListDataResult<Track>(
                SearchStringBuilder(new[] { p1, p2 }),
                "track");

        }

        public LoadLastFmListDataResult<Artist> WeeklyArtistChart(Tag tag)
        {
            var p1 = new Parameter
            {
                Name = LastFmDataAccess.ParamMethod,
                Value = LastFmDataAccess.MethodTagGetWeeklyArtistChart
            };
            var p2 = new Parameter
            {
                Name = LastFmDataAccess.ParamTag,
                Value = tag.Name
            };

            return new LoadLastFmListDataResult<Artist>(
                SearchStringBuilder(new[] { p1, p2 }),
                "artist");
        }

        #endregion

        /// <summary>
        /// This method is just a cross-reference to <see cref="ServiceHelper"/>
        /// </summary>
        /// <param name="parameter">The list of <see cref="Parameter"/></param>
        /// <returns>The REST-Url-string</returns>
        internal static string SearchStringBuilder(IEnumerable<Parameter> parameter)
        {
            return ServiceHelper.LastFmSearchStringBuilder(parameter);
        }

    }
}