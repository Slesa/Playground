using System.ComponentModel;
using Godot.IcsEditor.Ui.Localization;
using Machine.Specifications;

namespace Godot.IcsEditor.Ui.Model
{
    [Subject(typeof(EditPurchaseFamily))]
    public class When_creating_invalid_purchasefamily
    {
        Establish context = () => { _purchaseFamily = EditUnitType.CreateUnitType(); };

        Because of = () => { _nameError = (_purchaseFamily as IDataErrorInfo)["Name"]; };

        It should_be_invalid = () => _purchaseFamily.IsValid.ShouldBeFalse();
        It should_provide_name_error = () => _nameError.ShouldEqual(Strings.Model_EditUnitType_Name_is_missing);

        static EditUnitType _purchaseFamily;
        static string _nameError;
    }

    [Subject(typeof(EditPurchaseFamily))]
    public class When_creating_valid_purchasefamily
    {
        Establish context = () =>
            {
                _purchaseFamily = EditUnitType.CreateUnitType();
                _purchaseFamily.Name = "Purchase Family";
            };

        Because of = () => { _nameError = (_purchaseFamily as IDataErrorInfo)["Name"]; };

        It should_be_valid = () => _purchaseFamily.IsValid.ShouldBeTrue();
        It should_not_provide_name_error = () => _nameError.ShouldBeEmpty();

        static EditUnitType _purchaseFamily;
        static string _nameError;
    }

}