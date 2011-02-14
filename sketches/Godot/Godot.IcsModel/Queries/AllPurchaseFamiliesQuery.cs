using System.Collections.Generic;
using Godot.IcsModel.Entities;
using Godot.Model;
using NHibernate;
using NHibernate.Linq;

namespace Godot.IcsModel.Queries
{
    public class AllPurchaseFamiliesQuery : IDomainQuery<IEnumerable<PurchaseFamily>>
    {
        public IEnumerable<PurchaseFamily> Execute(ISession session)
        {
            return session.Linq<PurchaseFamily>();
        }
    }
}