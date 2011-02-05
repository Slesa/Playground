using System.Collections.Generic;
using Godot.IcsModel.Entities;
using Godot.Model;
using NHibernate;
using NHibernate.Linq;

namespace Godot.IcsModel.Queries
{
    public class AllRecipeableItemsQuery : IDomainQuery<IEnumerable<RecipeableItem>>
    {
        public IEnumerable<RecipeableItem> Execute(ISession session)
        {
            return session.Linq<RecipeableItem>();
        }
    }
}