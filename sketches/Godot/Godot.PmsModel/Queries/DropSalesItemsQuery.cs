using Godot.Model;
using NHibernate;

namespace Godot.PmsModel.Queries
{
    public class DropSalesItemsQuery : IDomainQuery<object>
    {
        public object Execute(ISession session)
        {
            var q = session.CreateQuery("delete from SalesItem");
            q.ExecuteUpdate();
            return null;
        }
    }
}