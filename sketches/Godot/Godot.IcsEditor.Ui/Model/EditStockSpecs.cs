using System.ComponentModel;
using Godot.IcsEditor.Ui.Localization;
using Machine.Specifications;

namespace Godot.IcsEditor.Ui.Model
{
    [Subject(typeof(EditStock))]
    public class When_creating_invalid_stock
    {
        static EditStock _stock;
        static string _nameError;

        Establish context = () => { _stock = EditStock.CreateStock(); };

        Because of = () =>
        {
            var errorInfo = _stock as IDataErrorInfo;
            _nameError = errorInfo["Name"];
        };

        It should_not_be_valid = () => _stock.IsValid.ShouldBeFalse();

        It should_provide_name_error =
            () => _nameError.ShouldEqual(Strings.Model_EditStock_Name_is_missing);
    }

    [Subject(typeof(EditStock))]
    public class When_creating_valid_stock
    {
        static EditStock _stock;
        static string _nameError;

        Establish context = () =>
        {
            _stock = EditStock.CreateStock();
            _stock.Name = "Stock";
        };

        Because of = () =>
        {
            var errorInfo = _stock as IDataErrorInfo;
            _nameError = errorInfo["Name"];
        };

        It should_be_valid = () => _stock.IsValid.ShouldBeTrue();
        It should_not_provide_name_error = () => _nameError.ShouldBeEmpty();

    }
}