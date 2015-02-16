namespace Database.Models
{
    public class DomainEntity
    {
        int _id;

        protected DomainEntity()
        {
        }

        protected DomainEntity(int id)
        {
            _id = id;
        }

        public virtual int Id
        {
            get { return _id; }
            protected set { _id = value; }
        }

        public virtual int Version { get; protected set; }
    }
}