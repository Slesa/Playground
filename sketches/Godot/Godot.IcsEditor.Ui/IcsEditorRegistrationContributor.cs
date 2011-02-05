using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Godot.IcsEditor.Ui.Commands;
using Godot.IcsEditor.Ui.Events;
using Godot.IcsEditor.Ui.Infrastructure;
using Godot.IcsEditor.Ui.ViewModel;
using Godot.Infrastructure;

namespace Godot.IcsEditor.Ui
{
    public class IcsEditorRegistrationContributor : IRegistrationContributor
    {
        public IEnumerable<IRegistration> GetRegistrations()
        {
            yield return AllTypes
                .FromAssemblyContaining(GetType())
                .BasedOn(typeof(IRequireConfigurationOnStartup))
                .WithService.Base(); //.Configure(x => x.LifeStyle.Transient);

            yield return AllTypes
                .FromAssemblyContaining(GetType())
                .BasedOn(typeof(IRegisterComponentsOnStartup))
                .WithService.Base(); //.Configure(x => x.LifeStyle.Transient);

            yield return Component
                .For<IViewActivator>()
                .ImplementedBy<ViewActivator>();

            yield return Component
                .For<IWorkspaceCollector>()
                .ImplementedBy<WorkspaceCollector>();

            yield return Component
                .For<IEventAggregator>()
                .ImplementedBy<EventAggregator>();

            yield return AllTypes
                .FromAssemblyContaining(GetType())
                .BasedOn(typeof(IEntityCommands)).Configure(x => x.LifeStyle.Transient);

            yield return AllTypes
                .FromAssemblyContaining(GetType())
                .BasedOn(typeof(ViewModelBase)).Configure(x=> x.LifeStyle.Transient);
        }
    }
}