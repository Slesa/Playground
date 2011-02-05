using System.Collections.Generic;
using System.Linq;
using System.Windows.Browser;

namespace MediaOwl.Services
{
    public class ServiceHelper
    {
        /// <summary>
        /// Decodes a html-string and strips the tags.
        /// </summary>
        /// <param name="source">The source-string.</param>
        /// <returns>A formatted string.</returns>
        public static string FormatHtmlString(string source)
        {
            return HttpUtility.HtmlDecode(StripTagsCharArray(source));
        }

        /// <summary>
        /// Builds the url-search-string for the Last.Fm-REST-service
        /// </summary>
        /// <param name="parameters">The parameters defining the search</param>
        /// <returns>The search-url-string</returns>
        internal static string LastFmSearchStringBuilder(IEnumerable<Parameter> parameters)
        {
            const char starter = '?', and = '&', equal = '=';
            string searchString = LastFmDataAccess.BaseUri + starter;

            var paramList = new List<Parameter>(parameters);

            if (!paramList.Any(x => x.Name == LastFmDataAccess.ApiKey))
            {
                paramList.Add(new Parameter
                {
                    Name = LastFmDataAccess.ParamApiKey,
                    Value = LastFmDataAccess.ApiKey
                });
            }

            foreach (var p in paramList)
            {
                if (!searchString.EndsWith(starter.ToString()))
                    searchString += and;

                if (!string.IsNullOrEmpty(p.Name) || !string.IsNullOrEmpty(p.Value))
                    searchString += p.Name + equal + HttpUtility.UrlEncode(p.Value);
            }

            return searchString;
        }

        /// <summary>
        /// Remove HTML tags from string using char array.
        /// </summary>
        private static string StripTagsCharArray(string source)
        {
            var array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            foreach (char let in source)
            {
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }
    }

    /// <summary>
    /// A Parameter of the Url-string of a REST-service.
    /// </summary>
    public struct Parameter
    {
        /// <summary>
        /// The name of the <see cref="Parameter"/>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The value of the <see cref="Parameter"/>
        /// </summary>
        public string Value { get; set; }
    }
}