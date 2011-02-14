using FluentNHibernate.Mapping;
using Godot.IcsModel.Entities;

namespace Godot.IcsNHibernate.Mappings
{
    public class StockMoveTransferMap : SubclassMap<StockMoveTransfer>
    {
        public StockMoveTransferMap()
        {
            References(d => d.FromStock);
        }
    }
}