namespace Nubis.Maths.Model
{
    public class InterestData
    {
        public InterestData()
        {
            PaymentsInYear = 1;
            NominalInterest = 1;
            EffectiveInterest = 1;
        }

        int _paymentsInYear;
        public int PaymentsInYear
        {
            get { return _paymentsInYear; }
            set 
            { 
                _paymentsInYear = value;
                _effectiveInterest = CalculateEffective(_paymentsInYear, _nominalInterest);
            }
        }

        decimal _nominalInterest;
        public decimal NominalInterest
        {
            get { return _nominalInterest; }
            set 
            { 
                _nominalInterest = value;
                _effectiveInterest = CalculateEffective(_paymentsInYear, _nominalInterest);
            }
        }

        decimal _effectiveInterest;
        public decimal EffectiveInterest
        {
            get { return _effectiveInterest; }
            set
            {
                _effectiveInterest = value;
                _nominalInterest = CalculateNominal(_paymentsInYear, _effectiveInterest);
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