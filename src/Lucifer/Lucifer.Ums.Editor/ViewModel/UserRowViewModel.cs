using Lucifer.Editor;
using Lucifer.Ums.Model.Entities;

namespace Lucifer.Ums.Editor.ViewModel
{
    public class UserRowViewModel: SelectableRowViewModelBase<User>
    {
        public UserRowViewModel(User user)
        {
            ElementData = user;
        }
        public void ExchangeData(User user)
        {
            ElementData = user;
        }

        public int Id { get { return ElementData.Id; } }
        public string Name { get { return ElementData.Name; } }
    }
}