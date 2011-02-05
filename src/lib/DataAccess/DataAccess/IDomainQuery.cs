using NHibernate;

namespace DataAccess
{
	public interface IDomainQuery<out TResult>
	{
		TResult Execute(ISession session);
	}
}