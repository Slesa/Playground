using Lucifer.Editor;
using Lucifer.Ums.Model.Entities;

namespace Lucifer.Ums.Editor.ViewModel
{
    public class UserRowViewModel: SelectableRowViewModelBase
    {
        readonly User _user;

        public UserRowViewModel(User user)
        {
            _user = user;
        }

        public int Id { get { return _user.Id; } }
        public string Name { get { return _user.Name; } }
    }
}