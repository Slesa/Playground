using System.ComponentModel;
using Lucifer.Pms.Editor.Model;
using Lucifer.Pms.Editor.Resources;
using Machine.Fakes;
using Machine.Specifications;

namespace Lucifer.Pms.Editor.Specs
{
    [Subject(typeof(CurrencyModel))]
    public class When_creating_invalid_currency : WithSubject<CurrencyModel>
    {
        Because of = () =>
            {
                var errorInfo = Subject as IDataErrorInfo;
                _nameError = errorInfo["Name"];
                _symbolError = errorInfo["Symbol"];
            };

        It should_not_be_valid = () => Subject.Error.ShouldNotBeNull();
        It should_provide_name_error = () => _nameError.ShouldEqual(Strings.CurrencyModel_Name_missing);
        It should_provide_symbol_error = () => _symbolError.ShouldEqual(Strings.CurrencyModel_Symbol_missing);

        static string _nameError;
        static string _symbolError;
    }

    [Subject(typeof(CurrencyModel))]
    public class When_creating_valid_currency : WithSubject<CurrencyModel>
    {
        Establish context = () =>
            {
                Subject.Name = "Currency";
                Subject.Symbol = "Symbol";
            };

        Because of = () =>
            {
                _error = Subject.Error;
            };

        It should_be_valid = () => _error.ShouldBeNull();

        static string _error;
    }
}