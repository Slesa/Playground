using System.ComponentModel;
using Godot.IcsEditor.Ui.Localization;
using Godot.IcsModel.Entities;
using Machine.Specifications;

namespace Godot.IcsEditor.Ui.Model
{
    [Subject(typeof(EditPurchaseItem))]
    public class When_creating_invalid_purchaseitem
    {
        Establish context = () => { _purchaseItem = EditPurchaseItem.CreatePurchaseItem(); };

        Because of = () =>
        {
            var errorInfo = _purchaseItem as IDataErrorInfo;
            _nameError = errorInfo["Name"];
            _recipeUnitError = errorInfo["RecipeUnit"];
            _purchaseUnitError = errorInfo["PurchaseUnit"];
            _purchaseFamilyError = errorInfo["PurchaseFamily"];
        };

        It should_not_be_valid = () => _purchaseItem.IsValid.ShouldBeFalse();
        It should_provide_name_error = () => _nameError.ShouldEqual(Strings.Model_EditPurchaseItem_Name_is_missing);
        It should_provide_recipeunit_error = () => _recipeUnitError.ShouldEqual(Strings.Model_EditPurchaseItem_RecipeUnit_is_missing);
        It should_provide_purchaseunit_error = () => _purchaseUnitError.ShouldEqual(Strings.Model_EditPurchaseItem_PurchaseUnit_is_missing);
        It should_provide_purchasefamily_error = () => _purchaseFamilyError.ShouldEqual(Strings.Model_EditPurchaseItem_PurchaseFamily_is_missing);

        static EditPurchaseItem _purchaseItem;
        static string _nameError;
        static string _recipeUnitError;
        static string _purchaseUnitError;
        static string _purchaseFamilyError;
    }

    [Subject(typeof(EditPurchaseItem))]
    public class When_creating_valid_purchaseitem
    {
        static EditPurchaseItem _purchaseItem;
        static string _nameError;
        static string _recipeUnitError;
        static string _purchaseUnitError;
        static string _purchaseFamilyError;

        Establish context = () =>
        {
            _purchaseItem = EditPurchaseItem.CreatePurchaseItem();
            _purchaseItem.Name = "PurchaseItem";
            _purchaseItem.RecipeUnit = new Unit();
            _purchaseItem.PurchaseUnit = new Unit();
            _purchaseItem.PurchaseFamily = new PurchaseFamily();
        };

        Because of = () =>
        {
            var errorInfo = _purchaseItem as IDataErrorInfo;
            _nameError = errorInfo["Name"];
            _recipeUnitError = errorInfo["RecipeUnit"];
            _purchaseUnitError = errorInfo["PurchaseUnit"];
            _purchaseFamilyError = errorInfo["PurchaseFamily"];
        };

        It should_be_valid = () => _purchaseItem.IsValid.ShouldBeTrue();
        It should_not_provide_name_error = () => _nameError.ShouldBeEmpty();
        It should_not_provide_recipeunit_error = () => _recipeUnitError.ShouldBeEmpty();
        It should_not_provide_purchaseunit_error = () => _purchaseUnitError.ShouldBeEmpty();
        It should_not_provide_purchasefamily_error = () => _purchaseFamilyError.ShouldBeEmpty();

    }

}