using System;
using Machine.Fakes;
using Machine.Specifications;

namespace FakeTest
{
    [Subject(typeof (XamlContentParser))]
    internal class When_parsing_a_xaml_file_with_empty_dictionary : XamlContentParserSpecBase
    {
        Establish context = () =>
            {
                var contentFetcher = An<IFetchUriContent>();
                contentFetcher.WhenToldTo(x => x.FetchAsString(Param<Uri>.IsNotNull)).Return(
                    new XamlDefinitions()
                        .AddNamespace("xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'")
                        .AddNamespace("xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'")
                        .AddNamespace(
                            "xmlns:extern='clr-namespace:Centigrade.XamlCollector.Specs.Resources.Converters;assembly=TestOnly.Collector.Xaml.Specs.Resources'")
                        .AddNamespace("xmlns:local='clr-namespace:Centigrade.XamlCollector.Specs.Resources.Converters'")
                        .XamlCode);
                Subject = new XamlContentParser(contentFetcher);
            };

        Because of = () => _resourceFile = Subject.ParseUri(MemoryFileUri);

        It should_return_a_resource_file = () => _resourceFile.ShouldNotBeNull();

        It should_have_no_parent = () => _resourceFile.Parent.ShouldBeNull();
        It should_have_no_children = () => _resourceFile.Children.ShouldBeEmpty();

        It should_set_source_uri = () => _resourceFile.SourceUri.ShouldEqual(MemoryFileUri);

        static XamlResourceFile _resourceFile;
    }



    [Subject(typeof (XamlContentParser))]
    internal class XamlContentParserSpecBase : WithFakes
    {
        Establish context = () =>
            {
                // log4net.configure...
            };

        protected static Uri MemoryFileUri = new Uri("InMemory", UriKind.Relative);
        protected static XamlContentParser Subject;
    }
}