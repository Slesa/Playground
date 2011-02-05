using System.ComponentModel;
using Godot.IcsEditor.Ui.Localization;
using Machine.Specifications;

namespace Godot.IcsEditor.Ui.Model
{
    [Subject(typeof(EditRecipe))]
    public class When_creating_invalid_recipe
    {
        static EditRecipe _recipe;
        static string _pluError;

        Establish context = () => { _recipe = EditRecipe.CreateRecipe(); };

        Because of = () =>
        {
            var errorInfo = _recipe as IDataErrorInfo;
            _pluError = errorInfo["Plu"];
        };

        It should_not_be_valid = () => _recipe.IsValid.ShouldBeFalse();

        It should_provide_plu_error =
            () => _pluError.ShouldEqual(Strings.Model_Recipe_SalesItem_is_missing);
    }

    [Subject(typeof(EditRecipe))]
    public class When_creating_valid_recipe
    {
        static EditRecipe _recipe;
        static string _pluError;

        Establish context = () =>
        {
            _recipe = EditRecipe.CreateRecipe();
            _recipe.Plu = 42;
        };

        Because of = () =>
        {
            var errorInfo = _recipe as IDataErrorInfo;
            _pluError = errorInfo["Plu"];
        };

        It should_be_valid = () => _recipe.IsValid.ShouldBeTrue();
        It should_not_provide_plu_error = () => _pluError.ShouldBeEmpty();

    }
}