using Machine.Specifications;

namespace Godot.IcsEditor.Ui.Events
{
    [Subject(typeof(SubscriptionToken))]
    public class When_token_passed_is_null
    {
        Establish context = () => { _token = new SubscriptionToken(); };

        Because of = () => { _equal = _token.Equals(null); };

        It should_be_different = () => _equal.ShouldBeFalse();

        static SubscriptionToken _token;
        static bool _equal;
    }

    [Subject(typeof(SubscriptionToken))]
    public class When_token_passed_is_the_same
    {
        Establish context = () => { _token = new SubscriptionToken(); };

        Because of = () => { _equal = _token.Equals(_token); };

        It should_be_equal = () => _equal.ShouldBeTrue();

        static SubscriptionToken _token;
        static bool _equal;
    }

    [Subject(typeof(SubscriptionToken))]
    public class When_comparing_same_object_instance
    {
        Establish context = () => { _token = new SubscriptionToken(); };

        Because of = () =>
            {
                object tokenObject = _token;
                _equal = _token.Equals(tokenObject);
            };

        It should_be_equal = () => _equal.ShouldBeTrue();

        static SubscriptionToken _token;
        static bool _equal;
    }

    [Subject(typeof(SubscriptionToken))]
    public class When_comparing_different_object_instance
    {
        Establish context = () => { _token = new SubscriptionToken(); };

        Because of = () =>
        {
            object tokenObject = new SubscriptionToken();
            _equal = _token.Equals(tokenObject);
        };

        It should_be_different = () => _equal.ShouldBeFalse();

        static SubscriptionToken _token;
        static bool _equal;
    }

    [Subject(typeof(SubscriptionToken))]
    public class When_comparing_hashcode_for_same_token
    {
        Establish context = () => { _token = new SubscriptionToken();
                                      _hashcode = _token.GetHashCode();
        };

        Because of = () =>
        {
            object tokenObject = _token;
            _equal = _token.Equals(tokenObject);
        };

        It should_be_equal = () => _token.GetHashCode().ShouldEqual(_hashcode);

        static SubscriptionToken _token;
        static bool _equal;
        static int _hashcode;
    }
}