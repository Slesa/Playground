using System.ComponentModel;
using Lucifer.Ics.Editor.Model;
using Lucifer.Ics.Editor.Resources;
using Machine.Fakes;
using Machine.Specifications;

namespace Lucifer.Ics.Editor.Specs
{
    [Subject(typeof(RecipeModel))]
    public class When_creating_invalid_recipe : WithSubject<RecipeModel>
    {
        Because of = () =>
            {
                var errorInfo = Subject as IDataErrorInfo;
                _pluError = errorInfo["Plu"];
            };

        It should_not_be_valid = () => Subject.Error.ShouldNotBeNull();
        It should_provide_plu_error = () => _pluError.ShouldEqual(Strings.RecipeModel_Plu_missing);

        static string _pluError;
    }

    [Subject(typeof(RecipeModel))]
    public class When_creating_valid_recipe : WithSubject<RecipeModel>
    {
        Establish context = () =>
            {
                Subject.Plu = 42;
            };

        Because of = () =>
            {
                _error = Subject.Error;
            };

        It should_be_valid = () => _error.ShouldBeNull();

        static string _error;
    }
}