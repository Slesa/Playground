using Machine.Specifications;
using Nubis.Maths.Model;

namespace Nubis.Maths.Specs
{
    [Subject(typeof(InterestData))]
    public class When_creating_interestdata
    {
        Because of = () => _interestData = new InterestData();

        It should_initialize_year_payments = () => _interestData.PaymentsInYear.ShouldEqual(1);
        It should_initialize_nominal_interest = () => _interestData.NominalInterest.ShouldEqual(1);
        It should_initialize_effective_interest = () => _interestData.EffectiveInterest.ShouldEqual(1);

        static InterestData _interestData;
    }

    [Subject(typeof (InterestData))]
    public class When_setting_nominal_interest
    {
        Establish context = () => _interestData = new InterestData();

        Because of = () => _interestData.NominalInterest = 2;

        It should_change_effective_interest = () => _interestData.EffectiveInterest.ShouldEqual(2);

        static InterestData _interestData;
    }

    [Subject(typeof (InterestData))]
    public class When_setting_effecive_interest
    {
        Establish context = () => _interestData = new InterestData();

        Because of = () => _interestData.EffectiveInterest = 2;

        It should_change_nominal_interest = () => _interestData.NominalInterest.ShouldEqual(2);

        static InterestData _interestData;
    }

    [Subject(typeof (InterestData))]
    public class When_setting_year_payments
    {
        Establish context = () =>
            {
                _interestData = new InterestData();
                _effectiveInterest = 2 * _interestData.NominalInterest * 12 / 13;
            };

        Because of = () => _interestData.PaymentsInYear = 12;

        It should_have_nominal_interest = () => _interestData.NominalInterest.ShouldEqual(1);
        It should_calculate_effective_interest = () => _interestData.EffectiveInterest.ShouldEqual(_effectiveInterest);

        static InterestData _interestData;
        static decimal _effectiveInterest;
    }

    [Subject(typeof (InterestData))]
    public class When_changing_nominal_interest
    {
        Establish context = () =>
            {
                _interestData = new InterestData {PaymentsInYear = 12};
                _effectiveInterest = 2 * 5.0m * 12 / 13;
            };

        Because of = () => _interestData.NominalInterest = 5.0m;

        It should_have_year_payments = () => _interestData.PaymentsInYear.ShouldEqual(12);
        It should_have_nominal_interest = () => _interestData.NominalInterest.ShouldEqual(5.0m);
        It should_calculate_effective_interest = () => _interestData.EffectiveInterest.ShouldEqual(_effectiveInterest);
        It should_have_effective_interest = () => _interestData.EffectiveInterest.ShouldNotEqual(0);
        It should_have_higher_nominal_then_effective_interest =
            () => _interestData.EffectiveInterest.ShouldBeGreaterThan(_interestData.NominalInterest);

        static InterestData _interestData;
        static decimal _effectiveInterest;
    }

    [Subject(typeof (InterestData))]
    public class When_changing_effective_interest
    {
        Establish context = () =>
            {
                _interestData = new InterestData {PaymentsInYear = 12};
                _nominalZins = (5.0m * 13) / (2*12);
            };

        Because of = () => _interestData.EffectiveInterest = 5.0m;

        It should_have_year_payments = () => _interestData.PaymentsInYear.ShouldEqual(12);
        It should_calculate_nominalinterest = () => _interestData.NominalInterest.ShouldEqual(_nominalZins);
        It should_have_effective_interest = () => _interestData.EffectiveInterest.ShouldEqual(5.0m);
        It should_have_nominal_interest = () => _interestData.NominalInterest.ShouldNotEqual(0);
        It should_have_lower_nominal_than_effective_interest =
            () => _interestData.NominalInterest.ShouldBeLessThan(_interestData.EffectiveInterest);


        static InterestData _interestData;
        static decimal _nominalZins;
    }


}