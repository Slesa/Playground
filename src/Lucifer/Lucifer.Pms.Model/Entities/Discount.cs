using Lucifer.DataAccess;

namespace Lucifer.Pms.Model.Entities
{
    public class Discount : DomainEntity
    {
        public virtual string Name { get; set; }
        public virtual decimal Rate { get; set; }
        
    }
}