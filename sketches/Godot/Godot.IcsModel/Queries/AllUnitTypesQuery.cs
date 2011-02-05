using System.Collections.Generic;
using Godot.IcsModel.Entities;
using Godot.Model;
using NHibernate;
using NHibernate.Linq;

namespace Godot.IcsModel.Queries
{
    public class AllUnitTypesQuery : IDomainQuery<IEnumerable<UnitType>>
    {
        public IEnumerable<UnitType> Execute(ISession session)
        {
            return session.Linq<UnitType>();
        }
    }
}