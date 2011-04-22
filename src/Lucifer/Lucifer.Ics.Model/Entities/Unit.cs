using Lucifer.DataAccess;

namespace Lucifer.Ics.Model.Entities
{
    public class Unit : DomainEntity
    {
        public virtual string Name { get; set; }
        public virtual string Contraction { get; set; }
        public virtual Unit Parent { get; set; }
        public virtual UnitType UnitType { get; set; }
        public virtual decimal FactorToParent { get; set; }
        public virtual bool Purchasing { get; set; }
        public virtual bool Reciping { get; set; }

    }
}