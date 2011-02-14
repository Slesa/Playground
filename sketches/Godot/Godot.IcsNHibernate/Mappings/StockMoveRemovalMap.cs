using FluentNHibernate.Mapping;
using Godot.IcsModel.Entities;

namespace Godot.IcsNHibernate.Mappings
{
    public class StockMoveRemovalMap : SubclassMap<StockMoveRemoval>
    {
        public StockMoveRemovalMap()
        {
            Map(d => d.Reason).Length(50);
        }
    }
}