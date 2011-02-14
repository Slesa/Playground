using System.Collections.Generic;
using System.Linq;
using Godot.IcsModel.Entities;
using Godot.Model;
using NHibernate;
using NHibernate.Linq;

namespace Godot.IcsModel.Queries
{
    public class AllPurchaseUnitsQuery : IDomainQuery<IEnumerable<Unit>>
    {
        public IEnumerable<Unit> Execute(ISession session)
        {
            return session.Linq<Unit>().Where(x=>x.Purchasing);
        }
    }
}