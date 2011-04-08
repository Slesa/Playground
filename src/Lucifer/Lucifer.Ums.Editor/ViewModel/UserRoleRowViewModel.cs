using Lucifer.Editor;
using Lucifer.Ums.Model.Entities;

namespace Lucifer.Ums.Editor.ViewModel
{
    public class UserRoleRowViewModel : SelectableRowViewModelBase
    {
        readonly UserRole _userRole;

        public UserRoleRowViewModel(UserRole userRole)
        {
            _userRole = userRole;
        }

        public int Id { get { return _userRole.Id; } }
        public string Name { get { return _userRole.Name; } }
        
    }
}