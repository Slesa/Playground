using System.ComponentModel;
using Lucifer.Ums.Editor.Model;
using Lucifer.Ums.Editor.Resources;
using Lucifer.Ums.Model.Entities;
using Machine.Fakes;
using Machine.Specifications;

namespace Lucifer.Ums.Editor.Specs
{
    [Subject(typeof(UserModel))]
    public class When_creating_invalid_user : WithSubject<UserModel>
    {
        Because of = () =>
        {
            var errorInfo = Subject as IDataErrorInfo;
            _nameError = errorInfo["Name"];
            _roleError = errorInfo["UserRole"];
        };

        It should_not_be_valid = () => Subject.Error.ShouldNotBeNull();
        It should_provide_name_error = () => _nameError.ShouldEqual(Strings.UserModel_Name_missing);
        It should_provide_user_role_error = () => _roleError.ShouldEqual(Strings.UserModel_UserRole_missing);

        static string _nameError;
        static string _roleError;
    }

    [Subject(typeof(UserModel))]
    public class When_creating_valid_user : WithSubject<UserModel>
    {
        Establish context = () =>
        {
            Subject.Name = "User";
            Subject.UserRole = new UserRole();
        };

        Because of = () =>
        {
            _error = Subject.Error;
        };

        It should_be_valid = () => _error.ShouldBeNull();

        static string _error;
    }}