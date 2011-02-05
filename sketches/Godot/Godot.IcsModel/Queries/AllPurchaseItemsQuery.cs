using System.Collections.Generic;
using Godot.IcsModel.Entities;
using Godot.Model;
using NHibernate;
using NHibernate.Linq;

namespace Godot.IcsModel.Queries
{
    public class AllPurchaseItemsQuery : IDomainQuery<IEnumerable<PurchaseItem>>
    {
        public IEnumerable<PurchaseItem> Execute(ISession session)
        {
            return session.Linq<PurchaseItem>();
        }
    }
}