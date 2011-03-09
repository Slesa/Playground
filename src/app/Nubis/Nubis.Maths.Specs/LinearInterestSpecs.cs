using Machine.Specifications;
using Nubis.Maths.Model;

namespace Nubis.Maths.Specs
{
    #region Simple interest

    [Subject(typeof(LinearInterest))]
    public class When_calculate_amount_for_credit_linear_for_one_year
    {
        Establish context = () => _linearInterest = new LinearInterest();

        Because of = () =>
            {
                _linearInterest.Interest = 3.5m;
                _linearInterest.Credit = 1500;
                _amount = _linearInterest.Amount;
            };

        It should_have_calculated_amount = () => _amount.ShouldEqual(1552.50m);

        static LinearInterest _linearInterest;
        static decimal _amount;
    }

    [Subject(typeof(LinearInterest))]
    public class When_calculate_credit_for_amount_linear_for_one_year
    {
        Establish context = () => _linearInterest = new LinearInterest();

        Because of = () =>
            {
                _linearInterest.Interest = 3.5m;
                _linearInterest.Amount = 1552.50m;
                _credit = _linearInterest.Credit;
            };

        It should_have_calculated_credit = () => _credit.ShouldEqual(1500m);

        static LinearInterest _linearInterest;
        static decimal _credit;
    }

    [Subject(typeof(LinearInterest))]
    public class When_calculate_interest_for_linear_for_one_year
    {
        Establish context = () => _linearInterest = new LinearInterest();

        Because of = () =>
            {
                _linearInterest.Credit = 1500.0m;
                _linearInterest.Amount = 1552.50m;
                _interest = _linearInterest.Interest;
            };

        It should_have_calculated_interest = () => _interest.ShouldEqual(3.5m);

        static LinearInterest _linearInterest;
        static decimal _interest;
    }

    [Subject(typeof(LinearInterest))]
    public class When_calculate_amount_for_credit_linear_for_a_half_year
    {
        Establish context = () => _linearInterest = new LinearInterest();

        Because of = () =>
            {
                _linearInterest.Months = 6;
                _linearInterest.Interest = 3.5m;
                _linearInterest.Credit = 1500;
                _amount = _linearInterest.Amount;
            };

        It should_have_calculated_amount = () => _amount.ShouldEqual(1526.25m);

        static LinearInterest _linearInterest;
        static decimal _amount;
    }

    [Subject(typeof(LinearInterest))]
    public class When_calculate_credit_for_amount_linear_for_a_half_year
    {
        Establish context = () => _linearInterest = new LinearInterest();

        Because of = () =>
            {
                _linearInterest.Months = 6;
                _linearInterest.Interest = 3.5m;
                _linearInterest.Amount = 1526.25m;
                _credit = _linearInterest.Credit;
            };

        It should_have_calculated_credit = () => _credit.ShouldEqual(1500m);

        static LinearInterest _linearInterest;
        static decimal _credit;
    }

    [Subject(typeof(LinearInterest))]
    public class When_calculate_interest_for_linear_for_a_half_year
    {
        Establish context = () => _linearInterest = new LinearInterest();

        Because of = () =>
            {
                _linearInterest.Months = 6;
                _linearInterest.Credit = 1500.0m;
                _linearInterest.Amount = 1526.25m;
                _interest = _linearInterest.Interest;
            };

        It should_have_calculated_interest = () => _interest.ShouldEqual(3.5m);

        static LinearInterest _linearInterest;
        static decimal _interest;
    }

    #endregion

    [Subject(typeof(LinearInterest))]
    public class When_calculate_cash_value_for_linear_for_a_half_year
    {
        Establish context = () => _linearInterest = new LinearInterest();

        Because of = () =>
        {
            _linearInterest.Months = 6;
            _linearInterest.Credit = 4000.0m;
            _linearInterest.Interest = 0.05m;
            _cashValue = _linearInterest.CashValue;
        };

        It should_have_calculated_interest = () => _cashValue.ShouldEqual(3902.44m);

        static LinearInterest _linearInterest;
        static decimal _cashValue;
    }

}