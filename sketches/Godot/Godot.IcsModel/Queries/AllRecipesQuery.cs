using System.Collections.Generic;
using Godot.IcsModel.Entities;
using Godot.Model;
using NHibernate;
using NHibernate.Linq;

namespace Godot.IcsModel.Queries
{
    public class AllRecipesQuery : IDomainQuery<IEnumerable<Recipe>>
    {
        public IEnumerable<Recipe> Execute(ISession session)
        {
            return session.Linq<Recipe>();
        }
    }
}