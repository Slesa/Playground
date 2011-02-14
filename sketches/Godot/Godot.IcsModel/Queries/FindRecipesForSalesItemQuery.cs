using System.Collections.Generic;
using System.Linq;
using Godot.IcsModel.Entities;
using Godot.Model;
using NHibernate;
using NHibernate.Linq;

namespace Godot.IcsModel.Queries
{
    public class FindRecipesForSalesItemQuery : IDomainQuery<IEnumerable<Recipe>>
    {
        readonly int _plu;

        public FindRecipesForSalesItemQuery(int plu)
        {
            _plu = plu;
        }

        public IEnumerable<Recipe> Execute(ISession session)
        {
            return session.Linq<Recipe>().Where(x => x.Plu==_plu);
        }
    }
}