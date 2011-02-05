using System.ComponentModel;
using Godot.IcsEditor.Ui.Localization;
using Godot.IcsModel.Entities;
using Machine.Specifications;

namespace Godot.IcsEditor.Ui.Model
{
    [Subject(typeof(EditProductionItem))]
    public class When_creating_invalid_productionitem
    {
        Establish context = () => { _productionItem = EditProductionItem.CreateProductionItem(); };

        Because of = () =>
        {
            var errorInfo = _productionItem as IDataErrorInfo;
            _nameError = errorInfo["Name"];
            _recipeUnitError = errorInfo["RecipeUnit"];
        };

        It should_not_be_valid = () => _productionItem.IsValid.ShouldBeFalse();
        It should_provide_name_error = () => _nameError.ShouldEqual(Strings.Model_EditProductionItem_Name_is_missing);
        It should_provide_recipeunit_error = () => _recipeUnitError.ShouldEqual(Strings.Model_EditProductionItem_RecipeUnit_is_missing);

        static EditProductionItem _productionItem;
        static string _nameError;
        static string _recipeUnitError;
    }

    [Subject(typeof(EditUnit))]
    public class When_creating_valid_productionitem
    {
        static EditProductionItem _productionItem;
        static string _nameError;
        static string _recipeUnitError;

        Establish context = () =>
        {
            _productionItem = EditProductionItem.CreateProductionItem();
            _productionItem.Name = "PurchaseItem";
            _productionItem.RecipeUnit = new Unit();
        };

        Because of = () =>
        {
            var errorInfo = _productionItem as IDataErrorInfo;
            _nameError = errorInfo["Name"];
            _recipeUnitError = errorInfo["RecipeUnit"];
        };

        It should_be_valid = () => _productionItem.IsValid.ShouldBeTrue();
        It should_not_provide_name_error = () => _nameError.ShouldBeEmpty();
        It should_not_provide_recipeunit_error = () => _recipeUnitError.ShouldBeEmpty();

    }


}