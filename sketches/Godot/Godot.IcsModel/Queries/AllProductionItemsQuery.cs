using System.Collections.Generic;
using Godot.IcsModel.Entities;
using Godot.Model;
using NHibernate;
using NHibernate.Linq;

namespace Godot.IcsModel.Queries
{
    public class AllProductionItemsQuery : IDomainQuery<IEnumerable<ProductionItem>>
    {
        public IEnumerable<ProductionItem> Execute(ISession session)
        {
            return session.Linq<ProductionItem>();
        }
    }
}