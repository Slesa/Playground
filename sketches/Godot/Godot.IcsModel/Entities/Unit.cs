using System;
using System.Collections.Generic;
using Godot.Model;

namespace Godot.IcsModel.Entities
{
    // Die Repräsentation einer Einheit
    // http://blogs.hibernatingrhinos.com/nhibernate/archive/2008/08/13/a-fluent-interface-to-nhibernate---part-2---value.aspx
    public class Unit : DomainEntity
    {
        readonly IList<Unit> _children = new List<Unit>();
        Unit _parent;

        public Unit()
        {
            Purchasing = Reciping = true;
        }
        public virtual string Name { get; set; }
        public virtual string Contraction { get; set; }
        public virtual Unit Parent { get; set; }
        public virtual UnitType UnitType { get; set; }
        public virtual Decimal FactorToParent { get; set; }
        public virtual bool Purchasing { get; set; }
        public virtual bool Reciping { get; set; }

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