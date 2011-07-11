namespace RestPms.Model
{
    public class Discount
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual decimal Rate { get; set; }
    }
}