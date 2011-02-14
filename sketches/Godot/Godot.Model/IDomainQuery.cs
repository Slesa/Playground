using NHibernate;

namespace Godot.Model
{
	public interface IDomainQuery<TResult>
	{
		TResult Execute(ISession session);
	}
}