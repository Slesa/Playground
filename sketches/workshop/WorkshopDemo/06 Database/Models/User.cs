namespace Database.Models
{
    public class User : DomainEntity
    {
        public string Name { get; set; }

        public UserRole UserRole { get; set; }
    }
}