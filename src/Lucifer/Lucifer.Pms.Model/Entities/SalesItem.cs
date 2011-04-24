using Lucifer.DataAccess;

namespace Lucifer.Pms.Model.Entities
{
    public class SalesItem : DomainEntity
    {
        public virtual string Name { get; set; }
        public virtual SalesFamily SalesFamily { get; set; }        
    }
}