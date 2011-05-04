using System.ComponentModel;
using Lucifer.Pms.Editor.Model;
using Lucifer.Pms.Editor.Resources;
using Machine.Fakes;
using Machine.Specifications;

namespace Lucifer.Pms.Editor.Specs
{
    [Subject(typeof(SalesFamilyModel))]
    public class When_creating_invalid_sales_family : WithSubject<SalesFamilyModel>
    {
        Because of = () =>
        {
            var errorInfo = Subject as IDataErrorInfo;
            _nameError = errorInfo["Name"];
        };

        It should_not_be_valid = () => Subject.Error.ShouldNotBeNull();
        It should_provide_name_error = () => _nameError.ShouldEqual(Strings.SalesFamilyModel_Name_missing);

        static string _nameError;
    }

    [Subject(typeof(SalesFamilyModel))]
    public class When_creating_valid_sales_family : WithSubject<SalesFamilyModel>
    {
        Establish context = () =>
        {
            Subject.Name = "Sales family";
        };

        Because of = () =>
        {
            _error = Subject.Error;
        };

        It should_be_valid = () => _error.ShouldBeNull();

        static string _error;
    }
}