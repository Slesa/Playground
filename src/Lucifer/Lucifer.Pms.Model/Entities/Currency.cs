using Lucifer.DataAccess;

namespace Lucifer.Pms.Model.Entities
{
    public class Currency : DomainEntity
    {
        public Currency()
        {
            DecimalChar = ',';
            ThousandChar = '.';
        }
        public virtual string Name { get; set; }
        public virtual string Contraction { get; set; }
        public virtual string Symbol { get; set; }
        public virtual decimal Rate { get; set; }
        public virtual int DecimalPosition { get; set; }
        public virtual char DecimalChar { get; set; }
        public virtual char ThousandChar { get; set; }
    }
}
