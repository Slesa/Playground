using System.ComponentModel;
using Lucifer.Pms.Editor.Model;
using Lucifer.Pms.Editor.Resources;
using Machine.Fakes;
using Machine.Specifications;

namespace Lucifer.Pms.Editor.Specs
{
    [Subject(typeof(DiscountModel))]
    public class When_creating_invalid_discount : WithSubject<DiscountModel>
    {
        Because of = () =>
            {
                var errorInfo = Subject as IDataErrorInfo;
                _nameError = errorInfo["Name"];
                _rateError = errorInfo["Rate"];
            };

        It should_not_be_valid = () => Subject.Error.ShouldNotBeNull();
        It should_provide_name_error = () => _nameError.ShouldEqual(Strings.DiscountModel_Name_missing);
        It should_provide_rate_error = () => _rateError.ShouldEqual(Strings.DiscountModel_Rate_missing);

        static string _nameError;
        static string _rateError;
    }

    [Subject(typeof(DiscountModel))]
    public class When_creating_valid_discount : WithSubject<DiscountModel>
    {
        Establish context = () =>
            {
                Subject.Name = "Discount";
                Subject.Rate = 42m;
            };

        Because of = () =>
            {
                _error = Subject.Error;
            };

        It should_be_valid = () => _error.ShouldBeNull();

        static string _error;
    }
}