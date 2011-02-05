using System.Collections.Generic;
using Godot.Model;
using Godot.PmsModel.Entities;
using NHibernate;
using NHibernate.Linq;

namespace Godot.PmsModel.Queries
{
    public class AllCostcentersQuery : IDomainQuery<IEnumerable<Costcenter>>
    {
        public IEnumerable<Costcenter> Execute(ISession session)
        {
            return session.Linq<Costcenter>();
        }
    }
}