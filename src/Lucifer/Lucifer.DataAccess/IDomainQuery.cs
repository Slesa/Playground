using NHibernate;

namespace Lucifer.DataAccess
{
    public interface IDomainQuery<TResult>
    {
        TResult Execute(ISession session);
    }
}