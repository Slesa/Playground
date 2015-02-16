using NHibernate;

namespace Database
{
    public interface IDomainQuery<out TResult>
    {
        TResult Execute(ISession session);         
    }
}