using Machine.Specifications;
using Microsoft.VisualBasic;

namespace Nubis.Maths.Specs
{
    /// <summary>
    /// SYD:        Returns a Double specifying the sum-of-years digits depreciation of an asset for a specified period
    ///             Jahresabschreibung eines Vermögenswertes über einen angegebenen Zeitraum
    /// _cost:      specifying the initial cost of the asset
    ///             Anschaffnungskosten
    /// _salvage:   specifying the value of the asset at the end of its useful life
    ///             Vermögenswert am Ende der Nutzungsdauer
    /// _life:      specifying the length of the useful life of the asset
    ///             Länge der Nutzungsdauer
    /// _period:    specifying the period for which asset depreciation is calculated
    ///             Zeitraum
    /// </summary>
    [Subject(typeof(Financial))]
    public class When_calculating_syd
    {
        Establish context = () =>
            {
                _cost = 10000.0;
                _salvage = 5000.0;
                _life = 4.0;
                _period = 1.0;
            };

        Because of = () => _result = Financial.SYD(_cost, _salvage, _life, _period);

        It should_calculate_value = () => _result.ShouldEqual(2000);

        static double _result;
        static double _cost;
        static double _salvage;
        static double _life;
        static double _period;
    }

    /// <summary>
    /// DDB:        Returns a Double specifying the depreciation of an asset for a specific time period using the double-declining balance method or some other method you specify
    ///             Degressive Abschreibung eines Vermögenswertes über den angegebenen Zeitraum. 
    ///             Standard für den Wertminderungsfaktor ist 2
    /// _cost:      specifying initial cost of the asset
    ///             Anschaffnungskosten
    /// _salvage:   specifying value of the asset at the end of its useful life
    ///             Vermögenswert am Ende der Nutzungsdauer
    /// _life:      specifying length of useful life of the asset
    ///             Länge der Nutzungsdauer
    /// _period:    specifying period for which asset depreciation is calculated
    ///             Zeitraum
    /// _factor:    specifying rate at which the balance declines. If omitted, 2 (double-declining method) is assumed
    ///             Wertminderungsfaktor
    /// </summary>
    [Subject(typeof(Financial))]
    public class When_calculating_ddb
    {
        Establish context = () =>
            {
                _cost = 10000.0;
                _salvage = 5000.0;
                _life = 4.0;
                _period = 1.0;
                _factor = 2.0;
            };

        Because of = () => _result = Financial.DDB(_cost, _salvage, _life, _period, _factor);

        It should_calculate_value = () => _result.ShouldEqual(5000);

        static double _result;
        static double _cost;
        static double _salvage;
        static double _life;
        static double _period;
        static double _factor;
    }

    /// <summary>
    /// SLN:        Returns a Double specifying the straight-line depreciation of an asset for a single period.
    ///             Arithmetische Abschreibung eines Vermögenswertes über einen angegebenen Zeitraum
    /// _cost:      specifying initial cost of the asset
    ///             Anschaffungskosten
    /// _salvage:   specifying value of the asset at the end of its useful life
    ///             Vermögenswert am Ende der Nutzungsdauer
    /// _life:      specifying length of the useful life of the asset
    ///             Länge der Nutzungsdauer
    /// </summary>
    [Subject(typeof(Financial))]
    public class When_calculating_sln
    {
        Establish context = () =>
            {
                _cost = 10000.0;
                _salvage = 5000.0;
                _life = 4.0;
            };

        Because of = () => _result = Financial.SLN(_cost, _salvage, _life);

        It should_calculcate_value = () => _result.ShouldEqual(1250);

        static double _result;
        static double _cost;
        static double _salvage;
        static double _life;
    }
}
