using System.Collections.Generic;
using Lucifer.DataAccess;

namespace Lucifer.Ics.Model.Entities
{
    public class Unit : DomainEntity
    {
        readonly IList<Unit> _children = new List<Unit>();

        public virtual string Name { get; set; }
        public virtual string Contraction { get; set; }
        public virtual Unit Parent { get; set; }
        public virtual UnitType UnitType { get; set; }
        public virtual decimal FactorToParent { get; set; }

        public virtual IEnumerable<Unit> Children
        {
            get { return _children; }
        }

        public virtual void AddChild(Unit unit)
        {
            _children.Add(unit);
        }

        public virtual void RemoveChild(Unit unit)
        {
            _children.Remove(unit);
        }
    }
}