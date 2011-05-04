using System.ComponentModel;
using Lucifer.Ics.Editor.Model;
using Lucifer.Ics.Editor.Resources;
using Lucifer.Ics.Model.Entities;
using Machine.Fakes;
using Machine.Specifications;

namespace Lucifer.Ics.Editor.Specs
{
    [Subject(typeof(PurchaseItemModel))]
    public class When_creating_invalid_purchase_item : WithSubject<PurchaseItemModel>
    {
        Because of = () =>
            {
                var errorInfo = Subject as IDataErrorInfo;
                _nameError = errorInfo["Name"];
                _recipeUnitError = errorInfo["RecipeUnit"];
                _purchaseUnitError = errorInfo["PurchaseUnit"];
                _purchaseFamilyError = errorInfo["PurchaseFamily"];
            };

        It should_not_be_valid = () => Subject.Error.ShouldNotBeNull();
        It should_provide_name_error = () => _nameError.ShouldEqual(Strings.PurchaseItemModel_Name_missing);
        It should_provide_recipeunit_error = () => _recipeUnitError.ShouldEqual(Strings.PurchaseItemModel_RecipeUnit_is_missing);
        It should_provide_purchaseunit_error = () => _purchaseUnitError.ShouldEqual(Strings.PurchaseItemModel_PurchaseUnit_is_missing);
        It should_provide_purchasefamily_error = () => _purchaseFamilyError.ShouldEqual(Strings.PurchaseItemModel_PurchaseFamily_is_missing);

        static string _nameError;
        static string _recipeUnitError;
        static string _purchaseUnitError;
        static string _purchaseFamilyError;
    }

    [Subject(typeof(PurchaseItemModel))]
    public class When_creating_valid_purchase_item : WithSubject<PurchaseItemModel>
    {
        Establish context = () =>
            {
                Subject.Name = "PurchaseItem";
                Subject.RecipeUnit = new Unit();
                Subject.PurchaseUnit = new Unit();
                Subject.PurchaseFamily = new PurchaseFamily();
            };

        Because of = () =>
            {
                _error = Subject.Error;
            };

        It should_be_valid = () => _error.ShouldBeNull();

        static string _error;
    }
}