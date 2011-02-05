using System.ComponentModel;
using Godot.IcsEditor.Ui.Localization;
using Godot.IcsModel.Entities;
using Machine.Specifications;

namespace Godot.IcsEditor.Ui.Model
{
    [Subject(typeof(EditStockItem))]
    public class When_creating_invalid_stockitem
    {
        static EditStockItem _stockItem;
        static string _unitError;
        static string _recipeableItemError;

        Establish context = () => { _stockItem = EditStockItem.CreateStockItem(); };

        Because of = () =>
        {
            var errorInfo = _stockItem as IDataErrorInfo;
            _unitError = errorInfo["Unit"];
            _recipeableItemError = errorInfo["RecipeableItem"];
        };

        It should_not_be_valid = () => _stockItem.IsValid.ShouldBeFalse();

        It should_provide_unit_error =
            () => _unitError.ShouldEqual(Strings.Model_EditStock_Unit_is_missing);
        It should_provide_recipeableitem_error =
            () => _recipeableItemError.ShouldEqual(Strings.Model_EditStock_StockItem_is_missing);
    }

    [Subject(typeof(EditStockItem))]
    public class When_creating_valid_stockitem
    {
        static EditStockItem _stockItem;
        static string _unitError;
        static string _recipeableItemError;

        Establish context = () =>
        {
            _stockItem = EditStockItem.CreateStockItem();
            _stockItem.Unit = new Unit();
            _stockItem.RecipeableItem = new RecipeableItem();
        };

        Because of = () =>
        {
            var errorInfo = _stockItem as IDataErrorInfo;
            _unitError = errorInfo["Unit"];
            _recipeableItemError = errorInfo["RecipeableItem"];
        };

        It should_be_valid = () => _stockItem.IsValid.ShouldBeTrue();
        It should_not_provide_unit_error = () => _unitError.ShouldBeEmpty();
        It should_not_provide_recipeableitem_error = () => _recipeableItemError.ShouldBeEmpty();

    }
}