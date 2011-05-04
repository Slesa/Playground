using System.ComponentModel;
using Lucifer.Ics.Editor.Model;
using Lucifer.Ics.Editor.Resources;
using Machine.Fakes;
using Machine.Specifications;

namespace Lucifer.Ics.Editor.Specs
{
    [Subject(typeof(StockModel))]
    public class When_creating_invalid_stock : WithSubject<StockModel>
    {
        Because of = () =>
            {
                var errorInfo = Subject as IDataErrorInfo;
                _nameError = errorInfo["Name"];
            };

        It should_not_be_valid = () => Subject.Error.ShouldNotBeNull();
        It should_provide_name_error = () => _nameError.ShouldEqual(Strings.StockModel_Name_missing);

        static string _nameError;
    }

    [Subject(typeof(StockModel))]
    public class When_creating_valid_stock : WithSubject<StockModel>
    {
        Establish context = () =>
            {
                Subject.Name = "Stock";
            };

        Because of = () =>
            {
                _error = Subject.Error;
            };

        It should_be_valid = () => _error.ShouldBeNull();

        static string _error;
    }
}