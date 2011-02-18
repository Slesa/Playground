using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using NightHawkSL.Resources;
using NightHawkSL.Ui.Core;

namespace NightHawkSL.ViewModels
{
    [Export(typeof(IShell))]
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IShell, IHandle<ErrorMessage>
    {
        [ImportMany(AllowRecomposition = true)] 
        public IEnumerable<IScreen> Modules;

        [ImportingConstructor]
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
            DisplayName = AppStrings.AppTitle;

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
            Application.Current.RootVisual.SetValue(Control.IsEnabledProperty, true);
            foreach (var screen in Items)
                yield return Show.NotBusy(screen);
        } 

        public bool HasActiveDialog { get; set; }
    }
}