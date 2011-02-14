using System;
using Godot.Model;
using NHibernate;

namespace Godot.PmsModel.Queries
{
    public class DropCostcentersQuery : IDomainQuery<object>
    {
        public object Execute(ISession session)
        {
            var q = session.CreateQuery("delete from Costcenter");
            q.ExecuteUpdate();
            return null;
        }
    }
}