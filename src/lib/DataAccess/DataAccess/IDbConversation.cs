using System;

namespace DataAccess
{
	public interface IDbConversation : IDisposable
	{
		TResult Query<TResult>(IDomainQuery<TResult> query);
		void InsertObjectOnCommit(object instance);
		TResult GetById<TResult>(object key);
	    void UsingTransaction(Action action);
	    void Delete(object instance);
	}
}