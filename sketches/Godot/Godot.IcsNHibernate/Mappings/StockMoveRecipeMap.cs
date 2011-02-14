using FluentNHibernate.Mapping;
using Godot.IcsModel.Entities;

namespace Godot.IcsNHibernate.Mappings
{
    public class StockMoveRecipeMap : SubclassMap<StockMoveRecipe>
    {
        public StockMoveRecipeMap()
        {
            References(d => d.Recipe);
        }
    }
}