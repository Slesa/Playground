using System;
using System.Linq;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace MediaOwl.Model.LastFm
{
    /// <summary>
    /// Represents an Artist delivered from Last.fm.
    /// This class contains only the basic information of an artist.
    /// The full information is in <see cref="Artist"/>.
    /// </summary>
    public class ArtistBase : EntityBase
    {
        public ArtistBase()
        {
            
        }

        public ArtistBase(XElement artistBaseXml)
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            FromXml(artistBaseXml);
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        public string Name { get; set; }
        public string MusicBrainzId { get; set; }
        public int SimilarMatch { get; set; }
        public Uri Url { get; set; }
        public BitmapImage PictureSmall { get; set; }

        public override void FromXml(XElement artistBaseXml)
        {
            // ReSharper disable PossibleNullReferenceException
            string uriString;
            if (artistBaseXml.Descendants("image")
                    .FirstOrDefault(x =>x.Attribute("size").Value == "medium") != null)
            {
                uriString = artistBaseXml.Descendants("image")
                    .FirstOrDefault(x => x.Attribute("size").Value == "medium")
                    .Value;
            }
            else
            {
                uriString = artistBaseXml.Descendants("image")
                    .FirstOrDefault()
                    .Value;
            }

            BitmapImage img = null;
            if (!string.IsNullOrEmpty(uriString))
                img = new BitmapImage(new Uri(uriString));

            Name = artistBaseXml.Element("name").Value;
            MusicBrainzId = artistBaseXml.Element("mbid").Value;

            if (artistBaseXml.Element("match") == null
                || string.IsNullOrEmpty(artistBaseXml.Element("match").Value))
                SimilarMatch = 0;
            else
                SimilarMatch = (int) Math.Round(Convert.ToDouble(artistBaseXml.Element("match").Value), 0);

            Url = new Uri(artistBaseXml.Element("url").Value, UriKind.RelativeOrAbsolute);
            if (img != null) PictureSmall = img;
            // ReSharper restore PossibleNullReferenceException
        }
    }
}