using System.Collections.Generic;
using System.Linq;
using Godot.IcsModel.Entities;
using Godot.Model;
using NHibernate;
using NHibernate.Linq;

namespace Godot.IcsModel.Queries
{
    public class AllStockMovementsQuery : IDomainQuery<IEnumerable<StockMovement>>
    {
        public IEnumerable<StockMovement> Execute(ISession session)
        {
            return session.Linq<StockMovement>();
        }
    }

    /*
    public class StockMovementsOf : IDomainQuery<IEnumerable<StockMovement>>
    {
        readonly Stock _stock;

        public StockMovementsOf(Stock stock)
        {
            _stock = stock;
        }

        public IEnumerable<StockMovement> Execute(ISession session)
        {
            return session.Linq<StockMovement>().Where(x=>x.FromStock==_stock || x.ToStock==_stock);
        }
    }*/
}