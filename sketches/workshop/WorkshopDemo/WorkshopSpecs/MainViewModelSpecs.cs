using Machine.Fakes;
using Machine.Specifications;
using WorkshopDemo;

namespace WorkshopSpecs
{
    [Subject(typeof (MainViewModel))]
    internal class When_starting_mainviewmodel : WithSubject<MainViewModel>
    {
        It should_have_no_status_text = () => string.IsNullOrEmpty(Subject.StatusText).ShouldBeTrue();
        It should_not_display_statusbar = () => Subject.StatusVisible.ShouldBeFalse();
    }


    [Subject(typeof (MainViewModel))]
    internal class When_setting_statustext_in_mainviewmodel : WithSubject<MainViewModel>
    {
        Establish context = () => Subject.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == "StatusText") _raisedText = true;
            if (args.PropertyName == "StatusVisible") _raisedVisible = true;
        };

        Because of = () => Subject.StatusText = "Status text";

        It should_change_status_text = () => Subject.StatusText.ShouldEqual("Status text");
        It should_display_statusbar = () => Subject.StatusVisible.ShouldBeTrue();
        It should_raise_text_change = () => _raisedText.ShouldBeTrue();
        It should_raise_visible_change = () => _raisedVisible.ShouldBeTrue();

        static bool _raisedText;
        static bool _raisedVisible;
    }
}