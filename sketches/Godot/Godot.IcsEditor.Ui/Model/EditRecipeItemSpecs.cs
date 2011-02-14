using System.ComponentModel;
using Godot.IcsEditor.Ui.Localization;
using Godot.IcsModel.Entities;
using Machine.Specifications;

namespace Godot.IcsEditor.Ui.Model
{
    [Subject(typeof(EditRecipeItem))]
    public class When_creating_invalid_recipeitem
    {
        static EditRecipeItem _recipeItem;
        static string _unitError;
        static string _recipeableItemError;

        Establish context = () => { _recipeItem = EditRecipeItem.CreateRecipeItem(); };

        Because of = () =>
        {
            var errorInfo = _recipeItem as IDataErrorInfo;
            _unitError = errorInfo["Unit"];
            _recipeableItemError = errorInfo["RecipeableItem"];
        };

        It should_not_be_valid = () => _recipeItem.IsValid.ShouldBeFalse();

        It should_provide_unit_error =
            () => _unitError.ShouldEqual(Strings.Model_EditStock_Name_is_missing);
        It should_provide_recipeableitem_error =
            () => _recipeableItemError.ShouldEqual(Strings.Model_EditStock_Name_is_missing);
    }

    [Subject(typeof(EditRecipeItem))]
    public class When_creating_valid_recipeitem
    {
        static EditRecipeItem _recipeItem;
        static string _unitError;
        static string _recipeableItemError;

        Establish context = () =>
        {
            _recipeItem = EditRecipeItem.CreateRecipeItem();
            _recipeItem.Unit = new Unit();
            _recipeItem.RecipeableItem = new RecipeableItem();
        };

        Because of = () =>
        {
            var errorInfo = _recipeItem as IDataErrorInfo;
            _unitError = errorInfo["Unit"];
            _recipeableItemError = errorInfo["RecipeableItem"];
        };

        It should_be_valid = () => _recipeItem.IsValid.ShouldBeTrue();
        It should_not_provide_unit_error = () => _unitError.ShouldBeEmpty();
        It should_not_provide_recipeableitem_error = () => _recipeableItemError.ShouldBeEmpty();

    }
}