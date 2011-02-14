using System;
using System.Collections.Generic;
using Godot.IcsModel.Entities;
using Godot.Model;
using Machine.Specifications;
using Rhino.Mocks;

namespace Godot.IcsModel
{
    [Subject(typeof(UnitConverter))]
    public class When_converting_to_the_same_unit : WithMockedDbConversation
    {
        Because of = () => { _toValue = UnitConverter.Convert(FromValue, null, null); };

        It should_be_the_same_value = () => _toValue.ShouldEqual(FromValue);

        static decimal _toValue;
        const decimal FromValue = 33.33m;
    }

    [Subject(typeof(UnitConverter))]
    public class When_converting_to_invalid_unit : WithMockedDbConversation
    {
        Because of = () =>
            {
                _fromUnitError = Catch.Exception(() => UnitConverter.Convert(0.0m, null, new Unit()));
                _toUnitError = Catch.Exception(() => UnitConverter.Convert(0.0m, new Unit(), null));
            };

        It should_handle_invalid_from_unit = () => _fromUnitError.ShouldBeOfType<InvalidUnitException>();
        It should_handle_invalid_to_unit = () => _toUnitError.ShouldBeOfType<InvalidUnitException>();

        static Exception _toUnitError;
        static Exception _fromUnitError;
    }

    [Subject(typeof(UnitConverter))]
    public class When_converting_to_inconvertible_unit : WithDemoDbConversation
    {
        Because of = () =>
            {
                _convertError = Catch.Exception(() => UnitConverter.Convert(0.0m, Km, Ton));
            };

        It should_fail = () => _convertError.ShouldBeOfType<UnitNotConvertableException>();

        static Exception _convertError;
    }

    [Subject(typeof(UnitConverter))]
    public class When_converting_to_parent_unit : WithDemoDbConversation
    {
        Because of = () =>
            {
                _fromMmToMe = UnitConverter.Convert(FromValue, Mm, Me);
                _fromCmToMe = UnitConverter.Convert(FromValue, Cm, Me);
                _fromDmtoMe = UnitConverter.Convert(FromValue, Dm, Me);
                _fromMeToKm = UnitConverter.Convert(FromValue, Me, Km);
            };

        It should_convert_mm_to_m = () => _fromMmToMe.ShouldEqual(FromValue/1000);
        It should_convert_cm_to_m = () => _fromCmToMe.ShouldEqual(FromValue/100);
        It should_convert_dm_to_m = () => _fromDmtoMe.ShouldEqual(FromValue/10);
        It should_convert_m_to_mk = () => _fromMeToKm.ShouldEqual(FromValue/1000);

        static decimal _fromMmToMe;
        static decimal _fromCmToMe;
        static decimal _fromDmtoMe;
        static decimal _fromMeToKm;
        const decimal FromValue = 42.42m;
    }

    [Subject(typeof(UnitConverter))]
    public class When_converting_to_parent_tree_unit : WithDemoDbConversation
    {
        Because of = () =>
            {
                _fromMmToKm = UnitConverter.Convert(FromValue, Mm, Km);
            };

        It should_convert_mm_to_km = () => _fromMmToKm.ShouldEqual(FromValue / 1000 / 1000);

        static decimal _fromMmToKm;
        const decimal FromValue = 42.42m;
    }

    [Subject(typeof(UnitConverter))]
    public class When_converting_to_child_unit : WithDemoDbConversation
    {
        Because of = () =>
        {
            _fromMeToMm = UnitConverter.Convert(FromValue, Me, Mm);
            _fromMeToCm = UnitConverter.Convert(FromValue, Me, Cm);
            _fromMetoDm = UnitConverter.Convert(FromValue, Me, Dm);
            _fromKmToMe = UnitConverter.Convert(FromValue, Km, Me);
        };

        It should_convert_m_to_mm = () => _fromMeToMm.ShouldEqual(FromValue * 1000);
        It should_convert_m_to_cm = () => _fromMeToCm.ShouldEqual(FromValue * 100);
        It should_convert_m_to_dm = () => _fromMetoDm.ShouldEqual(FromValue * 10);
        It should_convert_m_to_mk = () => _fromKmToMe.ShouldEqual(FromValue * 1000);

        static decimal _fromMeToMm;
        static decimal _fromMeToCm;
        static decimal _fromMetoDm;
        static decimal _fromKmToMe;
        const decimal FromValue = 42.42m;
    }

    [Subject(typeof(UnitConverter))]
    public class When_converting_to_child_tree_unit : WithDemoDbConversation
    {
        Because of = () =>
        {
            _fromKmToMm = UnitConverter.Convert(FromValue, Km, Mm);
            _fromKmToCm = UnitConverter.Convert(FromValue, Km, Cm);
            _fromKmToDm = UnitConverter.Convert(FromValue, Km, Dm);
            _fromKmToMe = UnitConverter.Convert(FromValue, Km, Me);
        };

        It should_convert_km_to_mm = () => _fromKmToMm.ShouldEqual(FromValue * 1000 * 1000);
        It should_convert_km_to_Cm = () => _fromKmToCm.ShouldEqual(FromValue * 1000 * 100);
        It should_convert_km_to_Dm = () => _fromKmToDm.ShouldEqual(FromValue * 1000 * 10);
        It should_convert_km_to_Me = () => _fromKmToMe.ShouldEqual(FromValue * 1000 );

        static decimal _fromKmToMm;
        static decimal _fromKmToCm;
        static decimal _fromKmToDm;
        static decimal _fromKmToMe;
        const decimal FromValue = 42.42m;
    }

    public class WithDemoDbConversation
    {
        static protected IUnitConverter UnitConverter;
        static protected readonly Unit Km = new Unit {Contraction = "km"};
        static protected readonly Unit Me = new Unit {Contraction = "m", Parent = Km, FactorToParent = 1000m};
        static protected readonly Unit Dm = new Unit {Contraction = "dm", Parent = Me, FactorToParent = 10m};
        static protected readonly Unit Cm = new Unit {Contraction = "cm", Parent = Me, FactorToParent = 100m};
        static protected readonly Unit Mm = new Unit {Contraction = "mm", Parent = Me, FactorToParent = 1000m};
        static protected readonly Unit Nm = new Unit {Contraction = "nm", Parent = Mm, FactorToParent = 10m};
        static protected readonly Unit Ton = new Unit { Contraction = "t" };

        Establish context = () =>
            {
                var unitList = new List<Unit> {Km, Me, Dm, Cm, Mm};

                var dbConversation = MockRepository.GenerateStub<IDbConversation>();
                //dbConversation.Stub(x => x.UsingTransaction((Action).)).WhenCalled(action);
           //     dbConversation.Stub(x => x.Query<IDomainQuery<IEnumerable<Unit>>>(null)).IgnoreArguments().Return(unitList);
                UnitConverter = new UnitConverter();
            };
    }

    public class WithMockedDbConversation
    {
        static protected IUnitConverter UnitConverter;

        Establish context = () =>
        {
            var dbConversation = MockRepository.GenerateStub<IDbConversation>();
            UnitConverter = new UnitConverter();
        };
    }


}