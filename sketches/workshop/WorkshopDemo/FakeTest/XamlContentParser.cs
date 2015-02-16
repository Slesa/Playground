using System;

namespace FakeTest
{
    public class XamlContentParser
    {
        readonly IFetchUriContent _contentFetcher;

        public XamlContentParser(IFetchUriContent contentFetcher)
        {
            _contentFetcher = contentFetcher;
        }

        public XamlResourceFile ParseUri(Uri fileToRead)
        {
            var source = _contentFetcher.FetchAsString(fileToRead);
            return new XamlResourceFile();
        }
    }
}