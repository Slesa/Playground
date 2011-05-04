using System.ComponentModel;
using Lucifer.Ics.Editor.Model;
using Lucifer.Ics.Editor.Resources;
using Lucifer.Ics.Model.Entities;
using Machine.Fakes;
using Machine.Specifications;

namespace Lucifer.Ics.Editor.Specs
{
    [Subject(typeof(ProductionItemModel))]
    public class When_creating_invalid_production_item : WithSubject<ProductionItemModel>
    {
        Because of = () =>
            {
                var errorInfo = Subject as IDataErrorInfo;
                _nameError = errorInfo["Name"];
                _recipeUnitError = errorInfo["RecipeUnit"];
            };

        It should_not_be_valid = () => Subject.Error.ShouldNotBeNull();
        It should_provide_name_error = () => _nameError.ShouldEqual(Strings.ProductionItemModel_Name_missing);
        It should_provide_recipeunit_error = () => _recipeUnitError.ShouldEqual(Strings.ProductionItemModel_RecipeUnit_is_missing);

        static string _nameError;
        static string _recipeUnitError;
    }

    [Subject(typeof(ProductionItemModel))]
    public class When_creating_valid_production_item : WithSubject<ProductionItemModel>
    {
        Establish context = () =>
            {
                Subject.Name = "Production Item";
                Subject.RecipeUnit = new Unit();
            };

        Because of = () =>
            {
                _error = Subject.Error;
            };

        It should_be_valid = () => _error.ShouldBeNull();
        
        static string _error;
    }}
