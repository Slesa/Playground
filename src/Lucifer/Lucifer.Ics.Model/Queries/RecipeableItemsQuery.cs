using System.Collections.Generic;
using Lucifer.DataAccess;
using Lucifer.Ics.Model.Entities;
using NHibernate;
using NHibernate.Linq;

namespace Lucifer.Ics.Model.Queries
{
    public class AllRecipeableItemsQuery : IDomainQuery<IEnumerable<RecipeableItem>>
    {
        public IEnumerable<RecipeableItem> Execute(ISession session)
        {
            return session.Query<RecipeableItem>();
        }
    }
}