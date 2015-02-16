using System;

namespace FakeTest
{
    public interface IFetchUriContent
    {
        string FetchAsString(Uri location);
    }
}