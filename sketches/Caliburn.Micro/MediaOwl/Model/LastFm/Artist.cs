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
    /// Represents an Artist delivered from Last.fm.
    /// Inherits from <see cref="ArtistBase"/>.
    /// </summary>
    public sealed class Artist : ArtistBase
    {
        public Artist()
        {
            
        }

        public Artist(XElement artistXml)
        {
            FromXml(artistXml);
        }

        //Basic Properties
        public int Listeners { get; set; }
        public string ListenersString { get { return Listeners.ToString("#,0", CultureInfo.CurrentCulture); } }
        public string UrlString { get { return Url.ToString(); } }
        public bool Streamable { get; set; }
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

        //Additional Properties
        public double Points { get; set; }
        public double Weight { get; set; }
        public Biography Bio { get; set; }
        public IList<ArtistBase> SimilarArtists { get; set; }
        public IList<Album> Albums { get; set; }
        public IList<Track> Tracks { get; set; }
        public IList<Tag> Tags { get; set; }
        public IList<Event> NextEvents { get; set; }

        public override void FromXml(XElement artistXml)
        {
            // ReSharper disable PossibleNullReferenceException
            string uriString;
            if (artistXml.Descendants("image")
                    .FirstOrDefault(x => x.Attribute("size").Value == "medium") != null)
            {
                uriString = artistXml.Descendants("image")
                    .FirstOrDefault(x => x.Attribute("size").Value == "medium")
                    .Value;
            }
            else
            {
                uriString = artistXml.Descendants("image")
                    .FirstOrDefault()
                    .Value;
            }

            BitmapImage img = null;
            if (!string.IsNullOrEmpty(uriString))
                img = new BitmapImage(new Uri(uriString));

            Name = artistXml.Element("name").Value;
            MusicBrainzId = artistXml.Element("mbid").Value;

            if (artistXml.Element("match") == null
                || string.IsNullOrEmpty(artistXml.Element("match").Value))
                SimilarMatch = 0;
            else
                SimilarMatch = (int)Math.Round(Convert.ToDouble(artistXml.Element("match").Value), 0);

            Url = new Uri(artistXml.Element("url").Value, UriKind.RelativeOrAbsolute);
            if (img != null) PictureSmall = img;
            // ReSharper restore PossibleNullReferenceException
            // ReSharper disable PossibleNullReferenceException
            uriString = artistXml.Descendants("image")
                .FirstOrDefault(descendant => descendant.Attribute("size").Value == "large")
                .Value;

            img = null;
            if (!string.IsNullOrEmpty(uriString))
                img = new BitmapImage(new Uri(uriString));
            if (img != null) Picture = img;


            uriString = artistXml.Descendants("image")
                .FirstOrDefault(descendant => descendant.Attribute("size").Value == "mega")
                .Value;

            img = null;
            if (!string.IsNullOrEmpty(uriString))
                img = new BitmapImage(new Uri(uriString));

            if (img != null) PictureLarge = img;

            Listeners = artistXml.Descendants("listeners").FirstOrDefault() == null
                            ? 0
                            : Convert.ToInt32(artistXml.Descendants("listeners").FirstOrDefault().Value);

            Streamable = artistXml.Element("streamable").Value != "0";

            if (artistXml.Element("weight") != null
                && !string.IsNullOrEmpty(artistXml.Element("weight").Value))
            {
                Weight = Convert.ToDouble(artistXml.Element("weight").Value, new CultureInfo("en-US"));
            }
            // ReSharper restore PossibleNullReferenceException
        }
    }
}