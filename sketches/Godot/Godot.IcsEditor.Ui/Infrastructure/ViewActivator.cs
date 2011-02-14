using Castle.Windsor;
using Godot.IcsEditor.Ui.ViewModel;

namespace Godot.IcsEditor.Ui.Infrastructure
{
    public class ViewActivator : IViewActivator
    {
        private readonly IWindsorContainer _container;

        public ViewActivator(IWindsorContainer container)
        {
            _container = container;
        }

        public TView Display<TView>() where TView : WorkspaceViewModel
        {
            return _container.Resolve<TView>();
        }

        public TView Display<TView>(int id) where TView : WorkspaceViewModel
        {
            return _container.Resolve<TView>(new { entityId = id});
        }

        public TView Display<TView>(object arguments) where TView : WorkspaceViewModel
        {
            return _container.Resolve<TView>(arguments);
        }
    }
}