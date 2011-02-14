using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace MediaOwl.Model.LastFm
{
    /// <summary>
    /// Represents a Track delivered from Last.fm
    /// </summary>
    public class Track : EntityBase
    {
        public Track()
        {
            
        }

        public Track(XElement trackXml)
        {
            FromXml(trackXml);
        }

        public int Rank { get; set; }
        public int Position { get; set; }
        public string Name { get; set; }
        public string MusicBrainzId { get; set; }
        public TimeSpan Duration { get; set; }
        public string DurationString
        {
            get
            {
                return string.Format("{0:D} Minutes {1:D} Seconds",
                        Duration.Minutes,
                        Duration.Seconds);
            }
        }
        public Uri Url { get; set; }
        public string UrlString { get { return Url.ToString(); } }
        public bool Streamable { get; set; }
        public string ArtistName { get; set; }
        public string ArtistMusicBrainzId { get; set; }
        public string AlbumName { get; set; }
        public string AlbumMusicBrainzId { get; set; }
        public int Listeners { get; set; }
        public string ListenersString { get { return Listeners.ToString("#,0", CultureInfo.CurrentCulture); } }
        public int PlayCount { get; set; }
        public string PlayCountString { get { return PlayCount.ToString("#,0", CultureInfo.CurrentCulture); } }
        public Biography Wiki { get; set; }
        public BitmapImage PictureSmall { get; set; }
        public BitmapImage Picture { get; set; }
        public BitmapImage PictureLarge { get; set; }
        public Visibility PictureVisibility
        {
            get
            {
                return Picture == null && PictureLarge == null
                    ? Visibility.Collapsed
                    : Visibility.Visible;
            }
        }
        public int SimilarMatch { get; set; }

        public IList<Tag> Tags { get; set; }
        public IList<Track> SimilarTracks { get; set; }

        public override void FromXml(XElement trackXml)
        {
            // ReSharper disable PossibleNullReferenceException

            if (trackXml.Attribute("rank") != null
                && !string.IsNullOrEmpty(trackXml.Attribute("rank").Value))
            {
                Rank = Convert.ToInt32(trackXml.Attribute("rank").Value);
            }

            Name = trackXml.Element("name").Value;

            if (string.IsNullOrEmpty(trackXml.Element("mbid").Value))
                MusicBrainzId = trackXml.Element("mbid").Value;

            if (trackXml.Element("duration") != null
                && !string.IsNullOrEmpty(trackXml.Element("duration").Value))
            {
                Duration = TimeSpan.FromMilliseconds(Convert.ToDouble(trackXml.Element("duration").Value));
            }

            Url = new Uri(trackXml.Element("url").Value);
            if (trackXml.Element("streamable") != null)
            {
                Streamable = trackXml.Element("streamable").Value != "0";
            }

            if (trackXml.Descendants("artist").FirstOrDefault() != null)
            {
                if (trackXml.Descendants("artist").FirstOrDefault().HasElements)
                {
                    ArtistName = trackXml.Descendants("artist").FirstOrDefault().Element("name").Value;
                    if (trackXml.Descendants("artist").FirstOrDefault().Element("mbid") != null)
                        ArtistMusicBrainzId = trackXml.Descendants("artist").FirstOrDefault().Element("mbid").Value;
                }
                else
                {
                    ArtistName = trackXml.Descendants("artist").FirstOrDefault().Value;
                }
            }

            if (trackXml.Descendants("album").FirstOrDefault() != null
                && trackXml.Descendants("album").FirstOrDefault().HasElements)
            {
                Position = Convert.ToInt32(trackXml.Descendants("album").FirstOrDefault().Attribute("position").Value);
                AlbumName = trackXml.Descendants("album").FirstOrDefault().Element("title").Value;
                if (trackXml.Descendants("album").FirstOrDefault().Element("mbid") != null)
                    AlbumMusicBrainzId = trackXml.Descendants("album").FirstOrDefault().Element("mbid").Value;
            }

            if (trackXml.Element("image") == null)
            {
                if (trackXml.Descendants("album").FirstOrDefault() != null
                && trackXml.Descendants("album").FirstOrDefault().HasElements)
                {
                    var images = trackXml.Descendants("album").FirstOrDefault().Descendants("image");
                    foreach (var element in images)
                    {
                        switch (element.Attribute("size").Value)
                        {
                            case "medium":
                                if (!string.IsNullOrEmpty(element.Value))
                                {
                                    PictureSmall = new BitmapImage(new Uri(element.Value));
                                    PictureLarge = Picture;
                                }
                                break;
                            case "large":
                                if (!string.IsNullOrEmpty(element.Value))
                                {
                                    Picture = new BitmapImage(new Uri(element.Value));
                                    PictureLarge = Picture;
                                }
                                break;
                            case "extralarge":
                                if (!string.IsNullOrEmpty(element.Value))
                                    PictureLarge = new BitmapImage(new Uri(element.Value));
                                break;
                        }
                        
                    }
                }
            }
            else
            {
                if (trackXml.Descendants("image").FirstOrDefault() != null
                && trackXml.Descendants("image").FirstOrDefault(descendant => descendant.Attribute("size").Value == "large") != null)
                {
                    string uriString = trackXml.Descendants("image")
                    .FirstOrDefault(descendant => descendant.Attribute("size").Value == "large")
                    .Value;

                    if (!string.IsNullOrEmpty(uriString))
                        Picture = new BitmapImage(new Uri(uriString));
                }

                if (trackXml.Descendants("image").FirstOrDefault() != null
                    && trackXml.Descendants("image").FirstOrDefault(descendant => descendant.Attribute("size").Value == "medium") != null)
                {
                    string uriString = trackXml.Descendants("image")
                    .FirstOrDefault(descendant => descendant.Attribute("size").Value == "medium")
                    .Value;

                    if (!string.IsNullOrEmpty(uriString))
                        PictureSmall = new BitmapImage(new Uri(uriString));
                }

                if (trackXml.Descendants("image").FirstOrDefault() != null)
                {
                    string uriString = trackXml.Descendants("image")
                    .LastOrDefault()
                    .Value;

                    if (!string.IsNullOrEmpty(uriString))
                        PictureLarge = new BitmapImage(new Uri(uriString));
                }
            }

            Listeners = trackXml.Element("listeners") == null
                            ? 0
                            : Convert.ToInt32(trackXml.Element("listeners").Value);
            PlayCount = trackXml.Element("playcount") == null
                            ? 0
                            : Convert.ToInt32(trackXml.Element("playcount").Value);

            Tags = new List<Tag>();
            foreach (var tagXml in trackXml.Descendants("tag"))
                Tags.Add(new Tag(tagXml));

            Wiki = new Biography(trackXml.Descendants("wiki").FirstOrDefault());

            if (trackXml.Element("match") == null
                || string.IsNullOrEmpty(trackXml.Element("match").Value))
            {
                SimilarMatch = 0;
            }
            else
            {
                SimilarMatch = (int)Math.Round(Convert.ToDouble(trackXml.Element("match").Value), 0);
            }
            // ReSharper restore PossibleNullReferenceException
        }
    }
}