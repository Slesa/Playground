using System;

namespace Database
{
    public interface IDbConversation : IDisposable
    {
        TResult Query<TResult>(IDomainQuery<TResult> query);
        void UsingTransaction(Action action);

        TResult GetById<TResult>(object key);
        void Insert(object instance);
        void Remove(object instance);
    }
}