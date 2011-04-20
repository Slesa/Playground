using Lucifer.DataAccess;

namespace Lucifer.Ums.Model.Entities
{
    public class User : DomainEntity
    {
        public virtual string Name { get; set; }
        public virtual UserRole UserRole { get; set; }

    }
}
