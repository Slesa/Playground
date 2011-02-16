using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using NightHawk.Core;
using NightHawk.Resources;
using NightHawk.Views;

namespace NightHawk.ViewModels
{
    [Export(typeof(IShell))]
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IShell, IHandle<ErrorMessage>
    {
        [ImportingConstructor]
        public ShellViewModel(IEventAggregator eventAggregator)
        {
            HasActiveDialog = false;
            eventAggregator.Subscribe(this);
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            DisplayName = AppStrings.AppTitle;
            ActivateItem(Items.FirstOrDefault());
        }

        public void MenuButtonClick(object o)
        {
            ActivateItem( (IScreen) o);
        }

        public void Handle(ErrorMessage message)
        {
            if (HasActiveDialog)
                return;
            HasActiveDialog = true;
            Run.Coroutine(ShowError(message));
        }

        IEnumerable<IResult> ShowError(ErrorMessage message)
        {
            yield return
                Show.Dialog<ErrorDialogViewModel>().Configured(x => x.WithError(message.EventArgs.Error.Message));
            HasActiveDialog = false;
            //Application.Current.RootVisual.SetValue(Control.IsEnabledProperty, true);
            foreach (var screen in Items)
                yield return Show.NotBusy(screen);

        }

        public bool HasActiveDialog { get; set; }
    }
}