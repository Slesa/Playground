using Lucifer.DataAccess;

namespace Lucifer.Pms.Model.Entities
{
    public class Currency : DomainEntity
    {
        public virtual string Name { get; set; }
        public virtual string Contraction { get; set; }
    }
}
