using System;
using Machine.Specifications;

namespace Godot.IcsEditor.Ui.ViewModel
{
    [Subject(typeof(ViewModelBase))]
    public class When_property_is_changed
    {
        Establish context = () =>
            {
                _viewModel = new TestViewModel();
                _eventWasRaised = false;
                _viewModel.PropertyChanged += (sender, e) => _eventWasRaised = e.PropertyName == "GoodProperty";
            };
        Because of = () => _viewModel.GoodProperty = "Some new value...";
        It should_rise_event = () => _eventWasRaised.ShouldBeTrue();

        static TestViewModel _viewModel;
        static bool _eventWasRaised;
    }

    [Subject(typeof(ViewModelBase))]
    public class When_using_invalid_properties
    {
        Establish context = () => { _viewModel = new TestViewModel(); };
        Because of = () => _exception = Catch.Exception(() => _viewModel.BadProperty = "Some new value...");
        It should_fail = () => _exception.ShouldNotBeNull();

        static TestViewModel _viewModel;
        static Exception _exception;
    }

    class TestViewModel : ViewModelBase
    {
        protected override bool ThrowOnInvalidPropertyName
        {
            get { return true; }
        }

        public string GoodProperty
        {
            get { return null; }
            set
            {
                base.OnPropertyChanged("GoodProperty");
            }
        }

        public string BadProperty
        {
            get { return null; }
            set
            {
                base.OnPropertyChanged("ThisIsAnInvalidPropertyName!");
            }
        }
    }
}