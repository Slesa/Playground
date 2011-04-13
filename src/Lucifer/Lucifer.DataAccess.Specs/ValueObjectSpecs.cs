using Machine.Specifications;

namespace Lucifer.DataAccess.Specs
{
    public class StreetAddress : ValueObject<StreetAddress>
    {
        readonly string _city;
        readonly string _state;
        readonly string _street;

        public StreetAddress(string street, string city, string state)
        {
            _street = street;
            _city = city;
            _state = state;
        }

        public string Street { get { return _street; } }
        public string City { get { return _city; } }
        public string State { get { return _state; } }
    }

    public class StreetAddressWithNumber : StreetAddress
    {
        readonly string _streetNumber;

        public StreetAddressWithNumber(string street, string streetNumber, string city, string state)
            : base(street, city, state)
        {
            _streetNumber = streetNumber;
        }

        public string StreetNumber { get { return _streetNumber; } }
    }

    [Behaviors]
    internal class ValueObjectInequality
    {
        protected static StreetAddress Address1;
        protected static StreetAddress Address2;

        It should_be_considered_as_unequal = () => ((object)Address1).Equals(Address2).ShouldBeFalse();
        It should_be_considered_as_unequal_with_equality_operator = () => (Address1 == Address2).ShouldBeFalse();
        It should_be_considered_as_unequal_with_unequality_operator = () => (Address1 != Address2).ShouldBeTrue();
        It should_have_symmetric_unequality = () => ((object)Address2).Equals(Address1).ShouldBeFalse();
        It should_compute_a_different_hash = () => Address1.GetHashCode().ShouldNotEqual(Address2.GetHashCode());
    }

    [Behaviors]
    internal class ValueObjectEquality
    {
        protected static StreetAddress Address1;
        protected static StreetAddress Address2;

        It should_be_considered_as_equal = () => ((object)Address1).Equals(Address2).ShouldBeTrue();
        It should_be_considered_as_equal_with_equality_operator = () => (Address1 == Address2).ShouldBeTrue();
        It should_be_considered_as_equal_with_inequality_operator = () => (Address1 != Address2).ShouldBeFalse();
        It should_have_symmetric_equality = () => ((object)Address2).Equals(Address1).ShouldBeTrue();
        It should_compute_the_same_hash = () => Address1.GetHashCode().ShouldEqual(Address2.GetHashCode());
    }

    [Subject(typeof(ValueObject<>))]
    public class When_comparing_value_objects_with_identical_values
    {
        protected static StreetAddress Address1;
        protected static StreetAddress Address2;

        Establish context = () =>
        {
            Address1 = new StreetAddress("Street", "Austin", "TX");
            Address2 = new StreetAddress("Street", "Austin", "TX");
        };

        Behaves_like<ValueObjectEquality> equal_value_objects;
    }

    [Subject(typeof(ValueObject<>))]
    public class When_comparing_value_objects_with_different_values
    {
        protected static StreetAddress Address1;
        protected static StreetAddress Address2;

        Establish context = () =>
        {
            Address1 = new StreetAddress("Street", "Austin", "TX");
            Address2 = new StreetAddress("Foo", "Austin", "TX");
        };

        Behaves_like<ValueObjectInequality> unequal_value_objects;
    }

    [Subject(typeof(ValueObject<>))]
    public class When_comparing_different_value_objets_with_null_values_on_one_object
    {
        protected static StreetAddress Address1;
        protected static StreetAddress Address2;

        Establish context = () =>
        {
            Address1 = new StreetAddress(null, "Austin", "TX");
            Address2 = new StreetAddress("Street", "Austin", "TX");
        };

        Behaves_like<ValueObjectInequality> unequal_value_objects;
    }

    [Subject(typeof(ValueObject<>))]
    public class When_comparing_a_value_object_against_null
    {
        static StreetAddress _streetAddress;

        Establish context = () => _streetAddress = new StreetAddress("Street", "Austin", "TX");

        It should_be_considered_as_unequal = () => _streetAddress.Equals(null).ShouldBeFalse();
    }

    [Subject(typeof(ValueObject<>))]
    public class When_comparing_value_object_with_itself
    {
        static StreetAddress _streetAddress;

        Establish context = () => { _streetAddress = new StreetAddress("StreetAddress", "Austin", "TX"); };

        It should_have_erflexive_equality = () => _streetAddress.Equals(_streetAddress).ShouldBeTrue();
    }

    [Subject(typeof(ValueObject<>))]
    public class When_comparing_three_value_objects_with_identical_values
    {
        static StreetAddress _address1;
        static StreetAddress _address2;
        static StreetAddress _address3;

        Establish context = () =>
        {
            _address1 = new StreetAddress("Street", "Austin", "TX");
            _address2 = new StreetAddress("Street", "Austin", "TX");
            _address3 = new StreetAddress("Street", "Austin", "TX");
        };

        It should_have_transitive_equality_for_a_and_b = () => ((object)_address1).Equals(_address2).ShouldBeTrue();
        It should_have_transitive_equality_for_b_and_c = () => ((object)_address2).Equals(_address3).ShouldBeTrue();
        It should_have_transitive_equality_for_a_and_c = () => ((object)_address1).Equals(_address3).ShouldBeTrue();
    }

    [Subject(typeof(ValueObject<>))]
    public class When_comparing_value_objects_with_transposed_field_values
    {
        protected static StreetAddress Address1;
        protected static StreetAddress Address2;

        Establish context = () =>
        {
            Address1 = new StreetAddress(null, "Austin", "TX");
            Address2 = new StreetAddress("TX", "Austin", null);
        };

        Behaves_like<ValueObjectInequality> unequal_value_objects;
    }

    [Subject(typeof(ValueObject<>))]
    public class When_comparing_value_objects_against_derived_one_with_same_values
    {
        protected static StreetAddress Address1;
        protected static StreetAddressWithNumber Address2;

        Establish context = () =>
        {
            Address1 = new StreetAddress("Street", "Austin", "TX");
            Address2 = new StreetAddressWithNumber("Street", "StreetNumber", "Austin", "TX");
        };

        Behaves_like<ValueObjectInequality> unequal_value_objects;
    }
}