using System;
using System.Xml.Linq;
using MediaOwl.Services;

namespace MediaOwl.Model.LastFm
{
    /// <summary>
    /// Represents a Wiki-Entry (<see cref="Album"/>, <see cref="Track"/>) 
    /// or a Biography-Entry (<see cref="Artist"/>) delivered from Last.fm
    /// </summary>
    public class Biography : EntityBase
    {
        public Biography()
        {
            
        }

        public Biography(XElement bioXml)
        {
            FromXml(bioXml);
        }

        public DateTime Published { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }

        public override void FromXml(XElement bioXml)
        {
            // ReSharper disable PossibleNullReferenceException
            if (bioXml != null)
            {
                Summary = ServiceHelper.FormatHtmlString(bioXml.Element("summary").Value);
                Content = ServiceHelper.FormatHtmlString(bioXml.Element("content").Value);
                if (bioXml.Element("published") != null &&
                    !string.IsNullOrEmpty(bioXml.Element("published").Value))
                {
                    Published = Convert.ToDateTime(bioXml.Element("published").Value);
                }
            }
            // ReSharper enable PossibleNullReferenceException
        }
    }
}