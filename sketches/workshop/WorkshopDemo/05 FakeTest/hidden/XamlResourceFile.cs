using System;
using System.Collections.Generic;

namespace FakeTest
{
    public class XamlResourceFile
    {
        public XamlResourceFile Parent { get; set; }

        public List<XamlResourceFile> Children { get; set; }

        public Uri SourceUri { get; set; }
    }
}