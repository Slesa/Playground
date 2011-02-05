using System.ComponentModel;
using Godot.IcsEditor.Ui.Localization;
using Machine.Specifications;

namespace Godot.IcsEditor.Ui.Model
{
    [Subject(typeof(EditUnitType))]
    public class When_creating_invalid_unit_type
    {
        Establish context = () => { _unitType = EditUnitType.CreateUnitType(); };

        Because of = () => { _nameError = (_unitType as IDataErrorInfo)["Name"]; };

        It should_be_invalid = () => _unitType.IsValid.ShouldBeFalse();
        It should_provide_name_error = () => _nameError.ShouldEqual(Strings.Model_EditUnitType_Name_is_missing);

        static EditUnitType _unitType;
        static string _nameError;
    }

    [Subject(typeof(EditUnitType))]
    public class When_creating_valid_unit_type
    {
        Establish context = () =>
            {
                _unitType = EditUnitType.CreateUnitType();
                _unitType.Name = "Unit Type";
            };

        Because of = () => { _nameError = (_unitType as IDataErrorInfo)["Name"]; };

        It should_be_valid = () => _unitType.IsValid.ShouldBeTrue();
        It should_not_provide_name_error = () => _nameError.ShouldBeEmpty();

        static EditUnitType _unitType;
        static string _nameError;
    }
}