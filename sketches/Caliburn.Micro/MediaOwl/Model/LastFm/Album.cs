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
    /// Represents an Album delivered from Last.fm
    /// </summary>
    public sealed class Album : EntityBase
    {
        public Album()
        {
            
        }

        public Album(XElement albumXml)
        {
            FromXml(albumXml);
        }

        //Basic Properties
        public string Name { get; set; }
        public string ArtistName { get; set; }
        public string MusicBrainzId { get; set; }
        public Uri Url { get; set; }
        public string UrlString { get { return Url.ToString(); } }
        public DateTime ReleaseDate { get; set; }
        public string ReleaseDateString { get { return ReleaseDate.ToShortDateString(); } }
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
        public int Listeners { get; set; }
        public string ListenersString { get { return Listeners.ToString("0,0", CultureInfo.CurrentCulture); } }
        public int PlayCount { get; set; }
        public string PlayCountString { get { return PlayCount.ToString("0,0", CultureInfo.CurrentCulture); } }

        //Additional Properties
        public Biography Wiki { get; set; }
        public IList<Track> Tracks { get; set; }
        public IList<Tag> Tags { get; set; }

        public override void FromXml(XElement albumXml)
        {
            // ReSharper disable PossibleNullReferenceException
            Name = albumXml.Element("name").Value;

            if (albumXml.Element("artist") != null)
            {
                ArtistName = albumXml.Element("artist").HasElements 
                    ? albumXml.Element("artist").Element("name").Value 
                    : albumXml.Element("artist").Value;
            }
                
            MusicBrainzId = albumXml.Element("mbid").Value;
            Url = new Uri(albumXml.Element("url").Value, UriKind.RelativeOrAbsolute);
            if (albumXml.Element("releasedate") != null
                && !string.IsNullOrEmpty(albumXml.Element("releasedate").Value))
                ReleaseDate = Convert.ToDateTime(albumXml.Element("releasedate").Value);

            string uriString = albumXml.Descendants("image")
                .First(descendant => descendant.Attribute("size").Value == "medium")
                .Value;
            if (!string.IsNullOrEmpty(uriString))
                Picture = new BitmapImage(new Uri(uriString));

            uriString = albumXml.Descendants("image")
                .First(descendant => descendant.Attribute("size").Value == "small")
                .Value;
            if (!string.IsNullOrEmpty(uriString))
                PictureSmall = new BitmapImage(new Uri(uriString));

            uriString = albumXml.Descendants("image")
                .LastOrDefault()
                .Value;
            if (!string.IsNullOrEmpty(uriString))
                PictureLarge = new BitmapImage(new Uri(uriString));

            Listeners = albumXml.Element("listeners") == null
                            ? 0
                            : Convert.ToInt32(albumXml.Element("listeners").Value);
            PlayCount = albumXml.Element("playcount") == null
                            ? 0
                            : Convert.ToInt32(albumXml.Element("playcount").Value);

            Wiki = new Biography(albumXml.Descendants("wiki").FirstOrDefault());

            var tags = albumXml.Descendants("tag");
            Tags = new List<Tag>(tags.Count());
            foreach (var tag in tags)
                Tags.Add(new Tag(tag));

            var tracks = albumXml.Descendants("track");
            Tracks = new List<Track>(tracks.Count());
            foreach (var track in tracks)
                Tracks.Add(new Track(track));

            // ReSharper restore PossibleNullReferenceException
        }
    }
}