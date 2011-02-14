using System.Collections.Generic;
using Godot.IcsModel.Entities;
using Godot.Model;
using NHibernate;
using NHibernate.Linq;

namespace Godot.IcsModel.Queries
{
    public class AllStocksQuery : IDomainQuery<IEnumerable<Stock>>
    {
        public IEnumerable<Stock> Execute(ISession session)
        {
            return session.Linq<Stock>();
        }
    }
}