namespace Nubis.Maths.Model
{
    public class InterestData
    {
        public InterestData()
        {
            YearPayments = 1;
            NominalInterest = 1;
            EffectiveInterest = 1;
        }

        int _yearPayments;
        public int YearPayments
        {
            get { return _yearPayments; }
            set 
            { 
                _yearPayments = value;
                _effectiveInterest = CalculateEffective(_yearPayments, _nominalInterest);
            }
        }

        decimal _nominalInterest;
        public decimal NominalInterest
        {
            get { return _nominalInterest; }
            set 
            { 
                _nominalInterest = value;
                _effectiveInterest = CalculateEffective(_yearPayments, _nominalInterest);
            }
        }

        decimal _effectiveInterest;
        public decimal EffectiveInterest
        {
            get { return _effectiveInterest; }
            set
            {
                _effectiveInterest = value;
                _nominalInterest = CalculateNominal(_yearPayments, _effectiveInterest);
            }
        }

        static decimal CalculateEffective(int rates, decimal rate)
        {
            return (2*rates*rate)/(rates + 1);
        }

        static decimal CalculateNominal(int rates, decimal rate)
        {
            return (rate*(rates + 1))/(2*rates);
        }
    }
}