using System.ComponentModel;
using Lucifer.Ics.Editor.Model;
using Lucifer.Ics.Editor.Resources;
using Machine.Fakes;
using Machine.Specifications;

namespace Lucifer.Ics.Editor.Specs
{
    [Subject(typeof(UnitTypeModel))]
    public class When_creating_invalid_unit_type : WithSubject<UnitTypeModel>
    {
        Because of = () =>
        {
            var errorInfo = Subject as IDataErrorInfo;
            _nameError = errorInfo["Name"];
        };

        It should_not_be_valid = () => Subject.Error.ShouldNotBeNull();
        It should_provide_name_error = () => _nameError.ShouldEqual(Strings.UnitTypeModel_Name_missing);

        static string _nameError;
    }

    [Subject(typeof(UnitTypeModel))]
    public class When_creating_valid_unit_type : WithSubject<UnitTypeModel>
    {
        Establish context = () =>
        {
            Subject.Name = "Unit Type";
        };

        Because of = () =>
        {
            _error = Subject.Error;
        };

        It should_be_valid = () => _error.ShouldBeNull();

        static string _error;
    }
}