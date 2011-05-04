using System.ComponentModel;
using Lucifer.Ics.Editor.Model;
using Lucifer.Ics.Editor.Resources;
using Machine.Fakes;
using Machine.Specifications;

namespace Lucifer.Ics.Editor.Specs
{
    [Subject(typeof(PurchaseFamilyModel))]
    public class When_creating_invalid_purchase_family : WithSubject<PurchaseFamilyModel>
    {
        Because of = () =>
            {
                var errorInfo = Subject as IDataErrorInfo;
                _nameError = errorInfo["Name"];
            };

        It should_be_invalid = () => Subject.Error.ShouldNotBeNull();
        It should_provide_name_error = () => _nameError.ShouldEqual(Strings.PurchaseFamilyModel_Name_missing);

        static string _nameError;
    }

    [Subject(typeof(PurchaseFamilyModel))]
    public class When_creating_valid_purchase_family : WithSubject<PurchaseFamilyModel>
    {
        Establish context = () =>
            {
                Subject.Name = "Purchase Family";
            };

        Because of = () =>
            {
                _error = Subject.Error;
            };

        It should_be_valid = () => _error.ShouldBeNull();
        static string _error;
    }
}