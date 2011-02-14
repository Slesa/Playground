using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace MediaOwl.Model.LastFm
{
    /// <summary>
    /// Represents a Tag delivered from Last.fm
    /// </summary>
    public class Tag : EntityBase
    {
        public Tag()
        {
            
        }

        public Tag(XElement tagXml)
        {
            FromXml(tagXml);
        }

        public string Name { get; set; }
        public Uri Url { get; set; }

        public IList<Artist> Artists { get; set; }
        public IList<Album> Albums { get; set; }
        public IList<Track> Tracks { get; set; }
        public IList<Artist> WeeklyArtistChart { get; set; }

        public override void FromXml(XElement tagXml)
        {
            // ReSharper disable PossibleNullReferenceException
            Name = tagXml.Element("name").Value;
            Url = new Uri(tagXml.Element("url").Value, UriKind.RelativeOrAbsolute);
            // ReSharper restore PossibleNullReferenceException
        }
    }
}