using System.Collections.ObjectModel;
using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using Machine.Specifications.Model;
using WorkshopDemo;

namespace WorkshopSpecs
{
    [Subject(typeof (MainViewModel))]
    internal class When_starting_mainviewmodel : WithSubject<MainViewModel>
    {
        It should_have_no_status_text = () => string.IsNullOrEmpty(Subject.StatusText).ShouldBeTrue();
        It should_not_display_statusbar = () => Subject.StatusVisible.ShouldBeFalse();
        It should_not_have_items = () => Subject.Items.ShouldBeEmpty();
        It should_not_have_selectedItem = () => Subject.SelectedItem.ShouldBeNull();
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


    [Subject(typeof (MainViewModel))]
    internal class When_no_item_is_selected : WithSubject<MainViewModel>
    {
        // First test told us that we do not have any item, and nothing is slected. We can rely on that
        It should_allow_add_item = () => Subject.AddCommand.CanExecute(null).ShouldBeTrue();
        It should_forbid_edit_item = () => Subject.EditCommand.CanExecute(null).ShouldBeFalse();
        It should_forbid_remove_item = () => Subject.RemoveCommand.CanExecute(null).ShouldBeFalse();
    }


    [Subject(typeof (MainViewModel))]
    internal class When_item_gets_selected : WithSubject<MainViewModel>
    {
        Establish context = () =>
        {
            Subject.Items = _items;
            Subject.AddCommand.CanExecuteChanged += (sender, args) => _canAddChanged = true;
            Subject.EditCommand.CanExecuteChanged += (sender, args) => _canEditChanged = true;
            Subject.RemoveCommand.CanExecuteChanged += (sender, args) => _canRemoveChanged = true;
        };

        Because of = () => Subject.SelectedItem = _items.First();

        It should_not_fire_add_changes = () => _canAddChanged.ShouldBeFalse();
        It should_fire_edit_changed = () => _canEditChanged.ShouldBeTrue();
        It should_fire_remove_changed = () => _canRemoveChanged.ShouldBeTrue();

        static ObservableCollection<WorkItem> _items = new ObservableCollection<WorkItem>()
        {
            new WorkItem {Id = 1, Name = "First item"},
            new WorkItem {Id = 2, Name = "Second item"},
        };

        static bool _canAddChanged;
        static bool _canEditChanged;
        static bool _canRemoveChanged;
    }


    [Subject(typeof (MainViewModel))]
    internal class When_item_is_selected : WithSubject<MainViewModel>
    {
        Establish context = () =>
        {
            Subject.Items = _items;
            Subject.SelectedItem = _items.First();
        };

        It should_allow_add_item = () => Subject.AddCommand.CanExecute(null).ShouldBeTrue();
        It should_allow_edit_item = () => Subject.EditCommand.CanExecute(null).ShouldBeTrue();
        It should_allow_remove_item = () => Subject.RemoveCommand.CanExecute(null).ShouldBeTrue();

        static ObservableCollection<WorkItem> _items = new ObservableCollection<WorkItem>()
        {
            new WorkItem {Id = 1, Name = "First item"},
            new WorkItem {Id = 2, Name = "Second item"},
        };
    }
}