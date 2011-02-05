using Machine.Specifications;

namespace MatrixAccess
{
    [Subject(typeof(DatFileNameMapper))]
    public class When_mapping_type_names
    {
        class SalesItem {}
        class Costcenter {}
        class Country {}
        class MyDongleInfo {}
        class AnyItem {}

        Establish context = () =>
            {
                _datFileNameMapper = new DatFileNameMapper();
            };

        Because of = () =>
            {
                _salesItems = _datFileNameMapper.GetFileNameFor<SalesItem>();
                _costCenters = _datFileNameMapper.GetFileNameFor<Costcenter>();
                _countries = _datFileNameMapper.GetFileNameFor<Country>();
                _dongleInfos = _datFileNameMapper.GetFileNameFor<MyDongleInfo>();
                _anyItem = _datFileNameMapper.GetFileNameFor<AnyItem>();
            };

        It should_redefine_sales_items = () =>_salesItems.ShouldEqual("articles.dat");
        It should_redefine_cost_centers = () => _costCenters.ShouldEqual("centers.dat");
        It should_redefine_countries = () => _countries.ShouldEqual("countries.dat");
        It should_redefine_dongle_infos = () => _dongleInfos.ShouldEqual("dongles.dat");
        It should_generate_file_names = () => _anyItem.ShouldEqual("anyitems.dat");

        static DatFileNameMapper _datFileNameMapper;
        static string _salesItems;
        static string _costCenters;
        static string _countries;
        static string _dongleInfos;
        static string _anyItem;
    }
}