using Lucifer.Editor;
using Lucifer.Ums.Model.Entities;

namespace Lucifer.Ums.Editor.ViewModel
{
    public class UserRoleRowViewModel : SelectableRowViewModelBase<UserRole>
    {
        public UserRoleRowViewModel(UserRole userRole)
        {
            ElementData = userRole;
        }

        public int Id { get { return ElementData.Id; } }
        public string Name { get { return ElementData.Name; } }
        
    }
}