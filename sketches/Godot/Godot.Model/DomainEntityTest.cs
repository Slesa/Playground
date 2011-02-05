using System;
using Machine.Specifications;

#pragma warning disable 169

namespace Godot.Model
{
    public class Person : DomainEntity
    {
        public Person(string name)
        {
            Name = name;
        }

        public Person(int id, string name)
            : base(id)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }

    [Behaviors]
    class EntityInequality
    {
        protected static Person Person1;
        protected static Person Person2;

        It should_be_considered_as_unequal = () => ((Object) Person1).Equals(Person2).ShouldBeFalse();
        It should_be_considered_as_unequal_with_equality_operator = () => (Person1 == Person2).ShouldBeFalse();
        It should_be_considered_as_unequal_with_inequality_operator = () => (Person1 != Person2).ShouldBeTrue();
        It should_have_symmetric_inequality = () => ((Object) Person2).Equals(Person1).ShouldBeFalse();
        It should_compute_a_different_hash = () => Person1.GetHashCode().ShouldNotEqual(Person2.GetHashCode());
    }

    [Behaviors]
    class EntityEquality
    {
        protected static Person Person1;
        protected static Person Person2;

        It should_be_considered_as_equal = () => ((Object) Person1).Equals(Person2).ShouldBeTrue();
        It should_be_considered_as_equal_with_equality_operator = () => (Person1 == Person2).ShouldBeTrue();
        It should_be_considered_as_equal_with_inequality_operator = () => (Person1 != Person2).ShouldBeFalse();

        It should_have_symmetric_inequality = () => ((Object) Person2).Equals(Person1).ShouldBeTrue();

        It should_compute_the_same_hash = () => Person1.GetHashCode().ShouldEqual(Person2.GetHashCode());
    }

    [Subject(typeof (DomainEntity))]
    public class When_comparing_unsaved_entities_with_identical_values
    {
        protected static Person Person1;
        protected static Person Person2;

        Establish context = () =>
            {
                Person1 = new Person("Darth Vader");
                Person2 = new Person("Darth Vader");
            };

        Behaves_like<EntityInequality> inequal_entities;
    }

    [Subject(typeof (DomainEntity))]
    public class When_comparing_unsaved_entities_with_different_values
    {
        protected static Person Person1;
        protected static Person Person2;

        Establish context = () =>
            {
                Person1 = new Person("Darth Vader");
                Person2 = new Person("Senator Palpatine");
            };

        Behaves_like<EntityInequality> inequal_entities;
    }

    [Subject(typeof (DomainEntity))]
    public class When_comparing_saved_entities_with_different_id_s
    {
        protected static Person Person1;
        protected static Person Person2;

        Establish context = () =>
            {
                Person1 = new Person(42, "Darth Vader");
                Person2 = new Person(43, "Senator Palpatine");
            };

        Behaves_like<EntityInequality> inequal_entities;
    }

    [Subject(typeof (DomainEntity))]
    public class When_comparing_saved_entities_with_identical_id_s
    {
        protected static Person Person1;
        protected static Person Person2;

        Establish context = () =>
            {
                Person1 = new Person(42, "Darth Vader");
                Person2 = new Person(42, "Senator Palpatine");
            };

        Behaves_like<EntityEquality> equal_entities;
    }

    [Subject(typeof (DomainEntity))]
    public class When_comparing_the_same_entity_with_itself
    {
        static Person _person;

        Establish context = () => { _person = new Person("Darth Vader"); };

        It should_have_reflexive_equality = () => _person.Equals(_person).ShouldBeTrue();
    }
}

