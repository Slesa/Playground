using FluentNHibernate.Mapping;
using Godot.IcsModel.Entities;

namespace Godot.IcsNHibernate.Mappings
{
    public class StockMoveDepositMap : SubclassMap<StockMoveDeposit>
    {
        public StockMoveDepositMap()
        {
            Map(d => d.Reason).Length(50);
        }
    }
}