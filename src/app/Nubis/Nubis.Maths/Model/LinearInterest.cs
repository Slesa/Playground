namespace Nubis.Maths.Model
{
    public class LinearInterest
    {
        public LinearInterest()
        {
            Months = 12;
        }

        public int Months { get; set; }

        decimal _interest;
        public decimal Interest
        {
            get { return _interest; }
            set { _interest = value; CalculateMissings(); }
        }

        decimal _credit;
        public decimal Credit
        {
            get { return _credit; }
            set { _credit = value; CalculateMissings(); }
        }

        decimal _amount;
        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; CalculateMissings(); }
        }

        public decimal CashValue
        {
            get { return Amount*1/(1 + GetInterestRate()); }
        }

        void CalculateMissings()
        {
            if (GivenValues < 2) return;

            if (_interest != 0m)
            {
                if (_amount != 0m)
                    _credit = CalculateCredit();
                else
                    _amount = CalculateAmount();
            }
            else
                _interest = CalculateInterest();
        }

        int GivenValues
        {
            get
            {
                var values = 0;
                if (_interest != 0m) values++;
                if (_amount != 0m) values++;
                if (_credit != 0m) values++;
                return values;
            }
        }

        decimal CalculateAmount()
        {
            return (1m + GetInterestRate()) * Credit;
        }

        decimal CalculateCredit()
        {
            return Amount / (1m + GetInterestRate());
        }

        decimal CalculateInterest()
        {
            return ((Amount / Credit - 1.0m) / (Months/12m) ) * 100m;
        }

        decimal GetInterestRate()
        {
            return Interest * Months / 12m / 100.0m;
        }
    }
}