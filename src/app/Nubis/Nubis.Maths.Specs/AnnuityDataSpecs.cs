using Machine.Specifications;
using Nubis.Maths.Model;

namespace Nubis.Maths.Specs
{
    [Subject(typeof (AnnuityData))]
    public class When_creating_annuity_data
    {
        Establish context = () => _annuityData = new AnnuityData(new InterestData());
        Because of = () =>
            {
                _annuityData.Kreditsumme = 10000;
                _annuityData.Laufzeit = 144;
                //_annuityData.NominalZins = 5.0m;
            };

        It should_have_kreditsumme = () => _annuityData.Kreditsumme.ShouldEqual(10000);
        It should_have_laufzeit = () => _annuityData.Laufzeit.ShouldEqual(144);
        //It should_have_zinssatz = () => _annuityData.NominalZins.ShouldEqual(5);

        static AnnuityData _annuityData;
    }
}