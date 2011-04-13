using System;

namespace Lucifer.DataAccess
{
    public interface IDbConversation : IDisposable
    {
        TResult Query<TResult>(IDomainQuery<TResult> query);
        void UsingTransaction(Action action);

        TResult GetById<TResult>(object key);
        void InsertOnCommit(object instance);
        void DeleteOnCommit(object instance);
    }
}