using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using MediaOwl.Core;
using MediaOwl.Resources;

namespace MediaOwl.ViewModels
{
    [Export(typeof(IShell))]
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive,
        IShell,
        IHandle<ErrorMessage>
    {

        #region Constructor

        [ImportingConstructor]
        public ShellViewModel(TestViewModel testViewModel,
            MusicViewModel musicViewModel,
            MovieViewModel movieViewModel,
            WelcomeViewModel welcomeViewModel, 
            IEventAggregator eventAggregator)
        {
            HasActiveDialog = false;
            eventAggregator.Subscribe(this);
            Items.Add(welcomeViewModel);
            Items.Add(movieViewModel);
            Items.Add(musicViewModel);
            Items.Add(testViewModel);
        }

        #endregion

        #region Methods

        protected override void OnInitialize()
        {
            base.OnInitialize();
            DisplayName = AppStrings.AppTitle;
            ActivateItem(Items.FirstOrDefault());
        }

        public void MenuBtnClick(object o)
        {
            ActivateItem((IScreen)o);
        }

        #endregion

        #region Implementation of IHandle<in ErrorMessage>

        /// <summary>
        /// Handles the message.
        /// </summary>
        /// <param name="message">The message.</param>
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

        #endregion

        #region Implementation of IShell

        public bool HasActiveDialog { get; set; }

        #endregion
    }
}