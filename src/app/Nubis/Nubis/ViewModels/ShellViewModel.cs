using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Nubis.Caliburn;
using Nubis.Resources;

namespace Nubis.ViewModels
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IShell //, IHandle<ErrorMessage>
    {
        //IEnumerable<IModule> _modules;
        //public IEnumerable<IModule> Modules { get { return _modules ?? (_modules = _container.ResolveAll<IModule>()); }}
        public IModule[] Modules { get; set; }

        public ShellViewModel(
            //TestViewModel testViewModel,
            /*MusicViewModel musicViewModel,
            MovieViewModel movieViewModel, */
            //WelcomeViewModel welcomeViewModel,
            IEventAggregator eventAggregator)
        {
            HasActiveDialog = false;
            eventAggregator.Subscribe(this);
            //Items.Add(welcomeViewModel);
            //Items.Add(welcomeViewModel);
            //Items.Add(movieViewModel);
            //Items.Add(musicViewModel);
            //Items.Add(testViewModel);
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            DisplayName = Strings.AppTitle;


            foreach (var module in Modules)
            {
                Items.Add(module);
            }
            ActivateItem(Items.FirstOrDefault());
        }

        public void MenuBtnClick(object o)
        {
            ActivateItem((IScreen)o);
        }

        /*
        public void Handle(ErrorMessage message)
        {
            if (HasActiveDialog)
                return;
            HasActiveDialog = true;
            Run.Coroutine(ShowError(message));
        }

        private IEnumerator<IResult> ShowError(ErrorMessage message)
        {
            yield return Show.Dialog<ErrorDialogViewModel>().Configured(x => x.WithError(message.CompletionEventArgs.Error.Message));
            HasActiveDialog = false;
            //Application.Current.RootVisual.SetValue(Control.IsEnabledProperty, true);
            foreach (var screen in Items)
                yield return Show.NotBusy(screen);
        } 
        */
        public bool HasActiveDialog { get; set; }
    }
}