using System;
using Microsoft.Phone.Tasks;

namespace Caliburn.Micro.HelloWp7
{

    [SurviveTombstone]
    public class TabViewModel : Screen, ILaunchChooser<PhoneNumberResult> 
    {
        string _text;

        [SurviveTombstone]
        public string Text {
            get { return _text; }
            set {
                _text = value;
                NotifyOfPropertyChange(() => Text);
            }
        }

        public event EventHandler<TaskLaunchEventArgs> TaskLaunchRequested = delegate { };

        public void Handle(PhoneNumberResult message)
        {
            if (message.TaskResult == TaskResult.OK)
                Text = message.PhoneNumber;
//            MessageBox.Show("The result was " + message.TaskResult, DisplayName, MessageBoxButton.OK);
        }

        public void Choose() {
            TaskLaunchRequested(this, TaskLaunchEventArgs.For<PhoneNumberChooserTask>());
        }
    }
}