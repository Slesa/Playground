namespace HelloFluentNH.Entities
{
    public class Employee
    {
        public virtual int Id { get; private set; }
        public virtual string Name { get; set; }
        public virtual Employee Manager { get; set; }

        public override string ToString()
        {
            return Id + " " + Name;
        }
    }
}