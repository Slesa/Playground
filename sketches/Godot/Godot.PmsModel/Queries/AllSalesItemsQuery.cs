using System.Collections.Generic;
using Godot.Model;
using Godot.PmsModel.Entities;
using NHibernate;
using NHibernate.Linq;

namespace Godot.PmsModel.Queries
{
    public class AllSalesItemsQuery : IDomainQuery<IEnumerable<SalesItem>>
    {
        public IEnumerable<SalesItem> Execute(ISession session)
        {
            return session.Linq<SalesItem>();
        }
    }
}