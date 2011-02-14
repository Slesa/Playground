using System;
using System.Linq;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace MediaOwl.Model.LastFm
{
    /// <summary>
    /// Represents an Event delivered from Last.fm
    /// </summary>
    public class Event : EntityBase
    {
        public Event()
        {
            
        }

        public Event(XElement eventXml)
        {
            FromXml(eventXml);
        }

        public string Name { get; set; }
        public string Headliner { get; set; }
        public string VenueName { get; set; }
        public string VenueLocation { get; set; }
        public DateTime StartDate { get; set; }
        public string StartDateString { get { return StartDate.ToShortDateString(); } }
        public TimeSpan StartTime { get; set; }
        public string StartTimeString { get { return StartTime.ToString(); } }
        public BitmapImage Picture { get; set; }
        public Uri Url { get; set; }

        public override void FromXml(XElement eventXml)
        {
            // ReSharper disable PossibleNullReferenceException
            string uriString = eventXml.Descendants("image")
                .First(descendant => descendant.Attribute("size").Value == "large")
                .Value;

            BitmapImage img = null;
            if (!string.IsNullOrEmpty(uriString))
                img = new BitmapImage(new Uri(uriString));

            Name = eventXml.Element("title").Value;
            Headliner = eventXml.Element("artists").Element("headliner").Value;

            XElement venueXml = eventXml.Element("venue");
            if (venueXml != null)
            {
                VenueName = venueXml.Element("name").Value;
                VenueLocation = venueXml.Element("location").Element("city").Value
                    + " / " + venueXml.Element("location").Element("country").Value;
            }

            if (eventXml.Element("startDate") != null
                && !string.IsNullOrEmpty(eventXml.Element("startDate").Value))
            {
                StartDate = Convert.ToDateTime(eventXml.Element("startDate").Value);
            }
            if (eventXml.Element("startTime") != null
                && !string.IsNullOrEmpty(eventXml.Element("startTime").Value))
            {
                StartTime = Convert.ToDateTime(eventXml.Element("startTime").Value).TimeOfDay;
            }
            else if (StartDate.TimeOfDay != TimeSpan.Zero)
            {
                StartTime = StartDate.TimeOfDay;
            }

            Url = new Uri(eventXml.Element("url").Value);
            if (img != null) Picture = img;
            // ReSharper restore PossibleNullReferenceException
        }
    }
}