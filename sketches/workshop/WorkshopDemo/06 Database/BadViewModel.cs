using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Input;
using FluentNHibernate.Conventions;

namespace Database
{
    public class BadViewModel
    {
        public BadViewModel()
        {
            CreateDatasets();
        }

        public ObservableCollection<UserViewModel> Users { get; private set; }

        void CreateDatasets()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            using (var set = new DataSet("users"))
            {
                // Put code that adds stuff to DataSet here.
                // ... The DataSet will be cleaned up outside the block.
            }
            Mouse.OverrideCursor = null;
        }
    }

    #region Hidden

    public class DoNotUse
    {
        public DoNotUse()
        {
            var viewModel = new UsersViewModel(new DbConversation());
            if (viewModel.Users.IsEmpty()) {
            }
        }
    }

    public class DbConversation : IDbConversation
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public TResult Query<TResult>(IDomainQuery<TResult> query)
        {
            throw new NotImplementedException();
        }

        public void UsingTransaction(Action action)
        {
            throw new NotImplementedException();
        }

        public TResult GetById<TResult>(object key)
        {
            throw new NotImplementedException();
        }

        public void Insert(object instance)
        {
            throw new NotImplementedException();
        }

        public void Remove(object instance)
        {
            throw new NotImplementedException();
        }
    }

    #endregion 
}
