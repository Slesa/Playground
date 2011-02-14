using System;
using Godot.IcsEditor.Ui.Events;
using Godot.IcsEditor.Ui.ViewModel;
using Godot.Model;
using Machine.Specifications;
using Rhino.Mocks;

namespace Godot.IcsEditor.Ui.Infrastructure
{
    class SimpleWorkspaceViewModel : WorkspaceViewModel
    {
        public SimpleWorkspaceViewModel(IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(dbConversation, eventAggregator)
        {
        }
    }

    class NumberedWorkspaceViewModel : ResponsibleWorkspaceViewModel
    {
        public NumberedWorkspaceViewModel(int number, IDbConversation dbConversation, IEventAggregator eventAggregator)
            : base(dbConversation, eventAggregator)
        {
            ContainedObject = number;
        }
    }


    [Subject(typeof (WorkspaceCollector))]
    public class When_finding_in_an_empty_collector
    {
        static WorkspaceViewModel _workspaceViewModel;
        static WorkspaceCollector _workspaceCollector;
        Establish context = () => { _workspaceCollector = new WorkspaceCollector(); };

        Because of = () => { _workspaceViewModel = _workspaceCollector.FindView<SimpleWorkspaceViewModel>(); };
        It should_result_in_nullptr = () => _workspaceViewModel.ShouldBeNull();
    }

    [Subject(typeof (WorkspaceCollector))]
    public class When_finding_only_view_in_collector
    {
        static WorkspaceViewModel _insertedWorkspace;
        static WorkspaceViewModel _foundWorkspace;
        static WorkspaceCollector _workspaceCollector;

        Establish context = () =>
            {
                var dbConversation = MockRepository.GenerateStub<IDbConversation>();
                var eventAggregator = MockRepository.GenerateStub<IEventAggregator>();
                _insertedWorkspace = new SimpleWorkspaceViewModel(dbConversation, eventAggregator);
                _workspaceCollector = new WorkspaceCollector();
                _workspaceCollector.SetActiveWorkspace(_insertedWorkspace);
            };

        Because of = () => { _foundWorkspace = _workspaceCollector.FindView<SimpleWorkspaceViewModel>(); };
        It should_find_the_only_view = () => _foundWorkspace.ShouldBeTheSameAs(_insertedWorkspace);
    }

    [Subject(typeof (WorkspaceCollector))]
    public class When_finding_only_view_of_one_type_in_collector
    {
        static WorkspaceViewModel _insertedWorkspace;
        static WorkspaceViewModel _foundWorkspace;
        static WorkspaceCollector _workspaceCollector;

        Establish context = () =>
            {
                var dbConversation = MockRepository.GenerateStub<IDbConversation>();
                var eventAggregator = MockRepository.GenerateStub<IEventAggregator>();
                _insertedWorkspace = new SimpleWorkspaceViewModel(dbConversation, eventAggregator);
                _workspaceCollector = new WorkspaceCollector();
                _workspaceCollector.SetActiveWorkspace(_insertedWorkspace);
                for (int i = 1; i < 5; i++)
                    _workspaceCollector.SetActiveWorkspace(new NumberedWorkspaceViewModel(i, dbConversation,
                                                                                          eventAggregator));
            };

        Because of = () => { _foundWorkspace = _workspaceCollector.FindView<SimpleWorkspaceViewModel>(); };
        It should_find_the_only_view_of_this_type = () => _foundWorkspace.ShouldBeTheSameAs(_insertedWorkspace);
    }

    [Subject(typeof (WorkspaceCollector))]
    public class When_finding_responsible_view_in_collector
    {
        static WorkspaceViewModel _insertedWorkspace;
        static WorkspaceViewModel _foundWorkspace;
        static WorkspaceCollector _workspaceCollector;
        static NumberedWorkspaceViewModel _unfoundWorkspace;

        Establish context = () =>
            {
                var dbConversation = MockRepository.GenerateStub<IDbConversation>();
                var eventAggregator = MockRepository.GenerateStub<IEventAggregator>();

                _insertedWorkspace = new NumberedWorkspaceViewModel(0, dbConversation, eventAggregator);
                _workspaceCollector = new WorkspaceCollector();
                _workspaceCollector.SetActiveWorkspace(_insertedWorkspace);
                _workspaceCollector.SetActiveWorkspace(new SimpleWorkspaceViewModel(dbConversation, eventAggregator));
                for (int i = 1; i < 5; i++)
                    _workspaceCollector.SetActiveWorkspace(new NumberedWorkspaceViewModel(i, dbConversation,
                                                                                          eventAggregator));
            };

        Because of = () =>
            {
                _foundWorkspace = _workspaceCollector.FindView<NumberedWorkspaceViewModel>(0);
                _unfoundWorkspace = _workspaceCollector.FindView<NumberedWorkspaceViewModel>(8);
            };

        It should_find_responsible_view = () => _foundWorkspace.ShouldBeTheSameAs(_insertedWorkspace);
        It should_not_find_unresponsible_view = () => _unfoundWorkspace.ShouldBeNull();
    }

    [Subject(typeof(WorkspaceCollector))]
    public class When_finding_unresponsible_view_with_responsible_data
    {
        static WorkspaceViewModel _insertedWorkspace;
        static WorkspaceViewModel _foundWorkspace;
        static WorkspaceCollector _workspaceCollector;

        Establish context = () =>
        {
            var dbConversation = MockRepository.GenerateStub<IDbConversation>();
            var eventAggregator = MockRepository.GenerateStub<IEventAggregator>();
            _insertedWorkspace = new SimpleWorkspaceViewModel(dbConversation, eventAggregator);
            _workspaceCollector = new WorkspaceCollector();
            _workspaceCollector.SetActiveWorkspace(_insertedWorkspace);
        };

        Because of = () => { _exception = Catch.Exception(()=>_foundWorkspace = _workspaceCollector.FindView<SimpleWorkspaceViewModel>(1)); };
        It should_fail = () => _exception.ShouldBeOfType(typeof(InvalidOperationException));
        static Exception _exception;
    }

}