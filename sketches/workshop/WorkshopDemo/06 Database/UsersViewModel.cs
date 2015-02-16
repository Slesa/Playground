using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Database.Models;
using Database.Queries;

namespace Database
{
    public class UsersViewModel
    {
        readonly IDbConversation _dbConversation;

        public UsersViewModel(IDbConversation dbConversation)
        {
            _dbConversation = dbConversation;

            CreateDatasets();
        }

        public ObservableCollection<UserViewModel> Users { get; private set; }

        void CreateDatasets()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                _dbConversation.UsingTransaction(() =>
                {
                    Users = new ObservableCollection<UserViewModel>(_dbConversation.Query(new AllUsersQuery()).Select(x => new UserViewModel(x)));
                });
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }
    }

    public class UserViewModel
    {
        public UserViewModel(User user)
        {
        }
    }

}
