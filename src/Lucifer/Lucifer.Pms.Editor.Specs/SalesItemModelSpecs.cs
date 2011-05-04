using System.ComponentModel;
using Lucifer.Pms.Editor.Model;
using Lucifer.Pms.Editor.Resources;
using Lucifer.Pms.Model.Entities;
using Machine.Fakes;
using Machine.Specifications;

namespace Lucifer.Pms.Editor.Specs
{
    [Subject(typeof(SalesItemModel))]
    public class When_creating_invalid_sales_item : WithSubject<SalesItemModel>
    {
        Because of = () =>
            {
                var errorInfo = Subject as IDataErrorInfo;
                _nameError = errorInfo["Name"];
                _familyError = errorInfo["SalesFamily"];
            };

        It should_not_be_valid = () => Subject.Error.ShouldNotBeNull();
        It should_provide_name_error = () => _nameError.ShouldEqual(Strings.SalesItem_Name_missing);
        It should_provide_family_error = () => _familyError.ShouldEqual(Strings.SalesItem_SalesFamily_missing);

        static string _nameError;
        static string _familyError;
    }

    [Subject(typeof(SalesItemModel))]
    public class When_creating_valid_sales_item : WithSubject<SalesItemModel>
    {
        Establish context = () =>
            {
                Subject.Name = "Sales item";
                Subject.SalesFamily = new SalesFamily();
            };

        Because of = () =>
            {
                _error = Subject.Error;
            };

        It should_be_valid = () => _error.ShouldBeNull();

        static string _error;
    }
}