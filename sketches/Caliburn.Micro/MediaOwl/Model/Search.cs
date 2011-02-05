using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;

namespace MediaOwl.Model
{
    /// <summary>
    /// Represents a Search delivered from Last.fm or Netflix
    /// </summary>
    public class Search
    {
        public Search()
        {
            
        }

        public Search(IEnumerable<XElement> openSearchXmlData)
        {
            FromXml(openSearchXmlData);
        }

        public string SearchTerms { get; set; }
        public int StartPage { get; set; }
        public long TotalResults { get; set; }
        public int StartIndex { get; set; }
        public int ItemsPerPage { get; set; }
        public string SearchResultText
        {
            get {
                if (string.IsNullOrEmpty(SearchTerms))
                    return "No Search executed;";
                if (TotalResults == 0)
                    return string.Format("No Results found for \"{0}\";", SearchTerms);
                
                long stopIndex;
                if (StartIndex + ItemsPerPage > TotalResults)
                    stopIndex = TotalResults;
                else
                    stopIndex = StartIndex + ItemsPerPage;

                return string.Format("Total Results for \"{0}\": {1}; Results {2} to {3} are shown below;",
                new object[]
                    {
                        SearchTerms,
                        TotalResults.ToString("#,0", CultureInfo.CurrentCulture),
                        (StartIndex + 1).ToString("#,0", CultureInfo.CurrentCulture),
                        stopIndex.ToString("#,0", CultureInfo.CurrentCulture)
                    }); }
        }

        public void FromXml(IEnumerable<XElement> openSearchXmlData)
        {
            foreach (var xElement in openSearchXmlData)
            {
                //<opensearch:Query role="request" searchTerms="cher" startPage="1"/>

                if (xElement.Name.LocalName == "Query")
                {
                    if (xElement.Attribute("searchTerms") != null)
                    {
                        // ReSharper disable PossibleNullReferenceException
                        SearchTerms = xElement.Attribute("searchTerms").Value;
                        // ReSharper restore PossibleNullReferenceException
                    }
                    if (xElement.Attribute("startPage") != null)
                    {
                        // ReSharper disable PossibleNullReferenceException
                        StartPage = Convert.ToInt32(xElement.Attribute("startPage").Value);
                        // ReSharper restore PossibleNullReferenceException
                    }
                    
                }

                if (xElement.Name.LocalName == "totalResults")
                    TotalResults = Convert.ToInt32(xElement.Value);

                if (xElement.Name.LocalName == "startIndex")
                    StartIndex = Convert.ToInt32(xElement.Value);

                if (xElement.Name.LocalName == "itemsPerPage")
                    ItemsPerPage = Convert.ToInt32(xElement.Value);
            }
        }
    }
}