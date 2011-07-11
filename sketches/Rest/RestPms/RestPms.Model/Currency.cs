namespace RestPms.Model
{
    public class Currency
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Contraction { get; set; }
        public virtual string Symbol { get; set; }
    }
}
